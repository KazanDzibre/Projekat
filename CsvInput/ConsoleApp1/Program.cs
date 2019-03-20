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
        // Globalna lista izlaznih podataka, da bi se videla u thraed-u
        public class globals
        {

            public static List<config> outputs;

        }
        // ovo ce biti funkcija za ispisivanje trenutnih stanja promenljivih koje ce cuvati podatke sa adama
        public static void interruptHandler()
        {
            Console.Write("Usao u interrupt\n");
            using (var writer = new StreamWriter("output.csv"))         //output.csv kada mu ne definisemo putanju se po default-u nalazi u debug folderu 
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(globals.outputs);
            }
        }

        // thread koji za sad pali interruptHandler samo kad mu se posalje 1 iz konzole, kad budemo primali podatke sa adama to ce mu biti interrupt
        public static void threadFun()
        {
            while (true)
            {
                var ch = Console.Read();
                if (ch == '1')
                {
                    interruptHandler();
                }
            }
        }
        public class config
        {
            [Index(0)]
            public string Name { get; set; }
            [Index(1)]
            public int Id { get; set; }
        }
        static void Main(string[] args)
        {
            using (var reader = new StreamReader("input.txt"))
            using (var csv = new CsvReader(reader))
            {
                //csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();  //Ovo za slucaj da ne odgovara header opisu
                var records = csv.GetRecords<config>();

            }

            globals.outputs = new List<config>
            {
                new config {Name = "Something", Id = 1},
                new config {Name = "Something else", Id = 2},
            };

            Thread myThread = new Thread(new ThreadStart(threadFun));

            myThread.Start();

            //Console.Write("Hello World");

            while (true)
            {
                Thread.Sleep(5000);
                Console.Write("Ovde ce biti ispis\n");
            }
        }
    }
}
