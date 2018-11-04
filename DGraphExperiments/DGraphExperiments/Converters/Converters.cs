// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CsvFlightType = DGraphExperiments.Csv.Model.Flight;
using DgraphFlightType = DGraphExperiments.DGraph.Model.Flight;


namespace DGraphExperiments.Converters
{
    public interface IConverter<in TSourceType, out TTargetType>
    {
        TTargetType Convert(TSourceType source);
    }

    public abstract class BaseConverter<TSourceType, TTargetType> : IConverter<TSourceType, TTargetType>
        where TSourceType : class
        where TTargetType : class, new()
    {
        public TTargetType Convert(TSourceType source)
        {
            if (source == null)
            {
                return null;
            }

            TTargetType target = new TTargetType();

            InternalConvert(source, target);

            return target;
        }

        protected abstract void InternalConvert(TSourceType source, TTargetType target);
    }

    public class FlightConverter : BaseConverter<CsvFlightType, DgraphFlightType>
    {
        protected override void InternalConvert(CsvFlightType source, DgraphFlightType target)
        {
            target.FlightNo = source.FlightNumber;
            target.FlightDate = source.FlightDate;
            target.Year = source.Year;
            target.Month = source.Month;
            target.DayOfMonth = source.DayOfMonth;
            target.DayOfWeek = source.DayOfWeek;
            target.IsCancelled = source.CancelledFlight.HasValue ? source.CancelledFlight.Value : false;
            target.CancellationCode = source.CancellationCode;
            target.Origin = source.Origin;
            target.OriginAirport = source.OriginAirport;
            target.Destination = source.Destination;
            target.DestinationAirport = source.DestinationAirport;
            target.DepartureDelay = source.DepartureDelay.HasValue ? source.DepartureDelay.Value : 0;
            target.ArrivalDelay = source.ArrivalDelay.HasValue ? source.ArrivalDelay.Value : 0;
            target.NasDelay = source.NasDelay.HasValue ? source.NasDelay.Value : 0;
            target.SecurityDelay = source.SecurityDelay.HasValue ? source.SecurityDelay.Value : 0;
            target.WeatherDelay = source.WeatherDelay.HasValue ? source.WeatherDelay.Value : 0;
            target.TaxiIn = source.TaxiIn.HasValue ? source.TaxiIn.Value : 0;
            target.TaxiOut = source.TaxiOut.HasValue ? source.TaxiOut.Value: 0;
        }
    }
}
