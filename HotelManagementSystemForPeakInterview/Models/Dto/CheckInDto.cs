using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystemForPeakInterview.Models.Dto
{
    public class CheckInDto
    {
        public CheckInDto()
        {

        }

        public CheckInDto(string room, string name, int age)
        {
            Room = room;
            Name = name;
            Age = age;
        }

        public string Room { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
       
    }

    public class CheckInResultDto : CheckInDto
    {
        public CheckInResultDto()
        {

        }
        public int KeyCardNo { get; set; }
    }
}
