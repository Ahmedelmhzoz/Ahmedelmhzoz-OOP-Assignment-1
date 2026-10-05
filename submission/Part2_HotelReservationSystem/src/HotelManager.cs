namespace Part2_HotelReservationSystem;

public class HotelManager{
     private readonly List<Reservation> _reservations = [];
     private readonly List<Room> _rooms = [];
     private readonly List<Guest> _guests = [];

     Reservation? _FindReservation(int reservationId) {
          foreach (Reservation reservation in _reservations) {
               if (reservation.ReservationId == reservationId) return reservation;
          }
          return null;
     }
     Guest? _FindGuest(int guestId) {
          foreach (Guest guest in _guests) {
               if (guest.GuestId == guestId) return guest;
          }
          return null;
     }
     Room? _FindRoom(int roomNumber) {
          foreach (Room room in _rooms) {
               if (room.RoomNumber == roomNumber) return room;
          }
          return null;
     }
     public int AddGuest(string name, string phone) {
          ArgumentException.ThrowIfNullOrEmpty(phone);
          ArgumentException.ThrowIfNullOrEmpty(phone);
          var guest = new Guest(name, phone);
          _guests.Add(guest);
          return  guest.GuestId;
     }
     public Room? GetGuestChechedInRoom(IReadOnlyList<Reservation> reservations) {
          foreach (var reservation in reservations) {
               if (reservation.Status == ReservationStatus.CheckedIn)
                    return reservation.ReservedRoom;
          }

          return null;
     }
     public Guest GetGuestById(int guestId) {
          var guest = _FindGuest(guestId);
          if (guest == null) throw new Exception($"Guest with ID #{guestId} not found.");
          return guest;
     }
     public void ShowReservationsHistoryForGuest(IReadOnlyList<Reservation> reservations) {
          Console.WriteLine("------------------- RESERVATION HISTORY -------------------------");

          if (reservations.Count == 0) {
               Console.WriteLine("  No reservation history found.");
               Console.WriteLine("-----------------------------------------------------------------");
               return;
          }

          Console.WriteLine(
               $"  {"Res ID",-8} {"Room",-6} {"Check-In",-12} {"Check-Out",-12} {"Status",-12} {"Cost",-8}");
          Console.WriteLine("  " + new string('-', 50));
          foreach (var res in reservations) {
               Console.WriteLine($"  #{res.ReservationId,-7} {res.ReservedRoom.RoomNumber,-6}" +
                                 $" {res.CheckInDate:yyyy-MM-dd}  {res.CheckOutDate:yyyy-MM-dd}" +
                                 $"  {res.Status,-12} {res.ReservationCost():F2}");
          }

          Console.WriteLine("-----------------------------------------------------------------");
     }
     public void ShowGuestDetails(int guestId) {
          ArgumentOutOfRangeException.ThrowIfNegativeOrZero(guestId);
          var guest = _FindGuest(guestId);
          if (guest == null)
               throw new KeyNotFoundException($"Guest with ID #{guestId} not found.");
          var guestRoom = GetGuestChechedInRoom(guest.Reservations);
          string roomNumber = guestRoom != null ? guestRoom.RoomNumber.ToString() : "There's no room!";
          Console.WriteLine("\n----------------------- GUEST DETAILS ---------------------------");
          Console.WriteLine($"  ID                  : #{guest.GuestId}");
          Console.WriteLine($"  Name                : {guest.GuestName}");
          Console.WriteLine($"  Phone               : {guest.GuestPhone}");
          Console.WriteLine($"  Current Room Number : {roomNumber}");
          Console.WriteLine("-----------------------------------------------------------------");
          ShowReservationsHistoryForGuest(guest.Reservations);
     }
     public void AddRoom(int roomNumber, Room.RoomType type, decimal nightlyRate,  bool inMaintenance) {
          _rooms.Add(new Room(roomNumber, type, nightlyRate, inMaintenance));
     }
     public void ShowRooms() {
          Console.WriteLine("\n------------------------- ROOMS LIST -------------------------");
          if (_rooms.Count == 0) {
               Console.WriteLine("  No rooms registered in the system.");
               Console.WriteLine("--------------------------------------------------------------");
               return;
          }
          Console.WriteLine($"  {"Room #",-8} {"Type",-10} {"Nightly Rate",-14} {"Status",-15}");
          Console.WriteLine("  " + new string('-', 55));
          foreach (var room in _rooms) {
               string maintenanceStatus = room.InMaintenance ? "Under Maintenance" : "Available";
               Console.WriteLine(
                    $"  #{room.RoomNumber,-7} {room.Type,-10} {room.NightlyRate,-14:F2} {maintenanceStatus,-15}");
          }
          Console.WriteLine("--------------------------------------------------------------");
     }
     public Room ChooseRoom(int roomNumber) {
          var room = _FindRoom(roomNumber);
          if (room == null) throw new Exception("Error, Invalid room number"); 
          if (room.InMaintenance) throw new Exception($"This room with #{room.RoomNumber} is under maintenance now");
          return room;
     }
     bool _IsRoomReserved(int selectedRoomNumber, DateTime checkInDate, DateTime checkOutDate) {
          foreach (var reservation in _reservations) {
               bool isSameRoom = reservation.ReservedRoom.RoomNumber == selectedRoomNumber;
               bool isActive = reservation.Status != ReservationStatus.Cancelled &&
                               reservation.Status != ReservationStatus.CheckedOut;
               bool isOverlapping = checkInDate < reservation.CheckOutDate && 
                                    reservation.CheckInDate < checkOutDate;

               if (isSameRoom && isActive && isOverlapping) {
                    return true; 
               }
          }
          return false;
     }
     public Reservation ReserveRoom(DateTime checkInDate, DateTime checkOutDate, Room selectedRoom, Guest guest) {
          if (_IsRoomReserved(selectedRoom.RoomNumber, checkInDate, checkOutDate)) 
              throw new Exception("This room is already reserved, Change check-in date");
          var reservation = new Reservation(checkInDate, checkOutDate, selectedRoom);
          _reservations.Add(reservation);
          guest.ReseveRoom(reservation);
          return reservation;
     }
     public void ChangeStatus(int reservationId, ReservationStatus targetStatus) {
          var reservation = _FindReservation(reservationId);
          if (reservation == null) throw new Exception($"Reservation with ID #{reservationId} not found.");
          switch (targetStatus) {
               case ReservationStatus.Confirmed: reservation.ConfirmReservation(); break;
               case ReservationStatus.Cancelled: reservation.CancelReservation(); break;
               case ReservationStatus.CheckedOut: reservation.CheckOutReservation(); break;
               case ReservationStatus.CheckedIn: reservation.CheckInReservation(); break;
          }
     }
     public void ChangeRoomRate(int roomNumber, decimal newRate) {
          var room = _FindRoom(roomNumber);
          if (room == null) throw new Exception($"Room with #{roomNumber} not found.");
          room.ChangeNightlyRate(newRate);
     }
     public void MaintainRoom(int roomNumber) {
          var room = _FindRoom(roomNumber);
          if (room == null) throw new Exception($"Room with #{roomNumber} not found.");
          room.Maintenance();
     }
     public void EndMaintenance(int roomNumber) {
          var room = _FindRoom(roomNumber);
          if (room == null) throw new Exception($"Room with #{roomNumber} not found.");
          room.EndMaintenance();
     }
}