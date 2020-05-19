using RICAssemblee.DataImport.RawData;
using System.Collections.Generic;
using System.Linq;

namespace RICAssemblee.DataImport.Models
{
    public class OrganeModel : BaseModel
    {
        public string Libelle { get; set; }

        public TypeOrgane Type { get; set; }

        public OrganeModel Parent { get; set; }

        internal static IEnumerable<OrganeModel> Parse(IEnumerable<RawData.Organe> rawOrganes)
        {
            // TODO: faster
            var tmp = new Dictionary<string, OrganeModel>();
            foreach (var rawOrgane in rawOrganes)
            {
                if(!tmp.ContainsKey(rawOrgane.Uid))
                {
                    tmp.Add(rawOrgane.Uid, new OrganeModel
                    {
                        Libelle = rawOrgane.Libelle,
                        Type = rawOrgane.CodeType,
                        Uid = rawOrgane.Uid
                    });
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

        public static IEnumerable<OrganeModel> FromDirectory(string path)
        {
            return Parse(RawOrgane.FromDirectory(path));
        }
    }
}
