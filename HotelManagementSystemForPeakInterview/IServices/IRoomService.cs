using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystemForPeakInterview.IServices
{
    internal interface IRoomService
    {
        string CreateRooms(int floorTotal, int roomPerFloor);
        List<string> GetAvaliableRooms();
    }
}
