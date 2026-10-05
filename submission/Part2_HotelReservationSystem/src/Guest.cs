namespace Part2_HotelReservationSystem;

public class Guest{
    private static int _nextId = 1;
    public int GuestId {get;}
    public string GuestName { get; } 
    public string GuestPhone { get; } 
    private readonly List<Reservation> _reservations = [];
    public IReadOnlyList<Reservation> Reservations => _reservations;

    public Guest(string guestName, string guestPhone) {
        ArgumentException.ThrowIfNullOrEmpty(guestName);
        ArgumentException.ThrowIfNullOrEmpty(guestPhone);
        GuestId = _nextId++;
        GuestName = guestName;
        GuestPhone = guestPhone;
    }
    public void ReseveRoom(Reservation reservation){
        ArgumentNullException.ThrowIfNull(reservation);
        _reservations.Add(reservation);
    }
}