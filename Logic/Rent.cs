using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Rent : IDomainObject
    {
        public int ID { get; }
        public int ClientID { get; set; }
        public int SkisID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Price { get; set; }
        public bool Done { get; set; }
        public Rent(int id)
        {
            this.ID = id;
        }
        public override string ToString()
        {
            return $"Аренда лыж №{ID} от {StartTime} до {EndTime}";
        }
    }
}
