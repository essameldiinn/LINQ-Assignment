using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQAssignment
{
    class Program
    {
        #region ClassProduct
        public class Product
        {
            public string Name { get; set; }
            public int UnitsInStock { get; set; }
            public decimal UnitPrice { get; set; }
            public string Category { get; set; }
        }
        #endregion

        #region ClassCustomer
        public class Customer
        {
            public string Name { get; set; }
            public string Region { get; set; }
            public List<Order> Orders { get; set; }
        }
        #endregion

        #region ClassOrder
        public class Order
        {
            public decimal Total { get; set; }
            public DateTime OrderDate { get; set; }
        } 
        #endregion

        static void Main(string[] args)
        {
            #region Data
            var products = new List<Product>
            {
                new Product { Name = "Laptop", UnitsInStock = 0, UnitPrice = 999.99m, Category = "Electronics" },
                new Product { Name = "Mouse", UnitsInStock = 50, UnitPrice = 25.00m, Category = "Electronics" },
                new Product { Name = "Apple", UnitsInStock = 100, UnitPrice = 2.50m, Category = "Food" },
                new Product { Name = "Bread", UnitsInStock = 0, UnitPrice = 3.50m, Category = "Food" },
                new Product { Name = "TV", UnitsInStock = 10, UnitPrice = 1500.00m, Category = "Electronics" }
            };

            var customers = new List<Customer>
            {
                new Customer
                {
                    Name = "John Doe", Region = "Washington",
                    Orders = new List<Order>
                    {
                        new Order { Total = 300.00m, OrderDate = new DateTime(1997, 1, 1) },
                        new Order { Total = 600.00m, OrderDate = new DateTime(1998, 2, 1) }
                    }
                },
                new Customer
                {
                    Name = "Jane Smith", Region = "Washington",
                    Orders = new List<Order>
                    {
                        new Order { Total = 450.00m, OrderDate = new DateTime(1999, 3, 1) },
                        new Order { Total = 700.00m, OrderDate = new DateTime(2000, 4, 1) }
                    }
                }
            };

            var dictionary = new string[] { "hello", "world", "weird", "code", "programming" }; // Simulate dictionary_english.txt

            #endregion

            #region LINQ - Restriction Operators
            // LINQ Queries
            Console.WriteLine("=== LINQ - Restriction Operators ===");
            // 1. Find all products that are out of stock
            var outOfStock = from p in products where p.UnitsInStock == 0 select p;
            Console.WriteLine("1. Out of stock products:");
            foreach (var p in outOfStock) Console.WriteLine($"  {p.Name}");

            // 2. Find all products that are in stock and cost more than 3.00 per unit
            var inStockExpensive = from p in products where p.UnitsInStock > 0 && p.UnitPrice > 3.00m select p;
            Console.WriteLine("\n2. In stock and > $3.00:");
            foreach (var p in inStockExpensive) Console.WriteLine($"  {p.Name}: ${p.UnitPrice}");

            // 3. Digits whose name is shorter than their value
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var shortNames = from i in Enumerable.Range(0, digits.Length) where digits[i].Length < i select digits[i];
            Console.WriteLine("\n3. Digits with name shorter than value:");
            Console.WriteLine($"  {string.Join(", ", shortNames)}");
            #endregion

            #region Element Operators
            Console.WriteLine("\n=== LINQ - Element Operators ===");
            // 1. Get first Product out of Stock
            var firstOutOfStock = (from p in products where p.UnitsInStock == 0 select p).FirstOrDefault();
            Console.WriteLine($"1. First out of stock: {(firstOutOfStock?.Name ?? "None")}");

            // 2. First product whose Price > 1000, or null
            var expensiveProduct = (from p in products where p.UnitPrice > 1000 select p).FirstOrDefault();
            Console.WriteLine($"2. First product > $1000: {(expensiveProduct?.Name ?? "None")}");

            // 3. Second number greater than 5
            int[] arr1 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var secondGreaterThan5 = arr1.Where(n => n > 5).Skip(1).FirstOrDefault();
            Console.WriteLine($"3. Second number > 5: {secondGreaterThan5}");

            #endregion

            #region Aggregate Operators
            Console.WriteLine("\n=== LINQ - Aggregate Operators ===");
            // 1. Count odd numbers
            var oddCount = arr1.Count(n => n % 2 != 0);
            Console.WriteLine($"1. Count of odd numbers: {oddCount}");

            // 2. Customers and their order count
            var customerOrders = from c in customers select new { c.Name, OrderCount = c.Orders.Count() };
            Console.WriteLine("\n2. Customer order counts:");
            foreach (var c in customerOrders) Console.WriteLine($"  {c.Name}: {c.OrderCount}");

            // 3. Categories and product count
            var categoryCounts = from p in products group p by p.Category into g select new { Category = g.Key, ProductCount = g.Count() };
            Console.WriteLine("\n3. Category product counts:");
            foreach (var c in categoryCounts) Console.WriteLine($"  {c.Category}: {c.ProductCount}");

            // 4. Total of numbers
            var total = arr1.Sum();
            Console.WriteLine($"\n4. Total of numbers: {total}");

            // 5. Total characters in dictionary
            var totalChars = dictionary.Sum(w => w.Length);
            Console.WriteLine($"5. Total characters in dictionary: {totalChars}");

            // 6. Length of shortest word
            var shortestLength = dictionary.Min(w => w.Length);
            Console.WriteLine($"6. Shortest word length: {shortestLength}");

            // 7. Length of longest word
            var longestLength = dictionary.Max(w => w.Length);
            Console.WriteLine($"7. Longest word length: {longestLength}");

            // 8. Average word length
            var avgLength = dictionary.Average(w => w.Length);
            Console.WriteLine($"8. Average word length: {avgLength:F2}");

            // 9. Total units in stock per category
            var unitsByCategory = from p in products group p by p.Category into g select new { Category = g.Key, TotalUnits = g.Sum(p => p.UnitsInStock) };
            Console.WriteLine("\n9. Total units in stock by category:");
            foreach (var c in unitsByCategory) Console.WriteLine($"  {c.Category}: {c.TotalUnits}");

            // 10. Cheapest price per category
            var cheapestByCategory = from p in products group p by p.Category into g select new { Category = g.Key, CheapestPrice = g.Min(p => p.UnitPrice) };
            Console.WriteLine("\n10. Cheapest price by category:");
            foreach (var c in cheapestByCategory) Console.WriteLine($"  {c.Category}: ${c.CheapestPrice}");

            // 11. Products with cheapest price per category
            var cheapestProducts = from p in products
                                   group p by p.Category into g
                                   let minPrice = g.Min(p => p.UnitPrice)
                                   from p in g
                                   where p.UnitPrice == minPrice
                                   select new { p.Category, p.Name, p.UnitPrice };
            Console.WriteLine("\n11. Cheapest products by category:");
            foreach (var p in cheapestProducts) Console.WriteLine($"  {p.Category}: {p.Name} (${p.UnitPrice})");

            // 12. Most expensive price per category
            var mostExpensiveByCategory = from p in products group p by p.Category into g select new { Category = g.Key, MaxPrice = g.Max(p => p.UnitPrice) };
            Console.WriteLine("\n12. Most expensive price by category:");
            foreach (var c in mostExpensiveByCategory) Console.WriteLine($"  {c.Category}: ${c.MaxPrice}");

            // 13. Products with most expensive price per category
            var mostExpensiveProducts = from p in products
                                        group p by p.Category into g
                                        let maxPrice = g.Max(p => p.UnitPrice)
                                        from p in g
                                        where p.UnitPrice == maxPrice
                                        select new { p.Category, p.Name, p.UnitPrice };
            Console.WriteLine("\n13. Most expensive products by category:");
            foreach (var p in mostExpensiveProducts) Console.WriteLine($"  {p.Category}: {p.Name} (${p.UnitPrice})");

            // 14. Average price per category
            var avgPriceByCategory = from p in products group p by p.Category into g select new { Category = g.Key, AvgPrice = g.Average(p => p.UnitPrice) };
            Console.WriteLine("\n14. Average price by category:");
            foreach (var c in avgPriceByCategory) Console.WriteLine($"  {c.Category}: ${c.AvgPrice:F2}");
            #endregion

            #region Ordering Operators
            Console.WriteLine("\n=== LINQ - Ordering Operators ===");
            // 1. Sort products by name
            var sortedProducts = from p in products orderby p.Name select p;
            Console.WriteLine("1. Products sorted by name:");
            foreach (var p in sortedProducts) Console.WriteLine($"  {p.Name}");

            // 2. Case-insensitive sort
            string[] arr2 = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var sortedWords = arr2.OrderBy(w => w, StringComparer.OrdinalIgnoreCase);
            Console.WriteLine("\n2. Case-insensitive sorted words:");
            Console.WriteLine($"  {string.Join(", ", sortedWords)}");

            // 3. Sort products by units in stock (descending)
            var sortedByStock = from p in products orderby p.UnitsInStock descending select p;
            Console.WriteLine("\n3. Products sorted by stock (desc):");
            foreach (var p in sortedByStock) Console.WriteLine($"  {p.Name}: {p.UnitsInStock}");

            // 4. Sort digits by length then name
            var sortedDigits = from d in digits orderby d.Length, d select d;
            Console.WriteLine("\n4. Digits sorted by length then name:");
            Console.WriteLine($"  {string.Join(", ", sortedDigits)}");

            // 5. Sort words by length then case-insensitive
            var sortedWordsByLength = arr2.OrderBy(w => w.Length).ThenBy(w => w, StringComparer.OrdinalIgnoreCase);
            Console.WriteLine("\n5. Words sorted by length then case-insensitive:");
            Console.WriteLine($"  {string.Join(", ", sortedWordsByLength)}");

            // 6. Sort products by category then price (descending)
            var sortedByCategoryPrice = from p in products orderby p.Category, p.UnitPrice descending select p;
            Console.WriteLine("\n6. Products sorted by category then price (desc):");
            foreach (var p in sortedByCategoryPrice) Console.WriteLine($"  {p.Category}: {p.Name} (${p.UnitPrice})");

            // 7. Sort words by length then case-insensitive descending
            var sortedWordsDesc = arr2.OrderBy(w => w.Length).ThenByDescending(w => w, StringComparer.OrdinalIgnoreCase);
            Console.WriteLine("\n7. Words sorted by length then case-insensitive (desc):");
            Console.WriteLine($"  {string.Join(", ", sortedWordsDesc)}");

            // 8. Digits with second letter 'i', reversed
            var reversedDigits = (from d in digits where d.Length > 1 && d[1] == 'i' select d).Reverse();
            Console.WriteLine("\n8. Digits with second letter 'i' (reversed):");
            Console.WriteLine($"  {string.Join(", ", reversedDigits)}");
            #endregion

            #region Transformation Operators
            Console.WriteLine("\n=== LINQ - Transformation Operators ===");
            // 1. Product names
            var productNames = from p in products select p.Name;
            Console.WriteLine("1. Product names:");
            Console.WriteLine($"  {string.Join(", ", productNames)}");

            // 2. Uppercase and lowercase words
            string[] words = { "aPPLE", "BlUeBeRrY", "cHeRry" };
            var caseVersions = from w in words select new { Upper = w.ToUpper(), Lower = w.ToLower() };
            Console.WriteLine("\n2. Uppercase and lowercase words:");
            foreach (var w in caseVersions) Console.WriteLine($"  Upper: {w.Upper}, Lower: {w.Lower}");

            // 3. Product properties with renamed price
            var productInfo = from p in products select new { p.Name, p.Category, Price = p.UnitPrice };
            Console.WriteLine("\n3. Product info with renamed price:");
            foreach (var p in productInfo) Console.WriteLine($"  {p.Name}, {p.Category}, ${p.Price}");

            // 4. Numbers matching position
            var matchesPosition = arr1.Select((n, i) => new { Number = n, Index = i, Matches = n == i });
            Console.WriteLine("\n4. Numbers matching position:");
            foreach (var m in matchesPosition) Console.WriteLine($"  {m.Number} at {m.Index}: {m.Matches}");

            // 5. Pairs where A < B
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            var pairs = from a in numbersA from b in numbersB where a < b select new { A = a, B = b };
            Console.WriteLine("\n5. Pairs where A < B:");
            foreach (var p in pairs) Console.WriteLine($"  ({p.A}, {p.B})");

            // 6. Orders with total < 500
            var smallOrders = from c in customers from o in c.Orders where o.Total < 500.00m select o;
            Console.WriteLine("\n6. Orders with total < $500:");
            foreach (var o in smallOrders) Console.WriteLine($"  Total: ${o.Total}, Date: {o.OrderDate:yyyy-MM-dd}");

            // 7. Orders from 1998 or later
            var recentOrders = from c in customers from o in c.Orders where o.OrderDate.Year >= 1998 select o;
            Console.WriteLine("\n7. Orders from 1998 or later:");
            foreach (var o in recentOrders) Console.WriteLine($"  Total: ${o.Total}, Date: {o.OrderDate:yyyy-MM-dd}");


            #endregion            Console.WriteLine("\n=== LINQ - Set Operators ===");

            #region Set Operators
            // 1. Unique category names
            var uniqueCategories = (from p in products select p.Category).Distinct();
            Console.WriteLine("1. Unique categories:");
            Console.WriteLine($"  {string.Join(", ", uniqueCategories)}");

            // 2. Unique first letters from product and customer names
            var firstLetters = (from p in products select p.Name[0]).Union(from c in customers select c.Name[0]);
            Console.WriteLine("\n2. Unique first letters:");
            Console.WriteLine($"  {string.Join(", ", firstLetters)}");

            // 3. Common first letters
            var commonLetters = (from p in products select p.Name[0]).Intersect(from c in customers select c.Name[0]);
            Console.WriteLine("\n3. Common first letters:");
            Console.WriteLine($"  {string.Join(", ", commonLetters)}");

            // 4. Product first letters not in customer names
            var productOnlyLetters = (from p in products select p.Name[0]).Except(from c in customers select c.Name[0]);
            Console.WriteLine("\n4. Product-only first letters:");
            Console.WriteLine($"  {string.Join(", ", productOnlyLetters)}");

            // 5. Last three characters of names
            var lastThreeChars = (from p in products select p.Name.Length >= 3 ? p.Name.Substring(p.Name.Length - 3) : p.Name)
                                .Concat(from c in customers select c.Name.Length >= 3 ? c.Name.Substring(c.Name.Length - 3) : c.Name);
            Console.WriteLine("\n5. Last three characters of names:");
            Console.WriteLine($"  {string.Join(", ", lastThreeChars)}");
            #endregion

            #region LINQ - Quantifiers
            Console.WriteLine("\n=== LINQ - Quantifiers ===");
            // 1. Any word contains 'ei'
            var hasEi = dictionary.Any(w => w.Contains("ei"));
            Console.WriteLine($"1. Any word contains 'ei': {hasEi}");

            // 2. Categories with at least one out-of-stock product
            var outOfStockCategories = from p in products
                                       group p by p.Category into g
                                       where g.Any(p => p.UnitsInStock == 0)
                                       select new { Category = g.Key, Products = g };
            Console.WriteLine("\n2. Categories with out-of-stock products:");
            foreach (var c in outOfStockCategories) Console.WriteLine($"  {c.Category}: {string.Join(", ", c.Products.Select(p => p.Name))}");

            // 3. Categories with all products in stock
            var allInStockCategories = from p in products
                                       group p by p.Category into g
                                       where g.All(p => p.UnitsInStock > 0)
                                       select new { Category = g.Key, Products = g };
            Console.WriteLine("\n3. Categories with all products in stock:");
            foreach (var c in allInStockCategories) Console.WriteLine($"  {c.Category}: {string.Join(", ", c.Products.Select(p => p.Name))}");

            #endregion

            #region LINQ - Partitioning Operators
            //Console.WriteLine("\n=== LINQ - Partitioning Operators ===");
            //// 1. First 3 orders from Washington
            //var firstThreeOrders = (from c in customers where c.Region == "Washington" from o in c.Orders select o).Take(3);
            //Console.WriteLine("1. First 3 orders from Washington:");
            //foreach (var o in firstThreeOrders) Console.WriteLine($"  Total: ${o.Total}, Date: {o.OrderDate:yyyy-MM-dd}");

            //// 2. All but first 2 orders from Washington
            //var skipTwoOrders = (from c in customers where c.Region == "Washington" from o in c.Orders select o).Skip(2);
            //Console.WriteLine("\n2. Orders from Washington (skip first 2):");
            //foreach (var o in skipTwoOrders) Console.WriteLine($"  Total: ${o.Total}, Date: {o.OrderDate:yyyy-MM-dd}");

            //// 3. Elements until number < position
            //var untilLessThanPosition = arr1.TakeWhile((n, i) => n >= i);
            //Console.WriteLine("\n3. Elements until number < position:");
            //Console.WriteLine($"  {string.Join(", ", untilLessThanPosition)}");

            //// 4. Elements from first divisible by 3
            //var fromDivisibleBy3 = arr1.SkipWhile(n => n % 3 != 0);
            //Console.WriteLine("\n4. Elements from first divisible by 3:");
            //Console.WriteLine($"  {string.Join(", ", fromDivisibleBy3)}");

            #endregion

            #region LINQ - Grouping Operators
            //Console.WriteLine("\n=== LINQ - Grouping Operators ===");
            //// 1. Group numbers by remainder when divided by 5
            //List<int> numbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            //var groupedByRemainder = from n in numbers group n by n % 5 into g select new { Remainder = g.Key, Numbers = g };
            //Console.WriteLine("1. Numbers grouped by remainder (mod 5):");
            //foreach (var g in groupedByRemainder) Console.WriteLine($"  Remainder {g.Remainder}: {string.Join(", ", g.Numbers)}");

            //// 2. Group words by first letter
            //var groupedByFirstLetter = from w in dictionary group w by w[0] into g select new { FirstLetter = g.Key, Words = g };
            //Console.WriteLine("\n2. Words grouped by first letter:");
            //foreach (var g in groupedByFirstLetter) Console.WriteLine($"  {g.FirstLetter}: {string.Join(", ", g.Words)}");

            //// 3. Group by anagram (same characters)
            //string[] arr3 = { "from", "salt", "earn", "last", "near", "form" };
            //var groupedByAnagram = arr3.GroupBy(w => string.Concat(w.OrderBy(c => c)), StringComparer.OrdinalIgnoreCase)
            //                           .Select(g => new { Key = g.Key, Words = g });
            //Console.WriteLine("\n3. Words grouped by anagram:");
            //foreach (var g in groupedByAnagram) Console.WriteLine($"  {g.Key}: {string.Join(", ", g.Words)}");

            //Console.WriteLine("\nPress any key to exit...");
            //Console.ReadKey(); 
            #endregion
        }
    }
}