using System;

namespace Logic
{
    public class Client : IDomainObject
    {
        public int ID { get; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string FIO { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int BonusPoints { get; set; }
        public Client(int id)
        {
            this.ID = id;
        }
        public override string ToString()
        {
            return $"Клиент №{ID}: {FIO}";
        }
    }
}
