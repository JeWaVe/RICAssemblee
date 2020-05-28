namespace RICAssemblee.DataImport.Models
{
    public class BaseModel
    {
        // TODO: IOC here instead of ugly singleton
        protected IObjectStorage<BaseModel> _modelStorage = ObjectStorage<BaseModel>.Singleton();

        public string Uid { get; set; }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            return (obj as BaseModel) ?.Uid == Uid;
        }

        public override int GetHashCode()
        {
            return Uid.GetHashCode();
        }
    }
}
