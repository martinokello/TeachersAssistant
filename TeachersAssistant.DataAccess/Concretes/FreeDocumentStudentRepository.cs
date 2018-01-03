using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace  TeachersAssistant.DataAccess.Concretes
{
    public class FreeDocumentStudentRepository : IRepository<FreeDocumentStudent>, IFreeDocumentStudentRepositoryMarker
    {
        public TeachersAssistant DbContextTeachersAssistant  { get; set; }
        public bool Add(FreeDocumentStudent item)
        {
            try
            {
                DbContextTeachersAssistant.FreeDocumentStudents.Add(item);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool Delete(FreeDocumentStudent item)
        {
            try
            {
                var freeDocStu = DbContextTeachersAssistant.FreeDocumentStudents.SingleOrDefault(p => p.FreeDocumentStudentId == item.FreeDocumentStudentId);
                DbContextTeachersAssistant.FreeDocumentStudents.Remove(freeDocStu);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public FreeDocumentStudent[] GetAll()
        {
            return DbContextTeachersAssistant.FreeDocumentStudents.ToArray();
        }

        public FreeDocumentStudent GetById(int id)
        {
            return DbContextTeachersAssistant.FreeDocumentStudents.SingleOrDefault(p => p.FreeDocumentStudentId == id);
        }

        public bool Update(FreeDocumentStudent item)
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
