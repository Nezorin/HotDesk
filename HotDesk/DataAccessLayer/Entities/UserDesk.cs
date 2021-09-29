using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class UserDesk : BaseEntity
    {
        public int DeskId { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "date")]
        public DateTime ReservationTime { get; set; }
        public ICollection<Device> Devices { get; set; }
        public Desk Desk { get; set; }
        public User User { get; set; }
        public UserDesk()
        {
            Devices = new List<Device>();
        }
    }
}
