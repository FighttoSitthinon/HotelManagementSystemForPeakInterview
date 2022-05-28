using HotelManagementSystemForPeakInterview.IServices;
using HotelManagementSystemForPeakInterview.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystemForPeakInterview.Services
{
    public class BookingService : IBookingService
    {
        public BookingService()
        {

        }

        public CheckInResultDto CheckIn(CheckInDto model)
        {
            throw new NotImplementedException();
        }

        public List<CheckInResultDto> CheckInByFloor(CheckInDto model, int floor)
        {
            throw new NotImplementedException();
        }

        public string CheckOut(CheckOutDto model)
        {
            throw new NotImplementedException();
        }

        public List<string> CheckOutByFloor(int floor)
        {
            throw new NotImplementedException();
        }
    }
}
