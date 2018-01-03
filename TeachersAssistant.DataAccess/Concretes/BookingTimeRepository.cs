using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.Domain.Model.Models;
using TeachersAssistant.DataAccess.Interfaces;

namespace TeachersAssistant.DataAccess.Concretes
{
    public class BookingTimeRepository : IRepository<BookingTime>,IBookingTimeRepositoryMarker
    {
        public TeachersAssistant DbContextTeachersAssistant { get; set; }
        public bool Add(BookingTime item)
        {
            try
            {
                DbContextTeachersAssistant.BookingTimes.Add(item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(BookingTime item)
        {
            var bookingTime = DbContextTeachersAssistant.BookingTimes.SingleOrDefault(p => p.BookingTimeId == item.BookingTimeId);
            DbContextTeachersAssistant.BookingTimes.Remove(bookingTime);
            return true;
        }

        public BookingTime[] GetAll()
        {
            return DbContextTeachersAssistant.BookingTimes.ToArray();
        }

        public BookingTime GetById(int id)
        {
            return DbContextTeachersAssistant.BookingTimes.SingleOrDefault(p => p.BookingTimeId == id);
        }

        public bool Update(BookingTime item)
        {
            try
            {
                var bookingTime = DbContextTeachersAssistant.BookingTimes.SingleOrDefault(p => p.BookingTimeId == item.BookingTimeId);
                bookingTime.StartTime = item.StartTime;
                bookingTime.EndTime = item.EndTime;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
