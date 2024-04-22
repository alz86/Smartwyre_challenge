using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Utils.Rebates
{
    /// <summary>
    /// Class that calculates the rebate amount for a fixed cash amount
    /// </summary>
    public class FixedCashAmountCalculationStrategy : RebateCalculationStrategyBase
    {
        protected override IncentiveType IncentiveType => IncentiveType.FixedCashAmount;

        protected override SupportedIncentiveType SupportedIncentiveType => SupportedIncentiveType.FixedCashAmount;

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        protected override decimal? DoRebateCalculation(CalculateRebateRequest request, Rebate rebate, Product product) => (rebate.Amount == 0 ? null : rebate.Amount);
    }
}
