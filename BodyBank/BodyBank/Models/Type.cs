namespace BodyBank.Model
{
    public class Type
    {

        public int TypeId { get; set; }

        public string Nom { get; set; }
        public double PrixBase { get; set; }
        public string Desc { get; set; }
        public string Image { get; set; }
        public Type() { }
    }
}
