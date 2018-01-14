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
    public class StudentTypeRepository : AbstractRepository<StudentType>, IStudentTypeRepositoryMarker
    {
 
        public override StudentType GetById(int id)
        {
            return DbContextTeachersAssistant.StudentTypes.SingleOrDefault(p => p.StudentTypeId == id);
        }

        public override bool Update(StudentType item)
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
