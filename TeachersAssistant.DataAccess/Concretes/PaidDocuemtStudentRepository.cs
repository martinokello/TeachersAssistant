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
    public class PaidDocuemtStudentRepository : AbstractRepository<PaidDocumentStudent>, IPaidDocuemtStudentRepositoryMarker
    {
        public override PaidDocumentStudent GetById(int id)
        {
            return DbContextTeachersAssistant.PaidDocumentStudents.SingleOrDefault(p => p.PaidDocumentStudentId == id);
        }

        public override bool Update(PaidDocumentStudent item)
        {
           try
            {
                var paidDocStu = DbContextTeachersAssistant.PaidDocumentStudents.SingleOrDefault(p => p.PaidDocumentStudentId == item.PaidDocumentStudentId);
                paidDocStu.PaidDocumentId = item.PaidDocumentId;
                paidDocStu.StudentId = item.StudentId;
                paidDocStu.StudentType = item.StudentType;
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
