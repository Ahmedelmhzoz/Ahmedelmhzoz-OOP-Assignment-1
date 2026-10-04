namespace Part1_ProceduralToOOP;

public class OrderLine{
    public Product Product { get; }
    public int ProductQuantity { get; }
    
    public OrderLine(Product product, int productQuantity) {
        Product = product;
        ProductQuantity = productQuantity;
    }
}
