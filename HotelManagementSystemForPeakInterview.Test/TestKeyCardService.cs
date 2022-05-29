using HotelManagementSystemForPeakInterview.IServices;
using HotelManagementSystemForPeakInterview.Models;
using HotelManagementSystemForPeakInterview.Services;
using System.Collections.Generic;
using Xunit;

namespace HotelManagementSystemForPeakInterview.Test
{
    public class TestKeyCardService
    {
        private List<KeyCard> KeyCards;
        private readonly IKeyCardService keyCardService;

        public TestKeyCardService()
        {
            KeyCards = new List<KeyCard>();
            this.keyCardService = new KeyCardService(KeyCards);
        }

        [Fact]
        public void Get_Avaliable_KeyCard_Return_Index_1()
        {
            var result = keyCardService.GetAvaliableKayCard();
            Assert.IsType<KeyCard>(result);
            Assert.NotNull(result);
            Assert.Equal(1, result.Number);
        }

        [Fact]
        public void Set_KeyCard_1_Status_And_Get_Avaliable_KeyCard_Return_Index_2()
        {
            var keycard1 = keyCardService.GetAvaliableKayCard();
            Assert.IsType<KeyCard>(keycard1);
            Assert.NotNull(keycard1);
            Assert.Equal(1, keycard1.Number);

            keyCardService.SetKeyCardStatus(keycard1.Number, true);

            var keycard2 = keyCardService.GetAvaliableKayCard();
            Assert.IsType<KeyCard>(keycard2);
            Assert.NotNull(keycard2);
            Assert.Equal(2, keycard2.Number);
        }

        [Fact]
        public void Set_KeyCard_2_Status_And_Get_Avaliable_KeyCard_Return_Index_3()
        {
            var keycard1 = keyCardService.GetAvaliableKayCard();
            Assert.IsType<KeyCard>(keycard1);
            Assert.NotNull(keycard1);
            Assert.Equal(1, keycard1.Number);

            keyCardService.SetKeyCardStatus(keycard1.Number, true);

            var keycard2 = keyCardService.GetAvaliableKayCard();
            Assert.IsType<KeyCard>(keycard2);
            Assert.NotNull(keycard2);
            Assert.Equal(2, keycard2.Number);

            keyCardService.SetKeyCardStatus(keycard2.Number, true);

            var keycard3 = keyCardService.GetAvaliableKayCard();
            Assert.IsType<KeyCard>(keycard3);
            Assert.NotNull(keycard3);
            Assert.Equal(3, keycard3.Number);
        }
    }
}
