using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystemForPeakInterview.Models.Dto
{
    public class CheckOutDto
    {
        public CheckOutDto()
        {
                
        }

        public CheckOutDto(int keyCardNo, string name)
        {
            KeyCardNo = keyCardNo;
            Name = name;
        }

        public int KeyCardNo { get; set; }
        public string Name { get; set; }
    }
}
