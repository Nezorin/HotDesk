using DataAccessLayer.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Contracts
{
    public interface IUserService
    {
        public Task<int> Create(User user);
        public Task<User> GetByLogin(string login);
        public IQueryable<User> GetAll();
    }
}
