using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Desk : BaseEntity
    {
        [MaxLength(20)]
        [Required]
        public string DeskName { get; set; }
        public ICollection<UserDesk> UserDesks { get; set; }
        public ICollection<Device> Devices { get; set; }
        public Desk()
        {
            UserDesks = new List<UserDesk>();
            Devices = new List<Device>();
        }
    }
}
