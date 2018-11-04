// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Newtonsoft.Json;

namespace DGraphExperiments.DGraph.Model
{
    public class Flight
    {
        [JsonProperty("flight_no")]
        public string FlightNo { get; set; }

        [JsonProperty("flight_date")]
        public DateTime FlightDate { get; set; }

        [JsonProperty("taxi_in")]
        public int TaxiIn { get; set; }

        [JsonProperty("taxi_out")]
        public int TaxiOut { get; set; }

        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("arrival_delay")]
        public int ArrivalDelay { get; set; }

        [JsonProperty("departure_delay")]
        public int DepartureDelay { get; set; }

        [JsonProperty("cancellation_code")]
        public string CancellationCode { get; set; }

        [JsonProperty("weather_delay")]
        public int WeatherDelay { get; set; }

        [JsonProperty("nas_delay")]
        public int NasDelay { get; set; }

        [JsonProperty("security_delay")]
        public int SecurityDelay { get; set; }
    }
}
