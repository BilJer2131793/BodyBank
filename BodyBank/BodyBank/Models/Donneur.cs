namespace BodyBank.Model
{
    public class Donneur
    {
        public int IdDonneur { get; set; }

        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Sexe { get; set; }
        public int? Age { get; set; }
        public Double? Poids { get; set; }
        public Double? Taille { get; set; }




        public Donneur() 
        {
            Nom = "Doe";
            Prenom = "John";
        }
    }
}
