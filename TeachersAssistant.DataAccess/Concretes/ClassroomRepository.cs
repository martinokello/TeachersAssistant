using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace  TeachersAssistant.DataAccess.Concretes
{
    public class ClassroomRepository : IRepository<Classroom>, IClassroomRepositoryMarker
    {
        public TeachersAssistant DbContextTeachersAssistant  { get; set; }
        public bool Add(Classroom item)
        {
            try
            {
                DbContextTeachersAssistant.Classrooms.Add(item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(Classroom item)
        {
            try
            {
                var classroom =
                    DbContextTeachersAssistant.Classrooms.SingleOrDefault(p => p.ClassroomId == item.ClassroomId);
                DbContextTeachersAssistant.Classrooms.Remove(classroom);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Classroom[] GetAll()
        {
            return DbContextTeachersAssistant.Classrooms.ToArray();
        }

        public Classroom GetById(int id)
        {
            return DbContextTeachersAssistant.Classrooms.SingleOrDefault(p => p.ClassroomId == id);
        }

        public bool Update(Classroom item)
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
