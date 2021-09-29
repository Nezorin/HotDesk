using BusinessLogic.Contracts;
using DataAccessLayer.Entities;
using HotDeskMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HotDeskMVC.Controllers
{
    public class DeskController : Controller
    {
        private readonly IDeskService _deskService;
        private readonly IDeviceService _deviceService;

        public DeskController(IDeskService deskService, IDeviceService deviceService)
        {
            _deskService = deskService;
            _deviceService = deviceService;
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new DeskViewModel
            {
                Desks = _deskService.GetAll().ToList()
            };
            return View(viewModel);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddDesk(AddDeskViewModel deskModel)
        {
            // Get all the devices that are contained in recieved model
            var devices = await _deviceService.Get(x => deskModel.SelectedDevicesId.Contains(x.Id)).ToListAsync();
            var desk = new Desk
            {
                DeskName = deskModel.NewDeskName,
                Devices = devices
            };
            await _deskService.CreateAsync(desk);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddDesk()
        {
            var model = new AddDeskViewModel()
            {
                Devices = _deviceService.GetAll().ToList()
            };
            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditDeskName(EditDeskViewModel model)
        {
            var desk = _deskService.GetAll().FirstOrDefault(d => d.Id == model.Desk.Id);
            desk.DeskName = model.NewDeskName;
            await _deskService.UpdateAsync(desk);
            return RedirectToAction("EditDesk", new { id = model.Desk.Id });
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteDeviceFromDesk(int deskId, int deviceId)
        {
            await _deskService.DeleteDeviceFromDeskAsync(deskId, deviceId);
            return RedirectToAction("EditDesk", new { id = deskId });
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddDeviceToDesk(int deskId, int deviceId)
        {
            await _deskService.AddDeviceToDeskAsync(deskId, deviceId);
            return RedirectToAction("EditDesk", new { id = deskId });
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult EditDesk(int id)
        {
            var model = new EditDeskViewModel
            {
                Desk = _deskService.GetAll().FirstOrDefault(d => d.Id == id)
            };
            //Get all devices that are not in the current desk
            model.DevicesToAdd = _deviceService.GetAll().Where(d => !model.Desk.Devices.Contains(d)).ToArray();
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteDesk(int id)
        {
            var deskToDelete = _deskService.GetAll().FirstOrDefault(d => d.Id == id);
            await _deskService.DeleteAsync(deskToDelete);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Check in model does desk with such desk name exist
        /// </summary>
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckDeskName(string newDeskName)
        {
            var desk = _deskService.GetAll().FirstOrDefault(d => d.DeskName == newDeskName);
            if (desk is not null)
                return Json(false);
            return Json(true);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
