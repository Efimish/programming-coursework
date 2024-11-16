﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Client : IDomainObject
    {
        public int ID { get; set; }
        public string FIO;
        public string Phone;
        public string Email;
        public DateTime RegistrationDate;
        public int BonusPoints;
    }
}