using HotelManagementSystemForPeakInterview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystemForPeakInterview.IServices
{
    public interface IRoomService
    {
        bool CreateRooms(int floorTotal, int roomPerFloor);
        List<string> GetAvaliableRooms();
        List<string> GetRoomsByFloor(int floor, BookingStatus? status = null);
        bool IsRoomExist(string roomId);
        bool UpdateStatusRoom(string roomId, BookingStatus status);
        bool IsRoomAvaliable(string room);
        bool IsFloorAvaliable(int floor);
    }
}
