using DataAccessLayer.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Contracts
{
    public interface IDeskService
    {
        public Task<int> CreateAsync(Desk desk);
        public IQueryable<Desk> GetAll();
        public Task UpdateAsync(Desk desk);
        public Task DeleteAsync(Desk desk);
        public Task AddDeviceToDeskAsync(int deskId, int deviceId);
        public Task DeleteDeviceFromDeskAsync(int deskId, int deviceId);
    }
}
