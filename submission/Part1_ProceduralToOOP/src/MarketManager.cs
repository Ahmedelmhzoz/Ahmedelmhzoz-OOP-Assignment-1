namespace Part1_ProceduralToOOP;

public record Result(bool IsSuccess, string? ErrorMessage = null){
    public static Result Success() => new (true);
    public static Result Failure(string message) => new (false, message);
}
public class MarketManager
{
    private const int MaxProducts = 50;
    private const int MaxCustomers = 50;
    private const int MaxOrders = 100;

    private readonly List<Customer> _customers = [];
    private readonly List<Product> _products = [];
    private readonly List<Order> _orders = [];

    private Customer? _FindCustomer(int id) => _customers.FirstOrDefault(c => c.CustomerId == id);
    private Product? _FindProduct(int id) => _products.FirstOrDefault(p => p.ProductId == id);
    private Order? _FindOrder(int id) => _orders.FirstOrDefault(o => o.OrderId == id);

    public Result AddCustomer(Customer customer) {
        if (_customers.Count >= MaxCustomers)
            return Result.Failure("ERROR: customer list is full.");

        if (_FindCustomer(customer.CustomerId) != null)
            return Result.Failure($"ERROR: customer id {customer.CustomerId} already exists.");

        _customers.Add(customer);
        return Result.Success();
    }

    public void PrintCustomers() {
        Console.WriteLine($"\n=== CUSTOMERS ({_customers.Count}) ===");
        foreach (var customer in _customers) {
            customer.PrintCustomer();
        }
    }

    public Result AddProduct(Product product) {
        if (_products.Count >= MaxProducts)
            return Result.Failure("ERROR: product list is full.");

        if (_FindProduct(product.ProductId) != null)
            return Result.Failure($"ERROR: product id {product.ProductId} already exists.");

        _products.Add(product);
        return Result.Success();
    }

    public void PrintProducts() {
        Console.WriteLine($"\n=== PRODUCTS ({_products.Count}) ===");
        foreach (var product in _products) {    
            product.PrintProduct();
        }
    }

    public Result CreateOrder(int orderId, int customerId, DateTime orderDate) {
        if (_orders.Count >= MaxOrders)
            return Result.Failure("ERROR: order list is full.");

        if (orderId < 0)
            return Result.Failure("ERROR: Invalid Negative order id");
        
        if (_FindOrder(orderId) != null)
            return Result.Failure($"ERROR: order id {orderId} already exists.");

        Customer? customer = _FindCustomer(customerId);
        if (customer == null)
            return Result.Failure($"ERROR: customer id {customerId} not found.");

        _orders.Add(new Order(orderId, customer, orderDate));
        return Result.Success();
    }

    public Result AddLineToOrder(int orderId, int productId, int quantity) {
        var order = _FindOrder(orderId);
        if (order == null)
            return Result.Failure($"ERROR: order id {orderId} not found.");

        if (order.IsPaid)
            return Result.Failure("ERROR: cannot change a paid order.");

        if (order.OrderIsFull)
            return Result.Failure("ERROR: order has too many lines.");

        var product = _FindProduct(productId);
        if (product == null)
            return Result.Failure($"ERROR: product id {productId} not found.");

        if (quantity <= 0)
            return Result.Failure("ERROR: quantity must be positive.");

        if (product.ProductStock < quantity)
            return Result.Failure($"ERROR: not enough stock for product #{productId}.");

        product.ProductStock -= quantity;
        order.AddLine(product, quantity);

        return Result.Success();
    }

    public Result MarkOrderPaid(int orderId) {
        var order = _FindOrder(orderId);
        if (order == null)
            return Result.Failure($"ERROR: order id {orderId} not found.");

        if (order.IsPaid)
            return Result.Failure("ERROR: order is already paid.");

        if (order.OrderLinesCount == 0)
            return Result.Failure("ERROR: cannot pay an empty order.");

        order.IsPaid = true;
        return Result.Success();
    }

    public decimal TotalSalesPaidOnly() {
        decimal cost = 0.0m;
        foreach (var order in _orders) {
            if (order.IsPaid)
                cost += order.OrderCost;
        }
        return cost;
    }

    public void PrintAllOrders() {
        Console.WriteLine($"\n=== ALL ORDERS ({_orders.Count}) ===\n");
        foreach (var order in _orders) {
            order.PrintOrder();
        }
    }

    public Result PrintOrderByID(int id) {
        if (id < 0) return Result.Failure("ERROR: Invalid negative Order ID");
        var order = _FindOrder(id);
        if (order == null) return Result.Failure("ERROR: order not found.");

        order.PrintOrder();
        return Result.Success();
    }
}