using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;

namespace HotDeskMVC.Models
{
    public class BookDeskViewModel
    {
        public int DeskId { get; set; }
        public List<Device> Devices { get; set; }
        public List<DateTime> AvailableDates { get; set; }
        public DateTime SelectedTime { get; set; }
        public int[] SelectedDevicesId { get; set; }
    }
}
