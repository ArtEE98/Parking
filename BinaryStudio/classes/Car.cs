using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryStudio
{
    class Car
    {
        private int id;
        public int Id { get => id; }

        private CarType type;
        public CarType Type { get => type; }

        private double balance;
        public double Balance { get => balance; }

        private double fine = 0;
        public double Fine { get => fine; set => fine = value; }

        public Car(int id, CarType type, double balance)
        {
            this.id = id;
            this.type = type;
            this.balance = balance;
        }


        public void changeBalance(double changeValue)
        {
            balance += changeValue;
        }
    }
}
