using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    class ThreadFunctions
    {
        private static Boolean trenutna = true;

        private static List<SwitchOutput> switchOutList = new List<SwitchOutput>();

        private static SwitchOutput Switch_on = new SwitchOutput();

        private static SwitchOutput Switch_off = new SwitchOutput();

        public static void threadFun(AdamCNT AdamComponent)
        {
            Switch_off.State = "OFF";
            Switch_on.State = "ON";
            Switch_on.Time_start = DateTime.Now.ToString("H:mm:ss:fff");
            Switch_off.Time_start = DateTime.Now.ToString("H:mm:ss:fff");

            while (true)
            {
                if (AdamComponent.switchRead() == 1 && trenutna == true)
                {
                    Console.WriteLine("Usao na promenu ON");
                    Switch_on.Time_start = DateTime.Now.ToString("H:mm:ss:fff");
                    Switch_off.Time_end = DateTime.Now.ToString("H:mm:ss:fff");
                    FileIO.outputSwitch(switchOutList, Switch_off);
                    trenutna = false;
                }
                else if (AdamComponent.switchRead() == 0 && trenutna == false)
                {

                    Switch_on.Time_end = DateTime.Now.ToString("H:mm:ss:fff");
                    Switch_off.Time_start = DateTime.Now.ToString("H:mm:ss:fff");
                    FileIO.outputSwitch(switchOutList, Switch_on);
                    Console.WriteLine("Usao na promenu OFF");
                    trenutna = true;
                }
                AdamComponent.counterRead();
            }
        }
    }
}
