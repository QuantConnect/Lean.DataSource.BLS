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
using ProtoBuf;
using System.IO;
using System.Linq;
using ProtoBuf.Meta;
using Newtonsoft.Json;
using NUnit.Framework;
using QuantConnect.Data;
using QuantConnect.DataSource;
using QuantConnect.Configuration;

namespace QuantConnect.DataLibrary.Tests
{
    [TestFixture]
    public class BLSTests
    {
        [Test]
        public void JsonRoundTrip()
        {
            var expected = CreateNewInstance();
            var type = expected.GetType();
            var serialized = JsonConvert.SerializeObject(expected);
            var result = JsonConvert.DeserializeObject(serialized, type);

            AssertAreEqual(expected, result);
        }

        [Test]
        public void ProtobufRoundTrip()
        {
            var expected = CreateNewInstance();
            var type = expected.GetType();

            RuntimeTypeModel.Default[typeof(BaseData)].AddSubType(2000, type);

            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, expected);

                stream.Position = 0;

                var result = Serializer.Deserialize(type, stream);
                AssertAreEqual(expected, result, filterByCustomAttributes: true);
            }
        }

        [Test]
        public void Clone()
        {
            var expected = CreateNewInstance();
            var result = expected.Clone();

            AssertAreEqual(expected, result);
        }


        [Test]
        public void GetMetaTest()
        {
            Config.Set("data-folder", "../../../../output/");

            var seriesId = "NotASeriesId";
            var meta = BLS.GetMetaData(seriesId);
            Assert.AreEqual(meta, null);

            seriesId = "CES0000000001";
            meta = BLS.GetMetaData(seriesId);
            Assert.AreEqual(meta.SeriesTitle, "All employees, thousands, total nonfarm, seasonally adjusted");
            Assert.AreEqual(meta.SeriesId, seriesId);
            Assert.AreEqual(meta.Seasonality, "Seasonally Adjusted");
            Assert.AreEqual(meta.SurveyName, "Employment, Hours, and Earnings from the Current Employment Statistics survey (National)");
            Assert.AreEqual(meta.SurveyAbbreviation, "CE");
            Assert.AreEqual(meta.MeasureDataType, "ALL EMPLOYEES, THOUSANDS");
            Assert.AreEqual(meta.Area, "undefined");
            Assert.AreEqual(meta.Item, "undefined");
            Assert.AreEqual(meta.CommerceIndustry, "Total nonfarm");
            Assert.AreEqual(meta.CommerceSector, "Total nonfarm");
            Assert.AreEqual(meta.TypeOfCases, "undefined");
            Assert.AreEqual(meta.Occupation, "undefined");
            Assert.AreEqual(meta.CPSLaborForceStatus, "undefined");
            Assert.AreEqual(meta.DemographicAge, "undefined");
            Assert.AreEqual(meta.DemographicEthnicOrigin, "undefined");
            Assert.AreEqual(meta.DemographicRace, "undefined");
            Assert.AreEqual(meta.DemographicGender, "undefined");
            Assert.AreEqual(meta.DemographicEducation, "undefined");
        }

        [Test]
        public void BLSReader()
        {            
            var instance = new BLS();
            var fakeConfig = new SubscriptionDataConfig(
                typeof(BLS),
                Symbol.None,
                Resolution.Daily,
                TimeZones.NewYork,
                TimeZones.NewYork,
                false,
                false,
                false
            );
            var columnNames = "EndTime,Period Start Time,Period End Time,Value";
            Assert.DoesNotThrow(() => { instance.Reader(fakeConfig, columnNames, DateTime.MinValue, false); });
            var dataLine = "2015-01-31 00:00:00,2014-01-01,2015-01-01,3.4";
            var result = instance.Reader(fakeConfig, dataLine, DateTime.MinValue, false) as BLS;

            Assert.AreEqual(result.EndTime, new DateTime(2015, 1, 31));
            Assert.AreEqual(result.PeriodStartTime, new DateTime(2014, 1, 1));
            Assert.AreEqual(result.PeriodEndTime, new DateTime(2015, 1, 1));
            Assert.AreEqual(result.Value, 3.4m);
        }

        private void AssertAreEqual(object expected, object result, bool filterByCustomAttributes = false)
        {
            foreach (var propertyInfo in expected.GetType().GetProperties())
            {
                // we skip Symbol which isn't protobuffed
                if (filterByCustomAttributes && propertyInfo.CustomAttributes.Count() != 0)
                {
                    Assert.AreEqual(propertyInfo.GetValue(expected), propertyInfo.GetValue(result));
                }
            }
            foreach (var fieldInfo in expected.GetType().GetFields())
            {
                Assert.AreEqual(fieldInfo.GetValue(expected), fieldInfo.GetValue(result));
            }
        }

        private BaseData CreateNewInstance()
        {
            return new BLS
            {
                Symbol = Symbol.Empty,
                DataType = MarketDataType.Base,
                EndTime = new DateTime(2023, 1, 31),
                PeriodStartTime = new DateTime(2022, 12, 1),
                PeriodEndTime = new DateTime(2023, 1, 1),
                Value = 1.0m
            };
        }
    }
}