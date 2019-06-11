using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Abstracts;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace TeachersAssistant.DataAccess.Concretes
{
    public  class CourseRepository : AbstractRepository<Course>, ICourseRepositoryMarker
    {
        public override Course GetById(int id)
        {
            return DbContextTeachersAssistant.Courses.SingleOrDefault(p => p.CourseId == id);
        }

        public override bool Update(Course item)
        {
            try
            {
                var course = DbContextTeachersAssistant.Courses.SingleOrDefault(p => p.CourseId == item.CourseId);
                course.CourseDescription = item.CourseDescription;
                course.CourseName = item.CourseName;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
