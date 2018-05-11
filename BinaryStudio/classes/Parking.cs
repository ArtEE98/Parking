using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        private List<Car> cars = new List<Car>();
        public List<Car> Cars { get => cars; set => cars = value; }

        private List<Transaction> transactions = new List<Transaction>();
        public List<Transaction> Transactions { get => transactions; set => transactions = value; }

        private Parking()
        {
            Parking par = this;

            TimerCallback t1 = new TimerCallback(CarsPay);
            TimerCallback t2 = new TimerCallback(SaveTrancsactions);

            Timer timer = new Timer(t1, par, 0, 2000);
            Timer timer2 = new Timer(t2, par, 0, 6000);
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

        public static void SaveTrancsactions(object obj)
        {
            Parking p = (Parking)obj;
            double sum = 0;
            if (p.transactions == null)
                return;
            foreach(var t in p.transactions)
            {
                if (t.Write_offs == null)
                    continue;
                foreach(var pair in t.Write_offs)
                {
                    sum += pair.Value;
                }
            }
            p.transactions = new List<Transaction>();
            Logger.Log.Info("Sum of transactions: " + sum);
        }

        public static void CarsPay(object obj)
        {
            Transaction t = new Transaction();
            Parking p = (Parking)obj;
            if (p.cars == null)
                return;
            foreach(Car car in p.cars)
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
