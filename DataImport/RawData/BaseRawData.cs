using Newtonsoft.Json;
using RICAssemblee.DataImport.Models;

namespace RICAssemblee.DataImport.RawData
{
    public abstract class BaseRawData
    {
        public abstract string Uid { get; set; }
    }
}
