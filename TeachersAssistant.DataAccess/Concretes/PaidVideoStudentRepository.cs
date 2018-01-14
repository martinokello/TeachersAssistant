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
    public class PaidVideoStudentRepository : AbstractRepository<PaidVideoStudent>, IPaidVideoStudentRepositoryMarker
    {

        public override PaidVideoStudent GetById(int id)
        {
            return DbContextTeachersAssistant.PaidVideoStudents.SingleOrDefault(p => p.PaidVideoStudentId == id);
        }

        public override bool Update(PaidVideoStudent item)
        {
            try
            {
                var paidVideoStu = DbContextTeachersAssistant.PaidVideoStudents.SingleOrDefault(p => p.PaidVideoStudentId == item.PaidVideoStudentId);
                paidVideoStu.PaidVideoId = item.PaidVideoId;
                paidVideoStu.StudentId = item.StudentId;
                paidVideoStu.StudentType = item.StudentType;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
