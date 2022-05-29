using HotelManagementSystemForPeakInterview.IServices;
using HotelManagementSystemForPeakInterview.Models;
using HotelManagementSystemForPeakInterview.Services;
using System.Collections.Generic;
using Xunit;

namespace HotelManagementSystemForPeakInterview.Test
{
    public class TestRoomService
    {
        private List<Room> Rooms;
        private readonly IRoomService roomService;

        public TestRoomService()
        {
            Rooms = new List<Room>();
            this.roomService = new RoomService(Rooms);
        }

        [Fact]
        public void Can_Create_Hotel_Return_True()
        {
            int floor = 2, roomPerFloor = 3;
            bool result = roomService.CreateRooms(floor, roomPerFloor);
            Assert.True(result, $"Hotel created with {floor} floor(s), {roomPerFloor} room(s) per floor.");
        }

        [Fact]
        public void Get_Avaliable_Rooms_But_No_Room_Return_Count_Zero()
        {
            var result = roomService.GetAvaliableRooms();
            Assert.IsType<List<string>>(result);
            Assert.Equal(result.Count, 0);
        }

        [Fact]
        public void Check_Existing_Room_Return_True()
        {
            int floor = 1, roomPerFloor = 1;
            bool isRoomCreated = roomService.CreateRooms(floor, roomPerFloor);
            Assert.IsType<bool>(isRoomCreated);
            Assert.True(isRoomCreated);

            var result = roomService.IsRoomAvaliable("101");
            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public void Check_Existing_Room_But_Out_Of_Index_Should_Return_False()
        {
            int floor = 1, roomPerFloor = 1;
            bool isRoomCreated = roomService.CreateRooms(floor, roomPerFloor);
            Assert.IsType<bool>(isRoomCreated);
            Assert.True(isRoomCreated);

            var result = roomService.IsRoomAvaliable("103");
            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public void Check_Not_Existing_Room_Without_Room_Should_Return_False()
        {
            var result = roomService.IsRoomAvaliable("101");
            Assert.IsType<bool>(result);
            Assert.False(result);
        }
    }
}