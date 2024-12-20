using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Relation
    {
        public string pkTableName { get; set; }
        public string pkColumnName { get; set; }
        public string fkTableName { get; set; }
        public string fkColumnName { get; set; }
        public string fkName { get; set; }
    }
}
