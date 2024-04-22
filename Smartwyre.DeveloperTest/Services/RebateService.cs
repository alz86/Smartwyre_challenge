using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Utils.Rebates;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IProductDataStore _productDataStore;
    private readonly IRebateStrategyCalculationService _rebateStrategyCalculationService;

    public RebateService(IRebateDataStore rebateDataStore, IProductDataStore productDataStore, IRebateStrategyCalculationService rebateStrategyCalculationService)
    {
        _rebateDataStore = rebateDataStore;
        _productDataStore = productDataStore;
        _rebateStrategyCalculationService = rebateStrategyCalculationService;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var result = new CalculateRebateResult { Success = false };

        Rebate rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        IRebateCalculationStrategy strategy = _rebateStrategyCalculationService.GetStrategy(rebate.Incentive);

        if (strategy != null)
        {
            Product product = _productDataStore.GetProduct(request.ProductIdentifier);

            decimal? rebateAmount = strategy.CalculateRebate(request, rebate, product);
            if (rebateAmount.HasValue)
            {
                _rebateDataStore.StoreCalculationResult(rebate, rebateAmount.Value);
                result.Success = true;
            }
        }

        return result;
    }
}
