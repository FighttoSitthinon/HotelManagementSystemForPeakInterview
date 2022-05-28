using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystemForPeakInterview.Models
{
    internal class Booking
    {
        public string GuestId { get; set; }
        public string RoomNumber { get; set; }
        public string Status { get; set; }
        public int KeyCardNo { get; set; }
    }

    public enum BookingStatus
    {
        CheckOut = 0,
        CheckIn = 1,
    };
}
