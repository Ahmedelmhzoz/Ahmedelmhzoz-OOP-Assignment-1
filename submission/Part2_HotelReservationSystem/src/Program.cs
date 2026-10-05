using System.Globalization;

namespace Part2_HotelReservationSystem;

public class Program {
    
    public static void Main(string[] args) {
        var hotelManager = new HotelManager();
        HotelConsoleUi.SeedData(hotelManager);
        HotelConsoleUi.RunInteractiveMenu(hotelManager);
    }
}