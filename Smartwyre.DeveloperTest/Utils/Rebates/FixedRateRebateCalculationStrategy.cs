using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Utils.Rebates
{
    /// <summary>
    /// Class that calculates the rebate amount for a fixed rate rebate
    /// </summary>
    public class FixedRateRebateCalculationStrategy : RebateCalculationStrategyBase
    {
        protected override IncentiveType IncentiveType => IncentiveType.FixedRateRebate;

        protected override SupportedIncentiveType SupportedIncentiveType => SupportedIncentiveType.FixedRateRebate;


        /// <summary>
        /// <inheritdoc />
        /// </summary>
        protected override decimal? DoRebateCalculation(CalculateRebateRequest request, Rebate rebate, Product product)
        {
            return (rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0
                ? null
                : product.Price * rebate.Percentage * request.Volume);
        }
    }
}
