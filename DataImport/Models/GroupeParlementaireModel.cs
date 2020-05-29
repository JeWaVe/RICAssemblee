using RICAssemblee.DataImport.RawData;
using System;
using System.Collections.Generic;
namespace RICAssemblee.DataImport.Models
{
    public class GroupeParlementaireModel : OrganeModel, IComparable<GroupeParlementaireModel>
    {
        public HashSet<DeputeModel> Deputes { get; private set; }

        public DateTimeOffset Debut { get; set; }

        public DateTimeOffset? Fin { get; set; }

        public int Legislature { get; set; }


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
            Legislature = (int)rawOrgane.Legislature;
        }

        public int CompareTo(GroupeParlementaireModel other)
        {
            return this.Uid.CompareTo(other.Uid);
        }
    }
}
