using HotelManagementSystemForPeakInterview.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystemForPeakInterview.IServices
{
    internal interface IBookingService
    {
        CheckInResultDto CheckIn(CheckInDto model);
        List<CheckInResultDto> CheckInByFloor(CheckInDto model, int floor);
        string CheckOut(CheckOutDto model);
        List<string> CheckOutByFloor(int floor);
    }
}
