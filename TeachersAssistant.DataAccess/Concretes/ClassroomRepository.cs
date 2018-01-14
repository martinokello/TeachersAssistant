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
    public class ClassroomRepository : AbstractRepository<Classroom>, IClassroomRepositoryMarker
    {
 
        public override Classroom GetById(int id)
        {
            return DbContextTeachersAssistant.Classrooms.SingleOrDefault(p => p.ClassroomId == id);
        }

        public override bool Update(Classroom item)
        {
            try
            {
                var classroom =
                    DbContextTeachersAssistant.Classrooms.SingleOrDefault(p => p.ClassroomId == item.ClassroomId);

                classroom.CalendarId = item.CalendarId;
                classroom.StudentType = item.StudentType;
                classroom.SubjectId = item.SubjectId;
                classroom.TeacherId = item.TeacherId;
                
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
