using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  TeachersAssistant.DataAccess.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(int id);
        T[] GetAll();
        bool Add(T item);
        bool Delete(T item);
        bool Update(T item);
    }
}
