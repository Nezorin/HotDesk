using DataAccessLayer.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Contracts
{
    public interface IUserService
    {
        public Task<int> CreateAsync(User user);
        public Task<User> GetByLoginAsync(string login);
        public IQueryable<User> GetAll();
    }
}
