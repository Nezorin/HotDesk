using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Contracts
{
    public interface IBookingService
    {
        public IQueryable<UserDesk> GetAll();
        public Task AddReservation(int userId, int deskId, List<Device> devices, DateTime selectedTime);
        public List<DateTime> GetAllBookedTimesForDesk(int deskId);
        public List<DateTime> GetAllBookedTimesForUser(string userName);
        public List<DateTime> GetAllPossibleReservationTime(int deskId, string userName);
    }
}
