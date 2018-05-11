using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryStudio
{
    class Parking
    {
        private static Parking p;

        private double balance = 0;
        public double Income()
        {
            return balance;
        }

        private List<Car> cars;
        public List<Car> Cars { get => cars; set => cars = value; }

        private List<Transaction> transactions;
        public List<Transaction> Transactions { get => transactions; set => transactions = value; }

     

        private Parking()
        {
        }

        public static Parking getParking()
        {
            if (p == null)
                p = new Parking();
            return p;
        }

        public int FreeSize()
        {
            return Settings.ParkingSpace - cars.Count;
        }

        public Boolean AddCar(Car car)
        {
            if (FreeSize() > 0)
            {
                cars.Add(car);
                return true;
            }
            else
            {
                InfoLogger.Log.Info("Cant Add Car with id" + car.Id);
                return false;
            }
        }

        public Boolean DelCar(int id)
        {
            Car car = null;

            try
            {
                car = FindCarByIf(id);
            }catch (ArgumentException e)
            {
                Console.WriteLine(e);
                return false;
            }

            if (car.Fine != 0)
            {
                if (car.Balance >= car.Fine)
                {
                    Transaction t = new Transaction();
                    car.changeBalance(-car.Fine);
                    cars.Remove(car);
                    balance = balance + car.Fine;
                    t.Write_offs.Add(car.Id, car.Fine);
                    InfoLogger.Log.Info("The fine is paid by car " + car.Id);
                    return true;
                }
                else
                {
                    InfoLogger.Log.Info("Not enough money for pay the fine by car " + car.Id);
                    return false;
                }
            }
            else
            {
                cars.Remove(car);
                return true;
            }
        }

        private Car FindCarByIf(int id)
        {
            Car result = cars.Find(x => x.Id == id);
            if (result == null)
            {
                String errorString = "There is not Car with id " + id;
                InfoLogger.Log.Error(errorString);
                throw new ArgumentException(errorString);
            }
            return result;
        }

        public static void CarsPay(Parking p, List<Car> cars)
        {
            Transaction t = new Transaction();
            foreach(Car car in cars)
            {
                int pay = Settings.PricesDict[car.Type];
                int paid = 0;
                if (car.Balance >= pay)
                {
                    car.changeBalance(-pay);
                    paid = pay;
                }
                else
                {
                    car.Fine = car.Fine + pay*Settings.Fine;
                    paid = 0;
                    InfoLogger.Log.Info("Fine for car " + car.Id + " is increased");
                }
                p.balance = p.balance + paid;
                t.Write_offs.Add(car.Id, paid);
            }
            p.transactions.Add(t);
        }

    }
}
