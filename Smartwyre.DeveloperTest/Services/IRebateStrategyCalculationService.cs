using Smartwyre.DeveloperTest.Utils.Rebates;

namespace Smartwyre.DeveloperTest.Services;

/// <summary>
/// Represents a service that provides the right rebate 
/// calculation strategy for a given request
/// </summary>
public interface IRebateStrategyCalculationService
{
    /// <summary>
    /// Gets an <see cref="IRebateCalculationStrategy" />
    /// object to calculate the rebate for the given request
    /// </summary>
    IRebateCalculationStrategy GetStrategy(IncentiveType incentive);
}
