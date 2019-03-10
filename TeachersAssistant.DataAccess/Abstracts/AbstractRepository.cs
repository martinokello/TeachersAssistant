using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Interfaces;

namespace TeachersAssistant.DataAccess.Abstracts
{
    public abstract class AbstractRepository<T> : IRepository<T>  where T : class
    {
        public TeachersAssistantDbContext DbContextTeachersAssistant { get; set; }
        public bool Add(T item)
        {
            try
            {
                DbContextTeachersAssistant.Set<T>().Add(item);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool Delete(T item)
        {
            try
            {
                DbContextTeachersAssistant.Set<T>().Remove(item);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public T[] GetAll()
        {
            try
            {
                return DbContextTeachersAssistant.Set<T>().ToArray<T>();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public abstract T GetById(int id);


        public abstract bool Update(T item);
    }
}
