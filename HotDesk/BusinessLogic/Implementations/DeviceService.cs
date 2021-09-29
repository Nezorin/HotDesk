using BusinessLogic.Contracts;
using DataAccessLayer;
using DataAccessLayer.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinessLogic.Implementations
{
    public class DeviceService : IDeviceService
    {
        private readonly IDbRepository _db;

        public DeviceService(IDbRepository db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(Device device)
        {
            var result = await _db.Add(device);
            await _db.SaveChangesAsync();

            return result;
        }
        public IQueryable<Device> Get(Expression<Func<Device, bool>> selector)
        {
            return _db.Get(selector);
        }
        public IQueryable<Device> GetAll()
        {
            return _db.GetAll<Device>();
        }
        public async Task DeleteAsync(Device device)
        {
            await _db.Remove(device);
            await _db.SaveChangesAsync();
        }
    }
}
