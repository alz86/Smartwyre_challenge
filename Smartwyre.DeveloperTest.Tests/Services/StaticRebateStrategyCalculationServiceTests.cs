using Xunit;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Utils.Rebates;

public class StaticRebateStrategyCalculationServiceTests
{
    private readonly StaticRebateStrategyCalculationService _service;

    public StaticRebateStrategyCalculationServiceTests()
    {
        _service = new StaticRebateStrategyCalculationService();
    }

    [Theory]
    [InlineData(IncentiveType.FixedCashAmount, typeof(FixedCashAmountCalculationStrategy))]
    [InlineData(IncentiveType.FixedRateRebate, typeof(FixedRateRebateCalculationStrategy))]
    [InlineData(IncentiveType.AmountPerUom, typeof(AmountPerOumCalculationStrategy))]
    public void GetStrategy_ReturnsCorrectStrategy_ForIncentiveType(IncentiveType incentive, System.Type expectedType)
    {
        // Act
        var strategy = _service.GetStrategy(incentive);

        // Assert
        Assert.IsType(expectedType, strategy);
    }

    [Fact]
    public void GetStrategy_ReturnsNull_WhenStrategyDoesNotExist()
    {
        // Arrange
        var nonExistingIncentive = (IncentiveType)(-1); // Using an enum value that is not defined

        // Act
        var strategy = _service.GetStrategy(nonExistingIncentive);

        // Assert
        Assert.Null(strategy);
    }
}
