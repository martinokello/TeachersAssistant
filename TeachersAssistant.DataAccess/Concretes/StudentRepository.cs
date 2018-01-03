using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace  TeachersAssistant.DataAccess.Concretes
{
    public class StudentRepository : IRepository<Student>, IStudentRepositoryMarker
    {
        public TeachersAssistant DbContextTeachersAssistant  { get; set; }
        public bool Add(Student item)
        {
            try
            {
                DbContextTeachersAssistant.Students.Add(item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(Student item)
        {
            try
            {
                var stu = DbContextTeachersAssistant.Students.SingleOrDefault(p => p.StudentId == item.StudentId);
                DbContextTeachersAssistant.Students.Remove(stu);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Student[] GetAll()
        {
            return DbContextTeachersAssistant.Students.ToArray();
        }

        public Student GetById(int id)
        {
            return DbContextTeachersAssistant.Students.SingleOrDefault(p => p.StudentId == id);
        }

        public bool Update(Student item)
        {
            try
            {
                var stu = DbContextTeachersAssistant.Students.SingleOrDefault(p => p.StudentId == item.StudentId);
                stu.StudentFirsName = item.StudentFirsName;
                stu.StudentLastName = item.StudentLastName;
                stu.EmailAddress = item.EmailAddress;
                stu.StudentType = item.StudentType;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
