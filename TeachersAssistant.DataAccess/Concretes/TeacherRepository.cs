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
    public class TeacherRepository : AbstractRepository<Teacher>, ITeacherRepositoryMarker
    {
 
        public override Teacher GetById(int id)
        {
            return DbContextTeachersAssistant.Teachers.SingleOrDefault(p => p.TeacherId == id);
        }

        public override bool Update(Teacher item)
        {
            try
            {
                var teach = DbContextTeachersAssistant.Teachers.SingleOrDefault(p => p.TeacherId == item.TeacherId);
                teach.FirsName = item.FirsName;
                teach.LastName = item.LastName;
                teach.EmailAddress = item.EmailAddress;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
