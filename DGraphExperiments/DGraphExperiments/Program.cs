// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using DGraphExperiments.Converters;
using DGraphExperiments.Csv.Parser;
using DGraphExperiments.DGraph.Client;
using Microsoft.Extensions.Configuration;
using TinyCsvParser;

namespace DGraphExperiments
{
    public class Program
    {
        public class DGraphClientSettings : IDGraphClientSettings
        {
            public DGraphClientSettings(IConfiguration configuration)
            {
                ConnectionString = ReadConnectionString(configuration);

                Schema = ReadSchema(configuration);
            }

            public string ConnectionString { get; }

            public string Schema { get; }

            private string ReadConnectionString(IConfiguration configuration)
            {
                return configuration.GetConnectionString("DGraph");
            }

            private string ReadSchema(IConfiguration configuration)
            {
                var fileName = configuration
                    .GetSection("Schema")
                    .Get<string>();

                return File.ReadAllText(fileName);
            }
        }

        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var csvFlightFiles = configuration.GetSection("Files").Get<string[]>();

            // Initialize the Client Settings (Connection String, ...):
            var settings = new DGraphClientSettings(configuration);

            // Create the Client:
            var client = new DGraphClient(settings);

            // Initialize the Schema:
            client.CreateSchema();

            // Import all hourly weather data from 2014:
            foreach (var csvFlightStatisticsFile in csvFlightFiles)
            {
                ProcessFlights(client, csvFlightStatisticsFile);
            }
        }
        
        private static void ProcessFlights(DGraphClient client, string csvFilePath)
        {
            // Create the Converter:
            var converter = new FlightConverter();

            // Access to the List of Parsers:
            Parsers
                // Use the Flights Parser:
                .FlightStatisticsParser
                // Read the File:
                .ReadFromFile(csvFilePath, Encoding.UTF8)
                // As an Observable:
                .ToObservable()
                // Batch Entities by Time / Count:
                .Buffer(TimeSpan.FromSeconds(5), 100)
                // And subscribe to the Batch synchronously (we don't want to handle too much backpressure here):
                .Subscribe(records =>
                {
                    var validRecords = records
                        // Get the Valid Results:
                        .Where(x => x.IsValid)
                        // And get the populated Entities:
                        .Select(x => x.Result)
                        // Group by WBAN, Date and Time to avoid duplicates for this batch:
                        .GroupBy(x => new {x.UniqueCarrier, x.FlightNumber, x.FlightDate})
                        // If there are duplicates then make a guess and select the first one:
                        .Select(x => x.First())
                        // Convert into the DGraph Data Model:
                        .Select(x => converter.Convert(x))
                        // Evaluate:
                        .ToList();

                    // Finally write them with the Batch Writer:
                    client.Mutate(validRecords);
                });
        }
    }
}
