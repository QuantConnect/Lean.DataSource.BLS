import requests
import json
import os
import shutil

from series import Series
from series_to_download import SERIES_IDS_BY_SURVEY_NAME


HEADERS = {'Content-type': 'application/json'}
API_KEY = "01a6561acaec4a16bb62e92768f8753d" # "518d7f62dd3b4a2a809fe5ba9648082b"
OUTPUT_DIR = "../output/alternative/bls/"
META_DIR = OUTPUT_DIR + "meta/"
MAX_TIME_FRAME_YEARS = 20 # The API allows a time frame of up to 20 years
MAX_SERIES_IDS = 50 # The API endpoint only lets you select up to 50 series at one time (not documented)


docs_table_html = """
<div style="max-height: 400px; overflow-y: auto">
    <table class="table qc-table table-reflow">
        <thead>
            <tr>
                <th colspan="2">Name</th>
            </tr>
            <tr>
                <th>Series Id</th>
                <th>Accessor Code</th>
            </tr>
        </thead>
        <tbody>"""

# Create the meta data directory
if os.path.exists(META_DIR):
    shutil.rmtree(META_DIR)
os.mkdir(META_DIR)

for survey_name, series_ids in SERIES_IDS_BY_SURVEY_NAME.items():
    survey_code = series_ids[0][:2].lower()
    response = requests.get(f'https://download.bls.gov/pub/time.series/{survey_code}/{survey_code}.series', headers=HEADERS)
    lines = response.text.split("\n")
    column_names = lines[0].split("\t")
    column_names = [n.strip() for n in column_names if n != ""] # Handles the case where there can be a column names that's empty (survey CF is an example)
    column_to_drop = None

    series_by_series_id = {}
    for z, line in enumerate(lines[1:-1]):
        adj_line = line.split("\t")
        if len(adj_line) == 0:
            continue
        if adj_line[-1] == "": # Handles the case in the HS survey where there is an extra column at the end (which is empty)
            adj_line = adj_line[:-1]
        adj_line = [cell.strip() for cell in adj_line if cell != "n"] # Handles the case where this row has more columns than there are column names and the column is just filled with `n` (survey EE and SA are examples of this)
        series_id = adj_line[0]
        if series_id not in series_ids:
            continue
        if adj_line[1:] == []: # Handles the case where https://download.bls.gov/pub/time.series/... doesn't exist (there are 5 surveys)
            continue
        if len(adj_line) != len(column_names) and column_to_drop is None: # Handles the case where the number of cells in each row is greater than the number of columns (see surveys EB, EC, GP, HS, PD, SH, and SI)
            column_to_drop = adj_line.index("") # Drop the first empty column
        if column_to_drop is not None:
            del adj_line[column_to_drop]
        
        title = adj_line[column_names.index("series_title")]
        begin_year = int(adj_line[column_names.index("begin_year")])
        end_year = int(adj_line[column_names.index("end_year")])
        series_by_series_id[series_id] = Series(series_id, begin_year, end_year, title, survey_code)


    # Create an empty survey output directory
    output_dir = OUTPUT_DIR + survey_code
    if os.path.exists(output_dir):
        shutil.rmtree(output_dir)
    os.mkdir(output_dir)

    # Create a survey meta data directory
    meta_dir = META_DIR + survey_code
    os.mkdir(meta_dir)

    # Get and save time series data
    for j in range(0, len(series_ids), MAX_SERIES_IDS): 
        series_ids_subset = series_ids[j:j + MAX_SERIES_IDS]
        start_year = min([series_by_series_id[series_id].begin_year for series_id in series_ids_subset])
        end_year = max([series_by_series_id[series_id].end_year for series_id in series_ids_subset])

        for start_year in range(start_year, end_year+1, MAX_TIME_FRAME_YEARS): 
            data = {
                "seriesid": series_ids_subset, 
                "startyear": str(start_year), # inclusive
                "endyear": str(min(start_year + MAX_TIME_FRAME_YEARS -1, end_year)), # inclusive
                "catalog": True,
                "calculations": False,
                "annualaverage": False,
                "aspects": False,
                "registrationkey": API_KEY
            }
            response = requests.post('https://api.bls.gov/publicAPI/v2/timeseries/data/', data=json.dumps(data), headers=HEADERS)
            json_data = json.loads(response.text)

            for data in json_data["Results"]["series"]:
                series_id = data["seriesID"]
                series = series_by_series_id[series_id]
                if survey_code == "is": # The series in this survey all have the same title. Let's create a unique one and overwrite it.
                    catalog = data["catalog"]
                    measure_data_type = catalog["measure_data_type"]
                    commerce_industry = catalog["commerce_industry"]
                    type_of_cases = catalog["type_of_cases"]
                    area = catalog["area"]
                    seasonality = catalog["seasonality"]
                    title = f"{measure_data_type}, {commerce_industry}, {type_of_cases}, {area}, {seasonality}"
                    series.title = title

                series.save_data(data, f"{output_dir}/{series_id}.csv", f"{meta_dir}/{series_id}.json")

    # Create survey helper file and add survey helpers to docs table
    helper_file_text = """/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
*/

namespace QuantConnect.DataSource
{
    /// <summary>
    /// U.S. Bureau of Labor Statistics
    /// </summary>
    public partial class BLS
    {
        /// <summary>
        /// """
    helper_file_text += survey_name
    helper_file_text += """
        /// </summary>
        public static class """
    survey_name_code = survey_name.replace("-", " ").title().replace(",", "").replace("(", "").replace(")", "").replace(" ", "")
    helper_file_text += survey_name_code
    helper_file_text += """
        {"""

    # Sort dictionary by series title
    sorted_series_by_series_id = dict(sorted(series_by_series_id.items(), key=lambda kvp: kvp[1].title))

    for series_id, series in sorted_series_by_series_id.items():
        docs_table_html += f"""
            <tr>
                <td colspan="2">{series.title}</td>
            </tr>
            <tr>
                <td>{series_id}</td>
                <td><code>BLS.{survey_name_code}.{series.codified_title}</code></td>
            </tr>"""

        helper_file_text += f"""
            /// <summary>
            /// {series.title}
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/{series_id}
            /// </remarks>
            public static string {series.codified_title} = "{series_id}";
        """
   
    helper_file_text += """}
    }
}
"""

    # Save helper file
    with open("../" + survey_name_code + ".cs", "w") as f:
        f.write(helper_file_text)

docs_table_html += """
    </tbody>
</table>
</div>
"""

# Save docs table
with open("../docs-table.html", "w") as f:
    f.write(docs_table_html)
