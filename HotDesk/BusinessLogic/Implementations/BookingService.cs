using BusinessLogic.Contracts;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IDbRepository _db;

        public BookingService(IDbRepository db)
        {
            _db = db;
        }
        public async Task AddReservation(int userId, int deskId, List<Device> devices, DateTime selectedTime)
        {
            var reservation = new UserDesk()
            {
                ReservationTime = selectedTime.Date,
                DeskId = deskId,
                UserId = userId,
                Devices = devices
            };
            await _db.Add(reservation);
            await _db.SaveChangesAsync();
        }
        public IQueryable<UserDesk> GetAll()
        {
            return _db.GetAll<UserDesk>().Include(r => r.User)
            .Include(r => r.Devices)
            .Include(r => r.Desk);
        }
        public List<DateTime> GetAllBookedTimesForDesk(int deskId)
        {
            return _db.GetAll<UserDesk>().Where(r => r.DeskId == deskId).Select(r => r.ReservationTime).ToList();
        }

        public List<DateTime> GetAllBookedTimesForUser(string userName)
        {
            return _db.GetAll<UserDesk>().Include(r => r.User).Where(r => r.User.Login == userName).Select(r => r.ReservationTime).ToList();
        }

        public List<DateTime> GetAllPossibleReservationTime(int deskId, string userName)
        {
            var TimeForNextWeek = new List<DateTime>();
            for (int i = 0; i < 7; i++)
            {
                TimeForNextWeek.Add(DateTime.Now.AddDays(i).Date);
            }
            // Получаем все даты на след неделю, на которые пользователь не резервировал столы и сам стол не был никем резервирован
            var AvailableDates = TimeForNextWeek.Where(d =>
                !GetAllBookedTimesForDesk(deskId).Contains(d) &&
                !GetAllBookedTimesForUser(userName).Contains(d))
                .ToList();
            return AvailableDates;
        }
    }
}
