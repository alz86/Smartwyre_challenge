using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Utils.Rebates
{
    /// <summary>
    /// Interface for rebate calculation strategies
    /// </summary>
    public interface IRebateCalculationStrategy
    {
        /// <summary>
        /// Calculate the rebate amount for a particular
        /// rebate  request
        /// </summary>
        /// <param name="request">Request information</param>
        /// <param name="rebate">Rebate associated to the request.</param>
        /// <param name="product">Product associated to the request.</param>
        /// <returns>
        /// Null if the rebate cannot be calculated, either because rebate or product were
        /// not found, as if the information didn't meet the expected requirements. otherwise the 
        /// amount calculated.
        /// </returns>
        decimal? CalculateRebate(CalculateRebateRequest request, Rebate rebate, Product product);
    }

    /// <summary>
    /// Base class for those one that calculate the rebate 
    /// for a particular rebate request.
    /// </summary>
    public abstract class RebateCalculationStrategyBase : IRebateCalculationStrategy
    {
        protected abstract IncentiveType IncentiveType { get; }

        protected abstract SupportedIncentiveType SupportedIncentiveType { get; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual decimal? CalculateRebate(CalculateRebateRequest request, Rebate rebate, Product product)
        {
            if (rebate == null || product == null)
            {
                //rebate and product must be present
                return null;
            }
            else if (rebate.Incentive != IncentiveType)
            {
                //incentive type must match
                return null;
            }
            else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType))
            {
                //product must support the rebate incentive
                return null;
            }
            else
            {
                //everything is ok, we call the rebate calculation code
                return DoRebateCalculation(request, rebate, product);
            }
        }

        /// <summary>
        /// Calculates the rebate amount for a particular rebate request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="rebate"></param>
        /// <param name="product"></param>
        /// <remarks>
        /// This method assumes the rebate and product information
        /// are already loaded.
        /// </remarks>
        /// <returns>
        /// Null if the rebate cannot be calculated due to the request information, 
        /// otherwise the rebate amount.
        /// </returns>
        protected abstract decimal? DoRebateCalculation(CalculateRebateRequest request, Rebate rebate, Product product);

    }
}
