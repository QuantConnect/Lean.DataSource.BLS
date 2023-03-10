import pandas as pd
from datetime import datetime
from dateutil.relativedelta import relativedelta
import json
import re
import csv
import os

class Series:
    def __init__(self, series_id, begin_year, end_year, title, survey_code):
        self.series_id = series_id
        self.begin_year = begin_year
        self.end_year = end_year
        self.title = title
        self.survey_code = survey_code

    def save_data(self, data, output_path, meta_path):
        # Save catalog as meta data
        with open(meta_path, "w") as file:
            json.dump(data['catalog'], file)

        # Same time series data
        month_by_quarter_name = {
            "1st Quarter" : "March",
            "2nd Quarter" : "June",
            "3rd Quarter" : "September",
            "4th Quarter" : "December"
        }
        write_column_headers = not os.path.exists(output_path)
        with open(output_path, 'a', newline='') as file:
            writer = csv.writer(file)
            
            # Add the column headers when the file is empty
            if write_column_headers: 
                writer.writerow(("EndTime", "Period Start Time", "Period End Time", "Value"))

            # Write the time series data
            for data_point in data["data"][::-1]:
                year = data_point["year"] # Ex: "1981"
                period_name = data_point["periodName"] # Ex: "December"

                if period_name == "Annual":
                    # Handle yearly data
                    period_start_time = datetime.strptime(f"{year}", "%Y")
                    period_end_time = datetime.strptime(f"{int(year)+1}", "%Y")
                elif period_name in month_by_quarter_name:
                    # Handle quarterly data
                    period_name = month_by_quarter_name[period_name]
                    period_start_time = datetime.strptime(f"{period_name} {year}", "%B %Y")
                    period_end_time = period_start_time + relativedelta(months=3)
                else:
                    # Handle monthly data
                    period_start_time = datetime.strptime(f"{period_name} {year}", "%B %Y")
                    period_end_time = period_start_time + relativedelta(months=1)

                # Set "release date" to be almost 1 month after the reporting period (this isn't exactly correct)
                timestamp = period_end_time + relativedelta(months=1, days=-1) 

                writer.writerow((timestamp, period_start_time.date(), period_end_time.date(), data_point["value"]))