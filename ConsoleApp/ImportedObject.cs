using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal abstract class ImportedObject
    {
        internal string Name { get; set; }
        internal string Type { get; set; }
        internal string Schema { get; set; }

        internal string ParentName { get; set; }
        internal string ParentType { get; set; }

        internal string DataType { get; set; }
        internal string IsNullable { get; set; }
    }
}
