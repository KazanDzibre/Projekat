using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    public class config
    {
        [Index(0)]
        public string Name { get; set; }
        [Index(1)]
        public int Time { get; set; }
        [Index(2)]
        public string Ip { get; set; }
        
        public config(string Name, int Time, string Ip) {
            this.Name = Name;
            this.Time = Time;
            this.Ip = Ip;
        }
    }
}
