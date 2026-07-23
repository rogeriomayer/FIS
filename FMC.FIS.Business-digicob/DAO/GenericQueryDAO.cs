using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMC.FIS.Business.DAO
{
    public class GenericQueryDAO<T> : AbstractRepositoryPersistence<T> where T : class
    {
        public GenericQueryDAO() : base("CNN_FIS") { }

        public ICollection<T> GetCollection(string query)
        {
            return Context.FromSqlRaw(query.ToString()).ToList();
        }

        public T GetSingle(string query)
        {
            return Context.FromSqlRaw(query.ToString()).FirstOrDefault();
        }


        public void Update(string update)
        {
            Database.ExecuteSqlRaw(update);
        }
    }
}
