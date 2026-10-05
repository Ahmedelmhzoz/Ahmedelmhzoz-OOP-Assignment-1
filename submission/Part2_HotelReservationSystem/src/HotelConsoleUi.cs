namespace Part2_HotelReservationSystem;
public class HotelConsoleUi{
    public enum MenuChoice {
        Exit,
        BookRoom,
        Confirm,
        CheckIn,
        CheckOut,
        CancelReservation,
        AddGuest,
        ShowGuestHistory,
        ShowRooms,
        ChangeRoomRate,
        LookRoom,
        EndMaintenance,
    }
    public static void SeedData(HotelManager hotelManager) {
        hotelManager.AddRoom(1, Room.RoomType.Single, 15.0m, false);
        hotelManager.AddRoom(2, Room.RoomType.Double, 35.0m, false);
        hotelManager.AddRoom(3, Room.RoomType.Suite, 55.0m, true);
        hotelManager.AddRoom(4, Room.RoomType.Suite, 60.0m, false);
        hotelManager.AddRoom(5, Room.RoomType.Single, 20.0m, false);
    }
    private static void PrintMenu() {
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("          HOTEL RESERVATION SYSTEM                ");
        Console.WriteLine("==================================================");
    
        Console.WriteLine(" [ RESERVATIONS ]");
        Console.WriteLine("  1) Book a Room");
        Console.WriteLine("  2) Confirm Reservation");
        Console.WriteLine("  3) Check-In Reservation");
        Console.WriteLine("  4) Check-Out Reservation");
        Console.WriteLine("  5) Cancel Reservation");
        Console.WriteLine();

        Console.WriteLine(" [ GUESTS ]");
        Console.WriteLine("  6) Register New Guest");
        Console.WriteLine("  7) View Guest Profile & History");
        Console.WriteLine();

        Console.WriteLine(" [ ROOMS ]");
        Console.WriteLine("  8) View All Rooms");
        Console.WriteLine("  9) Change Room Nightly Rate");
        Console.WriteLine("  10) Look room for maintenance");
        Console.WriteLine("  12) End maintenance");
        Console.WriteLine();

        Console.WriteLine(" [ SYSTEM ]");
        Console.WriteLine("  0) Exit");
        Console.WriteLine("==================================================");
    }
    private static void HandleException(string exceptionMessage) {
        Console.WriteLine($"\n{exceptionMessage}");
    }
    public static int GetNumberFromUser(string message) {
        int number;
        Console.Write($"\n{message}");
        while (!int.TryParse(Console.ReadLine(), out number))
            Console.Write($"\n{message}");
        return number;
    }
    private static DateTime GetDateFromUser(string message) {
        DateTime date;
        var format = "Format(yyyy-MM-dd)";
        Console.Write($"{message} | {format}: ");
        while (!DateTime.TryParse(Console.ReadLine(), out date)) 
            Console.Write("Invalid date format, Enter Date again like 2026-10-4: ");
        return date;
    }
    private static decimal GetDecimalFromUser(string message) {
        decimal number;
        Console.Write($"\n{message}");
        while (!decimal.TryParse(Console.ReadLine(), out number))
            Console.Write($"\n{message}");
        return number;
    }
    public static Guest SelectGuestFlow(HotelManager hotelManager) {
        Guest guest;
        while (true) {
            try {
                var guestId = GetNumberFromUser("Please enter guest ID: ");
                guest = hotelManager.GetGuestById(guestId);
                break;
            }
            catch (Exception e) {
                HandleException(e.Message);
                Console.WriteLine("Press any key to reselect a guest ID...");
                Console.ReadKey(intercept: true);
                Console.Clear();
            }
        }
        return guest;
    }

    public static Room SelectRoomFlow(HotelManager hotelManager) {
        Room room;
        while (true) {
            try {
                hotelManager.ShowRooms();
                var roomNumber = GetNumberFromUser("Please choose room by entering the room number: ");
                room = hotelManager.ChooseRoom(roomNumber);
                break;
            }
            catch (Exception e) {
                HandleException(e.Message);
                Console.WriteLine("Press any key to reselect a room...");
                Console.ReadKey(intercept: true);
                Console.Clear();
            }
        }
        return room;
    }
    
    public static void RequestReservation(HotelManager hotelManager, Room room, Guest guest) {
        while (true) {
            try {
                var checkInDate = GetDateFromUser("Please enter check-in date");
                var checkOutDate = GetDateFromUser("Please enter check-out date");
                var reservation = hotelManager.ReserveRoom(checkInDate, checkOutDate, room, guest);
                Console.WriteLine($"Reservation ID: {reservation.ReservationId} | TotalCost: {reservation.ReservationCost()}");
                break;
            }
            catch (Exception e) {
                HandleException(e.Message);
                Console.WriteLine("Press any key to reselect Date...");
                Console.ReadKey(intercept: true);
                Console.Clear();
            }
        }
    }

