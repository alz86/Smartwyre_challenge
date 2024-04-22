using Xunit;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Utils.Rebates;

namespace Smartwyre.DeveloperTest.Tests.Utils.Rebates;

public class FixedCashAmountCalculationStrategyTests : RebateCalculationStrategyBaseTests
{
    protected override RebateCalculationStrategyBase Strategy => new FixedCashAmountCalculationStrategy();

    protected override IncentiveType IncentiveType => IncentiveType.FixedCashAmount;

    protected override SupportedIncentiveType SupportedIncentiveType => SupportedIncentiveType.FixedCashAmount;

    [Fact]
    public void CalculateRebate_ReturnsFixedAmount_WhenAmountIsNonZero()
    {
        var request = new CalculateRebateRequest();
        var rebate = new Rebate { Amount = 200m, Incentive = IncentiveType };
        var product = new Product { SupportedIncentives = SupportedIncentiveType };

        var result = Strategy.CalculateRebate(request, rebate, product);

        Assert.Equal(200m, result);
    }

    [Fact]
    public void CalculateRebate_ReturnsNull_WhenAmountIsZero()
    {
        var request = new CalculateRebateRequest();
        var rebate = new Rebate { Amount = 0, Incentive = IncentiveType };
        var product = new Product { SupportedIncentives = SupportedIncentiveType };

        var result = Strategy.CalculateRebate(request, rebate, product);

        Assert.Null(result);
    }
}
