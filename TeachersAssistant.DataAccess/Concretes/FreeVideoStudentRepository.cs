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
    public class FreeVideoStudentRepository : AbstractRepository<FreeVideoStudent>, IFreeVideoStudentRepositoryMarker
    {
        public override FreeVideoStudent GetById(int id)
        {
            return DbContextTeachersAssistant.FreeVideoStudents.SingleOrDefault(p => p.FreeVideoStudentId == id);
        }

        public override bool Update(FreeVideoStudent item)
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
