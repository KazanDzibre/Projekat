using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    class SwitchOutput
    {
        [Index(0)]
        public string Name
        {
            get;
            set;
        }
        [Index(1)]
        public string Time_start
        {
            get;
            set;
        }

        [Index(2)]
        public string Time_end
        {
            get;
            set;
        }

        public string State
        {
            get;
            set;
        }

        public SwitchOutput() { }

        public SwitchOutput(string Name, string Time_start, string Time_end, string State)
        {
            this.Name = Name;
            this.Time_start = Time_start;
            this.Time_end = Time_end;
            this.State = State;
        }
    }
}
