using System.Collections.Generic;

namespace FMC.Generic
{
    public interface IAbstractBLL<TModel> where TModel : class
    {
        TModel Add(TModel model);
        void AddAsync(TModel model);
        int AddRangeBulk(List<TModel> listModel);
        int AddRangeNormal(List<TModel> listModel);
        TModel Update(TModel model);
        void UpdateAsync(TModel model);
        TModel UpdateNoContext(TModel entity);
        int UpdateRangeBulk(List<TModel> listModel);
        int UpdateRangeNormal(List<TModel> listModel);
        void Delete(TModel entity);
        void Delete(object key);
        int DeleteRange(List<TModel> listModel);
        TModel GetBykey(object key);
        ICollection<TModel> GetAll();
    }
}
