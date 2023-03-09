/*
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

using System;
using NodaTime;
using ProtoBuf;
using System.IO;
using QuantConnect.Data;
using QuantConnect;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace QuantConnect.DataSource
{
    /// <summary>
    /// BLS data type
    /// </summary>
    public partial class BLS : BaseData
    {
        private static HashSet<string> _seriesIds = new();

        private static Dictionary<string, Meta> _metaById = new();

        /// <summary>
        /// Gets the meta data for an individual survey series
        /// </summary>
        /// <param name="seriesId">The Id of the series</param>
        /// <returns>The series Meta data object</returns>
        public static Meta GetMetaData(string seriesId)
        {
            if (_metaById.TryGetValue(seriesId, out var meta))
            {
                return meta;
            }
            var seriesIds = GetAllSeriesIds();
            if (!seriesIds.Contains(seriesId))
            {
                return null;
            }
            var path = Path.Combine(
                Globals.DataFolder,
                "alternative",
                "bls",
                "meta",
                $"{seriesId.Substring(0, 2).ToLowerInvariant()}",
                $"{seriesId}.json"
            );
            var json = File.ReadAllText(path);
            meta = JsonConvert.DeserializeObject<Meta>(json);
            _metaById[seriesId] = meta;
            return meta;
        }

        /// <summary>
        /// Gets all of the series Ids for all the surveys included in the dataset
        /// </summary>
        /// <returns>HashSet that contains all the series Ids</returns>
        public static HashSet<string> GetAllSeriesIds()
        {
            if (_seriesIds.Count == 0)
            {
                var seriesIds = new List<string>();
                var path = Path.Combine(
                    Globals.DataFolder,
                    "alternative",
                    "bls",
                    "meta"
                );
                var surveyDirectories = Directory.GetDirectories(path);
                foreach (var dir in surveyDirectories)
                {
                    seriesIds.AddRange(Directory.GetFiles(dir).Select(filePath => filePath.Split("\\").Last().Split(".").First()));
                }
                _seriesIds = seriesIds.ToHashSet();
            }
            return _seriesIds;
        }

        /// <summary>
        /// A class that contains all the meta data for a survey series
        /// </summary>
        public class Meta
        {
            /// <summary>
            /// Title of series
            /// </summary>
            [JsonProperty(PropertyName = "series_title")]
            public string SeriesTitle { get; set; }

            /// <summary>
            /// Id of series
            /// </summary>
            [JsonProperty(PropertyName = "series_id")]
            public string SeriesId { get; set; }
            
            /// <summary>
            /// Seasonality of series (adjusted or unadjusted)
            /// </summary>
            [JsonProperty(PropertyName = "seasonality")]
            public string Seasonality { get; set; }
            
            /// <summary>
            /// The name of the survey that contains this series
            /// </summary>
            [JsonProperty(PropertyName = "survey_name")]
            public string SurveyName { get; set; }
            
            /// <summary>
            /// The abbrevation of the survey that contains this series
            /// </summary>
            [JsonProperty(PropertyName = "survey_abbreviation")]
            public string SurveyAbbreviation { get; set; }
            
            /// <summary>
            /// The type of measurement for the series
            /// </summary>
            [JsonProperty(PropertyName = "measure_data_type")]
            public string MeasureDataType { get; set; }


            // The rest of these properties aren't in all surveys

            /// <summary>
            /// Demographic area for the survey
            /// </summary>
            [JsonProperty(PropertyName = "area")]
            public string Area { get; set; } = "undefined";

            /// <summary>
            /// Items included in the survey series
            /// </summary>
            [JsonProperty(PropertyName = "item")]
            public string Item { get; set; } = "undefined";

            /// <summary>
            /// Industry covered by the survey series
            /// </summary>
            [JsonProperty(PropertyName = "commerce_industry")]
            public string CommerceIndustry { get; set; } = "undefined";

            /// <summary>
            /// Sector covered by the survey series
            /// </summary>
            [JsonProperty(PropertyName = "commerce_sector")]
            public string CommerceSector { get; set; } = "undefined";

            /// <summary>
            /// Types of cases included in the survey series
            /// </summary>
            [JsonProperty(PropertyName = "type_of_cases")]
            public string TypeOfCases { get; set; } = "undefined";

            /// <summary>
            /// Occupations covered in the survey series
            /// </summary>
            [JsonProperty(PropertyName = "occupation")]
            public string Occupation { get; set; } = "undefined";

            /// <summary>
            /// Labor force status of the individuals included in the survey series
            /// </summary>
            [JsonProperty(PropertyName = "cps_labor_force_status")]
            public string CPSLaborForceStatus { get; set; } = "undefined";

            /// <summary>
            /// Age of the individuals included in the survey series
            /// </summary>
            [JsonProperty(PropertyName = "demographic_age")]
            public string DemographicAge { get; set; } = "undefined";

            /// <summary>
            /// Ethnic origin of the individuals included in the survey series
            /// </summary>
            [JsonProperty(PropertyName = "demographic_ethnic_origin")]
            public string DemographicEthnicOrigin { get; set; } = "undefined";

            /// <summary>
            /// Race of the individuals included in the survey series
            /// </summary>
            [JsonProperty(PropertyName = "demographic_race")]
            public string DemographicRace { get; set; } = "undefined";

            /// <summary>
            /// Gender of the individuals included in the survey series
            /// </summary>
            [JsonProperty(PropertyName = "demographic_gender")]
            public string DemographicGender { get; set; } = "undefined";

            /// <summary>
            /// Education level of the individuals included in the survey series
            /// </summary>
            [JsonProperty(PropertyName = "demographic_education")]
            public string DemographicEducation { get; set; } = "undefined";

            /// <summary>
            /// Gets a string representation of the defined meta data properties in the series
            /// </summary>
            /// <returns>A string that displays all the defined meta data properties in the series</returns>
            public override string ToString()
            {
                var s = "";
                foreach (var property in this.GetType().GetProperties())
                {
                    var value = (string)property.GetValue(this);
                    if (value != "undefined")
                    {
                        s += $"{property.Name}: {value}\n";
                    }
                }
                return s;
            }
        }
    }
}