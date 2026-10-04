namespace Part1_ProceduralToOOP;

public class Product {
    public int ProductId { get;  }
    public string ProductName { get;  }
    public decimal ProductPrice { get;  }
    public int ProductStock { get; set; }
    
    public Product(int productId, string productName, decimal productPrice, int productStock) {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(productId);
        ArgumentOutOfRangeException.ThrowIfNegative(productStock);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(productPrice);
        ArgumentException.ThrowIfNullOrEmpty(productName);
        ProductId = productId;
        ProductName = productName;
        ProductPrice = productPrice;
        ProductStock = productStock;
    }
    public void PrintProduct() {
        Console.WriteLine($"#{ProductId,-4} {ProductName,-20} price={ProductPrice,-8:F2}" +
                          $" stock={ProductStock}");
    }
}
