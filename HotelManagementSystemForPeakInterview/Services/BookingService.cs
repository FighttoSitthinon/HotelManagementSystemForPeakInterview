using HotelManagementSystemForPeakInterview.IServices;
using HotelManagementSystemForPeakInterview.Models;
using HotelManagementSystemForPeakInterview.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystemForPeakInterview.Services
{
    public class BookingService : IBookingService
    {
        private List<Booking> Bookings;
        private List<Room> Rooms;
        private int keyCardCount;

        public BookingService(List<Booking> bookings, List<Room> rooms)
        {
            Bookings = bookings;
            Rooms = rooms;
            keyCardCount = 0;
        }

        public int CheckIn(CheckInDto model)
        {
            var isRoomAlreadyBook = !Bookings.Where(x => x.RoomNumber == model.Room && x.Status == (int)BookingStatus.CheckIn).Any();
            if (isRoomAlreadyBook) return -1;

            int lastCount = Bookings.Count;

            keyCardCount++;

            Bookings.Add(new Booking()
            {
                Id = lastCount + 1,
                Status = (int)BookingStatus.CheckIn,
                RoomNumber = model.Room,
                GuestName = model.Name,
                GuestAge = model.Age,
                KeyCardNo = keyCardCount,
            });

            return keyCardCount;
        }

        public List<CheckInResultDto> CheckInByFloor(CheckInDto model, int floor)
        {
            throw new NotImplementedException();
        }

        public string CheckOut(CheckOutDto model)
        {
            var index = Bookings.FindIndex(x => x.KeyCardNo == model.KeyCardNo && x.GuestName == model.Name && x.Status == (int)BookingStatus.CheckIn);
            if (index == -1) return string.Empty;

            Bookings[index].Status = (int)BookingStatus.CheckOut;

            return Bookings[index].RoomNumber;
        }

        public List<string> CheckOutByFloor(int floor)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllGuests()
        {
            return Bookings.Where(g => g.Status == (int)BookingStatus.CheckIn).Select(g => g.GuestName).ToList();
        }

        public string GetGuestByRoom(string room)
        {
            var book = Bookings.Where(x => x.RoomNumber == room && x.Status == (int)BookingStatus.CheckIn).FirstOrDefault();
            if (book == null) return String.Empty;

            return book.GuestName;
        }

        public List<string> GetGuestsByAge(string operation, int age)
        {
            throw new NotImplementedException();
        }
    }
}
