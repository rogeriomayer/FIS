using FMC.FIS.Business.DAO;
using FMC.Generic;
using System.Collections.Generic;

namespace FMC.FIS.Business.BLL
{
    public class GenericQueryBLL<T> : BLL<T, GenericQueryDAO<T>> where T : class
    {
        public ICollection<T> GetCollection(string query)
        {
            return persistence.GetCollection(query);
        }

        public T GetSingle(string query)
        {
            return persistence.GetSingle(query);
        }

        public void Update(string query)
        {
            persistence.Update(query);
        }
    }
}
