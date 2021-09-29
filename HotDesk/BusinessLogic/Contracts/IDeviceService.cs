using DataAccessLayer.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinessLogic.Contracts
{
    public interface IDeviceService
    {
        public Task<int> CreateAsync(Device device);
        public IQueryable<Device> GetAll();
        public IQueryable<Device> Get(Expression<Func<Device, bool>> selector);
        public Task DeleteAsync(Device device);
    }
}
