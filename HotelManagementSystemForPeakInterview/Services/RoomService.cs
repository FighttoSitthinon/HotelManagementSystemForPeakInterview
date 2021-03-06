using HotelManagementSystemForPeakInterview.IServices;
using HotelManagementSystemForPeakInterview.Models;

namespace HotelManagementSystemForPeakInterview.Services
{
    public class RoomService : IRoomService
    {
        private List<Room> Rooms;
        public RoomService(List<Room> Rooms)
        {
            this.Rooms = Rooms;
        }

        public bool CreateRooms(int floorTotal, int roomPerFloor)
        {
            try
            {
                for (int floor = 1; floor <= floorTotal; floor++)
                {
                    for (int room = 1; room <= roomPerFloor; room++)
                    {
                        Rooms.Add(new Room()
                        {
                            Floor = floor,
                            RoomNumber = floor.ToString() + (room <= 9 ? "0" : "") + room.ToString(),
                            Status = (int)BookingStatus.CheckOut,
                        });
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<string> GetAvaliableRooms()
        {
            return Rooms.Where(r => r.Status == (int)BookingStatus.CheckOut).Select(r => r.RoomNumber).ToList();
        }

        public List<string> GetRoomsByFloor(int floor, BookingStatus? status = null)
        {
            var _Rooms = Rooms.Where(r => r.Floor == floor);
            if (status.HasValue)
            {
                _Rooms = _Rooms.Where(r => r.Status == (int)status);
            }
            return _Rooms.Select(r => r.RoomNumber).ToList();
        }

        public bool IsFloorAvaliable(int floor)
        {
            return !Rooms.Where(r => r.Floor == floor).Any(x => x.Status == (int)BookingStatus.CheckIn);
        }

        public bool IsRoomAvaliable(string room)
        {
            var index = Rooms.FindIndex(x => x.RoomNumber == room);
            if (index == -1) return false;
            return Rooms[index].Status == (int)BookingStatus.CheckOut;
        }

        public bool IsRoomExist(string roomId)
        {
            return Rooms.Any(x => x.RoomNumber == roomId);
        }

        public bool UpdateStatusRoom(string roomId, BookingStatus status)
        {
            var index = Rooms.FindIndex(x => x.RoomNumber == roomId);
            if (index == -1) return false;

            Rooms[index].Status = (int)status;

            return true;
        }
    }
}
