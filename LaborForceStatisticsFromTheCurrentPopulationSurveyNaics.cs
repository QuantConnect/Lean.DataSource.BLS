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

namespace QuantConnect.DataSource
{
    /// <summary>
    /// U.S. Bureau of Labor Statistics
    /// </summary>
    public partial class BLS
    {
        /// <summary>
        /// Labor Force Statistics from the Current Population Survey (NAICS)
        /// </summary>
        public static class LaborForceStatisticsFromTheCurrentPopulationSurveyNaics
        {
            /// <summary>
            /// (Seas) Civilian Labor Force Level
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNS11000000
            /// </remarks>
            public static string SeasCivilianLaborForceLevel = "LNS11000000";
        
            /// <summary>
            /// (Seas) Employment Level
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNS12000000
            /// </remarks>
            public static string SeasEmploymentLevel = "LNS12000000";
        
            /// <summary>
            /// (Seas) Employment Level - Nonagricultural Industries
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNS12035019
            /// </remarks>
            public static string SeasEmploymentLevelNonagriculturalIndustries = "LNS12035019";
        
            /// <summary>
            /// (Seas) Employment-Population Ratio
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNS12300000
            /// </remarks>
            public static string SeasEmploymentPopulationRatio = "LNS12300000";
        
            /// <summary>
            /// (Seas) Labor Force Participation Rate
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNS11300000
            /// </remarks>
            public static string SeasLaborForceParticipationRate = "LNS11300000";
        
            /// <summary>
            /// (Seas) Labor Force Participation Rate - 20 yrs. & over, Men
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNS11300025
            /// </remarks>
            public static string SeasLaborForceParticipationRate20YrsOverMen = "LNS11300025";
        
            /// <summary>
            /// (Seas) Not in Labor Force
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNS15000000
            /// </remarks>
            public static string SeasNotInLaborForce = "LNS15000000";
        
            /// <summary>
            /// (Seas) Total unemployed, plus all persons marginally attached to the labor force, plus total employed part time for economic reasons, as a percent of the civilian labor force plus all persons marginally attached to the labor force (U-6)
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNS13327709
            /// </remarks>
            public static string SeasTotalUnemployedPlusAllPersonsMarginallyAttachedToTheLaborForcePlusTotalEmployedPartTimeForEconomicReasonsAsAPercentOfTheCivilianLaborForcePlusAllPersonsMarginallyAttachedToTheLaborForceU6 = "LNS13327709";
        
            /// <summary>
            /// (Seas) Unemployment Level
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNS13000000
            /// </remarks>
            public static string SeasUnemploymentLevel = "LNS13000000";
        
            /// <summary>
            /// (Seas) Unemployment Rate
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNS14000000
            /// </remarks>
            public static string SeasUnemploymentRate = "LNS14000000";
        
            /// <summary>
            /// (Seas) Unemployment Rate - 16-19 yrs.
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNS14000012
            /// </remarks>
            public static string SeasUnemploymentRate1619Yrs = "LNS14000012";
        
            /// <summary>
            /// (Seas) Unemployment Rate - 20 yrs. & over, Men
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNS14000025
            /// </remarks>
            public static string SeasUnemploymentRate20YrsOverMen = "LNS14000025";
        
            /// <summary>
            /// (Seas) Unemployment Rate - 20 yrs. & over, Women
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNS14000026
            /// </remarks>
            public static string SeasUnemploymentRate20YrsOverWomen = "LNS14000026";
        
            /// <summary>
            /// (Seas) Unemployment Rate - Asian
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNS14032183
            /// </remarks>
            public static string SeasUnemploymentRateAsian = "LNS14032183";
        
            /// <summary>
            /// (Seas) Unemployment Rate - Black or African American
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNS14000006
            /// </remarks>
            public static string SeasUnemploymentRateBlackOrAfricanAmerican = "LNS14000006";
        
            /// <summary>
            /// (Seas) Unemployment Rate - Hispanic or Latino
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNS14000009
            /// </remarks>
            public static string SeasUnemploymentRateHispanicOrLatino = "LNS14000009";
        
            /// <summary>
            /// (Seas) Unemployment Rate - White
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNS14000003
            /// </remarks>
            public static string SeasUnemploymentRateWhite = "LNS14000003";
        
            /// <summary>
            /// (Seas) Unemployment Rate - Women
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNS14000002
            /// </remarks>
            public static string SeasUnemploymentRateWomen = "LNS14000002";
        
            /// <summary>
            /// (Unadj) Employment Level
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNU02000000
            /// </remarks>
            public static string UnadjEmploymentLevel = "LNU02000000";
        
            /// <summary>
            /// (Unadj) Unemployment Level
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNU03000000
            /// </remarks>
            public static string UnadjUnemploymentLevel = "LNU03000000";
        
            /// <summary>
            /// (Unadj) Unemployment Rate
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNU04000000
            /// </remarks>
            public static string UnadjUnemploymentRate = "LNU04000000";
        
            /// <summary>
            /// (Unadj) Unemployment Rate - 75 yrs. & over, Hispanic or Latino Men
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNU04035203
            /// </remarks>
            public static string UnadjUnemploymentRate75YrsOverHispanicOrLatinoMen = "LNU04035203";
        
            /// <summary>
            /// (Unadj) Unemployment rate - 14 years and over
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNU04023554
            /// </remarks>
            public static string UnadjUnemploymentRate14YearsAndOver = "LNU04023554";
        
            /// <summary>
            /// (unadj) Unemployed - 14 years and over
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNU03023554
            /// </remarks>
            public static string UnadjUnemployed14YearsAndOver = "LNU03023554";
        
            /// <summary>
            /// Research series, employment adjusted to CES concepts, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/LNS16000000
            /// </remarks>
            public static string ResearchSeriesEmploymentAdjustedToCesConceptsSeasonallyAdjusted = "LNS16000000";
        }
    }
}
