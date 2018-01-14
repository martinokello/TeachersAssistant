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
    public class FreeDocumentRepository : AbstractRepository<FreeDocument>, IFreeDocumentRepositoryMarker
    {

        public override FreeDocument GetById(int id)
        {
            return DbContextTeachersAssistant.FreeDocuments.SingleOrDefault(p => p.FreeDocumentId == id);
        }

        public override bool Update(FreeDocument item)
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
