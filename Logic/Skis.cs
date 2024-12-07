namespace Logic
{
    public class Skis : IDomainObject
    {
        public int ID { get; }
        public string Model { get; set; }
        public int Size { get; set; }
        public string Condition { get; set; }
        public int PricePerHour { get; set; }
        public string Status { get; set; }
        public Skis(int id)
        {
            this.ID = id;
        }
        public override string ToString()
        {
            return $"Лыжи №{ID}: {Model}, Размер: {Size}, Состояние: {Condition}, Цена: {PricePerHour} руб/ч";
        }
        public bool Equals(Skis other)
        {
            return
                this.ID == other.ID &&
                this.Model == other.Model &&
                this.Size == other.Size &&
                this.Condition == other.Condition &&
                this.PricePerHour == other.PricePerHour &&
                this.Status == other.Status;
        }
    }
}
