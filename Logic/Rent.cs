﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Rent : IDomainObject
    {
        public int ID { get; set; }
        public int ClientID;
        public int SkisID;
        public DateTime StartTime;
        public DateTime EndTime;
        public int Price;
        public bool Done;

        public override string ToString()
        {
            return $"Аренда лыж №{ID} от {StartTime} до {EndTime}";
        }
    }
}
