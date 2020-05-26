using RICAssemblee.DataImport.RawData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RICAssemblee.DataImport.Models
{
    public class ModelFactory
    {

        // TODO: IOC here instead of ugly singleton
        private IModelStorage _modelStorage = ModelStorage.Singleton();

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
            // TODO : memoize that
            var organes = Organes(parentDir);
            return RawActeur.FromDirectory(Path.Combine(parentDir, "acteur")).Select(rawActeur => new DeputeModel(rawActeur));
        }
    }
}
