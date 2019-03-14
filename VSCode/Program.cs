using System;
using System.IO;

namespace VSCode
{
    class Program
    {
        static void Main(string[] args)
        {

            String st = File.ReadAllText(path: "C:\\Users\\Marko\\Desktop\\VSCode\\input.txt");

            var values = st.Split(",");

            Console.WriteLine(st);
            //Console.WriteLine("Hello World!");
        }
    }
}
