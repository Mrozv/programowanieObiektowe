using System;
using System.Collections.Generic;

public class Product
{
    public string Name { get; set; }
    private decimal netPrice;
    public decimal NetPrice
    {
        get => netPrice;
        set
        {
            if (value < 0)
                throw new ArgumentException("Cena netto nie może być ujemna.");
            netPrice = value;
        }
    }
    public string VATCategory { get; set; }
    public decimal GrossPrice => NetPrice * (1 + VATRates[VATCategory]);
    public string CountryOfOrigin { get; set; }

    private static readonly Dictionary<string, decimal> VATRates = new Dictionary<string, decimal>
    {
        { "Spożywcze", 0.08m },
        { "Napoje", 0.23m },
        { "Zdrowie", 0.05m },
        { "Inne", 0.18m }
    };
    public static decimal GetVATRate(string category)
    {
        if (VATRates.ContainsKey(category))
            return VATRates[category];
        else
            throw new ArgumentException("Nieprawidłowa kategoria VAT.");
    }
}

public abstract class FoodProduct : Product
{
    public FoodProduct()
    {
        VATCategory = "Spożywcze";
    }
    public decimal Calories { get; set; }
    public HashSet<string> Allergens { get; set; }
}

public class WeightedFoodProduct : FoodProduct
{
    public decimal Weight { get; set; }
}

public class PackagedFoodProduct : FoodProduct
{
    public decimal Weight { get; set; }
}

public class BeverageProduct : PackagedFoodProduct
{
    public decimal Volume { get; set; }
}

public class Multipack : Product
{
    public Product Product { get; set; }
    public ushort Quantity { get; set; }
    public decimal NetPrice { get; set; }
    public decimal GrossPrice => Product.NetPrice * (1 + Product.GetVATRate(Product.VATCategory));
    public string VATCategory => Product.VATCategory;
    public string CountryOfOrigin => Product.CountryOfOrigin;
}

class Program
{
    static void Main(string[] args)
    {
        WeightedFoodProduct product1 = new WeightedFoodProduct
        {
            Name = "Jogurt naturalny",
            NetPrice = 4.99m,
            VATCategory = "Spożywcze",
            CountryOfOrigin = "Polska",
            Calories = 60,
            Weight = 0.5m
        };

        Multipack multipack = new Multipack
        {
            Product = product1,
            Quantity = 3,
            NetPrice = product1.NetPrice * 3
        };

        Console.WriteLine($"Nazwa: {multipack.Product.Name}");
        Console.WriteLine($"Ilość: {multipack.Quantity}");
        Console.WriteLine($"Cena netto: {multipack.NetPrice}");
        Console.WriteLine($"Cena brutto: {multipack.GrossPrice}");
    }
}
