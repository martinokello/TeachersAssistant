using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace  TeachersAssistant.DataAccess.Concretes
{
    public class StudentTypeRepository : IRepository<StudentType>, IStudentTypeRepositoryMarker
    {
        public TeachersAssistant DbContextTeachersAssistant  { get; set; }
        public bool Add(StudentType item)
        {
            try
            {
                DbContextTeachersAssistant.StudentTypes.Add(item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(StudentType item)
        {
            try
            {
                var stuTypeName = DbContextTeachersAssistant.StudentTypes.SingleOrDefault(p => p.StudentTypeId == item.StudentTypeId);
                DbContextTeachersAssistant.StudentTypes.Remove(stuTypeName);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public StudentType[] GetAll()
        {
            return DbContextTeachersAssistant.StudentTypes.ToArray();
        }

        public StudentType GetById(int id)
        {
            return DbContextTeachersAssistant.StudentTypes.SingleOrDefault(p => p.StudentTypeId == id);
        }

        public bool Update(StudentType item)
        {
            try
            {
                var stuTypeName = DbContextTeachersAssistant.StudentTypes.SingleOrDefault(p => p.StudentTypeId == item.StudentTypeId);

                stuTypeName.StudentTypeName = item.StudentTypeName;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
