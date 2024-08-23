using System;
using System.Collections.Generic;

namespace FMC.Generic
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Add(TEntity entity);
        void AddAsync(TEntity entity);
        int AddRangeBulk(List<TEntity> listEntity);
        int AddRangeNormal(List<TEntity> listEntity);
        TEntity Update(TEntity entity);
        void UpdateAsync(TEntity entity);
        TEntity UpdateNoContext(TEntity entity);
        int UpdateRangeBulk(List<TEntity> listEntity);
        int UpdateRangeNormal(List<TEntity> listEntity);
        int Delete(TEntity entity);
        int DeleteRange(List<TEntity> listEntity);
        TEntity GetBykey(object key);
        ICollection<TEntity> GetAll();

    }
}