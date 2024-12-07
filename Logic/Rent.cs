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
        public bool Equals(Rent other)
        {
            return
                this.ID == other.ID &&
                this.ClientID == other.ClientID &&
                this.SkisID == other.SkisID &&
                this.StartTime == other.StartTime &&
                this.EndTime == other.EndTime &&
                this.Price == other.Price &&
                this.Done == other.Done;
        }
    }
}
