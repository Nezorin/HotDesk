using BusinessLogic.Contracts;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Implementations
{
    public class DeskService : IDeskService
    {
        private readonly IDbRepository _db;
        private readonly IDeviceService _deviceService;

        public DeskService(IDbRepository db, IDeviceService deviceService)
        {
            _db = db;
            _deviceService = deviceService;
        }
        public async Task<int> Create(Desk desk)
        {
            var result = await _db.Add(desk);
            await _db.SaveChangesAsync();

            return result;
        }
        public IQueryable<Desk> GetAll()
        {
            return _db.GetAll<Desk>().Include(d => d.Devices);
        }

        public async Task Update(Desk desk)
        {
            await _db.Update(desk);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(Desk desk)
        {
            await _db.Remove(desk);
            await _db.SaveChangesAsync();
        }

        public async Task AddDeviceToDesk(int deskId, int deviceId)
        {
            var desk = GetAll().FirstOrDefault(d => d.Id == deskId);
            var deviceToAdd = _deviceService.GetAll().FirstOrDefault(device => device.Id == deviceId);
            desk.Devices.Add(deviceToAdd);
            await Update(desk);
        }

        public async Task DeleteDeviceFromDesk(int deskId, int deviceId)
        {
            var desk = GetAll().FirstOrDefault(d => d.Id == deskId);
            var deviceToRemove = _deviceService.GetAll().FirstOrDefault(device => device.Id == deviceId);
            desk.Devices.Remove(deviceToRemove);
            await Update(desk);
        }
    }
}
