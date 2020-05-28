using System;
using System.Collections.Generic;
using System.IO;

namespace RICAssemblee.DataImport.Models
{
    public interface IObjectStorage<TBase> where TBase : class
    {
        IEnumerable<TBase> All { get; }

        TModel Get<TModel>(string id) where TModel : class, TBase;

        bool Contains<TModel>(string id) where TModel : class, TBase;

        void Register<TModel>(string id, TModel model) where TModel : class, TBase;
    }


    public class ObjectStorage<TBase> : IObjectStorage<TBase> where TBase : class
    {
        private Dictionary<string, TBase> _container;

        private ObjectStorage()
        {
            _container = new Dictionary<string, TBase>();
        }

        private static Dictionary<Type, object> _instances = new Dictionary<Type, object>();

        public static ObjectStorage<TBase> Singleton()
        {
            if(!_instances.ContainsKey(typeof(TBase)))
            {
                _instances.Add(typeof(TBase), new ObjectStorage<TBase>());
            }

            return _instances[typeof(TBase)] as ObjectStorage<TBase>;
        }

        public void Register<TModel>(string id, TModel model) where TModel : class, TBase
        {
            if(_container.ContainsKey(id.ToLowerInvariant()))
            {
                return;
            }

            _container.Add(id.ToLowerInvariant(), model);
        }

        public IEnumerable<TBase> All => _container.Values;

        public bool Contains<TModel>(string id) where TModel : class, TBase
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

        public TModel Get<TModel>(string id) where TModel : class, TBase
        {
            if (!_container.ContainsKey(id.ToLowerInvariant()))
            {
                throw new KeyNotFoundException(string.Format($"organe not found for id : {id}"));
            }

            var result = _container[id.ToLowerInvariant()] as TModel;

            if (result == null)
            {
                throw new InvalidDataException(string.Format($"object {id} is not of correct type"));
            }

            return result;
        }
    }
}
