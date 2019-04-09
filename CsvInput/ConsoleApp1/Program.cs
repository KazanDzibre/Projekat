using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace ServerApp
{

    class Program
    {
        private static System.Timers.Timer interruptGenerator;

        private static List<outputForm> objListOut = new List<outputForm>();

        private static AdamCNT AdamComponent;

      

        static void Main(string[] args)
        {

            Thread myThread = new Thread(() => ThreadFunctions.threadFun(AdamComponent));

            FileIO.inputFun();

            /*      Ucitavanje input fajla      */
            int time = FileIO.objListIn[0].Time;
            Constants.DEF_IP = FileIO.objListIn[0].Ip;

            AdamComponent = new AdamCNT();

            /*      Kreiranje socketa       */
            AdamComponent.createCounterSocket();
            AdamComponent.createSwitchSocket();

            AdamComponent.counterStart();

            myThread.Start();

            setTimer(time);
            Console.Write("Press ESC to exit...\n");

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                myThread.Abort();
                AdamComponent.resetCounter();
                Environment.Exit(0);
            }
        }

        private static void setTimer(int time) {
            interruptGenerator = new System.Timers.Timer(time * 1000);
            interruptGenerator.Elapsed += OnSignal;
            interruptGenerator.AutoReset = true;
            interruptGenerator.Enabled = true;
           
        }
        private static void OnSignal(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Entered timer... ");
            Console.WriteLine("######################");
            Console.WriteLine("Count: " + AdamComponent.getCnt());
            Console.WriteLine("State: " + AdamComponent.getSwitchState());
            Console.WriteLine("######################");

            FileIO.outputFunTimer(objListOut,AdamComponent);
           
        }
    }
}