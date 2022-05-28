using HotelManagementSystemForPeakInterview.IServices;
using HotelManagementSystemForPeakInterview.Models;
using HotelManagementSystemForPeakInterview.Models.Dto;
using HotelManagementSystemForPeakInterview.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystemForPeakInterview
{
    public class Main
    {
        private List<Booking> Bookings;
        private List<Room> Rooms;

        private IBookingService bookingService;
        private IRoomService roomService;

        public Main()
        {
            Bookings = new List<Booking>();
            Rooms = new List<Room>();
            bookingService = new BookingService(Bookings, Rooms);
            roomService = new RoomService(Rooms);
        }

        public void Start()
        {
            string _filePath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            string[] lines = File.ReadAllLines(_filePath + "/Data/input.txt");

            //foreach (string line in lines)
            //{
            //    var result = ExtractString(line);
            //    // Use a tab to indent each line of the file.
            //    Console.WriteLine("\t" + line);
            //}

            var result = ExtractString("create_hotel 2 3");
            // Use a tab to indent each line of the file.
            Console.WriteLine("\t" + result);

            result = ExtractString("list_available_rooms");
            // Use a tab to indent each line of the file.
            Console.WriteLine("\t" + result);

            result = ExtractString("book 203 TonyStark 48");
            // Use a tab to indent each line of the file.
            Console.WriteLine("\t" + result);
        }

        private string ExtractString(string line)
        {
            string result = "";

            string[] words = line.Split(" ");

            string cmd = words[0];

            switch (cmd)
            {
                case "create_hotel":
                    result = CreateHotel(words);
                    break;
                case "book":
                    result = Booking(words);
                    break;
                case "book_by_floor":
                    result = BookingByFloor(words);
                    break;
                case "checkout":
                    result = Checkout(words);
                    break;
                case "checkout_guest_by_floor":
                    result = CheckoutGuestByFloor(words);
                    break;
                case "list_available_rooms":
                    result = ListAvailableRooms();
                    break;
                case "list_guest":
                    result = ListGuests();
                    break;
                case "get_guest_in_room":
                    result = GetGuestInRoom(words);
                    break;
                case "list_guest_by_age":
                    result = ListGuestByAge(words);
                    break;
                case "list_guest_by_floor":
                    result = ListGuestByFloor(words);
                    break;
            }
                
            return result;
        }

        private string CreateHotel(string[] words)
        {
            try
            {
                int floor = -1, roomPerFloor = -1;
                bool parseFloor = int.TryParse(words[1], out floor);
                bool parseRoomPerFloor = int.TryParse(words[2], out roomPerFloor);
                if (parseFloor && parseRoomPerFloor)
                {
                    var isSuccess = roomService.CreateRooms(floor, roomPerFloor);
                    if (isSuccess)
                    {
                        return $"Hotel created with {floor} floor(s), {roomPerFloor} room(s) per floor.";
                    }
                }

                return "Something wrong";
            }
            catch (Exception)
            {
                return "Something wrong";
            }
        }

        private string Booking(string[] words)
        {
            try
            {
                CheckInDto model = new CheckInDto()
                {
                    Room = words[1],
                    Name = words[2],
                    Age = int.Parse(words[3]),
                };

                if (!roomService.IsRoomExist(model.Room))
                {
                    return $"Room {model.Room} does not exist.";
                }

                var KeyCardNo = bookingService.CheckIn(model);
                if (KeyCardNo != -1)
                {
                    return $"Room {model.Room} is booked by {model.Name} with keycard number {KeyCardNo}.";
                }

                return "Something wrong";
            }
            catch (Exception)
            {
                return "Something wrong";
            }
        }

        private string BookingByFloor(string[] words)
        {
            throw new NotImplementedException();
        }

        private string Checkout(string[] words)
        {
            CheckOutDto model = new CheckOutDto()
            {
                KeyCardNo = int.Parse(words[1]),
                Name = words[2]
            };

            var room = bookingService.CheckOut(model);
            if (!string.IsNullOrWhiteSpace(room))
            {
                return $"Room {room} is checkout.";
            }

            return "Something wrong";
        }

        private string CheckoutGuestByFloor(string[] words)
        {
            throw new NotImplementedException();
        }

        private string ListAvailableRooms()
        {
            try
            {
                var rooms = roomService.GetAvaliableRooms();
                if (rooms != null && rooms.Count > 0)
                {
                    string result = "";
                    foreach (var room in rooms)
                    {
                        result += room + " ";
                    }
                    return result;
                }

                return "Something wrong";
            }
            catch (Exception)
            {
                return "Something wrong";
            }
        }

        private string ListGuests()
        {
            try
            {
                var guests = bookingService.GetAllGuests();
                if (guests != null && guests.Count > 0)
                {
                    string result = "";
                    foreach (var guest in guests)
                    {
                        result += guest + " ";
                    }
                    return result;
                }

                return "Something wrong";
            }
            catch (Exception)
            {
                return "Something wrong";
            }
        }

        private string GetGuestInRoom(string[] words)
        {
            try
            {
                string room = words[1];

                if (!roomService.IsRoomExist(room))
                {
                    return $"Room {room} does not exist.";
                }

                string guestName = bookingService.GetGuestByRoom(room);
                if (!string.IsNullOrWhiteSpace(guestName))
                {
                    return guestName;
                }

                return "Something wrong";
            }
            catch (Exception)
            {
                return "Something wrong";
            }
        }

        private string ListGuestByAge(string[] words)
        {
            throw new NotImplementedException();
        }

        private string ListGuestByFloor(string[] words)
        {
            throw new NotImplementedException();
        }
    }
}
