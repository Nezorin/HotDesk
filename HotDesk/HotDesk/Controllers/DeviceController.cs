using BusinessLogic.Contracts;
using DataAccessLayer.Entities;
using HotDeskMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HotDeskMVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class DeviceController : Controller
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }
        public IActionResult Devices()
        {
            var model = new DeviceListViewModel()
            {
                Devices = _deviceService.GetAll().OrderByDescending(r => r.Id).ToArray()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddDevice(DeviceListViewModel model)
        {
            var device = new Device
            {
                DeviceName = model.NewDeviceName
            };
            await _deviceService.Create(device);
            return RedirectToAction("Devices");
        }
        public async Task<IActionResult> DeleteDevice(int id)
        {
            var deviceToDelete = _deviceService.GetAll().FirstOrDefault(d => d.Id == id);
            await _deviceService.Delete(deviceToDelete);
            return RedirectToAction("Devices");
        }
        /// <summary>
        /// Check in model does device with such name exist
        /// </summary>
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckDeviceName(string newDeviceName)
        {
            var device = _deviceService.GetAll().FirstOrDefault(d => d.DeviceName == newDeviceName);
            if (device is not null)
                return Json(false);
            return Json(true);
        }
    }
}
