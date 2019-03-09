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
    public class AssignmentSubmissionRepository : AbstractRepository<AssignmentSubmission>, IAssignmentSubmissionRepositoryMarker
    {
        public override AssignmentSubmission GetById(int id)
        {
            return DbContextTeachersAssistant.AssignmentSubmissions.SingleOrDefault(p => p.AssignmentId == id);
        }

        public override bool Update(AssignmentSubmission item)
        {
            try
            {
                var freeDoc = DbContextTeachersAssistant.AssignmentSubmissions.SingleOrDefault(p => p.AssignmentSubmissionId == item.AssignmentSubmissionId); ;
                freeDoc.AssignmentId = item.AssignmentId;
                freeDoc.FilePath = item.FilePath;
                freeDoc.StudentId = item.StudentId;
                freeDoc.DateSubmitted = item.DateSubmitted;
                freeDoc.StudentRole = item.StudentRole;
                freeDoc.AssignmentName = item.AssignmentName;
                freeDoc.DateDue = item.DateDue;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
