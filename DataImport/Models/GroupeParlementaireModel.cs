using System.Collections.Generic;

namespace RICAssemblee.DataImport.Models
{
    // ugly design once again
    public class GroupeParlementaireModel : BaseModel
    {
        public string Nom { get; private set; }

        public HashSet<DeputeModel> Deputes { get; private set; }

        private GroupeParlementaireModel()
        {
            Deputes = new HashSet<DeputeModel>();
        }

        private static Dictionary<string, GroupeParlementaireModel> idToGP = new Dictionary<string, GroupeParlementaireModel>();

        internal static GroupeParlementaireModel AddDepute(string organeRef, string groupName, DeputeModel depute)
        {
            if (!idToGP.ContainsKey(organeRef))
            {
                idToGP.Add(organeRef, new GroupeParlementaireModel());
            }

            idToGP[organeRef].Uid = organeRef;
            idToGP[organeRef].Nom = groupName;
            idToGP[organeRef].Deputes.Add(depute);
            return idToGP[organeRef];
        }
    }
}
