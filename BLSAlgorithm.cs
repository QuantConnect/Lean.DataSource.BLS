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

using QuantConnect.Data;
using QuantConnect.Util;
using QuantConnect.Orders;
using QuantConnect.Algorithm;
using QuantConnect.DataSource;

namespace QuantConnect.DataLibrary.Tests
{
    /// <summary>
    /// Example algorithm using the custom data type
    /// </summary>
    public class BLSAlgorithm : QCAlgorithm
    {
        private Symbol _symbol;

        /// <summary>
        /// Initialise the data and resolution required, as well as the cash and start-end dates for your algorithm. All algorithms must initialized.
        /// </summary>
        public override void Initialize()
        {
            SetStartDate(2013, 1, 7);  //Set Start Date
            SetEndDate(2014, 1, 1);    //Set End Date
            var seriesId = BLS.ConsumerPriceIndexAllUrbanConsumersCurrentSeries.ShelterInUSCityAverageAllUrbanConsumersNotSeasonallyAdjusted;
            var meta = BLS.GetMetaData(seriesId); // You can use this method to get the meta data from the series Id. This is useful in the Research Environment
            Log(meta.ToString());
            _symbol = AddData<BLS>(seriesId).Symbol;
        }

        /// <summary>
        /// OnData event is the primary entry point for your algorithm. Each new data point will be pumped in here.
        /// </summary>
        /// <param name="slice">Slice object keyed by symbol containing the stock data</param>
        public override void OnData(Slice slice)
        {
            foreach (var kvp in slice.Get<BLS>())
            {
                var datasetSymbol = kvp.Key;
                var dataPoint = kvp.Value;
                Log($"{slice.Time} -- {datasetSymbol} -- Value: {dataPoint.Value}");
            }
        }
    }
}
