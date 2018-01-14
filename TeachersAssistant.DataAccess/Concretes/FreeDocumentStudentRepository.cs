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
    public class FreeDocumentStudentRepository : AbstractRepository<FreeDocumentStudent>, IFreeDocumentStudentRepositoryMarker
    {
 
        public override FreeDocumentStudent GetById(int id)
        {
            return DbContextTeachersAssistant.FreeDocumentStudents.SingleOrDefault(p => p.FreeDocumentStudentId == id);
        }

        public override bool Update(FreeDocumentStudent item)
        {
            try
            {
                var freeDocStu = DbContextTeachersAssistant.FreeDocumentStudents.SingleOrDefault(p => p.FreeDocumentStudentId == item.FreeDocumentStudentId);
                freeDocStu.FreeDocumentId = item.FreeDocumentId;
                freeDocStu.StudentId = item.StudentId;
                freeDocStu.StudentType = item.StudentType;
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

    }
}
