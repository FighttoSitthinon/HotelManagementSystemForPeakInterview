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
        private List<KeyCard> KeyCards;

        private readonly IBookingService bookingService;
        private readonly IRoomService roomService;
        private readonly IKeyCardService keyCardService;

        public Main()
        {
            Bookings = new List<Booking>();
            Rooms = new List<Room>();
            KeyCards = new List<KeyCard>();
            keyCardService = new KeyCardService(KeyCards);
            bookingService = new BookingService(Bookings, keyCardService);
            roomService = new RoomService(Rooms);
        }

        public void Start()
        {
            string _filePath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            string[] lines = File.ReadAllLines(_filePath + "/Data/input.txt");

            foreach (string line in lines)
            {
                var result = ExtractString(line);
                // Use a tab to indent each line of the file.
                Console.WriteLine("\t" + result);
            }
        }

        public string ExtractString(string line)
        {
            string result = "";

            string[] words = line.Split(" ");

            string keyword = words[0];

            switch (keyword)
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

                if (!roomService.IsRoomAvaliable(model.Room))
                {
                    var GuestName = bookingService.GetGuestByRoom(model.Room);
                    return $"Cannot book room {model.Room} for {model.Name}, The room is currently booked by {GuestName}.";
                }

                var KeyCardNo = bookingService.CheckIn(model);
                if (KeyCardNo != -1)
                {
                    var updateResult = roomService.UpdateStatusRoom(model.Room, BookingStatus.CheckIn);
                    if (updateResult)
                    {
                        return $"Room {model.Room} is booked by {model.Name} with keycard number {KeyCardNo}.";
                    }
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
            try
            {
                int floor = int.Parse(words[1]);
                CheckInDto model = new CheckInDto()
                {
                    Room = null,
                    Name = words[2],
                    Age = int.Parse(words[3]),
                };

                if (!roomService.IsFloorAvaliable(floor))
                {
                    return $"Cannot book floor {floor} for {model.Name}.";
                }

                var roomIdList = roomService.GetRoomsByFloor(floor);
                if (roomIdList == null || roomIdList.Count == 0) return "Floor or room doesn't exist.";

                var keyCards = bookingService.CheckInByRoomIdList(model, roomIdList);

                foreach (var room in roomIdList)
                {
                    var updateResult = roomService.UpdateStatusRoom(room, BookingStatus.CheckIn);
                    if (!updateResult) return "Something wrong";
                }

                return $"Room {string.Join(", ", roomIdList)} are booked with keycard number {string.Join(", ", keyCards)}";
            }
            catch (Exception)
            {
                return "Something wrong";
            }
        }

        private string Checkout(string[] words)
        {
            try
            {
                CheckOutDto model = new CheckOutDto()
                {
                    KeyCardNo = int.Parse(words[1]),
                    Name = words[2]
                };

                var room = bookingService.CheckOut(model);
                if (string.IsNullOrWhiteSpace(room))
                {
                    string guestName = bookingService.GetGuestByKeyCard(model.KeyCardNo);
                    return $"Only {guestName} can checkout with keycard number {model.KeyCardNo}.";
                }

                var updateResult = roomService.UpdateStatusRoom(room, BookingStatus.CheckOut);
                if (updateResult)
                {
                    return $"Room {room} is checkout.";
                }

                return "Something wrong";
            }
            catch(Exception ex)
            {
                return "Something wrong";
            }
        }

        private string CheckoutGuestByFloor(string[] words)
        {
            try
            {
                int floor = int.Parse(words[1]);
                var roomIdList = roomService.GetRoomsByFloor(floor, BookingStatus.CheckIn);
                if (roomIdList == null) return "No rooms has guest.";

                bool isSuccess = bookingService.CheckOutByRoomIdList(roomIdList);
                if (!isSuccess) return "Something wrong";

                foreach (var room in roomIdList)
                {
                    var updateResult = roomService.UpdateStatusRoom(room, BookingStatus.CheckOut);
                    if (!updateResult) return "Something wrong";
                }

                return $"Room {string.Join(", ", roomIdList)} are checkout.";
            }
            catch(Exception ex)
            {
                return "Something wrong";
            }
        }

        private string ListAvailableRooms()
        {
            try
            {
                var rooms = roomService.GetAvaliableRooms();
                if (rooms != null && rooms.Count > 0)
                {
                    return string.Join(", ", rooms);
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
                    return string.Join(", ", guests);
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
            try
            {
                string operation = words[1];
                int age = int.Parse(words[2]);

                var guests = bookingService.GetGuestsByAge(operation, age);
                if (guests != null && guests.Count > 0)
                {
                    return string.Join(", ", guests);
                }

                return "Something wrong";
            }
            catch (Exception)
            {
                return "Something wrong";
            }
        }

        private string ListGuestByFloor(string[] words)
        {
            try
            {
                int floor = int.Parse(words[1]);

                var roomIdList = roomService.GetRoomsByFloor(floor);
                if (roomIdList == null || roomIdList.Count == 0) return "Floor or room doesn't exist.";

                var guests = bookingService.GetGuestsByRoomIdList(roomIdList);
                if (guests != null && guests.Count > 0)
                {
                    return string.Join(", ", guests);
                }

                return "Something wrong";
            }
            catch (Exception)
            {
                return "Something wrong";
            }
        }
    }
}
