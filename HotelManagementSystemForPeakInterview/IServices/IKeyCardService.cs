using HotelManagementSystemForPeakInterview.Models;

namespace HotelManagementSystemForPeakInterview.IServices
{
    public interface IKeyCardService
    {
        void SetKeyCardStatus(int Number, bool IsActive);
        KeyCard GetAvaliableKayCard();
    }
}
