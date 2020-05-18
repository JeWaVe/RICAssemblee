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

        internal static IEnumerable<Organe> Parse(IEnumerable<RawData.Organe> rawOrganes)
        {
            // TODO: faster
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

            return tmp.Values;
        }

        public static IEnumerable<Organe> FromDirectory(string path)
        {
            return Parse(RawOrgane.FromDirectory(path));
        }
    }
}
