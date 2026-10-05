namespace Part2_HotelReservationSystem;

public class Room{
    public enum RoomType {
        Single,
        Double,
        Suite
    }
    public int RoomNumber { get;}
    public RoomType Type {get;}
    public decimal NightlyRate {get; private set;}
    public bool InMaintenance {get; private set;}
    
    public Room(int roomNumber, RoomType type, decimal nightlyRate, bool inMaintenance) {
        RoomNumber = roomNumber;
        Type = type;
        NightlyRate = nightlyRate;
        InMaintenance = inMaintenance;
    }
    public void ChangeNightlyRate(decimal newRate) {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(newRate);
        NightlyRate =  newRate;
    }
    public void Maintenance() {
        InMaintenance = true;
    }
    public void EndMaintenance() {
        InMaintenance = false;
    }
}