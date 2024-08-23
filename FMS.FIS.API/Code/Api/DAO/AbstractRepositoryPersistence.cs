
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;




namespace FMC.Generic
{
    public class AbstractRepositoryPersistence<TEntity> : AbstractRepository<TEntity> where TEntity : class
    {

        public DbSet<TEntity> Context { get; set; }


        private bool controlaTransacao;

        public AbstractRepositoryPersistence(string stringConnection) : base(stringConnection)
        {
            controlaTransacao = true;
            Context = this.Set<TEntity>();
        }


        public override void AddAsync(TEntity entity)
        {
            Context.AddAsync(entity);
            SaveChangesAsync();
        }

        public override TEntity Add(TEntity entity)
        {
            return this.Add(entity, true);
        }

        public TEntity Add(TEntity entity, bool saveChanges)
        {
            if (entity == null)
                throw new ArgumentException("Não é possível Add um objeto nulo.");
            else
            {
                Context.Add(entity);

                if (saveChanges)
                {
                    this.SaveChanges();
                    return entity;
                }
            }
            return null;
        }

        public override int AddRangeBulk(List<TEntity> listEntity)
        {
            //listEntity.ForEach(entity => Context.Add(entity));
            // this.Configuration.AutoDetectChangesEnabled = false;
            // var result = this.BulkInsert(listEntity, InsertOptions.OutputIdentity | InsertOptions.OutputComputed);
            // this.BulkSaveChanges();
            // return result;
            throw new Exception("Método não implementado por enquanto");
        }

        public override int AddRangeNormal(List<TEntity> listEntity)
        {

            return this.SaveChanges();
        }

        public override TEntity UpdateNoContext(TEntity entity)
        {
            var properties = entity.GetType().GetProperties().Where(p => p.GetCustomAttributes(true).Any(atribute => atribute is System.ComponentModel.DataAnnotations.KeyAttribute));

            TEntity currentEntity = Context.Find(properties.Select(p => p.GetValue(entity)).ToArray());

            if (currentEntity == null)
                throw new ArgumentException("Não foi encontrado objeto com esta chave.");
            else
            {

                foreach (var prop in currentEntity.GetType().GetProperties().Where(p => p.Module.ToString() != "EntityProxyModule").ToList())
                {
                    var value = entity.GetType().GetProperty(prop.Name).GetValue(entity);

                    prop.SetValue(currentEntity, value);
                }


                if (this.Entry(currentEntity).State == EntityState.Detached)
                    Context.Attach(currentEntity);

                this.Entry(currentEntity).State = EntityState.Modified;

                this.SaveChanges();

                return currentEntity;
            }
        }




        public override void UpdateAsync(TEntity entity)
        {
            this.UpdateAsync(entity, true);
        }

        private void UpdateAsync(TEntity entity, bool saveChange)
        {
            if (this.Entry(entity).State == EntityState.Detached)
                Context.Attach(entity);

            this.Entry(entity).State = EntityState.Modified;

            if (saveChange)
                this.SaveChangesAsync();
        }

        public override TEntity Update(TEntity entity)
        {
            return this.Update(entity, true);
        }

        private TEntity Update(TEntity entity, bool saveChange)
        {
            if (this.Entry(entity).State == EntityState.Detached)
                Context.Attach(entity);

            this.Entry(entity).State = EntityState.Modified;

            if (saveChange)
                this.SaveChanges();

            return entity;
        }

        public override int UpdateRangeBulk(List<TEntity> listEntity)
        {
            //this.Configuration.AutoDetectChangesEnabled = false;
            //this.Database.CommandTimeout = 10 * 60;
            //var result = this.BulkUpdate(listEntity, UpdateOptions.OutputComputed);
            //this.BulkSaveChanges();
            //return result;

            throw new Exception("Metodo não implementado por enquanto");

        }

        public override int UpdateRangeNormal(List<TEntity> listEntity)
        {
            listEntity.ForEach(entity => Update(entity, false));
            return this.SaveChanges();
        }


        public override int SaveChanges()
        {
            if (controlaTransacao)
            {
                foreach (var item in ChangeTracker.Entries()
                   .Where(e => e.State == EntityState.Deleted &&
                   e.Metadata.GetProperties().Any(x => x.Name == "IsDeleted")))
                {
                    item.State = EntityState.Unchanged;
                    item.CurrentValues["IsDeleted"] = true;
                }
                return base.SaveChanges();
            }
            else
                return -1;

        }

        public override int Delete(TEntity entity)
        {
            return this.Delete(entity, true);
        }

        private int Delete(TEntity entity, bool saveChanges)
        {
            var properties = entity.GetType().GetProperties().Where(p => p.GetCustomAttributes(true).Any(atribute => atribute is System.ComponentModel.DataAnnotations.KeyAttribute));

            TEntity currentEntity = Context.Find(properties.Select(p => p.GetValue(entity)).ToArray());

            if (currentEntity == null)
                throw new ArgumentException("Não foi encontrado objeto com esta chave.");
            else
            {
                //DbEntityEntry dbEntityEntry = this.Entry<TEntity>(currentEntity);

                //if (dbEntityEntry.State == EntityState.Detached)
                //    this.Set<TEntity>().Attach(currentEntity);

                Context.Remove(currentEntity);

                if (saveChanges)
                    return this.SaveChanges();

                return this.SaveChanges();

            }
        }

        public override int DeleteRange(List<TEntity> listEntity)
        {
            //    var result = this.BulkDelete(listEntity);
            //    this.BulkSaveChanges();
            //    return result;
            throw new Exception("Metodo não implementado por enquanto");

        }

        public override TEntity GetBykey(object key)
        {
            return Context.Find(key);
        }

        public override IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Where(predicate);
        }

        public override ICollection<TEntity> GetAll()
        {
            return this.Set<TEntity>().ToList();
        }

    }
}
