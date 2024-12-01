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
        public string Model;
        public int Size;
        public string Condition;
        public int PricePerHour;
        public string Status;

        public override string ToString()
        {
            return $"Лыжи №{ID}: {Model}, Размер: {Size}, Цена: {PricePerHour} руб/ч";
        }
    }
}
