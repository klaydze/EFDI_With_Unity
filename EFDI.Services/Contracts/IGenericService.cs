using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDI.Services.Contracts
{
    public interface IGenericService<T>
    {
        T Add(T value);
        List<T> GetItems();
    }
}
