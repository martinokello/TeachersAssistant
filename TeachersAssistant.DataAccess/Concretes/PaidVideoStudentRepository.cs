using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace  TeachersAssistant.DataAccess.Concretes
{
    public class PaidVideoStudentRepository : IRepository<PaidVideoStudent>, IPaidVideoStudentRepositoryMarker
    {
        public TeachersAssistant DbContextTeachersAssistant  { get; set; }
        public bool Add(PaidVideoStudent item)
        {
            try
            {
                DbContextTeachersAssistant.PaidVideoStudents.Add(item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(PaidVideoStudent item)
        {
            try
            {
                var paidVideoStu = DbContextTeachersAssistant.PaidVideoStudents.SingleOrDefault(p => p.PaidVideoStudentId == item.PaidVideoStudentId);
                DbContextTeachersAssistant.PaidVideoStudents.Remove(paidVideoStu);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public PaidVideoStudent[] GetAll()
        {
            return DbContextTeachersAssistant.PaidVideoStudents.ToArray();
        }

        public PaidVideoStudent GetById(int id)
        {
            return DbContextTeachersAssistant.PaidVideoStudents.SingleOrDefault(p => p.PaidVideoStudentId == id);
        }

        public bool Update(PaidVideoStudent item)
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
