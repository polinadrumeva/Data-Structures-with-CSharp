using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace PublicTransportManagementSystem
{
    public class PublicTransportRepository : IPublicTransportRepository
    {
        private Dictionary<string, Passenger> passengers;
        private Dictionary<string, Bus> buses;
        private Dictionary<string, List<Passenger>> passengersOnBus;
        private Dictionary<string, List<Passenger>> busesByOccupancy;

        public PublicTransportRepository()
        {
            this.passengers = new Dictionary<string, Passenger>();
            this.buses = new Dictionary<string, Bus>();
            this.passengersOnBus = new Dictionary<string, List<Passenger>>();
            this.busesByOccupancy = new Dictionary<string, List<Passenger>>();
        }

        public void RegisterPassenger(Passenger passenger)
        {
            passengers.Add(passenger.Id, passenger);
        }

        public void AddBus(Bus bus)
        {
            buses.Add(bus.Id, bus);
            busesByOccupancy.Add(bus.Id, new List<Passenger>());
        }

        public bool Contains(Passenger passenger)
        {
            return passengers.ContainsKey(passenger.Id);
        }

        public bool Contains(Bus bus)
        {
            return buses.ContainsKey(bus.Id);
        }

        public IEnumerable<Bus> GetBuses()
        {
            return buses.Values;
        }

        public void BoardBus(Passenger passenger, Bus bus)
        {
            if (!passengers.ContainsKey(passenger.Id) || !buses.ContainsKey(bus.Id))
            {
                throw new ArgumentException();
            }

            if (!passengersOnBus.ContainsKey(bus.Id))
            {
				passengersOnBus.Add(bus.Id, new List<Passenger>());
			}

            passengersOnBus[bus.Id].Add(passenger);
            busesByOccupancy[bus.Id].Add(passenger);
        }

        public void LeaveBus(Passenger passenger, Bus bus)
        {
			if (!passengers.ContainsKey(passenger.Id) || !buses.ContainsKey(bus.Id))
			{
				throw new ArgumentException();
			}

            passengersOnBus[bus.Id].Remove(passenger);
		}


        public IEnumerable<Passenger> GetPassengersOnBus(Bus bus)
        {
            if (!passengersOnBus.ContainsKey(bus.Id))
            {
				return new List<Passenger>();
			}
            
            return passengersOnBus[bus.Id].ToList();
        }

        public IEnumerable<Bus> GetBusesOrderedByOccupancy()
        {
            return buses.Values.OrderBy(b => busesByOccupancy[b.Id].Count);
        }

        public IEnumerable<Bus> GetBusesWithCapacity(int capacity)
        {
            return buses.Values.Where(b => b.Capacity >= capacity);
        }
    }
}