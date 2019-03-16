using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static void interruptHandler() {
            Console.Write("Usao u interrupt\n");
        }

        public static void threadFun() {
            while (true) {
                var ch = Console.Read();
                if (ch == '1') {
                    interruptHandler();
                }
            }
        }
        public class config
        {
            [Index(0)]
            public string Name { get; set; }
            [Index(1)]
            public int Adress { get; set; }
        }
        static void Main(string[] args)
        {
            using (var reader = new StreamReader("input.txt")) 
            using (var csv = new CsvReader(reader))
            {
                //csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();  //Ovo za slucaj da ne odgovara header opisu
                var records = csv.GetRecords<config>();
            }

            Thread myThread = new Thread(new ThreadStart(threadFun));

            myThread.Start();

            //Console.Write("Hello World");

            while (true) {
                Thread.Sleep(5000);
                Console.Write("Ovde ce biti ispis\n");
            }
        }
    }
}
