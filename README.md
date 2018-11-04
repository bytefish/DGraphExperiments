# Dgraph Database Example #

The [Dgraph Documentation] says:

> Dgraph is a liberally licensed, scalable, distributed, highly available and fast graph 
> database, designed from ground up to be run in production.

In this example I want to see how to work with [Dgraph] in .NET.

## Starting Dgraph ##

Initialize the Dgraph Cluster:

```
dgraph.exe zero
```

Start the Dgraph Server (with a 2048 MB LRU Cache):

```
dgraph server -l 2048
```

## Example Query ##

[Dgraph GraqhQL+- Query] to query departures from Los Angeles Airport (LAX):

```
{
  lax_departures(func: eq(origin, "LAX")) {
	flight_no,
	year,
	month,
	day_of_month,
	origin,
	destination,
	arrival_delay,
	departure_delay,
	weather_delay
  }
}
```

I put the query in a file ``los_angeles_departures.txt`` and query [Dgraph] with curl:

```
curl localhost:8080/query -X POST -d @los_angeles_departures.txt
```

And it yields the following result:

```json
{
  "data": {
    "lax_departures": [
      {
        "flight_no": "457",
        "year": 2014,
        "month": 1,
        "day_of_month": 26,
        "origin": "LAX",
        "destination": "SEA",
        "arrival_delay": 6,
        "departure_delay": -6,
        "weather_delay": 0
      },
      {
        "flight_no": "467",
        "year": 2014,
        "month": 1,
        "day_of_month": 25,
        "origin": "LAX",
        "destination": "SEA",
        "arrival_delay": -20,
        "departure_delay": -8,
        "weather_delay": 0
      },
      {
        "flight_no": "453",
        "year": 2014,
        "month": 1,
        "day_of_month": 25,
        "origin": "LAX",
        "destination": "SEA",
        "arrival_delay": -23,
        "departure_delay": -10,
        "weather_delay": 0
      },
      {
        "flight_no": "561",
        "year": 2014,
        "month": 1,
        "day_of_month": 26,
        "origin": "LAX",
        "destination": "PDX",
        "arrival_delay": -12,
        "departure_delay": -12,
        "weather_delay": 0
      },
      {
        "flight_no": "469",
        "year": 2014,
        "month": 1,
        "day_of_month": 26,
        "origin": "LAX",
        "destination": "SEA",
        "arrival_delay": 15,
        "departure_delay": -5,
        "weather_delay": 0
      },
      {
        "flight_no": "445",
        "year": 2014,
        "month": 1,
        "day_of_month": 25,
        "origin": "LAX",
        "destination": "SEA",
        "arrival_delay": -15,
        "departure_delay": -2,
        "weather_delay": 0
      },
      {
        "flight_no": "461",
        "year": 2014,
        "month": 1,
        "day_of_month": 25,
        "origin": "LAX",
        "destination": "SEA",
        "arrival_delay": -16,
        "departure_delay": -4,
        "weather_delay": 0
      },
      {
        "flight_no": "6",
        "year": 2014,
        "month": 1,
        "day_of_month": 26,
        "origin": "LAX",
        "destination": "DCA",
        "arrival_delay": -25,
        "departure_delay": -5,
        "weather_delay": 0
      },
      {
        "flight_no": "453",
        "year": 2014,
        "month": 1,
        "day_of_month": 26,
        "origin": "LAX",
        "destination": "SEA",
        "arrival_delay": 8,
        "departure_delay": 3,
        "weather_delay": 0
      },
      {
        "flight_no": "477",
        "year": 2014,
        "month": 1,
        "day_of_month": 26,
        "origin": "LAX",
        "destination": "SEA",
        "arrival_delay": -25,
        "departure_delay": -10,
        "weather_delay": 0
      },
      {
        "flight_no": "461",
        "year": 2014,
        "month": 1,
        "day_of_month": 26,
        "origin": "LAX",
        "destination": "SEA",
        "arrival_delay": -2,
        "departure_delay": -7,
        "weather_delay": 0
      }
    ]
  },
  "extensions": {
    "server_latency": {
      "processing_ns": 76998700,
      "encoding_ns": 45999000
    },
    "txn": { "start_ts": 402 }
  }
}
```

## TODO ##

There are many things I need to look into like Node UIDs, Batch Processing, Indexes, Facets, Edges, RDF. So far this repository is a working example for reading the Airline on Time Performance Data from CSV into Dgraph, and make a first simple query to get data out of it. More to come as soon as I find the time ... 

[Dgraph GraqhQL+- Query]: https://docs.dgraph.io/query-language
[Dgraph]: https://dgraph.io/
[Dgraph Documentation]: https://docs.dgraph.io/