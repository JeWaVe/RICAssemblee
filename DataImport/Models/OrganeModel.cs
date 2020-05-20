using RICAssemblee.DataImport.RawData;
using System.Collections.Generic;

namespace RICAssemblee.DataImport.Models
{
    public class OrganeModel : BaseModel
    {
        public string Libelle { get; set; }

        public TypeOrgane Type { get; set; }

        public OrganeModel Parent { get; set; }

        internal static OrganeModel FromId(string id)
        {
            if(!IdToModel.ContainsKey(id))
            {
                throw new KeyNotFoundException(string.Format($"organe not found for id : {id}"));
            }

            return IdToModel[id];
        }

        private static Dictionary<string, OrganeModel> IdToModel = new Dictionary<string, OrganeModel>();

        internal static IEnumerable<OrganeModel> Parse(IEnumerable<RawData.Organe> rawOrganes)
        {
            // TODO: faster
            var tmp = new Dictionary<string, OrganeModel>();
            foreach (var rawOrgane in rawOrganes)
            {
                if(!tmp.ContainsKey(rawOrgane.Uid))
                {
                    var model = new OrganeModel
                    {
                        Libelle = rawOrgane.Libelle,
                        Type = rawOrgane.CodeType,
                        Uid = rawOrgane.Uid
                    };

                    tmp.Add(rawOrgane.Uid, model);
                    IdToModel.Add(rawOrgane.Uid, model);
                }
            }

            foreach(var rawOrgane in rawOrganes)
            {
                if(rawOrgane.OrganeParent != null && tmp.ContainsKey(rawOrgane.OrganeParent))
                {
                    tmp[rawOrgane.Uid].Parent = tmp[rawOrgane.OrganeParent];
                }
            }
            return tmp.Values;
        }
    }
}
