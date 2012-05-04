using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace F1Speed.Service.Repositories
{
    public abstract class AbstractRepository<TEntity> : IRepository<TEntity> 
    {
        protected static ISession GetSession()
        {
            return SessionProvider.SessionFactory.OpenSession();
        }

        #region IRepository<TEntity,TKey> Members


        public TEntity GetById(int id)
        {
            using (var session = GetSession())
            {
                return session.Get<TEntity>(id);
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (var session = GetSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(TEntity));
                return criteria.List<TEntity>();
            }
        }

        public void Save(TEntity entity)
        {
            using (var session = GetSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.SaveOrUpdate(entity);

                    trans.Commit();
                }

            }
        }

        public void SaveAll(IEnumerable<TEntity> entityBatch)
        {
            using (var session = GetSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    foreach (var entity in entityBatch)
                        session.SaveOrUpdate(entity);

                    trans.Commit();
                }

            }
        }

        public void Delete(TEntity entity)
        {
            using (var session = GetSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.Delete(entity);

                    trans.Commit();
                }

            }
        }

        #endregion
    }
}
