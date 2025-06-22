using KKBookstore.Orders;
using System;
using System.Linq;

namespace OrderNumberTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing New Order Number Format");
            Console.WriteLine("================================");
            Console.WriteLine();

            // Test 1: Create multiple orders and verify format
            Console.WriteLine("Test 1: Creating 5 orders to verify format and uniqueness");
            var orders = Enumerable.Range(1, 5).Select(_ => new Order()).ToList();
            
            foreach (var order in orders)
            {
                Console.WriteLine($"Order Number: {order.OrderNumber}");
                
                // Verify format
                if (order.OrderNumber.StartsWith("SO-") && order.OrderNumber.Contains("HCM"))
                {
                    Console.WriteLine("  ✓ Format correct");
                }
                else
                {
                    Console.WriteLine("  ✗ Format incorrect");
                }
            }
            
            Console.WriteLine();
            
            // Test 2: Verify uniqueness
            Console.WriteLine("Test 2: Verifying uniqueness");
            var uniqueNumbers = orders.Select(o => o.OrderNumber).Distinct().Count();
            Console.WriteLine($"Created {orders.Count} orders, {uniqueNumbers} unique numbers");
            
            if (uniqueNumbers == orders.Count)
            {
                Console.WriteLine("  ✓ All order numbers are unique");
            }
            else
            {
                Console.WriteLine("  ✗ Some order numbers are duplicated");
            }
            
            Console.WriteLine();
            
            // Test 3: Format analysis
            Console.WriteLine("Test 3: Format analysis");
            var firstOrder = orders.First().OrderNumber;
            Console.WriteLine($"Sample order number: {firstOrder}");
            
            var parts = firstOrder.Split('-');
            if (parts.Length == 2 && parts[0] == "SO")
            {
                var dateBranchUnique = parts[1];
                var datepart = dateBranchUnique.Substring(0, 6);
                var hcmIndex = dateBranchUnique.IndexOf("HCM");
                var uniquePart = hcmIndex >= 0 ? dateBranchUnique.Substring(hcmIndex + 3) : "";
                
                Console.WriteLine($"  Prefix: {parts[0]}");
                Console.WriteLine($"  Date part: {datepart}");
                Console.WriteLine($"  Branch code position: {hcmIndex} (should be 6)");
                Console.WriteLine($"  Unique part: {uniquePart} (length: {uniquePart.Length})");
                
                if (parts[0] == "SO" && hcmIndex == 6 && uniquePart.Length > 0)
                {
                    Console.WriteLine("  ✓ Format structure is correct");
                }
                else
                {
                    Console.WriteLine("  ✗ Format structure has issues");
                }
            }
            
            Console.WriteLine();
            Console.WriteLine("Testing completed!");
        }
    }
}
