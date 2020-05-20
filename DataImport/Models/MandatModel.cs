using RICAssemblee.DataImport.RawData;
using System;

namespace RICAssemblee.DataImport.Models
{
    public class MandatModel: BaseModel
    {
        public string Libelle { get; set; }

        public DateTimeOffset Debut { get; set; }

        // if null -> still running
        public DateTimeOffset? Fin { get; set; }

        public Qualite Qualite { get; set; }

        public OrganeModel[] Organes { get; set; }
    }
}
