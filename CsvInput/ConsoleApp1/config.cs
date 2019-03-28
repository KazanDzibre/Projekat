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
        public int Id { get; set; }

        public config(string Name, int Id) {
            this.Name = Name;
            this.Id = Id;
        }
    }
}
