using System.Collections.Generic;
using System.Data;
using System.IO;

namespace RICAssemblee.DataImport.Models
{
    public interface IModelStorage
    {
        TModel Get<TModel>(string id) where TModel : BaseModel;

        bool Contains<TModel>(string id) where TModel : BaseModel;

        void Register<TModel>(string id, TModel model) where TModel : BaseModel;
    }


    public class ModelStorage : IModelStorage
    {
        private Dictionary<string, BaseModel> _container;

        private ModelStorage()
        {
            _container = new Dictionary<string, BaseModel>();
        }

        private static ModelStorage _instance = null;

        public static ModelStorage Singleton()
        {
            if (_instance == null)
                _instance = new ModelStorage();

            return _instance;
        }

        public void Register<TModel>(string id, TModel model) where TModel : BaseModel
        {
            if(_container.ContainsKey(id.ToLowerInvariant()))
            {
                throw new DataException(string.Format($"object {id} already registered"));
            }

            _container.Add(id.ToLowerInvariant(), model);
        }

        public bool Contains<TModel>(string id) where TModel : BaseModel
        {
            if(!_container.ContainsKey(id.ToLowerInvariant()))
            {
                return false;
            }

            var result = _container[id.ToLowerInvariant()] as TModel;
            if (result == null)
                return false;

            return true;
        }

        public T Get<T>(string id) where T : BaseModel
        {
            if (!_container.ContainsKey(id.ToLowerInvariant()))
            {
                throw new KeyNotFoundException(string.Format($"organe not found for id : {id}"));
            }

            var result = _container[id.ToLowerInvariant()] as T;

            if (result == null)
            {
                throw new InvalidDataException(string.Format($"object {id} is not of correct type"));
            }

            return result;
        }
    }
}
