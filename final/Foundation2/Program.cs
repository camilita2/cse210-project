using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem
{
    // A street address that knows whether it is in the USA.
    public class Address
    {
        private string _street;
        private string _city;
        private string _stateProvince;
        private string _country;

        public Address(string street, string city, string stateProvince, string country)
        {
            _street = street;
            _city = city;
            _stateProvince = stateProvince;
            _country = country;
        }

        public string GetStreet() { return _street; }
        public void SetStreet(string street) { _street = street; }

        public string GetCity() { return _city; }
        public void SetCity(string city) { _city = city; }

        public string GetStateProvince() { return _stateProvince; }
        public void SetStateProvince(string stateProvince) { _stateProvince = stateProvince; }

        public string GetCountry() { return _country; }
        public void SetCountry(string country) { _country = country; }

        public bool IsInUSA()
        {
            return string.Equals(_country.Trim(), "USA", StringComparison.OrdinalIgnoreCase);
        }

        public string GetFullAddress()
        {
            return $"{_street}\n{_city}, {_stateProvince}\n{_country}";
        }
    }

    // A customer with a name and an address.
    public class Customer
    {
        private string _name;
        private Address _address;

        public Customer(string name, Address address)
        {
            _name = name;
            _address = address;
        }

        public string GetName() { return _name; }
        public void SetName(string name) { _name = name; }

        public Address GetAddress() { return _address; }
        public void SetAddress(Address address) { _address = address; }

        public bool LivesInUSA()
        {
            return _address.IsInUSA();
        }
    }

    // A product with a name, id, price per unit, and quantity.
    public class Product
    {
        private string _name;
        private string _productId;
        private decimal _pricePerUnit;
        private int _quantity;

        public Product(string name, string productId, decimal pricePerUnit, int quantity)
        {
            _name = name;
            _productId = productId;
            _pricePerUnit = pricePerUnit;
            _quantity = quantity;
        }

        public string GetName() { return _name; }
        public void SetName(string name) { _name = name; }

        public string GetProductId() { return _productId; }
        public void SetProductId(string productId) { _productId = productId; }

        public decimal GetPricePerUnit() { return _pricePerUnit; }
        public void SetPricePerUnit(decimal pricePerUnit) { _pricePerUnit = pricePerUnit; }

        public int GetQuantity() { return _quantity; }
        public void SetQuantity(int quantity) { _quantity = quantity; }

        public decimal GetTotalCost()
        {
            return _pricePerUnit * _quantity;
        }
    }

    // An order containing products and a customer.
    public class Order
    {
        private const decimal USA_SHIPPING = 5m;
        private const decimal INTERNATIONAL_SHIPPING = 35m;

        private List<Product> _products;
        private Customer _customer;

        public Order(Customer customer)
        {
            _customer = customer;
            _products = new List<Product>();
        }

        public Customer GetCustomer() { return _customer; }
        public void SetCustomer(Customer customer) { _customer = customer; }

        public List<Product> GetProducts() { return _products; }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public decimal GetShippingCost()
        {
            return _customer.LivesInUSA() ? USA_SHIPPING : INTERNATIONAL_SHIPPING;
        }

        public decimal GetTotalPrice()
        {
            decimal total = 0m;
            foreach (Product product in _products)
            {
                total += product.GetTotalCost();
            }
            return total + GetShippingCost();
        }

        public string GetPackingLabel()
        {
            StringBuilder label = new StringBuilder();
            foreach (Product product in _products)
            {
                label.AppendLine($"{product.GetName()} (ID: {product.GetProductId()})");
            }
            return label.ToString();
        }

        public string GetShippingLabel()
        {
            return $"{_customer.GetName()}\n{_customer.GetAddress().GetFullAddress()}\n";
        }
    }

    public class Program
    {
        static void DisplayOrder(int number, Order order)
        {
            Console.WriteLine($"===== Order {number} =====");
            Console.WriteLine("Packing Label:");
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine("Shipping Label:");
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine($"Total Price: ${order.GetTotalPrice():F2}");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            // Order 1: customer in the USA
            Address address1 = new Address("123 Maple St", "Salt Lake City", "UT", "USA");
            Customer customer1 = new Customer("Alice Johnson", address1);
            Order order1 = new Order(customer1);
            order1.AddProduct(new Product("Wireless Mouse", "P1001", 24.99m, 2));
            order1.AddProduct(new Product("USB-C Cable", "P1002", 9.50m, 3));
            order1.AddProduct(new Product("Laptop Stand", "P1003", 35.00m, 1));

            // Order 2: customer outside the USA
            Address address2 = new Address("45 Rua Augusta", "Sao Paulo", "SP", "Brazil");
            Customer customer2 = new Customer("Carlos Mendes", address2);
            Order order2 = new Order(customer2);
            order2.AddProduct(new Product("Mechanical Keyboard", "P2001", 89.99m, 1));
            order2.AddProduct(new Product("Webcam", "P2002", 49.95m, 2));

            DisplayOrder(1, order1);
            DisplayOrder(2, order2);
        }
    }
}