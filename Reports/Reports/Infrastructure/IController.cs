using System.Collections.Generic;

namespace Reports.Infrastructure
{
    public interface IController <T1, T2>
    {
        public T2 ConvertModelToBbl(T1 entity);
        
        public T1 ConvertBblToModel(T2 entity);
    }
}