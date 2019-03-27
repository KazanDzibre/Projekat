using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApp1
{
    
    class Program
    {
        public static int flag = 0;

        private static System.Timers.Timer interruptGenerator; //timer za merenje kada treba da ispisuje

        // ovo ce biti funkcija za ispisivanje trenutnih stanja promenljivih koje ce cuvati podatke sa adama
        public static void interruptHandler(List<config> objList)
        {
            Console.Write("Usao u interrupt\n");
            using (var writer = new StreamWriter("output.csv"))         //output.csv kada mu ne definisemo putanju se po default-u nalazi u debug folderu 
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(objList);
            }
        }

        // thread koji za sad pali interruptHandler samo kad mu se posalje 1 iz konzole, kad budemo primali podatke sa adama to ce mu biti interrupt
        public static void threadFun(List<config> objList)
        {
            SetTimer();
            while (true)
            {
                if (flag == 1)
                {
                    objList.Add(new config("Something",10000));
                    interruptHandler(objList);
                    flag = 0;
                }
            }
        }
        static void Main(string[] args)
        {
            
            using (var reader = new StreamReader("input.txt"))
            using (var csv = new CsvReader(reader))
            {
                //csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();  //Ovo za slucaj da ne odgovara header opisu
                var records = csv.GetRecords<config>();

            }

            List<config> objList = new List<config>();
            

            Thread myThread = new Thread(() => threadFun(objList));

            myThread.Start();
            


            while (true)
            {
                Console.Write("Press ESC to exit...\n");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true); //true here mean we won't output the key to the console, just cleaner in my opinion.
                if (keyInfo.Key == ConsoleKey.Escape)
                {                  
                    Environment.Exit(0);
                }
              
            }
        }

        private static void SetTimer()                                              // Za sad radi na 3 sekunde dize flag na 1 i ulazi u ispis i tako u krug
        {
            interruptGenerator = new System.Timers.Timer(3000);
            interruptGenerator.Elapsed += OnSignal;
            interruptGenerator.AutoReset = true;
            interruptGenerator.Enabled = true;
        }

        private static void OnSignal(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Entered timer...");
            flag = 1;
        }
    }
}
