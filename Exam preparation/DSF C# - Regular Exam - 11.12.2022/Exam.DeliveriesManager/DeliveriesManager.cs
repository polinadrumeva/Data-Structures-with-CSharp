using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Exam.DeliveriesManager
{
    public class DeliveriesManager : IDeliveriesManager
    {
        private Dictionary<string, Deliverer> deliverers;
        private Dictionary<string, Package> packages;
        private Dictionary<string, string> delivererPackages;
        public DeliveriesManager()
        {
            this.deliverers = new Dictionary<string, Deliverer>();
            this.packages = new Dictionary<string, Package>();
            this.delivererPackages = new Dictionary<string, string>();
        }

        public void AddDeliverer(Deliverer deliverer)
        {
            deliverers.Add(deliverer.Id, deliverer);
        }

        public void AddPackage(Package package)
        {
            packages.Add(package.Id, package);
        }

        public void AssignPackage(Deliverer deliverer, Package package)
        {
            if (!deliverers.ContainsKey(deliverer.Id) || !packages.ContainsKey(package.Id))
            {
                throw new ArgumentException();
            }
            
            delivererPackages.Add(deliverer.Id, package.Id);
            deliverer.PackagesCount++;
        }

        public bool Contains(Deliverer deliverer)
        {
            return deliverers.ContainsKey(deliverer.Id);
        }

        public bool Contains(Package package)
        {
            return packages.ContainsKey(package.Id);
        }

        public IEnumerable<Deliverer> GetDeliverers()
        {
            return deliverers.Values;
        }

        public IEnumerable<Deliverer> GetDeliverersOrderedByCountOfPackagesThenByName()
        {
            return deliverers.Values.OrderByDescending(p => p.PackagesCount).ThenBy(p => p.Name); 
        }

        public IEnumerable<Package> GetPackages()
        {
            return packages.Values;
        }

        public IEnumerable<Package> GetPackagesOrderedByWeightThenByReceiver() 
        {
            return packages.Values.OrderByDescending(x => x.Weight).ThenBy(x => x.Receiver);
        }

        public IEnumerable<Package> GetUnassignedPackages()
        {
          return packages.Where(x => !delivererPackages.ContainsValue(x.Key)).Select(x => x.Value);
        }
    }
}
