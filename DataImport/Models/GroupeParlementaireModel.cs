using RICAssemblee.DataImport.RawData;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RICAssemblee.DataImport.Models
{
    public class GroupeParlementaireModel : OrganeModel
    {
        private IModelStorage _modelStorage = ModelStorage.Singleton();

        public string Nom { get; private set; }

        public HashSet<DeputeModel> Deputes { get; private set; }

        internal GroupeParlementaireModel(Organe rawOrgane) : base(rawOrgane)
        {
            Deputes = new HashSet<DeputeModel>();
        }

        internal GroupeParlementaireModel AddDepute(string organeRef, string groupName, DeputeModel depute)
        {
            if(!_modelStorage.Contains<GroupeParlementaireModel>(organeRef))
            {
                throw new KeyNotFoundException(string.Format($"groupe parlementaire non trouvé : {organeRef}"));
            }

            var result = _modelStorage.Get<GroupeParlementaireModel>(organeRef);
            result.Uid = organeRef;
            result.Nom = groupName;
            result.Deputes.Add(depute);
            return result;
        }
    }
}
