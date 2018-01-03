using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace  TeachersAssistant.DataAccess.Concretes
{
    public class FreeDocumentRepository : IRepository<FreeDocument>, IFreeDocumentRepositoryMarker
    {
        public TeachersAssistant DbContextTeachersAssistant  { get; set; }
        public bool Add(FreeDocument item)
        {
            try
            {
                DbContextTeachersAssistant.FreeDocuments.Add(item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(FreeDocument item)
        {
            try
            {
                var freeDoc = DbContextTeachersAssistant.FreeDocuments.SingleOrDefault(p => p.FreeDocumentId == item.FreeDocumentId);
                DbContextTeachersAssistant.FreeDocuments.Remove(freeDoc);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public FreeDocument[] GetAll()
        {
            return DbContextTeachersAssistant.FreeDocuments.ToArray();
        }

        public FreeDocument GetById(int id)
        {
            return DbContextTeachersAssistant.FreeDocuments.SingleOrDefault(p => p.FreeDocumentId == id);
        }

        public bool Update(FreeDocument item)
        {
            try
            {
                var freeDoc = DbContextTeachersAssistant.FreeDocuments.SingleOrDefault(p => p.FreeDocumentId == item.FreeDocumentId); ;
                freeDoc.FilePath = item.FilePath;
                freeDoc.SubjectId = item.SubjectId;
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
