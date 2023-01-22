using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Table:ImportedObject
    {
        public double NumberOfChildren { get; set; }
        public List<Column> Columns { get; set; }

        public Table()//:base()
        {
            Columns = new List<Column>();
        }
    }
}
