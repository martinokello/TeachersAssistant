using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace  TeachersAssistant.DataAccess.Concretes
{
    public class PaidDocumentRepository : IRepository<PaidDocument>, IPaidDocumentRepositoryMarker
    {
        public TeachersAssistant DbContextTeachersAssistant  { get; set; }
        public bool Add(PaidDocument item)
        {
            try
            {
                DbContextTeachersAssistant.PaidDocuments.Add(item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(PaidDocument item)
        {
            try
            {
                var paidDoc = DbContextTeachersAssistant.PaidDocuments.SingleOrDefault(p => p.PaidDocumentId == item.PaidDocumentId);
                DbContextTeachersAssistant.PaidDocuments.Remove(paidDoc);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public PaidDocument[] GetAll()
        {
            return DbContextTeachersAssistant.PaidDocuments.ToArray();
        }

        public PaidDocument GetById(int id)
        {
            return DbContextTeachersAssistant.PaidDocuments.SingleOrDefault(p => p.PaidDocumentId == id);
        }

        public bool Update(PaidDocument item)
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