    public static void ChangeStatus(ReservationStatus status, HotelManager hotelManager) {
        var reservationId =  GetNumberFromUser("Please enter reservation ID: ");
        try { hotelManager.ChangeStatus(reservationId, status); }
        catch (Exception e) {
            HandleException(e.Message);
            Console.WriteLine("Press any key to go back to menu...");
            Console.ReadKey(intercept: true);
        }
    }

    private static void ShowGuestDetailsFlow(HotelManager hotelManager) {
        var guestId = GetNumberFromUser("Please enter guest ID: ");
        try {
            hotelManager.ShowGuestDetails(guestId); 
        }
        catch (Exception e) {
            HandleException(e.Message);
            Console.WriteLine("Press any key to go back to menu...");
            Console.ReadKey(intercept: true);
        }
    }
    private static void AddGuestFlow(HotelManager hotelManager) {
        Console.Write("Please enter guest name: ");
        var guestName = Console.ReadLine();
        Console.Write("Please enter guest Phone number: ");
        var guestPhone = Console.ReadLine();
        try {
            var id = hotelManager.AddGuest(guestName!, guestPhone!);
            Console.WriteLine($"New Guest ID: {id}");
        }
        catch (Exception e) {
            HandleException(e.Message);
            Console.WriteLine("Press any key to go back to menu...");
            Console.ReadKey(intercept: true);
        }
    }
    private static void DoProcess(MenuChoice choice, HotelManager hotelManager) {
        Console.Clear();
        switch (choice) {
            case MenuChoice.BookRoom: {
                var guest = SelectGuestFlow(hotelManager);
                Console.WriteLine();
                var room = SelectRoomFlow(hotelManager);
                Console.WriteLine();
                RequestReservation(hotelManager, room, guest);
                break;
            }
            case MenuChoice.Confirm: {
                ChangeStatus(ReservationStatus.Confirmed, hotelManager);
                break;
            }
            case MenuChoice.CheckIn:
                ChangeStatus(ReservationStatus.CheckedIn, hotelManager);
                break;
            case MenuChoice.CheckOut:
                ChangeStatus(ReservationStatus.CheckedOut, hotelManager);
                break;
            case MenuChoice.CancelReservation:
                ChangeStatus(ReservationStatus.Cancelled, hotelManager);
                break;
            case MenuChoice.AddGuest: {
                AddGuestFlow(hotelManager);
                break;
            }
            case MenuChoice.ShowGuestHistory:
                ShowGuestDetailsFlow(hotelManager);
                break;
            case MenuChoice.ShowRooms:
                hotelManager.ShowRooms();
                break;
            case MenuChoice.ChangeRoomRate: {
                try {
                    var roomNumber = GetNumberFromUser("Enter room number: ");
                    decimal roomRate = GetDecimalFromUser("Enter the new room rate: ");
                    hotelManager.ChangeRoomRate(roomNumber, roomRate);
                }
                catch(Exception e) { HandleException(e.Message); }
                break;
            }
            case MenuChoice.LookRoom: {
                try {
                    var roomNumber = GetNumberFromUser("Enter room number: ");
                    hotelManager.MaintainRoom(roomNumber);
                }
                catch(Exception e) { HandleException(e.Message); }
                break;
            }
            case MenuChoice.EndMaintenance: {
                try {
                    var roomNumber = GetNumberFromUser("Enter room number: ");
                    hotelManager.EndMaintenance(roomNumber);
                }
                catch(Exception e) { HandleException(e.Message); }
                break;
            }
            case MenuChoice.Exit:
                Console.WriteLine("Bye.");
                break;
        }
        Console.Write("Press any key to go back to menu...");
        Console.ReadKey(intercept: true);
    }

    public static void RunInteractiveMenu(HotelManager hotelManager) {
        var choiceNumber = -1;
        while (choiceNumber != 0) {
            Console.Clear();
            PrintMenu();
            choiceNumber = GetNumberFromUser("Enter choice: ");
            MenuChoice choiceEnum;
            if (Enum.IsDefined(typeof(MenuChoice), choiceNumber)) 
                choiceEnum = (MenuChoice)choiceNumber;
            else {
                Console.WriteLine("Invalid choice");
                continue;
            }
            DoProcess(choiceEnum, hotelManager);
        }
    }
}