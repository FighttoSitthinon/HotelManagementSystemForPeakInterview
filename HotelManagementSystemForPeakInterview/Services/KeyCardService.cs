using HotelManagementSystemForPeakInterview.IServices;
using HotelManagementSystemForPeakInterview.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystemForPeakInterview.Services
{
    public class KeyCardService : IKeyCardService
    {
        private List<KeyCard> KeyCards;

        public KeyCardService(List<KeyCard> keyCards)
        {
            KeyCards = keyCards;
        }

        public KeyCard GetAvaliableKayCard()
        {
            int AvaliableKayCardIndex = KeyCards.FindIndex(x => !x.IsActive);
            if (AvaliableKayCardIndex == -1)
            {
                int keyCardCount = KeyCards.Count + 1;
                KeyCards.Add(new KeyCard(keyCardCount, false));
                AvaliableKayCardIndex = keyCardCount - 1;
            }
            return KeyCards[AvaliableKayCardIndex];
        }

        public void SetKeyCardStatus(int Number, bool IsActive)
        {
            KeyCards[Number - 1].IsActive = IsActive;
        }
    }
}
