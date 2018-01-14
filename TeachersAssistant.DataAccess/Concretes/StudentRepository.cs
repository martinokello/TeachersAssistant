using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Abstracts;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace  TeachersAssistant.DataAccess.Concretes
{
    public class StudentRepository : AbstractRepository<Student>, IStudentRepositoryMarker
    {
        public override Student GetById(int id)
        {
            return DbContextTeachersAssistant.Students.SingleOrDefault(p => p.StudentId == id);
        }

        public override bool Update(Student item)
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
