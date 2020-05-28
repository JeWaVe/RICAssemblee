using RICAssemblee.DataImport.RawData;
using System.Collections.Generic;
using System.IO;

namespace RICAssemblee.DataImport.Models
{
    public class ModelFactory
    {

        // TODO: IOC here instead of ugly singleton
        private IObjectStorage<BaseModel> _modelStorage = ObjectStorage<BaseModel>.Singleton();

        public IEnumerable<OrganeModel> Organes(string parentDir)
        {
            var tmp = RawOrgane.FromDirectory(Path.Combine(parentDir, "organe"));
            var result = new List<OrganeModel>();
            foreach(var rawOrgane in tmp)
            {
                OrganeModel model;
                if(rawOrgane.CodeType == TypeOrgane.Gp)
                {
                    model = new GroupeParlementaireModel(rawOrgane);
                }
                else
                {
                    model = new OrganeModel(rawOrgane);
                }

                if (rawOrgane.OrganeParent != null && _modelStorage.Contains<OrganeModel>(rawOrgane.OrganeParent))
                {
                    model.Parent = _modelStorage.Get<OrganeModel>(rawOrgane.OrganeParent);
                }

                result.Add(model);
            }

            return result;
        }

        public IEnumerable<DeputeModel> Deputes(string parentDir)
        {
            var result = new List<DeputeModel>();
            foreach (var rawActeur in RawActeur.FromDirectory(Path.Combine(parentDir, "acteur")))
            {
                result.Add(new DeputeModel(rawActeur));
            }

            return result;
        }
        public IEnumerable<ScrutinModel> Scrutins(string directory)
        {
            var result = new List<ScrutinModel>();
            foreach(var rawScrutin in RawScrutin.FromDirectory(directory))
            {
                result.Add(new ScrutinModel(rawScrutin));
            }

            return result;
        }
    }
}
