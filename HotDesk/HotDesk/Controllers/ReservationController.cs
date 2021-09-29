using BusinessLogic.Contracts;
using HotDeskMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotDeskMVC.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IDeskService _deskService;
        private readonly IDeviceService _deviceService;
        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;

        public ReservationController(IDeskService deskService, IDeviceService deviceService, IBookingService bookingService, IUserService userService)
        {
            _deskService = deskService;
            _deviceService = deviceService;
            _bookingService = bookingService;
            _userService = userService;
        }
        [Authorize(Roles = "admin, user")]
        [HttpGet]
        public IActionResult BookDesk(int deskId)
        {
            var model = new BookDeskViewModel()
            {
                AvailableDates = _bookingService.GetAllPossibleReservationTime(deskId, User.Identity.Name),
                DeskId = deskId,
                Devices = _deskService.GetAll().FirstOrDefault(d => d.Id == deskId).Devices.ToList()
            };
            return View(model);
        }

        [Authorize(Roles = "admin, user")]
        [HttpPost]
        public async Task<IActionResult> BookDesk(BookDeskViewModel model)
        {
            // Get all the devices that are contained in recieved model
            var devices = await _deviceService.Get(x => model.SelectedDevicesId.Contains(x.Id)).ToListAsync();
            //Get the user
            var user = await _userService.GetByLoginAsync(User.Identity.Name);

            await _bookingService.AddReservationAsync(user.Id, model.DeskId, devices, model.SelectedTime);

            return RedirectToAction("Reservations");
        }
        [Authorize(Roles = "admin, user")]
        [HttpGet]
        public IActionResult Reservations()
        {
            var model = new ReservationsViewModel()
            {
                Reservations = _bookingService.GetAll().OrderByDescending(r => r.Id).ToArray()
            };
            return View(model);
        }
    }
}
