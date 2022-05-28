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
        int CheckIn(CheckInDto model);
        List<CheckInResultDto> CheckInByFloor(CheckInDto model, int floor);
        string CheckOut(CheckOutDto model);
        List<string> CheckOutByFloor(int floor);
        string GetGuestByRoom(string room);
        List<string> GetAllGuests();
        List<string> GetGuestsByAge(string operation, int age);
    }
}
