using Smartwyre.DeveloperTest.Utils.Rebates;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Services;

/// <summary>
/// Service that provides the right rebate calculation strategy 
/// for a given request
/// </summary>
/// <remarks>
/// This class provides a calculation strategy 
/// based on a fix list of strategies stored in memory.
/// While it is not an optimal approach, I implemented 
/// this way for the sake of simplicity.
/// </remarks>
public class StaticRebateStrategyCalculationService : IRebateStrategyCalculationService
{
    private readonly IDictionary<IncentiveType, RebateCalculationStrategyBase> _strategies;

    public StaticRebateStrategyCalculationService()
    {
        _strategies = new Dictionary<IncentiveType, RebateCalculationStrategyBase>
        {
            { IncentiveType.FixedCashAmount, new FixedCashAmountCalculationStrategy() },
            { IncentiveType.FixedRateRebate, new FixedRateRebateCalculationStrategy() },
            { IncentiveType.AmountPerUom, new AmountPerOumCalculationStrategy() }
        };
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public IRebateCalculationStrategy GetStrategy(IncentiveType incentive)
    {
        _strategies.TryGetValue(incentive, out var strategy);
        return strategy;
    }
}