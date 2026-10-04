namespace Part1_ProceduralToOOP;

public class Customer{
    public int CustomerId { get; }
    public string CustomerName { get; }
    public string CustomerEmail { get; }
    public string CustomerCity { get; }
    public bool IsVip { get; private set; }
    
    public Customer(int id, string fullName, string email, string city, bool isVip)  {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(fullName);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);

        CustomerId = id;
        CustomerName = fullName;
        CustomerEmail = email;
        CustomerCity = city;
        IsVip = isVip;
    }

    public void PrintCustomer() {
        string vipStatus = IsVip ? "yes" : "no";
        Console.WriteLine($"#{CustomerId,-4} {CustomerName,-15}" +
                          $" <{CustomerEmail,-22}> {CustomerCity,-12} vip={vipStatus}");
    }
}
