using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotDeskMVC.Models
{
    public class AddDeskViewModel
    {
        [Required]
        [Remote(action: "CheckDeskName", controller: "Desk", ErrorMessage = "This desk name is already in use")]
        public string NewDeskName { get; set; }
        public List<Device> Devices { get; set; }
        public int[] SelectedDevicesId { get; set; }
    }
}
