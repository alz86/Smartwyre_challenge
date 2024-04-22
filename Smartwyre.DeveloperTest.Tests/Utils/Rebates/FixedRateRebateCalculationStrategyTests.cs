using Xunit;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Utils.Rebates;

namespace Smartwyre.DeveloperTest.Tests.Utils.Rebates;

public class FixedRateRebateCalculationStrategyTests : RebateCalculationStrategyBaseTests
{
    protected override RebateCalculationStrategyBase Strategy => new FixedRateRebateCalculationStrategy();

    protected override IncentiveType IncentiveType => IncentiveType.FixedRateRebate;

    protected override SupportedIncentiveType SupportedIncentiveType => SupportedIncentiveType.FixedRateRebate;


    [Fact]
    public void CalculateRebate_ReturnsCalculatedAmount_WhenAllConditionsMet()
    {
        var request = new CalculateRebateRequest { Volume = 5 };
        var rebate = new Rebate { Percentage = 0.1m, Incentive = IncentiveType };
        var product = new Product { Price = 100, SupportedIncentives = SupportedIncentiveType };

        var result = Strategy.CalculateRebate(request, rebate, product);

        Assert.Equal(50m, result); // 100 * 0.1 * 5
    }

    [Theory]
    [InlineData(0, 100, 10)]  // Zero rebate percentage
    [InlineData(0.1, 0, 10)]  // Zero product price
    [InlineData(0.1, 100, 0)] // Zero volume
    public void CalculateRebate_ReturnsNull_WhenAnyInputIsZero(decimal percentage, decimal price, int volume)
    {
        // Arrange
        var request = new CalculateRebateRequest { Volume = volume };
        var rebate = new Rebate { Percentage = percentage, Incentive = IncentiveType };
        var product = new Product { Price = price, SupportedIncentives = SupportedIncentiveType };

        // Act
        var result = Strategy.CalculateRebate(request, rebate, product);

        // Assert
        Assert.Null(result);
    }
}
