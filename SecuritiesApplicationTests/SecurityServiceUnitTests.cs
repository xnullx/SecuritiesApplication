using Moq;
using SecuritiesApplication.ApiClients;
using SecuritiesApplication.Entities;
using SecuritiesApplication.Helpers;
using SecuritiesApplication.Repositories;
using SecuritiesApplication.Services;

namespace SecuritiesApplicationTests
{
    public class SecurityServiceUnitTests
    {
        private readonly ISecurityService _securityService;
        private readonly Mock<ILoggerInternal> _logger;
        private readonly Mock<IPriceApiClient> _priceApiClient;
        private readonly Mock<ISecurityRepository> _securityRepository;

        public SecurityServiceUnitTests()
        {
            _priceApiClient = new Mock<IPriceApiClient>();
            _securityRepository = new Mock<ISecurityRepository>();
            _logger = new Mock<ILoggerInternal>();
            _securityService = new SecurityService(_priceApiClient.Object, _securityRepository.Object, _logger.Object);
        }

        [Fact]
        public void ExecuteIsins_WhenNoErrorOccurs_ShouldProcessSuccessfully()
        {
            //Arrange
            var list = new List<string>() { "akaksmlks112", "avmv124oddsa" };
            _priceApiClient.Setup(x => x.GetPriceFromIsin(It.IsAny<string>())).ReturnsAsync(99.99m);

            //Act
            _securityService.ExecuteIsins(list);

            //Assert
            _securityRepository.Verify(x => x.StoreSecurities(It.IsAny<IEnumerable<Security>>()), Times.Once);
            _priceApiClient.Verify(x => x.GetPriceFromIsin(It.IsAny<string>()), Times.Exactly(list.Count));
            _logger.Verify(x => x.LogError(It.IsAny<string>(), It.IsAny<Exception>()), Times.Never);
        }

        [Fact]
        public void ExecuteIsins_WhenHasAnInvalidIsin_ShouldProcessSuccessfully_AndLogWarning()
        {
            //Arrange
            var list = new List<string>() { "", "avmv124oddsa" };
            _priceApiClient.Setup(x => x.GetPriceFromIsin(It.IsAny<string>())).ReturnsAsync(99.99m);

            //Act
            _securityService.ExecuteIsins(list);

            //Assert
            _securityRepository.Verify(x => x.StoreSecurities(It.IsAny<IEnumerable<Security>>()), Times.Once);
            _priceApiClient.Verify(x => x.GetPriceFromIsin(It.IsAny<string>()), Times.Once);
            _logger.Verify(x => x.LogWarn(It.IsAny<string>()), Times.Once);
            _logger.Verify(x => x.LogError(It.IsAny<string>(),It.IsAny<Exception>()), Times.Never);
        }

        [Fact]
        public void ExecuteIsins_WhenNullList_ShouldThrowException()
        {
            //Act and Assert
            Assert.ThrowsAsync<Exception>(() => _securityService.ExecuteIsins(null));
        }
    }
}
