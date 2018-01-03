using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.Domain.Model.Models;

namespace  TeachersAssistant.DataAccess.Concretes
{
    public class CalendarBookingRepository : IRepository<CalendarBooking>, ICalendarBookingRepositoryMarker
    {
        public TeachersAssistant DbContextTeachersAssistant {get; set; }
        public bool Add(CalendarBooking item)
        {
            try
            {
                DbContextTeachersAssistant.CalendarBookings.Add(item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(CalendarBooking item)
        {
            try
            {
                var calendar = DbContextTeachersAssistant.CalendarBookings.SingleOrDefault(p => p.CalendarBookingId == item.CalendarBookingId);
                DbContextTeachersAssistant.CalendarBookings.Remove(calendar);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        

        public CalendarBooking[] GetAll()
        {
            return DbContextTeachersAssistant.CalendarBookings.ToArray();
        }

        public CalendarBooking GetById(int id)
        {
            return DbContextTeachersAssistant.CalendarBookings.SingleOrDefault(p=> p.CalendarBookingId == id);
        }

        public bool Update(CalendarBooking item)
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
