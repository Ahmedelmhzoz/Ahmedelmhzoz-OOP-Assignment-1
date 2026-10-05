namespace Part2_HotelReservationSystem;
public class Reservation {
    private static int _nextId = 1;
    public int ReservationId { get; }
    public DateTime CheckInDate { get; }
    public DateTime CheckOutDate { get; }
    public Room ReservedRoom { get; }
    public decimal ReservationCost() {
        var diff = CheckOutDate - CheckInDate;
        int days = diff.Days + 1;
        return days * ReservedRoom.NightlyRate;
    }
    public ReservationStatus Status { get; private set; }
    public Reservation(DateTime checkInDate, DateTime checkOutDate, Room reservedRoom) {
        if (checkInDate > checkOutDate)
            throw new Exception("Error, Cannot assign the Check-out date before the Check-in date");
        ReservationId = _nextId++;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        Status = ReservationStatus.Pending;
        ReservedRoom = reservedRoom;
    }
    public void CheckInReservation() {
        if (Status == ReservationStatus.CheckedIn) 
            throw new Exception($"Reservation with ID # {ReservationId} is already checked-in.");
        if (Status == ReservationStatus.CheckedOut) 
            throw new Exception($"Reservation with ID # {ReservationId} is already Checked-out.");
        if (Status == ReservationStatus.Cancelled)
            throw new Exception($"Reservation with ID # {ReservationId} is already Cancelled.");
        if (Status == ReservationStatus.Pending)
            throw new Exception($"Reservation with ID # {ReservationId} is pending, confirm the request first.");
        
        Status = ReservationStatus.CheckedIn;
    }
    public void CheckOutReservation() {
        if (Status == ReservationStatus.Confirmed) 
            throw new Exception($"Reservation with ID # {ReservationId} has not been checked-in yet.");
        if (Status == ReservationStatus.CheckedOut) 
            throw new Exception($"Reservation with ID # {ReservationId} is already Checked-out.");
        if (Status == ReservationStatus.Cancelled)
            throw new Exception($"Reservation with ID # {ReservationId} is already Cancelled.");
        if (Status == ReservationStatus.Pending)
            throw new Exception($"Reservation with ID # {ReservationId} is pending, confirm the request first.");
        
        Status = ReservationStatus.CheckedOut;
    }
    public void CancelReservation() {
        if (Status == ReservationStatus.CheckedIn) 
            throw new Exception($"Reservation with ID # {ReservationId} is already checked-in.");
        if (Status == ReservationStatus.CheckedOut) 
            throw new Exception($"Reservation with ID # {ReservationId} is already Checked-out.");
        if (Status == ReservationStatus.Cancelled)
            throw new Exception($"Reservation with ID # {ReservationId} is already Cancelled.");
        Status = ReservationStatus.Cancelled;
    }
    public void ConfirmReservation() {
        if (Status == ReservationStatus.CheckedIn) 
            throw new Exception($"Reservation with ID # {ReservationId} is already checked-in.");
        if (Status == ReservationStatus.Confirmed) 
            throw new Exception($"Reservation with ID # {ReservationId} is already Confirmed.");
        if (Status == ReservationStatus.CheckedOut) 
            throw new Exception($"Reservation with ID # {ReservationId} is already Checked-out.");
        if (Status == ReservationStatus.Cancelled)
            throw new Exception($"Reservation with ID # {ReservationId} is already Cancelled.");
        
        Status = ReservationStatus.Confirmed;
    }
}
