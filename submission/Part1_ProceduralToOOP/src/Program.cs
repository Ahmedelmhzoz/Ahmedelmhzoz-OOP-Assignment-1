namespace Part1_ProceduralToOOP;
public class Program {
    static MarketManager SeedSampleData() {
        MarketManager manager = new MarketManager();
        manager.AddCustomer(new Customer(1, "John Doe", "john@gmai.com", "Cairo", false));
        manager.AddCustomer(new Customer(2, "Mona Ali", "mona@gmai.com", "Alexandria", true));
        manager.AddCustomer(new Customer(3, "Sara Nabil", "Sara@gmai.com", "Giza", true));

        manager.AddProduct(new Product(101, "USB Cable", 50.0m, 100));
        manager.AddProduct(new Product(102, "Wireless Mouse", 250.0m, 40));
        manager.AddProduct(new Product(103, "Mechanical Keyboard", 1200.0m, 15));
        manager.AddProduct(new Product(104, "Laptop Stand", 400.0m, 25));
        return manager;
    }
    static void RunDemoScenario(MarketManager manager) {
        manager.CreateOrder(1001, 1, new DateTime(2026, 9, 15));
        manager.AddLineToOrder(1001, 101, 2);
        manager.AddLineToOrder(1001, 102, 1);
        manager.MarkOrderPaid(1001);
        // Order 1002 (Unpaid)
        manager.CreateOrder(1002, 2, new DateTime(2026, 9, 15));
        manager.AddLineToOrder(1002, 103, 1);
        manager.AddLineToOrder(1002, 104, 1);
        // Order 1003
        manager.CreateOrder(1003, 3, new DateTime(2026, 9, 16));
        manager.AddLineToOrder(1003, 101, 5);
        manager.MarkOrderPaid(1003);
    }
    static void PrintMenu() {
        Console.WriteLine("\n---------- MENU ----------");
        Console.WriteLine("1- Print customers");
        Console.WriteLine("2- Print products");
        Console.WriteLine("3- Print all orders");
        Console.WriteLine("4- Print one order by id");
        Console.WriteLine("5- Create order");
        Console.WriteLine("6- Add line to order");
        Console.WriteLine("7 Mark order paid");
        Console.WriteLine("8- Show paid sales total");
        Console.WriteLine("0- Exit");
    }

    static void HandleResult(Result result) {
        if (!result.IsSuccess) {
            Console.WriteLine($"\n{result.ErrorMessage}");
        }
    }
    public static int GetNumberFromUser(string message) {
        int number;
        Console.Write($"\n{message}");
        while (!int.TryParse(Console.ReadLine(), out number))
            Console.Write($"\n{message}");
        return number;
    }
    public static void DoProcess(MenuChoice choice, MarketManager manager) {
        switch (choice) {
            case MenuChoice.PrintCustomers: manager.PrintCustomers(); break;
            case MenuChoice.PrintProducts: manager.PrintProducts(); break;
            case MenuChoice.PrintAllOrders: manager.PrintAllOrders(); break;
            case MenuChoice.PrintOrderById: {
                int orderId = GetNumberFromUser("Enter order ID: ");
                var result = manager.PrintOrderByID(orderId);
                HandleResult(result);
                break;
            }
            case MenuChoice.CreateOrder: {
                int orderId = GetNumberFromUser("Enter order ID: ");
                int customerId = GetNumberFromUser("Enter customer ID: ");
                var result = manager.CreateOrder(orderId, customerId, DateTime.Now);
                HandleResult(result);  
                break;
            }
            case MenuChoice.AddLineToOrder: {
                int orderId = GetNumberFromUser("Enter order ID: ");
                int productId = GetNumberFromUser("Enter product ID: ");
                int quantity = GetNumberFromUser("Enter product Quantity: ");
                var result = manager.AddLineToOrder(orderId, productId, quantity);
                HandleResult(result);  
                break;
            }
            case MenuChoice.MarkOrderPaid: {
                int orderId = GetNumberFromUser("Enter order ID: ");
                var result = manager.MarkOrderPaid(orderId);
                HandleResult(result);  
                break;
            }
            case MenuChoice.ShowPaidSalesTotal: {
                Console.WriteLine($"\nPaid sales total: {manager.TotalSalesPaidOnly()}");
                break;
            }
            case MenuChoice.Exit: Console.WriteLine("Bye."); break;
        }
    }
    public static void RunInteractiveMenu(MarketManager manager) {
        int choicNumber = -1;
        while (choicNumber != 0) {
            PrintMenu();
            choicNumber = GetNumberFromUser("Enter choice: ");
            MenuChoice choiceEnum;
            if (Enum.IsDefined(typeof(MenuChoice), choicNumber)) 
                choiceEnum = (MenuChoice)choicNumber;
            else {
                Console.WriteLine("Invalid choice");
                continue;
            }
            DoProcess(choiceEnum, manager);
        }
    }
    public static void Main(string[] args) { 
        var manager = SeedSampleData();
        RunDemoScenario(manager);
        RunInteractiveMenu(manager);
    }
}