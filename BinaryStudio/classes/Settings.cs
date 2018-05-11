using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryStudio
{
    static class Settings
    {
        static Dictionary<CarType, int> pricesDict = new Dictionary<CarType, int>()
        {
            { CarType.Motorcycle, 1 },
            { CarType.Bus, 2 },
            { CarType.Passenger, 3 },
            { CarType.Truck, 5 }
        };
        static public Dictionary<CarType, int> PricesDict { get { return pricesDict; } }

        private static int timeOut = 3;
        public static int TimeOut
        {
            get { return timeOut; }
            set
            {
                if (value > 0)
                    timeOut = value;
            }
        }

        private static int parkingSpace = 5;
        public static int ParkingSpace
        {
            get { return parkingSpace; }
            set
            {
                if (value > 0)
                    parkingSpace = value;
            }
        }

        private static double fine = 1.2;
        public static double Fine
        {
            get { return fine; }
            set
            {
                if (value > 0)
                    fine = value;
            }
        }

        
    }
}
