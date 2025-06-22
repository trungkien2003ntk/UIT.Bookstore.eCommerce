using KKBookstore.Orders;
using Xunit;

namespace KKBookstore.Domain.Tests.Orders;

public class OrderNumberFormatTests
{
    [Fact]
    public void GenerateOrderNumber_ShouldFollowNewFormat()
    {
        // Arrange
        var order = new Order();
        
        // Act
        var orderNumber = order.OrderNumber;
        
        // Assert
        Assert.NotNull(orderNumber);
        Assert.StartsWith("SO-", orderNumber);
        
        // Verify format: SO-yymmdd<HCM><unique>
        // Example: SO-241220HCM121534579912
        var parts = orderNumber.Split('-');
        Assert.Equal(2, parts.Length);
        Assert.Equal("SO", parts[0]);
        
        var dateBranchUnique = parts[1];
        Assert.True(dateBranchUnique.Length >= 15); // 6 (date) + 3 (HCM) + min 6 (unique)
        Assert.Contains("HCM", dateBranchUnique);
        
        // Verify date part (first 6 characters should be yymmdd)
        var datepart = dateBranchUnique.Substring(0, 6);
        Assert.True(datepart.All(char.IsDigit));
        
        // Verify HCM is present
        var hcmIndex = dateBranchUnique.IndexOf("HCM");
        Assert.Equal(6, hcmIndex); // Should be right after the date
        
        // Verify unique part exists after HCM
        var uniquePart = dateBranchUnique.Substring(9); // After HCM
        Assert.True(uniquePart.Length > 0);
        Assert.True(uniquePart.All(char.IsDigit));
    }
    
    [Fact]
    public void MultipleOrders_ShouldHaveUniqueOrderNumbers()
    {
        // Arrange & Act
        var order1 = new Order();
        var order2 = new Order();
        var order3 = new Order();
        
        // Assert
        Assert.NotEqual(order1.OrderNumber, order2.OrderNumber);
        Assert.NotEqual(order2.OrderNumber, order3.OrderNumber);
        Assert.NotEqual(order1.OrderNumber, order3.OrderNumber);
        
        // All should follow the same format
        Assert.All(new[] { order1.OrderNumber, order2.OrderNumber, order3.OrderNumber }, 
            orderNumber => {
                Assert.StartsWith("SO-", orderNumber);
                Assert.Contains("HCM", orderNumber);
            });
    }
    
    [Fact]
    public void OrderNumber_ShouldNotExposeOrderCount()
    {
        // Arrange & Act
        var orders = Enumerable.Range(1, 10)
            .Select(_ => new Order())
            .ToList();
        
        // Assert - Order numbers should not be sequential or reveal count
        var orderNumbers = orders.Select(o => o.OrderNumber).ToList();
        
        // Extract the unique parts (after HCM)
        var uniqueParts = orderNumbers
            .Select(on => on.Split('-')[1].Substring(9)) // Get part after SO-yymmddHCM
            .Select(int.Parse)
            .ToList();
            
        // Verify they are not sequential (would expose order count)
        for (int i = 1; i < uniqueParts.Count; i++)
        {
            Assert.NotEqual(uniqueParts[i-1] + 1, uniqueParts[i]);
        }
    }
}
