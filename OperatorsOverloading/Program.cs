using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorsOverloading
{
    class Program
    {
        static void Main(string[] args)
        {
            Angle demo = new Angle(715, 0, 62);

            Console.WriteLine("\n   Original :" + demo);
            Console.WriteLine("\n Normalized :" + demo.GetNormalizedAngle());
            Console.WriteLine("\n  Optimized :" + demo.GetOptimizedAngle());

            Angle a = new Angle(10, 15, 59);
            Angle b = new Angle(359, 0, 1);

            Console.WriteLine("\n a = {0}", a);
            Console.WriteLine("\n b = {0}", b);
            Console.WriteLine("\n a + b = {0}", a + b);
            Console.WriteLine("\n a - b = {0}", a + b);
            Console.WriteLine("\n a * 2 = {0}", a * 2);
            Console.WriteLine("\n 2 * a = {0}", 2 * a);
            Console.WriteLine("\n a / 10 = {0}", a / 2);

            Console.WriteLine("\n a > b = {0}", a > b);
            Console.WriteLine("\n a < b = {0}", a < b);
            


            Console.ReadKey();
        }
    }


}
