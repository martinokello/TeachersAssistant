using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Abstracts;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace TeachersAssistant.DataAccess.Concretes
{
    public class StudentResourcesRepository : AbstractRepository<StudentResource>, IStudentResourceRepositoryMarker
    {

        public override StudentResource GetById(int id)
        {
            return DbContextTeachersAssistant.StudentResources.SingleOrDefault(p => p.StudentResourceId == id);
        }

        public override bool Update(StudentResource item)
        {
            try
            {
                var doc = DbContextTeachersAssistant.StudentResources.SingleOrDefault(p => p.StudentResourceId == item.StudentResourceId);
                doc.StudentResourceId = item.StudentResourceId;
                doc.FilePath = item.FilePath;
                doc.SubjectId = item.SubjectId;
                doc.RoleName = item.RoleName;
                doc.StudentResourceName = item.StudentResourceName;
                doc.CourseId = item.CourseId;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
