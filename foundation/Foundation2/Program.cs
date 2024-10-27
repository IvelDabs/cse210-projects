using System;
using System.Collections.Generic;

public class Address
{
    private string streetAddress;
    private string city;
    private string stateProvince;
    private string country;

    public Address(string streetAddress, string city, string stateProvince, string country)
    {
        StreetAddress = streetAddress;
        City = city;
        StateProvince = stateProvince;
        Country = country;
    }

    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string StateProvince { get; set; }
    public string Country { get; set; }

    public bool IsUSA()
    {
        return Country.ToLower() == "usa";
    }

    public string GetFullAddress()
    {
        return $"{StreetAddress}\n{City}, {StateProvince}\n{Country}";
    }
}

public class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        Name = name;
        Address = address;
    }

    public string Name { get; set; }
    public Address Address { get; set; }

    public bool LivesInUSA()
    {
        return Address.IsUSA();
    }

    public string GetShippingAddress()
    {
        return Address.GetFullAddress();
    }
}

public class Product
{
    private string name;
    private string productId;
    private decimal price;
    private int quantity;

    public Product(string name, string productId, decimal price, int quantity)
    {
        Name = name;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }

    public string Name { get; set; }
    public string ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public decimal GetTotalCost()
    {
        return Price * Quantity;
    }
}

public class Order
{
    private List<Product> products;
    private Customer customer;
    private const decimal USA_SHIPPING_COST = 5.00m;
    private const decimal INTERNATIONAL_SHIPPING_COST = 35.00m;

    public Order(Customer customer)
    {
        Customer = customer;
        Products = new List<Product>();
    }

    public Customer Customer { get; set; }
    public List<Product> Products { get; set; }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public decimal GetTotalCost()
    {
        decimal total = 0;
        foreach (var product in Products)
        {
            total += product.GetTotalCost();
        }

        return Customer.LivesInUSA() ? total + USA_SHIPPING_COST : total + INTERNATIONAL_SHIPPING_COST;
    }

    public string GetPackingLabel()
    {
        string label = "";
        foreach (var product in Products)
        {
            label += $"{product.Name} ({product.ProductId})\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return Customer.GetShippingAddress();
    }
}

class Program
{
    static void Main()
    {
        // Create customers
        Address address1 = new Address("123 Main St", "New York", "NY", "USA");
        Customer customer1 = new Customer("John Doe", address1);

        Address address2 = new Address("456 Elm St", "London", "", "UK");
        Customer customer2 = new Customer("Jane Smith", address2);

        // Create orders
        Order order1 = new Order(customer1);
        Order order2 = new Order(customer2);

        // Add products to orders
        order1.AddProduct(new Product("Product A", "PA001", 10.99m, 2));
        order1.AddProduct(new Product("Product B", "PB002", 5.99m, 3));

        order2.AddProduct(new Product("Product C", "PC003", 7.99m, 1));
        order2.AddProduct(new Product("Product D", "PD004", 12.99m, 2));

        // Display order information
        Console.WriteLine("Order 1:");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order1.GetTotalCost():F2}");

        Console.WriteLine("\nOrder 2:");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order2.GetTotalCost():F2}");
    }
}
