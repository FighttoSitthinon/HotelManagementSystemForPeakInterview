using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystemForPeakInterview.IServices
{
    internal interface IGuestService
    {
        string GetGuestByRoom(string room);
        List<string> GetAllGuests();
        List<string> GetGuestsByAge(string condition);
    }
}
