using DataAccessLayer.Entities;
using System.Linq;

namespace BusinessLogic.Contracts
{
    public interface IRoleService
    {
        public IQueryable<Role> GetAll();
    }
}
