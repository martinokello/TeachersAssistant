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
    public class PaidDocumentRepository : AbstractRepository<PaidDocument>, IPaidDocumentRepositoryMarker
    {
        public override PaidDocument GetById(int id)
        {
            return DbContextTeachersAssistant.PaidDocuments.SingleOrDefault(p => p.PaidDocumentId == id);
        }

        public override bool Update(PaidDocument item)
        {
            try
            {
                var paidDoc = DbContextTeachersAssistant.PaidDocuments.SingleOrDefault(p => p.PaidDocumentId == item.PaidDocumentId);
                paidDoc.PaidDocumentId = item.PaidDocumentId;
                paidDoc.FilePath = item.FilePath;
                paidDoc.SubjectId = item.SubjectId;
                paidDoc.RoleName = item.RoleName;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
