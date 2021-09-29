using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<UserDesk> UserDesks { get; set; }
        public User()
        {
            UserDesks = new List<UserDesk>();
        }
    }
}
