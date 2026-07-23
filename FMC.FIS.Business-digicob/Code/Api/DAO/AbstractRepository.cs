using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;


namespace FMC.Generic
{
    public abstract class AbstractRepository<TEntity> : DbContext, IRepository<TEntity> where TEntity : class
    {

        private string StringConnection;
        public AbstractRepository(string stringConnection)
        {
            StringConnection = stringConnection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString(StringConnection),
                opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));

            optionsBuilder.UseLazyLoadingProxies();
        }

        public abstract TEntity Add(TEntity entity);
        public abstract void AddAsync(TEntity entity);
        public abstract int AddRangeBulk(List<TEntity> listEntity);
        public abstract int AddRangeNormal(List<TEntity> listEntity);
        public abstract TEntity Update(TEntity entity);
        public abstract void UpdateAsync(TEntity entity);
        public abstract TEntity UpdateNoContext(TEntity entity);
        public abstract int UpdateRangeBulk(List<TEntity> listEntity);
        public abstract int UpdateRangeNormal(List<TEntity> listEntity);
        public abstract int Delete(TEntity entity);
        public abstract int DeleteRange(List<TEntity> listEntity);
        public abstract TEntity GetBykey(object key);
        public abstract IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);
        public abstract ICollection<TEntity> GetAll();

    }
}
