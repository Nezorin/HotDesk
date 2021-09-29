using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Device : BaseEntity
    {
        [MaxLength(100)]
        [Required]
        public string DeviceName { get; set; }
        public ICollection<Desk> Desks { get; set; }
        public ICollection<UserDesk> UserDesks { get; set; }
        public Device()
        {
            Desks = new List<Desk>();
            UserDesks = new List<UserDesk>();
        }
    }
}
