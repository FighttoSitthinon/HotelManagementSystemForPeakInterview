using HotelManagementSystemForPeakInterview.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystemForPeakInterview.IServices
{
    public interface IBookingService
    {
        int CheckIn(CheckInDto model);
        List<int> CheckInByRoomIdList(CheckInDto model, List<string> rooms);
        string CheckOut(CheckOutDto model);
        bool CheckOutByRoomIdList(List<string> rooms);
        string GetGuestByRoom(string room);
        string GetGuestByKeyCard(int KeyCardNo);
        List<string> GetAllGuests();
        List<string> GetGuestsByAge(string operation, int age);
        List<string> GetGuestsByRoomIdList(List<string> rooms);
    }
}
