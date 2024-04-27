namespace BodyBank.Model
{
    public class Organne
    {
        public int OrganneId { get; set; }

        public bool Disponible { get; set; }
        public double Prix { get; set; }
        public Type Type { get; set; }
        public Donneur Donneur { get; set; }
        public Organne() 
        {
            Disponible = true;
        }
    }
}
