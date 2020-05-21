using RICAssemblee.DataImport.RawData;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RICAssemblee.DataImport.Models
{
    public class ModelFactory
    {
        public static IEnumerable<DeputeModel> MakeDeputes(string parentDir)
        {
            // TODO: move some that all in that class (OrganeModel/DeputeModel should not have all that knowledge)
            OrganeModel.Parse(RawOrgane.FromDirectory(Path.Combine(parentDir, "organe")));
            return Directory.GetFiles(Path.Combine(parentDir, "acteur")).Select(f => new DeputeModel(RawActeur.FromJson(File.ReadAllText(f)).Acteur));
        }
    }
}
