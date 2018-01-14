using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersAssistant.Domain.Model.Models;
using TeachersAssistant.DataAccess.Interfaces;
using TeachersAssistant.DataAccess.Abstracts;

namespace TeachersAssistant.DataAccess.Concretes
{
    public class BookingTimeRepository : AbstractRepository<BookingTime>,IBookingTimeRepositoryMarker
    {
        public override BookingTime GetById(int id)
        {
            return DbContextTeachersAssistant.BookingTimes.SingleOrDefault(p => p.BookingTimeId == id);
        }

        public override bool Update(BookingTime item)
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
