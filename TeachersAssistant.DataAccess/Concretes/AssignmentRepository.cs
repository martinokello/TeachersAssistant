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
    public class AssignmentRepository:AbstractRepository<Assignment>, IAssignmentRepositoryMarker
    {
        public override Assignment GetById(int id)
        {
            return DbContextTeachersAssistant.Assignments.SingleOrDefault(p => p.AssignmentId == id);
        }

        public override bool Update(Assignment item)
        {
            try
            {
                var freeDoc = DbContextTeachersAssistant.Assignments.SingleOrDefault(p => p.AssignmentId == item.AssignmentId); ;
                freeDoc.AssignmentName = item.AssignmentName;
                freeDoc.SubjectId = item.SubjectId;
                freeDoc.StudentId = item.StudentId;
                freeDoc.StudentRole = item.StudentRole;
                freeDoc.DateDue = item.DateDue;
                freeDoc.DateAssigned = item.DateAssigned;
                freeDoc.Description = item.Description;
                freeDoc.FilePath = item.FilePath;
                freeDoc.TeacherId = item.TeacherId;
                freeDoc.CourseId = item.CourseId;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
