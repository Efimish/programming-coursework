using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Employee : IDomainObject
    {
        public int ID { get; set; }
        string FIO;
        string Job;
        DateTime EmploymentDate;
        string Phone;
        string PasswordHash;
    }
}
