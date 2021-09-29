using BusinessLogic.Contracts;
using DataAccessLayer;
using DataAccessLayer.Entities;
using System.Linq;

namespace BusinessLogic.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IDbRepository _db;

        public RoleService(IDbRepository db)
        {
            _db = db;
        }
        public IQueryable<Role> GetAll()
        {
            return _db.GetAll<Role>();
        }
    }
}
