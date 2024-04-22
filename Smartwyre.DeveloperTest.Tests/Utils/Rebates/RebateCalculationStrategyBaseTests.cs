using Xunit;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Utils.Rebates;

namespace Smartwyre.DeveloperTest.Tests.Utils.Rebates;

/// <summary>
/// Base class for those ones that provide tests for 
/// different calculation strategies.
/// </summary>
public abstract class RebateCalculationStrategyBaseTests
{
    /// <summary>
    /// Gets the <see cref="RebateCalculationStrategyBase"/> to test.
    /// </summary>
    protected abstract RebateCalculationStrategyBase Strategy { get; }

    /// <summary>
    /// Gets the <see cref="IncentiveType"/> associated to the strategy.
    /// </summary>
    protected abstract IncentiveType IncentiveType { get; }

    /// <summary>
    /// Gets the <see cref="SupportedIncentiveType"/> associated to the strategy.
    /// </summary>
    protected abstract SupportedIncentiveType SupportedIncentiveType { get; }


    [Fact]
    public void CalculateRebate_ReturnsNull_WhenRebateIsNull()
    {
        var result = Strategy.CalculateRebate(new CalculateRebateRequest(), null, new Product());
        Assert.Null(result);
    }

    [Fact]
    public void CalculateRebate_ReturnsNull_WhenProductIsNull()
    {
        var result = Strategy.CalculateRebate(new CalculateRebateRequest(), new Rebate(), null);
        Assert.Null(result);
    }

    [Fact]
    public void CalculateRebate_ReturnsNull_WhenIncentiveIsIncorrect()
    {
        var request = new CalculateRebateRequest();
        var incorrectIncentiveType = EnumHelpers.GetDifferentEnumValue(IncentiveType);
        var rebate = new Rebate { Incentive = incorrectIncentiveType };
        var product = new Product { SupportedIncentives = SupportedIncentiveType };

        var result = Strategy.CalculateRebate(request, rebate, product);
        Assert.Null(result);
    }

    [Fact]
    public void CalculateRebate_ReturnsNull_WhenSupportedIncentiveIsIncorrect()
    {
        var request = new CalculateRebateRequest();
        var rebate = new Rebate { Incentive = IncentiveType };
        var incorrectType = EnumHelpers.GetDifferentEnumValue(SupportedIncentiveType);
        var product = new Product { SupportedIncentives = incorrectType };

        var result = Strategy.CalculateRebate(request, rebate, product);
        Assert.Null(result);
    }
}