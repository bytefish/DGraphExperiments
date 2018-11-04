// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using DGraphExperiments.Csv.Mapper;
using DGraphExperiments.Csv.Model;
using TinyCsvParser;

namespace DGraphExperiments.Csv.Parser
{
    public static class Parsers
    {
        public static CsvParser<Flight> FlightStatisticsParser
        {
            get
            {
                CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');

                return new CsvParser<Flight>(csvParserOptions, new FlightMapper());
            }
        }
    }
}