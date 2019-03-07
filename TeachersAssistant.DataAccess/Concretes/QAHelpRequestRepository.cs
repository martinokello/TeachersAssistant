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
    public class QAHelpRequestRepository : AbstractRepository<QAHelpRequest>, IQAHelpRequestRepositoryMarker
    {

        public override QAHelpRequest GetById(int id)
        {
            return DbContextTeachersAssistant.QAHelpRequests.SingleOrDefault(p => p.QAHelpRequestId == id);
        }

        public override bool Update(QAHelpRequest item)
        {
            try
            {
                var qaHelpReq = DbContextTeachersAssistant.QAHelpRequests.SingleOrDefault(p => p.QAHelpRequestId == item.QAHelpRequestId);
                qaHelpReq.TeacherId = item.TeacherId;
                qaHelpReq.StudentId = item.StudentId;
                qaHelpReq.SubjectId = item.SubjectId;
                qaHelpReq.StudentRole = item.StudentRole;
                qaHelpReq.StartTime = item.StartTime;
                qaHelpReq.EndTime = item.EndTime;
                qaHelpReq.IsScheduled = item.IsScheduled;
                qaHelpReq.Description = item.Description;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
