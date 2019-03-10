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
    public class SubjectRepository : AbstractRepository<Subject>, ISubjectRepositoryMarker
    {
 
        public override Subject GetById(int id)
        {
            try
            {
                return DbContextTeachersAssistant.Subjects.SingleOrDefault(p => p.SubjectId == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public override bool Update(Subject item)
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
