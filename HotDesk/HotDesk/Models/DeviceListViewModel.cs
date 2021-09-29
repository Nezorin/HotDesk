using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotDeskMVC.Models
{
    public class DeviceListViewModel
    {
        [Remote(action: "CheckDeviceName", controller: "Device", ErrorMessage = "This device name is already in use")]
        public string NewDeviceName { get; set; }
        public Device[] Devices { get; set; }
    }
}
