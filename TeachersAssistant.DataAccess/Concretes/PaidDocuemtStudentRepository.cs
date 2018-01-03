using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace  TeachersAssistant.DataAccess.Concretes
{
    public class PaidDocuemtStudentRepository : IRepository<PaidDocumentStudent>, IPaidDocuemtStudentRepositoryMarker
    {
        public TeachersAssistant DbContextTeachersAssistant  { get; set; }
        public bool Add(PaidDocumentStudent item)
        {
            try
            {
                DbContextTeachersAssistant.PaidDocumentStudents.Add(item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(PaidDocumentStudent item)
        {
            try
            {
                var paidDocStu = DbContextTeachersAssistant.PaidDocumentStudents.SingleOrDefault(p => p.PaidDocumentStudentId == item.PaidDocumentStudentId);
                DbContextTeachersAssistant.PaidDocumentStudents.Remove(paidDocStu);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public PaidDocumentStudent[] GetAll()
        {
            return DbContextTeachersAssistant.PaidDocumentStudents.ToArray();
        }

        public PaidDocumentStudent GetById(int id)
        {
            return DbContextTeachersAssistant.PaidDocumentStudents.SingleOrDefault(p => p.PaidDocumentStudentId == id);
        }

        public bool Update(PaidDocumentStudent item)
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
