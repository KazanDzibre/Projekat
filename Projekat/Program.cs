using System;
using CsvHelper;

namespace Projekat
{
    class Program
    {
        static void Main(string[] args)
        {
            TextReader reader = new StreamReader("import.txt");
            var csvReader = new CsvReader(reader);
            var records = csvReader.GetRecords<config>();

            /*class1 c1 = new class1();
            Console.WriteLine(c1.ReturnMessage());
            Console.WriteLine("Hello World!");
        */
        }
    }
}
