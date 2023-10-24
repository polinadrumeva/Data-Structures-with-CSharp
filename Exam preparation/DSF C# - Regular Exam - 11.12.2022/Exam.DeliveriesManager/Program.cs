using System;

namespace Exam.DeliveriesManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var deliveriesManager = new DeliveriesManager();
        
            deliveriesManager.AssignPackage(new Deliverer("D1", "Pesho"), new Package("P1", "Ivan", "Sofia", "0888 888 888", 1.5));
            deliveriesManager.AssignPackage(new Deliverer("D1", "Pesho"), new Package("P2", "Dragan", "Plovdiv", "0888 888 888", 1.5));
            deliveriesManager.AssignPackage(new Deliverer("D2", "Gosho"), new Package("P3", "Petkan", "Varna", "0888 888 888", 1.5));
            deliveriesManager.AssignPackage(new Deliverer("D2", "Gosho"), new Package("P4", "Stamat", "Burgas", "0888 888 888", 1.5));
            deliveriesManager.AssignPackage(new Deliverer("D2", "Gosho"), new Package("P5", "Stamat", "Burgas", "0888 888 888", 1.5));

            Console.WriteLine(string.Join(", ", deliveriesManager.GetDeliverersOrderedByCountOfPackagesThenByName()));
        }
    }
}
