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
        private List<KeyCard> KeyCards;

        public BookingService(List<Booking> bookings, List<KeyCard> keyCards)
        {
            Bookings = bookings;
            KeyCards = keyCards;
        }

        public int CheckIn(CheckInDto model)
        {
            int lastCount = Bookings.Count;

            int AvaliableKayCardIndex = KeyCards.FindIndex(x => !x.IsActive);
            if (AvaliableKayCardIndex == -1)
            {
                int keyCardCount = KeyCards.Count + 1;
                KeyCards.Add(new KeyCard(keyCardCount, false));
                AvaliableKayCardIndex = keyCardCount - 1;
            }

            Bookings.Add(new Booking()
            {
                Id = lastCount + 1,
                Status = (int)BookingStatus.CheckIn,
                RoomNumber = model.Room,
                GuestName = model.Name,
                GuestAge = model.Age,
                KeyCardNo = KeyCards[AvaliableKayCardIndex].Number,
            });

            KeyCards[AvaliableKayCardIndex].IsActive = true;

            return KeyCards[AvaliableKayCardIndex].Number;
        }

        public List<int> CheckInByRoomIdList(CheckInDto model, List<string> rooms)
        {
            List<int> KeyCards = new List<int>();
            foreach(var room in rooms)
            {
                model.Room = room;
                var KeyCard = CheckIn(model);
                KeyCards.Add(KeyCard);
            }

            return KeyCards;
        }

        public string CheckOut(CheckOutDto model)
        {
            var index = Bookings.FindIndex(x => x.KeyCardNo == model.KeyCardNo && x.GuestName == model.Name && x.Status == (int)BookingStatus.CheckIn);
            if (index == -1) return string.Empty;

            Bookings[index].Status = (int)BookingStatus.CheckOut;
            int keyCardIndex = Bookings[index].KeyCardNo - 1;
            KeyCards[keyCardIndex].IsActive = false;

            return Bookings[index].RoomNumber;
        }

        public bool CheckOutByRoomIdList(List<string> rooms)
        {
            try
            {
                foreach (var room in rooms)
                {
                    var index = Bookings.FindIndex(x => x.RoomNumber == room && x.Status == (int)BookingStatus.CheckIn);
                    if (index == -1) return false;

                    Bookings[index].Status = (int)BookingStatus.CheckOut;
                    int keyCardIndex = Bookings[index].KeyCardNo - 1;
                    KeyCards[keyCardIndex].IsActive = false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<string> GetAllGuests()
        {
            return Bookings.Where(g => g.Status == (int)BookingStatus.CheckIn).Select(g => g.GuestName).Distinct().ToList();
        }

        public string GetGuestByKeyCard(int KeyCardNo)
        {
            var book = Bookings.Where(x => x.KeyCardNo == KeyCardNo && x.Status == (int)BookingStatus.CheckIn).FirstOrDefault();
            if (book == null) return String.Empty;

            return book.GuestName;
        }

        public string GetGuestByRoom(string room)
        {
            var book = Bookings.Where(x => x.RoomNumber == room && x.Status == (int)BookingStatus.CheckIn).FirstOrDefault();
            if (book == null) return String.Empty;

            return book.GuestName;
        }

        public List<string> GetGuestsByAge(string operation, int age)
        {
            var bookings = Bookings.Where(x => x.Status == (int)BookingStatus.CheckIn);

            switch (operation)
            {
                case "<":
                    bookings = bookings.Where(x => x.GuestAge < age);
                    break;
                case ">":
                    bookings = bookings.Where(x => x.GuestAge > age);
                    break;
                case "=":
                    bookings = bookings.Where(x => x.GuestAge == age);
                    break;
                case "<=":
                    bookings = bookings.Where(x => x.GuestAge <= age);
                    break;
                case ">=":
                    bookings = bookings.Where(x => x.GuestAge >= age);
                    break;
            }

            return bookings.Select(x => x.GuestName).Distinct().ToList();
        }

        public List<string> GetGuestsByRoomIdList(List<string> rooms)
        {
            return Bookings.Where(x => rooms.Contains(x.RoomNumber) && x.Status == (int)BookingStatus.CheckIn).Select(x => x.GuestName).Distinct().ToList();
        }
    }
}
