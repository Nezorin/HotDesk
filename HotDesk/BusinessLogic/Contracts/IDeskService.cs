using DataAccessLayer.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Contracts
{
    public interface IDeskService
    {
        public Task<int> Create(Desk desk);
        public IQueryable<Desk> GetAll();
        public Task Update(Desk desk);
        public Task Delete(Desk desk);
        public Task AddDeviceToDesk(int deskId, int deviceId);
        public Task DeleteDeviceFromDesk(int deskId, int deviceId);
    }
}
