using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Utils.Rebates
{
    /// <summary>
    /// Class that calculates the rebate amount for an amount per OUM
    /// </summary>
    public class AmountPerOumCalculationStrategy : RebateCalculationStrategyBase
    {
        protected override IncentiveType IncentiveType => IncentiveType.AmountPerUom;

        protected override SupportedIncentiveType SupportedIncentiveType => SupportedIncentiveType.AmountPerUom;

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        protected override decimal? DoRebateCalculation(CalculateRebateRequest request, Rebate rebate, Product product)
        {
            return (rebate.Amount == 0 || request.Volume == 0
                        ? null
                        : rebate.Amount * request.Volume);
        }
    }
}
