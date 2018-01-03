using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace  TeachersAssistant.DataAccess.Concretes
{
    public class TeacherRepository : IRepository<Teacher>, ITeacherRepositoryMarker
    {
        public TeachersAssistant DbContextTeachersAssistant  { get; set; }
        public bool Add(Teacher item)
        {
            try
            {
                DbContextTeachersAssistant.Teachers.Add(item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(Teacher item)
        {
            try
            {
                var teach = DbContextTeachersAssistant.Teachers.SingleOrDefault(p => p.TeacherId == item.TeacherId);
                DbContextTeachersAssistant.Teachers.Remove(teach);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Teacher[] GetAll()
        {
            return DbContextTeachersAssistant.Teachers.ToArray();
        }

        public Teacher GetById(int id)
        {
            return DbContextTeachersAssistant.Teachers.SingleOrDefault(p => p.TeacherId == id);
        }

        public bool Update(Teacher item)
        {
            try
            {
                var teach = DbContextTeachersAssistant.Teachers.SingleOrDefault(p => p.TeacherId == item.TeacherId);
                teach.FirsName = item.FirsName;
                teach.LastName = item.LastName;
                teach.EmailAddress = item.EmailAddress;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
