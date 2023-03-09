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

namespace QuantConnect.DataSource
{
    /// <summary>
    /// BLS data type
    /// </summary>
    [ProtoContract(SkipConstructor = true)]
    public partial class BLS : BaseData
    {
        /// <summary>
        /// Start time of the reporting period
        /// </summary>
        public DateTime PeriodStartTime {get; set;}

        /// <summary>
        /// End time of the reporting period
        /// </summary>
        public DateTime PeriodEndTime  {get; set;}

        /// <summary>
        /// Return the URL string source of the file. This will be converted to a stream
        /// </summary>
        /// <param name="config">Configuration object</param>
        /// <param name="date">Date of this source file</param>
        /// <param name="isLiveMode">true if we're in live mode, false for backtesting mode</param>
        /// <returns>String URL of source file.</returns>
        public override SubscriptionDataSource GetSource(SubscriptionDataConfig config, DateTime date, bool isLiveMode)
        {
            var seriesId = config.Symbol.Value;
            return new SubscriptionDataSource(
                Path.Combine(
                    Globals.DataFolder,
                    "alternative",
                    "bls",
                    $"{seriesId.Substring(0, 2).ToLowerInvariant()}",
                    $"{seriesId}.csv"
                ),
                SubscriptionTransportMedium.LocalFile
            );
        }

        /// <summary>
        /// Parses the data from the line provided and loads it into LEAN
        /// </summary>
        /// <param name="config">Subscription configuration</param>
        /// <param name="line">Line of data</param>
        /// <param name="date">Date</param>
        /// <param name="isLiveMode">Is live mode</param>
        /// <returns>New instance</returns>
        public override BaseData Reader(SubscriptionDataConfig config, string line, DateTime date, bool isLiveMode)
        {
            var symbol = config.Symbol;

            // Catch the row of column names
            var data = line.Split(',');
            if (data[0] == "EndTime")
            {
                return null;
            }

            // Parse data rows
            return new BLS
            {
                Symbol = symbol,
                EndTime = Parse.DateTimeExact(data[0], "yyyy-MM-dd HH:mm:ss"),
                PeriodStartTime = Parse.DateTimeExact(data[1], "yyyy-MM-dd"),
                PeriodEndTime = Parse.DateTimeExact(data[2], "yyyy-MM-dd"),
                Value = data[3].IfNotNullOrEmpty<decimal>(s => Parse.Decimal(s))
            };
        }

        /// <summary>
        /// Clones the data
        /// </summary>
        /// <returns>A clone of the object</returns>
        public override BaseData Clone()
        {
            return new BLS
            {
                Symbol = Symbol,
                EndTime = EndTime,
                PeriodStartTime = PeriodStartTime,
                PeriodEndTime = PeriodEndTime,
                Value = Value
            };
        }

        /// <summary>
        /// Indicates whether the data source is tied to an underlying symbol and requires that corporate events be applied to it as well, such as renames and delistings
        /// </summary>
        /// <returns>false</returns>
        public override bool RequiresMapping()
        {
            return false;
        }

        /// <summary>
        /// Indicates whether the data is sparse.
        /// If true, we disable logging for missing files
        /// </summary>
        /// <returns>true</returns>
        public override bool IsSparseData()
        {
            return true;
        }

        /// <summary>
        /// Converts the instance to string
        /// </summary>
        public override string ToString()
        {
            return $"{Symbol}; {Value} for period {PeriodStartTime} - {PeriodEndTime}";
        }

        /// <summary>
        /// Gets the default resolution for this data and security type
        /// </summary>
        public override Resolution DefaultResolution()
        {
            return Resolution.Daily;
        }

        /// <summary>
        /// Gets the supported resolution for this data and security type
        /// </summary>
        public override List<Resolution> SupportedResolutions()
        {
            return DailyResolution;
        }

        /// <summary>
        /// Specifies the data time zone for this data type. This is useful for custom data types
        /// </summary>
        /// <returns>The <see cref="T:NodaTime.DateTimeZone" /> of this data type</returns>
        public override DateTimeZone DataTimeZone()
        {
            return TimeZones.NewYork;
        }
    }
}
