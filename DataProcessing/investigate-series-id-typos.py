import requests
import json
from json.decoder import JSONDecodeError
from datetime import datetime
from dateutil.relativedelta import relativedelta
from num2words import num2words
import pandas as pd
import os
import calendar
import pickle
import base64
import re 
import csv

headers = {'Content-type': 'application/json'}
API_KEY = "01a6561acaec4a16bb62e92768f8753d" # "518d7f62dd3b4a2a809fe5ba9648082b"
OUTPUT_DIR = "../output/alternative/bls/"
CACHE_DIR = "D:/quantconnect/datasets-cache/bls/"
CACHE_MAP_FILE = CACHE_DIR + "cache_map.json"
RAW_RESULTS_DIR = CACHE_DIR + "raw-results/"

def save_to_cache(response, url_endpoint, data=None):
    # Save response to cache map
    num_leaves = 0
    with open(CACHE_MAP_FILE, "r+") as f:
        try:
            json_data = json.load(f)
        except JSONDecodeError:
            json_data = {} # The file was empty

        # Count number of leaves in json_data
        for value in json_data.values():
            if type(value) is dict:
                for v in value.values():
                    num_leaves += 1
            else:
                num_leaves += 1
        
        # Save the cache response under the leaf number 
        if url_endpoint not in json_data:
            json_data[url_endpoint] = {}
        if type(data) is dict:
            del data["registrationkey"]
        json_data[url_endpoint][json.dumps(data)] = num_leaves

        f.seek(0) # reset file position to the beginning
        json.dump(json_data, f) # Save new json_data to cache
        f.truncate()

    # Save response to cache
    with open(RAW_RESULTS_DIR + f"{num_leaves}.txt", "w") as f:
        f.write(response.text)      

def load_from_cache(url_endpoint, data=None, join=True):
    if not os.path.exists(CACHE_MAP_FILE):
        # Create cache-map file
        open(CACHE_MAP_FILE, "w").close()
        return None

    if data is not None:
        data = {k: v for k, v in data.items() if k != "registrationkey"}
    
    # Find cache file name from cache map file
    cache_file_name = None
    with open(CACHE_MAP_FILE, "r") as f:
        try:
            json_data = json.load(f)
        except JSONDecodeError:
            return None # The file was empty

        if url_endpoint in json_data:
            cache_file_name = json_data[url_endpoint].get(str(json.dumps(data)))
    if cache_file_name is None:
        return None # In this case, call the API

    with open(RAW_RESULTS_DIR + f"{cache_file_name}.txt", "r") as f:
        lines = f.readlines()
        if join:
            return " ".join(lines)
        return lines


# Download surveys meta data
print("Downloading surveys meta data...")
url_endpoint = 'https://api.bls.gov/publicAPI/v2/surveys/'
response = load_from_cache(url_endpoint)
if not response:
    print("--> Calling API")
    response = requests.post(url_endpoint, headers=headers)
    save_to_cache(response, url_endpoint)
    response = response.text
json_data = json.loads(response)
surveys = json_data["Results"]["survey"] # Only use the first survey for now

num_surveys = len(surveys)

series_ids_to_do = [16, 17, 27, 29, 53, 58, 59]

for i, data in enumerate(surveys):
    if i+1 not in series_ids_to_do:
        continue
    survey_abbreviation = data["survey_abbreviation"]
    survey_name = data["survey_name"]

    lower_survey_abbreviation = survey_abbreviation.lower()

    print(f'{datetime.now()}[{i+1}/{num_surveys}] Downloading all series data for survey \"{survey_name}\" ({survey_abbreviation} abbreviation)...')

    # Download meta data for a single survey
    print(f'-> Getting {survey_abbreviation} series meta data...')
    url_endpoint = f'https://download.bls.gov/pub/time.series/{lower_survey_abbreviation}/{lower_survey_abbreviation}.series'
    response = load_from_cache(url_endpoint)
    if not response:
        print('--> Calling API')
        response = requests.get(url_endpoint, headers=headers)
        save_to_cache(response, url_endpoint)
        response = response.text
    lines = response.split("\n")
    column_names = lines[0].split("\t")
    column_names = [n.strip() for n in column_names if n != ""] # Handles the case where there can be a column names that's empty (survey CF is an example)
    column_to_drop = None
    for z, line in enumerate(lines[1:-1]):
        adj_line = line.split("\t")
        if len(adj_line) == 0:
            continue
        if adj_line[-1] == "":
            adj_line = adj_line[:-1]
        adj_line = [cell.strip() for cell in adj_line if cell != "n"]
        series_id = adj_line[0]
        if adj_line[1:] == []:
            continue
        if len(adj_line) != len(column_names) and column_to_drop is None:
            column_to_drop = adj_line.index("") # Drop the first empty column
        if column_to_drop is not None:
            del adj_line[column_to_drop]
        #print(series_id)
        #print(adj_line[-4])