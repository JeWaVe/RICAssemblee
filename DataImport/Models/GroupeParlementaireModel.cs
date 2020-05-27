using RICAssemblee.DataImport.RawData;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RICAssemblee.DataImport.Models
{
    public class GroupeParlementaireModel : OrganeModel
    {
        public string Nom { get; private set; }

        public HashSet<DeputeModel> Deputes { get; private set; }

        public DateTimeOffset Debut { get; set; }

        public DateTimeOffset? Fin { get; set; }

        public bool Active 
        { 
            get
            {
                return !Fin.HasValue || Fin.Value > DateTimeOffset.Now;
            } 
        }

        internal GroupeParlementaireModel(Organe rawOrgane) : base(rawOrgane)
        {
            Deputes = new HashSet<DeputeModel>();
            Debut = rawOrgane.ViMoDe.DateDebut.Value;
            Fin = rawOrgane.ViMoDe.DateFin;
        }
    }
}
