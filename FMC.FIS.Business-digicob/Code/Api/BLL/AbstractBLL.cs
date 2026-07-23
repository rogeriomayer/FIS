using System.Collections.Generic;

namespace FMC.Generic
{
    public abstract class AbstractBLL<TModel> : IAbstractBLL<TModel> where TModel : class
    {

        public abstract TModel Add(TModel model);
        public abstract void AddAsync(TModel model);
        public abstract int AddRangeBulk(List<TModel> listModel);
        public abstract int AddRangeNormal(List<TModel> listModel);
        public abstract TModel Update(TModel model);
        public abstract void UpdateAsync(TModel model);
        public abstract TModel UpdateNoContext(TModel model);
        public abstract int UpdateRangeBulk(List<TModel> listModel);
        public abstract int UpdateRangeNormal(List<TModel> listModel);
        public abstract void Delete(TModel model);
        public abstract void Delete(object key);
        public abstract int DeleteRange(List<TModel> listModel);
        public abstract TModel GetBykey(object key);
        public abstract ICollection<TModel> GetAll();
    }
}
