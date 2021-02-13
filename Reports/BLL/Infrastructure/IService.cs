using System.Collections;
using System.Collections.Generic;

namespace BLL.Infrastructure
{
    public interface IService<T1, T2>
    {
        public int Add(T1 entity);

        public Dictionary<int, T1> GetAll();

        public T1 ConvertDalToBbl(T2 entity);
        
        public T2 ConvertBblToDal(T1 entity);

        bool CheckExistence(int id);

        public T1 Get(int id);
    }
}