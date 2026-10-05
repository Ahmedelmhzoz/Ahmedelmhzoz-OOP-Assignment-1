public class Address{
    public string Street { get; }
    public string City { get; }
    public string? State { get; }
    public string? ZipCode { get; }
    public string? Country { get; }
    internal Address(string street, string city, string? state, string? zipCode, string? country) {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
    }
}

public class AddressBuilder{
    readonly string _street;
    readonly string _city;
    string? _state = string.Empty;
    string? _zipCode = string.Empty;
    string? _country = string.Empty;
    
    public AddressBuilder(string street, string city) {
        ArgumentException.ThrowIfNullOrWhiteSpace(street);
        ArgumentException.ThrowIfNullOrWhiteSpace(city);
        _street = street;
        _city = city;
    }

    public AddressBuilder WithState(string state) {
        ArgumentException.ThrowIfNullOrWhiteSpace(state);
        _state = state;
        return this;
    }
    
    public AddressBuilder WithZipCode(string zipCode) {
        ArgumentException.ThrowIfNullOrWhiteSpace(zipCode);
        _zipCode = zipCode;
        return this;
    }

    public AddressBuilder WithCountry(string country) {
        ArgumentException.ThrowIfNullOrWhiteSpace(country);
        _country = country;
        return this;
    }
    
    public Address Build() => new Address(_street, _city, _state, _zipCode, _country);
}

public class Payment{
    public decimal TotalAmount { get;}
    public string Currency { get;}
    public string? PaymentMethod { get; }
    public decimal SubTotal { get; }
    public decimal DiscountAmount { get; }
    public decimal TaxAmount { get; }
    
    public Payment(decimal totalAmount, string currency, string? paymentMethod, decimal subTotal, decimal discountAmount, decimal taxAmount) {
        TotalAmount = totalAmount;
        Currency = currency;
        PaymentMethod = paymentMethod;
        SubTotal = subTotal;
        DiscountAmount = discountAmount;
        TaxAmount = taxAmount;
    }
}

public class PaymentBuilder{
    private readonly decimal _totalAmount;
    private readonly string _currency;
    private string? _paymentMethod = null;
    private decimal _subTotal = 0m;
    private decimal _discountAmount = 0m;
    private decimal _taxAmount = 0m;

    public PaymentBuilder(decimal totalAmount, string currency) {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(totalAmount);
        ArgumentException.ThrowIfNullOrWhiteSpace(currency);
        _totalAmount = totalAmount;
        _currency = currency;
    }

    public PaymentBuilder WithSubTotal(decimal subTotal) {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(subTotal);
        _subTotal = subTotal;
        return this;
    }

    public PaymentBuilder WithDiscountAmount(decimal discountAmount) {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(discountAmount);
        _discountAmount = discountAmount;
        return this;
    }

    public PaymentBuilder WithPaymentMethod(string paymentMethod) {
        ArgumentException.ThrowIfNullOrWhiteSpace(paymentMethod);
        _paymentMethod = paymentMethod;
        return this;
    }

    public PaymentBuilder WithTaxAmount(decimal tax) {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(tax);
        _taxAmount = tax;
        return this;
    }

    public Payment Build() => new Payment(_totalAmount, _currency, _paymentMethod,
        _subTotal, _discountAmount, _taxAmount);
}

public class Invoice{
    public int InvoiceId { get; }
    public string CustomerName { get; }
    public string? CustomerEmail { get; }
    public string? CustomerPhone { get; }
    public DateTime OrderDate { get; }
    public Address? BillingAddress { get; }
    public Address? ShippingAddress { get; }
    public Payment Payment { get; }

    public Invoice(int invoiceId, string customerName, string? customerEmail, string? customerPhone, DateTime orderDate, Address? billingAddress, Address? shippingAddress, Payment payment) {
        InvoiceId = invoiceId;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CustomerPhone = customerPhone;
        OrderDate = orderDate;
        BillingAddress = billingAddress;
        ShippingAddress = shippingAddress;
        Payment = payment;
    }
}

public class InvoiceBuilder {
    // Mandatory
    private readonly int _invoiceId;
    private readonly string _customerName;  
    private readonly DateTime _orderDate;
    private readonly Payment _payment;
    
    // Optional
    private string? _customerEmail = null;
    private string? _customerPhone = null;

    Address? _billingAddress = null;
    Address? _shippingAddress = null;
    
    public InvoiceBuilder(int invoiceId, string customerName, DateTime orderDate, Payment payment) {
        ArgumentNullException.ThrowIfNull(payment);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerName);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(invoiceId);
        _invoiceId = invoiceId;
        _customerName = customerName;
        _orderDate = orderDate;
        _payment = payment;
    }
    
    public InvoiceBuilder WithContactInfo(string? email = null, string? phone = null) {
        _customerEmail =  email;
        _customerPhone  = phone;
        return this;
    }

    public InvoiceBuilder WithBillingAddress(Address billingAddress) {
        _billingAddress = billingAddress;
        return this;
    }
    
    public InvoiceBuilder WithShippingAddress(Address shippingAddress) {
        _shippingAddress = shippingAddress;
        return this;
    }
    
    public Invoice Build() 
        => new Invoice(_invoiceId, _customerName, _customerEmail, _customerPhone,
            _orderDate, _billingAddress, _shippingAddress, _payment);
}

public class Program{
    public static void Main(string[] args) {
        try {
            Address billingAddress = new AddressBuilder("Osman ebn affab", "15-may").WithCountry("Cairo").Build();
            Address shippingAddress = new AddressBuilder("Ahmed refaat", "ein-shams").WithCountry("Cairo").Build();
            Payment payment = new PaymentBuilder(1000.5m, "$").WithDiscountAmount(20.5m)
                .WithPaymentMethod("InstaPay").Build();
            Invoice invoice = new InvoiceBuilder(1, "Ahmed Osama", DateTime.Now, payment)
                .WithBillingAddress(billingAddress)
                .WithShippingAddress(shippingAddress)
                .WithContactInfo("ahmedalmhzoz@gmail.com")
                .Build();
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
    }
}