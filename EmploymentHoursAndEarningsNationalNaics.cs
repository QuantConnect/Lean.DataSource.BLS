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
        /// Employment, Hours, and Earnings-National (NAICS)
        /// </summary>
        public static class EmploymentHoursAndEarningsNationalNaics
        {
            /// <summary>
            /// All employees, thousands, child care services, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CES6562440001
            /// </remarks>
            public static string AllEmployeesThousandsChildCareServicesSeasonallyAdjusted = "CES6562440001";
        
            /// <summary>
            /// All employees, thousands, construction, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CES2000000001
            /// </remarks>
            public static string AllEmployeesThousandsConstructionSeasonallyAdjusted = "CES2000000001";
        
            /// <summary>
            /// All employees, thousands, financial activities, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CES5500000001
            /// </remarks>
            public static string AllEmployeesThousandsFinancialActivitiesSeasonallyAdjusted = "CES5500000001";
        
            /// <summary>
            /// All employees, thousands, food services and drinking places, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CES7072200001
            /// </remarks>
            public static string AllEmployeesThousandsFoodServicesAndDrinkingPlacesSeasonallyAdjusted = "CES7072200001";
        
            /// <summary>
            /// All employees, thousands, goods-producing, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CES0600000001
            /// </remarks>
            public static string AllEmployeesThousandsGoodsProducingSeasonallyAdjusted = "CES0600000001";
        
            /// <summary>
            /// All employees, thousands, government, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CES9000000001
            /// </remarks>
            public static string AllEmployeesThousandsGovernmentSeasonallyAdjusted = "CES9000000001";
        
            /// <summary>
            /// All employees, thousands, information, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CES5000000001
            /// </remarks>
            public static string AllEmployeesThousandsInformationSeasonallyAdjusted = "CES5000000001";
        
            /// <summary>
            /// All employees, thousands, leisure and hospitality, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CES7000000001
            /// </remarks>
            public static string AllEmployeesThousandsLeisureAndHospitalitySeasonallyAdjusted = "CES7000000001";
        
            /// <summary>
            /// All employees, thousands, manufacturing, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CES3000000001
            /// </remarks>
            public static string AllEmployeesThousandsManufacturingSeasonallyAdjusted = "CES3000000001";
        
            /// <summary>
            /// All employees, thousands, private education and health services, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CES6500000001
            /// </remarks>
            public static string AllEmployeesThousandsPrivateEducationAndHealthServicesSeasonallyAdjusted = "CES6500000001";
        
            /// <summary>
            /// All employees, thousands, professional and business services, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CES6000000001
            /// </remarks>
            public static string AllEmployeesThousandsProfessionalAndBusinessServicesSeasonallyAdjusted = "CES6000000001";
        
            /// <summary>
            /// All employees, thousands, retail trade, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CES4200000001
            /// </remarks>
            public static string AllEmployeesThousandsRetailTradeSeasonallyAdjusted = "CES4200000001";
        
            /// <summary>
            /// All employees, thousands, total nonfarm, not seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CEU0000000001
            /// </remarks>
            public static string AllEmployeesThousandsTotalNonfarmNotSeasonallyAdjusted = "CEU0000000001";
        
            /// <summary>
            /// All employees, thousands, total nonfarm, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CES0000000001
            /// </remarks>
            public static string AllEmployeesThousandsTotalNonfarmSeasonallyAdjusted = "CES0000000001";
        
            /// <summary>
            /// All employees, thousands, total private, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CES0500000001
            /// </remarks>
            public static string AllEmployeesThousandsTotalPrivateSeasonallyAdjusted = "CES0500000001";
        
            /// <summary>
            /// Average hourly earnings of all employees, food services and drinking places, not seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CEU7072200003
            /// </remarks>
            public static string AverageHourlyEarningsOfAllEmployeesFoodServicesAndDrinkingPlacesNotSeasonallyAdjusted = "CEU7072200003";
        
            /// <summary>
            /// Average hourly earnings of all employees, total private, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CES0500000003
            /// </remarks>
            public static string AverageHourlyEarningsOfAllEmployeesTotalPrivateSeasonallyAdjusted = "CES0500000003";
        
            /// <summary>
            /// Average hourly earnings of production and nonsupervisory employees, aerospace product and parts manufacturing, not seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CEU3133640008
            /// </remarks>
            public static string AverageHourlyEarningsOfProductionAndNonsupervisoryEmployeesAerospaceProductAndPartsManufacturingNotSeasonallyAdjusted = "CEU3133640008";
        
            /// <summary>
            /// Average hourly earnings of production and nonsupervisory employees, total private, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CES0500000008
            /// </remarks>
            public static string AverageHourlyEarningsOfProductionAndNonsupervisoryEmployeesTotalPrivateSeasonallyAdjusted = "CES0500000008";
        
            /// <summary>
            /// Average weekly earnings of all employees, 1982-1984 dollars, total private, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CES0500000012
            /// </remarks>
            public static string AverageWeeklyEarningsOfAllEmployees19821984DollarsTotalPrivateSeasonallyAdjusted = "CES0500000012";
        
            /// <summary>
            /// Average weekly hours of all employees, food services and drinking places, not seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CEU7072200002
            /// </remarks>
            public static string AverageWeeklyHoursOfAllEmployeesFoodServicesAndDrinkingPlacesNotSeasonallyAdjusted = "CEU7072200002";
        
            /// <summary>
            /// Average weekly hours of all employees, total private, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CES0500000002
            /// </remarks>
            public static string AverageWeeklyHoursOfAllEmployeesTotalPrivateSeasonallyAdjusted = "CES0500000002";
        
            /// <summary>
            /// Average weekly hours of production and nonsupervisory employees, food services and drinking places, not seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CEU7072200007
            /// </remarks>
            public static string AverageWeeklyHoursOfProductionAndNonsupervisoryEmployeesFoodServicesAndDrinkingPlacesNotSeasonallyAdjusted = "CEU7072200007";
        
            /// <summary>
            /// Average weekly hours of production and nonsupervisory employees, total private, seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CES0500000007
            /// </remarks>
            public static string AverageWeeklyHoursOfProductionAndNonsupervisoryEmployeesTotalPrivateSeasonallyAdjusted = "CES0500000007";
        
            /// <summary>
            /// Production and nonsupervisory employees, thousands, food services and drinking places, not seasonally adjusted
            /// </summary>
            /// <remarks>
            /// Retrieved from the BLS API - https://api.bls.gov/publicAPI/v2/timeseries/data/CEU7072200006
            /// </remarks>
            public static string ProductionAndNonsupervisoryEmployeesThousandsFoodServicesAndDrinkingPlacesNotSeasonallyAdjusted = "CEU7072200006";
        }
    }
}
