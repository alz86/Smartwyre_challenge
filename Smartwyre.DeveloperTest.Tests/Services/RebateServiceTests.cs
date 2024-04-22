using Xunit;
using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Utils.Rebates;

namespace Smartwyre.DeveloperTest.Tests.Services;

public class RebateServiceTests
{
    private readonly Mock<IRebateDataStore> _mockRebateDataStore;
    private readonly Mock<IProductDataStore> _mockProductDataStore;
    private readonly Mock<IRebateStrategyCalculationService> _mockStrategyCalculationService;
    private readonly RebateService _rebateService;

    private Rebate _defaultRebate;
    private Product _defaultProduct;

    public RebateServiceTests()
    {
        _mockRebateDataStore = new Mock<IRebateDataStore>();
        _mockProductDataStore = new Mock<IProductDataStore>();
        _mockStrategyCalculationService = new Mock<IRebateStrategyCalculationService>();
        _rebateService = new RebateService(_mockRebateDataStore.Object, _mockProductDataStore.Object, _mockStrategyCalculationService.Object);

        InitializeDefaultMocks();
    }

    private void InitializeDefaultMocks()
    {
        _defaultRebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 1, Identifier = "R1", Percentage = 1 };
        _defaultProduct = new Product { Identifier = "P1", SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

        // Set default mocks. If needed, the tests method can override them.
        _mockRebateDataStore.Setup(m => m.GetRebate(It.IsAny<string>())).Returns(_defaultRebate);
        _mockProductDataStore.Setup(m => m.GetProduct(It.IsAny<string>())).Returns(_defaultProduct);
        _mockStrategyCalculationService.Setup(s => s.GetStrategy(It.Is<IncentiveType>(v => v == _defaultRebate.Incentive))).Returns(new Mock<IRebateCalculationStrategy>().Object);
    }

    [Fact]
    public void Calculate_ReturnsSuccess_WhenRebateCalculationIsValid()
    {
        // Arrange
        var request = new CalculateRebateRequest { RebateIdentifier = "R1", ProductIdentifier = "P1" };
        var strategy = new Mock<IRebateCalculationStrategy>();
        strategy.Setup(s => s.CalculateRebate(request, It.Is<Rebate>(v => v == _defaultRebate), It.Is<Product>(v => v == _defaultProduct))).Returns(100m);
        _mockStrategyCalculationService.Setup(s => s.GetStrategy(_defaultRebate.Incentive)).Returns(strategy.Object);

        // Act
        var result = _rebateService.Calculate(request);

        // Assert
        Assert.True(result.Success);
        _mockRebateDataStore.Verify(m => m.StoreCalculationResult(It.IsAny<Rebate>(), 100m), Times.Once);
    }

    [Fact]
    public void Calculate_ReturnsFailure_WhenProductIsNull()
    {
        // Arrange
        var request = new CalculateRebateRequest { RebateIdentifier = "R1", ProductIdentifier = "P1" };
        _mockProductDataStore.Setup(m => m.GetProduct("P1")).Returns((Product)null);

        // Act
        var result = _rebateService.Calculate(request);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void Calculate_ReturnsFailure_WhenRebateCalculationReturnsNull()
    {
        // Arrange
        var request = new CalculateRebateRequest { RebateIdentifier = "R1", ProductIdentifier = "P1" };
        var strategy = new Mock<IRebateCalculationStrategy>();
        strategy.Setup(s => s.CalculateRebate(request, It.Is<Rebate>(v => v == _defaultRebate), It.Is<Product>(v => v == _defaultProduct))).Returns((decimal?)null);
        _mockStrategyCalculationService.Setup(s => s.GetStrategy(_defaultRebate.Incentive)).Returns(strategy.Object);

        // Act
        var result = _rebateService.Calculate(request);

        // Assert
        Assert.False(result.Success);
    }
}
