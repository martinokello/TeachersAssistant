using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace  TeachersAssistant.DataAccess.Concretes
{
    public class SubjectRepository : IRepository<Subject>, ISubjectRepositoryMarker
    {
        public TeachersAssistant DbContextTeachersAssistant  { get; set; }
        public bool Add(Subject item)
        {
            try
            {
                DbContextTeachersAssistant.Subjects.Add(item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(Subject item)
        {
            try
            {
                var stuTypeName = DbContextTeachersAssistant.Subjects.SingleOrDefault(p => p.SubjectId == item.SubjectId);
                DbContextTeachersAssistant.Subjects.Remove(stuTypeName);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Subject[] GetAll()
        {
            return DbContextTeachersAssistant.Subjects.ToArray();
        }

        public Subject GetById(int id)
        {
            return DbContextTeachersAssistant.Subjects.SingleOrDefault(p => p.SubjectId == id);
        }

        public bool Update(Subject item)
        {
            try
            {
                var subj = DbContextTeachersAssistant.Subjects.SingleOrDefault(p => p.SubjectId == item.SubjectId);
                subj.SubjectName = item.SubjectName;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
