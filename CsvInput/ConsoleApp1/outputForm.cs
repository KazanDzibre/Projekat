using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    public class outputForm
    {
        [Index(0)]
        public string time {
            get;
            set;
        }
        [Index(1)]
        public double counter_value {
            get;
            set;
        }

        [Index(2)]
        public string switcher_value {
            get;
            set;
        }

        public outputForm(double counter_value, string switcher_value)
        {
            time = DateTime.Now.ToString("H:mm:ss");
            this.counter_value = counter_value;
            this.switcher_value = switcher_value;
        }
    }
}
