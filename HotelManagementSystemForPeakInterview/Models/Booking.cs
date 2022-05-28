using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystemForPeakInterview.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int Status { get; set; }
        public int KeyCardNo { get; set; }
        public string GuestName { get; set; }
        public int GuestAge { get; set; }
    }

    public enum BookingStatus
    {
        CheckOut = 0,
        CheckIn = 1,
    };
}
