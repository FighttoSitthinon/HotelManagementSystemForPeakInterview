using HotelManagementSystemForPeakInterview.IServices;
using HotelManagementSystemForPeakInterview.Models;
using HotelManagementSystemForPeakInterview.Models.Dto;
using HotelManagementSystemForPeakInterview.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelManagementSystemForPeakInterview.Test
{
    public class TestBookingService
    {
        private List<Booking> Bookings;
        private List<KeyCard> KeyCards;

        private readonly IBookingService bookingService;
        private readonly IKeyCardService keyCardService;

        public TestBookingService()
        {
            Bookings = new List<Booking>();
            KeyCards = new List<KeyCard>();
            keyCardService = new KeyCardService(KeyCards);
            bookingService = new BookingService(Bookings, keyCardService);
        }

        [Fact]
        public void CheckIn_First_Person_Should_Return_KeyCard_1()
        {
            var model = new CheckInDto("101", "TonyStark", 100);
            var result = bookingService.CheckIn(model);
            Assert.IsType<int>(result);
            Assert.Equal(1, result);
        }

        [Fact]
        public void CheckIn_And_CheckOut_Should_Return_RoomNumber()
        {
            var CheckInModel = new CheckInDto("101", "TonyStark", 100);
            var keyCard = bookingService.CheckIn(CheckInModel);
            Assert.IsType<int>(keyCard);
            Assert.Equal(1, keyCard);

            var CheckOutModel = new CheckOutDto(keyCard, CheckInModel.Name);
            string Room = bookingService.CheckOut(CheckOutModel);
            Assert.Equal("101", Room);
        }

        [Fact]
        public void CheckIn_And_CheckOut_Wrong_KeyCard_Should_Return_Empty_String()
        {
            var CheckInModel = new CheckInDto("101", "TonyStark", 100);
            var keyCard = bookingService.CheckIn(CheckInModel);
            Assert.IsType<int>(keyCard);
            Assert.Equal(1, keyCard);

            var CheckOutModel = new CheckOutDto(keyCard + 1, CheckInModel.Name);
            string Room = bookingService.CheckOut(CheckOutModel);
            Assert.Empty(Room);
        }

        [Fact]
        public void CheckIn_Two_Guest_And_Get_All_Guests_Should_Return_Name_Of_Two_Guests()
        {
            var CheckInModel = new CheckInDto("101", "TonyStark", 100);
            var keyCard = bookingService.CheckIn(CheckInModel);
            Assert.Equal(1, keyCard);

            var CheckInModel2 = new CheckInDto("102", "PeterParker", 50);
            var keyCard2 = bookingService.CheckIn(CheckInModel2);
            Assert.Equal(2, keyCard2);

            var Guests = bookingService.GetAllGuests();
            Assert.IsType<List<string>>(Guests);
            Assert.Equal(2, Guests.Count);
        }

        [Fact]
        public void CheckIn_Three_Guest_And_Get_Age_Less_Than_50_Should_Return_PeterParker()
        {
            var CheckInModel = new CheckInDto("101", "TonyStark", 100);
            var keyCard = bookingService.CheckIn(CheckInModel);
            Assert.Equal(1, keyCard);

            var CheckInModel2 = new CheckInDto("102", "StephenStrange", 50);
            var keyCard2 = bookingService.CheckIn(CheckInModel2);
            Assert.Equal(2, keyCard2);

            var CheckInModel3 = new CheckInDto("103", "PeterParker", 25);
            var keyCard3 = bookingService.CheckIn(CheckInModel3);
            Assert.Equal(3, keyCard3);

            var Guests = bookingService.GetGuestsByAge("<", 50);
            Assert.IsType<List<string>>(Guests);
            Assert.Single(Guests);
            Assert.Equal("PeterParker", Guests[0]);
        }

        [Fact]
        public void CheckIn_Three_Guest_And_Get_Age_More_Than_50_Should_Return_TonyStark()
        {
            var CheckInModel = new CheckInDto("101", "TonyStark", 100);
            var keyCard = bookingService.CheckIn(CheckInModel);
            Assert.Equal(1, keyCard);

            var CheckInModel2 = new CheckInDto("102", "StephenStrange", 50);
            var keyCard2 = bookingService.CheckIn(CheckInModel2);
            Assert.Equal(2, keyCard2);

            var CheckInModel3 = new CheckInDto("103", "PeterParker", 25);
            var keyCard3 = bookingService.CheckIn(CheckInModel3);
            Assert.Equal(3, keyCard3);

            var Guests = bookingService.GetGuestsByAge(">", 50);
            Assert.IsType<List<string>>(Guests);
            Assert.Single(Guests);
            Assert.Equal("TonyStark", Guests[0]);
        }

        [Fact]
        public void CheckIn_Three_Guest_And_Get_Age_Equal_50_Should_Return_StephenStrange()
        {
            var CheckInModel = new CheckInDto("101", "TonyStark", 100);
            var keyCard = bookingService.CheckIn(CheckInModel);
            Assert.Equal(1, keyCard);

            var CheckInModel2 = new CheckInDto("102", "StephenStrange", 50);
            var keyCard2 = bookingService.CheckIn(CheckInModel2);
            Assert.Equal(2, keyCard2);

            var CheckInModel3 = new CheckInDto("103", "PeterParker", 25);
            var keyCard3 = bookingService.CheckIn(CheckInModel3);
            Assert.Equal(3, keyCard3);

            var Guests = bookingService.GetGuestsByAge("=", 50);
            Assert.IsType<List<string>>(Guests);
            Assert.Single(Guests);
            Assert.Equal("StephenStrange", Guests[0]);
        }

        [Fact]
        public void CheckIn_Three_Guest_And_Get_Age_Less_Than_Or_Equal_50_Should_Not_Return_TonyStark()
        {
            var CheckInModel = new CheckInDto("101", "TonyStark", 100);
            var keyCard = bookingService.CheckIn(CheckInModel);
            Assert.Equal(1, keyCard);

            var CheckInModel2 = new CheckInDto("102", "StephenStrange", 50);
            var keyCard2 = bookingService.CheckIn(CheckInModel2);
            Assert.Equal(2, keyCard2);

            var CheckInModel3 = new CheckInDto("103", "PeterParker", 25);
            var keyCard3 = bookingService.CheckIn(CheckInModel3);
            Assert.Equal(3, keyCard3);

            var Guests = bookingService.GetGuestsByAge("<=", 50);
            Assert.IsType<List<string>>(Guests);

            Assert.True(!Guests.Any(x => x == "TonyStark"));
        }

        [Fact]
        public void CheckIn_Three_Guest_And_Get_Age_More_Than_Or_Equal_50_Should_Not_Return_PeterParker()
        {
            var CheckInModel = new CheckInDto("101", "TonyStark", 100);
            var keyCard = bookingService.CheckIn(CheckInModel);
            Assert.Equal(1, keyCard);

            var CheckInModel2 = new CheckInDto("102", "StephenStrange", 50);
            var keyCard2 = bookingService.CheckIn(CheckInModel2);
            Assert.Equal(2, keyCard2);

            var CheckInModel3 = new CheckInDto("103", "PeterParker", 25);
            var keyCard3 = bookingService.CheckIn(CheckInModel3);
            Assert.Equal(3, keyCard3);

            var Guests = bookingService.GetGuestsByAge(">=", 50);
            Assert.IsType<List<string>>(Guests);

            Assert.True(!Guests.Any(x => x == "PeterParker"));
        }


    }
}
