using RICAssemblee.DataImport.RawData;
using System.Collections.Generic;
using System.Linq;

namespace RICAssemblee.DataImport.Models
{
    public class Organe
    {
        public string Libelle { get; set; }

        public TypeOrgane Type { get; set; }

        public string Uid { get; set; }

        public Organe Parent { get; set; }

        internal static List<Organe> Parse(IEnumerable<RawData.Organe> rawOrganes)
        {
            var tmp = new Dictionary<string, Organe>();
            foreach (var rawOrgane in rawOrganes)
            {
                if(!tmp.ContainsKey(rawOrgane.Uid))
                {
                    tmp.Add(rawOrgane.Uid, new Organe
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

            return tmp.Values.ToList();
        }

        public static List<Organe> FromDirectory(string path)
        {
            return Parse(RawOrgane.FromDirectory(path));
        }
    }
}
