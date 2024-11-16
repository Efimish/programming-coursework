using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Skis : IDomainObject
    {
        public int ID { get; set; }
        string Model;
        int Size;
        string Condition;
        int PricePerHour;
        string Status;
    }
}
