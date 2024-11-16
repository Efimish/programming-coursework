using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Rent : IDomainObject
    {
        public int ID { get; set; }
        int ClientID;
        int SkisID;
        int EmployeeID;
        DateTime StartTime;
        DateTime EndTime;
        int Price;
    }
}
