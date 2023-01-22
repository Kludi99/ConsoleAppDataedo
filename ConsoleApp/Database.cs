using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Database : ImportedObject
    {
        public double NumberOfChildren { get; set; }
        public List<Table> Tables { get; set; }
        public Database() : base()
        {
            Tables = new List<Table>();
        }
    }
}
