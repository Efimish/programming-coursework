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
        public bool Equals(Client other)
        {
            return
                this.ID == other.ID &&
                this.Login == other.Login &&
                this.PasswordHash == other.PasswordHash &&
                this.FIO == other.FIO &&
                this.Phone == other.Phone &&
                this.Email == other.Email &&
                this.RegistrationDate == other.RegistrationDate &&
                this.BonusPoints == other.BonusPoints;
        }
    }
}
