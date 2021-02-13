using System.Collections.Generic;

namespace DAL.Infrastructure
{
    public interface IRepository<T> where T : IEntity
    {
        Dictionary<int, T> GetAll();

        T GetById(int id);

        int Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        bool CheckIdExistence(int id);
    }
}