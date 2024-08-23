using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

namespace FMC.Automator.DistribuicaoDesk.DAO
{
    public abstract class AbstractBLLPersistencia<TObjectContext, TEntityObject> : AbstractBLLContexto<TObjectContext>
        where TObjectContext : ObjectContext, new()
        where TEntityObject : EntityObject, IEntityWithKey
    {
        public AbstractBLLPersistencia() { }

        public AbstractBLLPersistencia(TObjectContext context) : base(context) { }

        public virtual void Gravar(TEntityObject pEntity)
        {
            switch (pEntity.EntityState)
            {
                case System.Data.EntityState.Added:
                    this.Add(pEntity);
                    break;
                case System.Data.EntityState.Modified:
                    this.Update(pEntity);
                    break;
                case System.Data.EntityState.Deleted:
                    this.Delete(pEntity);
                    break;
                case System.Data.EntityState.Detached:
                    throw new Exception("Não é possível persistir uma entidade \"Detached\".");
                case System.Data.EntityState.Unchanged:
                    break;
            }
        }

        public virtual TEntityObject Add(TEntityObject pEntity)
        {
            Context.AddObject(pEntity.GetType().Name, pEntity);
            if (ControlaTransacao)
            {
                if (Context.SaveChanges() > 0)
                    return pEntity;
            }
            return null;
        }

        public virtual void AddRange(IList<TEntityObject> listEntity)
        {
            foreach (TEntityObject entity in listEntity)
                Context.AddObject(entity.GetType().Name, entity);
            if (ControlaTransacao)
                Context.SaveChanges();
        }

        public virtual void Update(TEntityObject pEntity)
        {
            Context.ApplyCurrentValues<TEntityObject>(pEntity.GetType().Name, pEntity);
            if (ControlaTransacao)
                Context.SaveChanges();
        }

        public virtual void UpdateRange(IList<TEntityObject> listEntity)
        {
            foreach (TEntityObject entity in listEntity)
                Context.ApplyCurrentValues<TEntityObject>(entity.GetType().Name, entity);
            if (ControlaTransacao)
                Context.SaveChanges();
        }

        public virtual void Delete(TEntityObject pEntity)
        {
            Context.DeleteObject(pEntity);
            if (ControlaTransacao)
                Context.SaveChanges();
        }

        public virtual void DeleteRange(IList<TEntityObject> listEntity)
        {
            foreach (TEntityObject entity in listEntity)
                Context.DeleteObject(entity);
            if (ControlaTransacao)
                Context.SaveChanges();
        }

        public virtual TEntityObject Detach(TEntityObject pEntity)
        {
            Context.ContextOptions.LazyLoadingEnabled = false;
            Context.Detach(pEntity);
            return pEntity;
        }

        public virtual IList<TEntityObject> DetachRange(IList<TEntityObject> listEntity)
        {
            foreach (TEntityObject entity in listEntity)
                Context.Detach(entity);
            return listEntity;
        }

        public T CriarObjetoNoMesmoContexto<T>() where T : AbstractBLLContexto<TObjectContext>
        {
            System.Reflection.ConstructorInfo construct = typeof(T).GetConstructor(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance, null, new Type[] { typeof(TObjectContext) }, null);
            if (construct == null)
                throw new Exception("A classe " + typeof(T).Name + " não possue um construtor \"internal " + typeof(T).Name + "(" + typeof(TObjectContext).Name + " context)\".");
            return construct.Invoke(new object[] { this.Context }) as T;
        }
    }
}
