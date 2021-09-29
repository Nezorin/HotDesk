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
        public async Task<int> CreateAsync(Desk desk)
        {
            var result = await _db.Add(desk);
            await _db.SaveChangesAsync();

            return result;
        }
        public IQueryable<Desk> GetAll()
        {
            return _db.GetAll<Desk>().Include(d => d.Devices);
        }

        public async Task UpdateAsync(Desk desk)
        {
            await _db.Update(desk);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(Desk desk)
        {
            await _db.Remove(desk);
            await _db.SaveChangesAsync();
        }

        public async Task AddDeviceToDeskAsync(int deskId, int deviceId)
        {
            var desk = GetAll().FirstOrDefault(d => d.Id == deskId);
            var deviceToAdd = _deviceService.GetAll().FirstOrDefault(device => device.Id == deviceId);
            desk.Devices.Add(deviceToAdd);
            await UpdateAsync(desk);
        }

        public async Task DeleteDeviceFromDeskAsync(int deskId, int deviceId)
        {
            var desk = GetAll().FirstOrDefault(d => d.Id == deskId);
            var deviceToRemove = _deviceService.GetAll().FirstOrDefault(device => device.Id == deviceId);
            desk.Devices.Remove(deviceToRemove);
            await UpdateAsync(desk);
        }
    }
}
