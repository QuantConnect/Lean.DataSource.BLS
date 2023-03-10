# QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
# Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.

from AlgorithmImports import *

### <summary>
### Example algorithm using the custom data type
### </summary>
class BLSAlgorithm(QCAlgorithm):
    def Initialize(self):
        ''' Initialise the data and resolution required, as well as the cash and start-end dates for your algorithm. All algorithms must initialized.'''
        self.SetStartDate(2013, 1, 7);  #Set Start Date
        self.SetEndDate(2014, 1, 1);    #Set End Date
        series_id = "CUUR0000SAH1"
        meta = BLS.GetMetaData(series_id) # You can use this method to get the meta data from the series Id. This is useful in the Research Environment
        self.Log(str(meta))
        self.symbol = self.AddData(BLS, series_id).Symbol

    def OnData(self, slice):
        ''' OnData event is the primary entry point for your algorithm. Each new data point will be pumped in here.

        :param Slice slice: Slice object keyed by symbol containing the stock data
        '''
        for dataset_symbol, data_point in slice.Get(BLS).items():
            self.Log(f"{slice.Time} -- {dataset_symbol} -- Value: {data_point.Value}")
