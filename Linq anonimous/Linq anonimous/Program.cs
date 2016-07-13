using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_anonimous
{
    class Program
    {
        static void Main(string[] args)
        {

            IList<Plane> airplanes = new List<Plane>
            {
                new Plane {Id = 1, Marka = "Boeing", Speed = 850, Category = Category.Military, ModelYear = 2015, CompanyId = 101},
                new Plane {Id = 2, Marka = "Airbus", Speed = 970, Category = Category.Fireplane, ModelYear = 2000, CompanyId = 101 },
                new Plane {Id = 3, Marka = "McDonald", Speed = 765, Category = Category.Passengers, ModelYear = 2015, CompanyId = 102 },
                new Plane {Id = 4, Marka = "TU", Speed = 1500, Category = Category.Cargo, ModelYear = 2000, CompanyId = 102 }


            };
            IList<Company> companyes = new List<Company>
            {
                new Company {Id=101, Name = "Qantas" },
                new Company {Id=102, Name = "Aeroflot" },
                new Company {Id=103, Name = "QantasNew" }
            };



            var airplaneSpeeds = airplanes.Select(p => new { p.Marka, p.Speed });
            Console.WriteLine("\nResult of select");

            foreach (var airplaneSpeed in airplaneSpeeds)
                Console.WriteLine($"Plane mark: {airplaneSpeed.Marka}, Plane speed: {airplaneSpeed.Speed}");

            var bigSpeed = airplanes.Where(p => p.Speed > 900);
            Console.WriteLine("\nResult of where:");
            foreach (var big in bigSpeed)
                Console.WriteLine($"Big speeds:{big.Speed}");





            var sortedCompanyesNames = companyes.Select(c => c.Name).OrderBy(b => b);
            Console.WriteLine("\nResult of order companyes:");
            foreach (var sort in sortedCompanyesNames)
                Console.WriteLine($"Companyes alphabetical:{sort} ");
        }
    }
}
