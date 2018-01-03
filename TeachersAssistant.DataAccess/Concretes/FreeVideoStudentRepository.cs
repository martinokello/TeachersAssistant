using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace  TeachersAssistant.DataAccess.Concretes
{
    public class FreeVideoStudentRepository : IRepository<FreeVideoStudent>, IFreeVideoStudentRepositoryMarker
    {
        public TeachersAssistant DbContextTeachersAssistant  { get; set; }
        public bool Add(FreeVideoStudent item)
        {
            try
            {
                DbContextTeachersAssistant.FreeVideoStudents.Add(item);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool Delete(FreeVideoStudent item)
        {
            try
            {
                var freeVidStu = DbContextTeachersAssistant.FreeVideoStudents.SingleOrDefault(p => p.FreeVideoStudentId == item.FreeVideoStudentId);
                DbContextTeachersAssistant.FreeVideoStudents.Remove(freeVidStu);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public FreeVideoStudent[] GetAll()
        {
            return DbContextTeachersAssistant.FreeVideoStudents.ToArray();
        }

        public FreeVideoStudent GetById(int id)
        {
            return DbContextTeachersAssistant.FreeVideoStudents.SingleOrDefault(p => p.FreeVideoStudentId == id);
        }

        public bool Update(FreeVideoStudent item)
        {
            try
            {
                var freeVidStu = DbContextTeachersAssistant.FreeVideoStudents.SingleOrDefault(p => p.FreeVideoStudentId == item.FreeVideoStudentId);
                freeVidStu.FreeVideoId = item.FreeVideoId;
                freeVidStu.StudentId = item.StudentId;
                freeVidStu.StudentType = item.StudentType;
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
