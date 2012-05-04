using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Speed.Service.Repositories
{
    public interface IRepository<TEntity> 
    {
        TEntity GetById(int id);

        IEnumerable<TEntity> GetAll();

        void Save(TEntity entity);

        void SaveAll(IEnumerable<TEntity> entityBatch);

        void Delete(TEntity entity);
    }    
}
