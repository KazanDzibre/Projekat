using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    class CntOutput
    {
        [Index(0)]
        public string Name
        {
            get;
            set;
        }
        [Index(1)]
        public string Time
        {
            get;
            set;
        }

        [Index(2)]
        public double cnt_value
        {
            get;
            set;
        }

        public CntOutput(double cnt_value)
        {
            Name = "Brojac 1";
            Time = DateTime.Now.ToString("H:mm:ss:fff");
            this.cnt_value = cnt_value;
        }
    }
}
