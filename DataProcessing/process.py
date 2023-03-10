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


docs_table_file = open("../docs-table.html", "w")
docs_table_file.write("<p>The BLS database contains over 77 million series from over 60 different surveys, but not all of them are available on QuantConnect. The following sections show the integrated surveys and the series they contain.</p>")

# Create the meta data directory
if os.path.exists(META_DIR):
    shutil.rmtree(META_DIR)
os.mkdir(META_DIR)

for survey_name, series_ids in SERIES_IDS_BY_SURVEY_NAME.items():
    docs_table_file.write(f"""
<h4>{survey_name}</h4>
<p>The following table show the supported series in the {survey_name} survey:</p>
<table class='qc-table table'>""")
    series_ids = sorted(series_ids)
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

    series_id_written_to_docs_file = []
    docs_table_column_names = []
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
                series.save_data(data, f"{output_dir}/{series_id}.csv", f"{meta_dir}/{series_id}.json")

                if len(series_id_written_to_docs_file) == 0:
                    # Write docs table column names
                    docs_table_file.write("""
    <thead>
        <tr>""")
                    for key in data["catalog"].keys():
                        if key in ["series_title", "survey_name", "survey_abbreviation"]:
                            continue
                        docs_table_column_names.append(key)
                        column_name = key.replace("_", " ").title()
                        docs_table_file.write(f"\n            <th>{column_name}</th>")
                    docs_table_file.write("""
        </tr>
    </thead>
    <tbody>""")

                if series_id not in series_id_written_to_docs_file:
                    docs_table_file.write("""\n        <tr>""")
                    for key, value in data["catalog"].items():
                        if key in docs_table_column_names:
                            docs_table_file.write(f"\n            <td>{value}</td>")    
                    docs_table_file.write("""\n        </tr>""")
                    series_id_written_to_docs_file.append(series_id)
    docs_table_file.write(f"""
    </tbody>
</table>""")

docs_table_file.close()
