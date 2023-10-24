using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Exam.AirlinesManager
{
    public class AirlinesManager : IAirlinesManager
    {
        private HashSet<Airline> airlines;
        private HashSet<Flight> flights;
        private Dictionary<Airline, HashSet<Flight>> flightsByAirline;
        public AirlinesManager()
        {
            this.airlines = new HashSet<Airline>();
            this.flights = new HashSet<Flight>();
            this.flightsByAirline = new Dictionary<Airline, HashSet<Flight>>();
        }

        public void AddAirline(Airline airline)
        {
            this.airlines.Add(airline);
        }

        public void AddFlight(Airline airline, Flight flight)
        {
            if (!this.airlines.Contains(airline))
            {
                throw new ArgumentException();
            }

            if (!this.flightsByAirline.ContainsKey(airline))
            {
                this.flightsByAirline.Add(airline, new HashSet<Flight>());
            }

            this.flightsByAirline[airline].Add(flight);
            this.flights.Add(flight);
        }

        public bool Contains(Airline airline)
        {
            return this.airlines.Contains(airline);
        }

        public bool Contains(Flight flight)
        {
           return this.flights.Contains(flight);
        }

        public void DeleteAirline(Airline airline)
        {
            if (!this.airlines.Contains(airline))
            {
                throw new ArgumentException();
            }

            this.airlines.Remove(airline);
            this.flightsByAirline.Remove(airline);
        }

        public IEnumerable<Airline> GetAirlinesOrderedByRatingThenByCountOfFlightsThenByName()
        {
           return this.airlines.OrderByDescending(a => a.Rating).ThenByDescending(a => this.flightsByAirline[a].Count()).ThenBy(a => a.Name);
        }

        public IEnumerable<Airline> GetAirlinesWithFlightsFromOriginToDestination(string origin, string destination)
        {
           return this.flightsByAirline.Where(f => f.Value.Any(flight => flight.Origin == origin && flight.Destination == destination)).Select(f => f.Key);
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            return this.flights;
        }

        public IEnumerable<Flight> GetCompletedFlights()
        {
            return this.flights.Where(f => f.IsCompleted);
        }

        public IEnumerable<Flight> GetFlightsOrderedByCompletionThenByNumber()
        {

            return this.flights.OrderBy(f => f.IsCompleted == false).ThenBy(f => f.Number);
        }

        public Flight PerformFlight(Airline airline, Flight flight)
        {
            if (!this.flightsByAirline.ContainsKey(airline) || this.flightsByAirline[airline].Contains(flight))
            {
                throw new ArgumentException();
            }

            var searchFlight = this.flightsByAirline[airline].Where(f => f.Id == flight.Id).First();
			searchFlight.IsCompleted = true;

            return searchFlight;
        }
    }
}
