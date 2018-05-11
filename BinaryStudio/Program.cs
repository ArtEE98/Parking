using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryStudio
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.InitLogger();
            InfoLogger.InitLogger();
            
            Parking p = Parking.getParking();
            p.AddCar(new Car(1, CarType.Bus, 12));
            p.AddCar(new Car(2, CarType.Bus, 13));
            p.AddCar(new Car(3, CarType.Bus, 14));
            p.AddCar(new Car(4, CarType.Bus, 15));
            p.AddCar(new Car(5, CarType.Bus, 16));
            p.AddCar(new Car(6, CarType.Bus, 16));

            Console.Read();
        }

    }
}
