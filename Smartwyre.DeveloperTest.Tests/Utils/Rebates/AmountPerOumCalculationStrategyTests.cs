using Xunit;
using Smartwyre.DeveloperTest.Utils.Rebates;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests.Utils.Rebates;

public class AmountPerOumCalculationStrategyTests : RebateCalculationStrategyBaseTests
{
    protected override RebateCalculationStrategyBase Strategy => new AmountPerOumCalculationStrategy();

    protected override IncentiveType IncentiveType => IncentiveType.AmountPerUom;
    protected override SupportedIncentiveType SupportedIncentiveType => SupportedIncentiveType.AmountPerUom;


    [Fact]
    public void CalculateRebate_ReturnsCalculatedAmount_WhenAllConditionsMet()
    {
        // Arrange
        var request = new CalculateRebateRequest { Volume = 10 };
        var rebate = new Rebate { Amount = 5m, Incentive = IncentiveType };
        var product = new Product { SupportedIncentives = SupportedIncentiveType };

        // Act
        var result = Strategy.CalculateRebate(request, rebate, product);

        // Assert
        Assert.Equal(50m, result); // 5 * 10
    }

    [Theory]
    [InlineData(0, 10)] // Zero amount
    [InlineData(5, 0)] // Zero volume
    public void CalculateRebate_ReturnsNull_WhenAmountOrVolumeIsZero(decimal amount, int volume)
    {
        // Arrange
        var request = new CalculateRebateRequest { Volume = volume };
        var rebate = new Rebate { Amount = amount, Incentive = IncentiveType };
        var product = new Product { SupportedIncentives = SupportedIncentiveType };

        // Act
        var result = Strategy.CalculateRebate(request, rebate, product);

        // Assert
        Assert.Null(result);
    }
}