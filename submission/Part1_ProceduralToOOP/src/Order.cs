namespace Part1_ProceduralToOOP;

public class Order{
    private const int MaxLinesPerOrder = 20;
    public int OrderId{ get; }
    public Customer Customer { get;}
    public DateTime OrderDate { get;}
    public bool IsPaid { get;  set; }
    private readonly List<OrderLine> _orderLines = [];
    public int OrderLinesCount => _orderLines.Count;
    public bool OrderIsFull => OrderLinesCount == MaxLinesPerOrder;
    public decimal OrderCost {
        get {
            decimal cost = 0.0m;
            foreach (var line in _orderLines) {
                cost += line.Product.ProductPrice * line.ProductQuantity;
            }
            if (Customer.IsVip) cost *= 0.90m;
            return cost;
        }
    }
    public Order(int orderId, Customer customer, DateTime orderDate) {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(orderId);
        ArgumentNullException.ThrowIfNull(customer);
        OrderId = orderId;
        Customer = customer;
        OrderDate = orderDate;
        IsPaid = false;
    }   
    public void AddLine(Product product, int quantity) {
        _orderLines.Add(new OrderLine(product, quantity));
    }
    public void PrintOrder() {
        Console.WriteLine($"\n=== ORDER #{OrderId} ===");
        Console.WriteLine($"Date: {OrderDate:yyyy-MM-dd}");
        Console.WriteLine($"Customer: {Customer.CustomerName} (#{Customer.CustomerId})");
        Console.WriteLine($"Paid: {(IsPaid ? "yes" : "no")}");
        Console.WriteLine("Lines:");
        foreach (var line in _orderLines) {
            decimal lineTotal = line.Product.ProductPrice * line.ProductQuantity;
            Console.WriteLine($"  - {line.Product.ProductName,-20} x{line.ProductQuantity,-4}" +
                              $" @{line.Product.ProductPrice,-8:F2} = {lineTotal:F2}");
        }
        Console.WriteLine($"TOTAL: {OrderCost:F2}");
    }
}