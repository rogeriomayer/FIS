using System;
using System.Collections.Generic;


namespace FMC.Generic
{
    public class BLL<TModel, TPersistence> : AbstractBLL<TModel>
        where TModel : class
        where TPersistence : class, IRepository<TModel>, new()
    {
        public BLL() { }

        public TPersistence persistence = new TPersistence();

        public override TModel Add(TModel model)
        {
            try
            {
                return persistence.Add(model);
            }
            catch (Exception ex)
            {
                persistence = new TPersistence();
                throw ex;
            }
        }

        public override void AddAsync(TModel model)
        {
            try
            {
                persistence.AddAsync(model);
            }
            catch (Exception ex)
            {
                persistence = new TPersistence();
                throw ex;
            }
        }

        public override int AddRangeBulk(List<TModel> listModel)
        {
            return persistence.AddRangeBulk(listModel);
        }

        public override int AddRangeNormal(List<TModel> listModel)
        {
            return persistence.AddRangeNormal(listModel);
        }

        public override TModel Update(TModel model)
        {
            return persistence.Update(model);
        }

        public override void UpdateAsync(TModel model)
        {
            persistence.UpdateAsync(model);
        }

        public override TModel UpdateNoContext(TModel model)
        {
            return persistence.UpdateNoContext(model);
        }

        public override int UpdateRangeBulk(List<TModel> listModel)
        {
            return persistence.UpdateRangeBulk(listModel);
        }

        public override int UpdateRangeNormal(List<TModel> listModel)
        {
            return persistence.UpdateRangeNormal(listModel);
        }

        public override void Delete(TModel model)
        {
            persistence.Delete(model);
        }

        public override void Delete(object key)
        {
            TModel user = this.GetBykey(key);
            this.Delete(user);
        }

        public override int DeleteRange(List<TModel> listModel)
        {
            return persistence.DeleteRange(listModel);
        }

        public override TModel GetBykey(object key)
        {
            return persistence.GetBykey(key);
        }

        public override ICollection<TModel> GetAll()
        {
            return persistence.GetAll();
        }
    }
}
