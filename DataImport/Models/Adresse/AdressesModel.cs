using RICAssemblee.DataImport.RawData;

namespace RICAssemblee.DataImport.Models
{
    public abstract class BaseAdresseModel : BaseModel
    {
        public abstract AdresseType Type { get; set; }
    }

    public class AdressePostaleModel : BaseAdresseModel
    {
        public override AdresseType Type { get; set; }

        public string NomRue { get; set; }
        public string NumeroRue { get; set; }
        public string Ville { get; set; }
        public string CodePostal { get; set; }
        public string Complement { get; set; }
    }

    public class AdresseReseauModel : BaseAdresseModel
    {
        public override AdresseType Type { get; set; }

        public string Valeur { get; set; }

        public AdressePostaleModel AdresseDeRattachement { get; set; }
    }
}
