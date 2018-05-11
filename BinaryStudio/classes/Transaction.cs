using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryStudio
{
    class Transaction
    {
        public Transaction()
        {
            data = DateTime.Now;
        }

        private DateTime data;
        public DateTime Data { get => data; }

        private Dictionary<int, double> write_offs = new Dictionary<int, double>();
        public Dictionary<int, double> Write_offs { get => write_offs; set => write_offs = value; }

        public static void Display(Transaction t)
        {
            Console.WriteLine("Data: " + t.data.Date + " Time:" + t.data.TimeOfDay);
            foreach(var pair in t.Write_offs)
            {
                Console.WriteLine("Car index " + pair.Key + "paid " + pair.Value);
            }
        }

    }
}
