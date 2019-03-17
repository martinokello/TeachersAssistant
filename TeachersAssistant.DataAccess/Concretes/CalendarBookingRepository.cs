using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Abstracts;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace  TeachersAssistant.DataAccess.Concretes
{
    public class CalendarBookingRepository : AbstractRepository<CalendarBooking>, ICalendarBookingRepositoryMarker
    {
        public override CalendarBooking GetById(int id)
        {
            return DbContextTeachersAssistant.CalendarBookings.Include("BookingTime").SingleOrDefault(p=> p.CalendarBookingId == id);
        }

        public override bool Update(CalendarBooking item)
        {
            try
            {
                var calendar = DbContextTeachersAssistant.CalendarBookings.SingleOrDefault(p => p.CalendarBookingId == item.CalendarBookingId);
                calendar.BookingTimeId = item.BookingTimeId;
                calendar.ClassId = item.ClassId;
                calendar.StudentId = item.StudentId;
                calendar.StudentId = item.StudentId;
                calendar.SubjectId = item.SubjectId;
                calendar.Description = item.Description;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
