using HotelManagementSystemForPeakInterview.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystemForPeakInterview.IServices
{
    public interface IKeyCardService
    {
        void SetKeyCardStatus(int Number, bool IsActive);
        KeyCard GetAvaliableKayCard();
    }
}
