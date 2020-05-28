using RICAssemblee.DataImport.RawData;

namespace RICAssemblee.DataImport.Models
{
    public class OrganeModel : BaseModel
    {
        internal OrganeModel(Organe rawOrgane)
        {
            this.Uid = rawOrgane.Uid.ToLowerInvariant();
            this.Nom = rawOrgane.Libelle;
            this.Type = rawOrgane.CodeType;

            _modelStorage.Register(this.Uid, this);
        }

        public string Nom { get; set; }

        public TypeOrgane Type { get; set; }

        public OrganeModel Parent { get; set; }


    }
}
