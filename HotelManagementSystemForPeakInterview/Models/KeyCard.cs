using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystemForPeakInterview.Models
{
    public class KeyCard
    {
        public KeyCard(int number, bool isActive)
        {
            Number = number;
            IsActive = isActive;
        }

        public int Number { get; set; }
        public bool IsActive { get; set; }
    }
}

