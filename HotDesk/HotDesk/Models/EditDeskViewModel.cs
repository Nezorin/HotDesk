using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotDeskMVC.Models
{
    public class EditDeskViewModel
    {
        public Desk Desk { get; set; }
        [Remote(action: "CheckDeskName", controller: "Desk", ErrorMessage = "This desk name is already in use")]
        public string NewDeskName { get; set; }
        public Device[] DevicesToAdd { get; set; }
    }
}
