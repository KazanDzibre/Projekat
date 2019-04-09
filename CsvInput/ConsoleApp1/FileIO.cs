using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    class FileIO
    {

        public static List<config> objListIn;

        public FileIO()
        {
            objListIn = new List<config>();
        }

        public static void inputFun()
        {
            using (var reader = new StreamReader("input.txt"))
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords<config>();
                objListIn = records.ToList();
            }
        }

        public static void outputFunTimer(List<outputForm> objListOut, AdamCNT AdamComponent)
        {
            objListOut.Add(new outputForm(AdamComponent.getCnt(), AdamComponent.getSwitchState()));
            using (var writer = new StreamWriter("TimerOutput.csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(objListOut);
            }
        }

        public static void outputSwitch(List<SwitchOutput> objListOutSwitch, SwitchOutput Switch)
        {
            objListOutSwitch.Add(new SwitchOutput("Prekidac 1", Switch.Time_start, Switch.Time_end, Switch.State));
            using (var writer = new StreamWriter("SwitchOutput.csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(objListOutSwitch);
            }
        }

        public static void outputCnt(List<CntOutput> cntOutList, double Cnt)
        {
            cntOutList.Add(new CntOutput(Cnt));
            using (var writer = new StreamWriter("CounterOutput.csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(cntOutList);
            }
        }
    }
}
