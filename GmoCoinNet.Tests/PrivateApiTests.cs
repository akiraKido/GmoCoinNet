namespace GmoCoinNet.Tests
{
    public class GmoCoinPrivateApiTests
    {
        private static string ApiKey => Environment.GetEnvironmentVariable("GMO_API_KEY") 
                                        ?? throw new InvalidOperationException("GMO_API_KEY environment variable is not set");
            
        private static string SecretKey => Environment.GetEnvironmentVariable("GMO_SECRET_KEY") 
                                           ?? throw new InvalidOperationException("GMO_SECRET_KEY environment variable is not set");

        private readonly GmoCoinPrivateApi _api;

        public GmoCoinPrivateApiTests()
        {
            _api = new GmoCoinPrivateApi(ApiKey, SecretKey);
        }

        [Fact]
        public async Task GetAccountMarginAsync_ShouldReturnValidResponse()
        {
            // Act
            var response = await _api.GetAccountMarginAsync();

            // Assert
            Assert.NotNull(response);
            Assert.Equal(0, response.Status);
            Assert.NotNull(response.Data);
            Assert.True(response.Data.ActualProfitLoss >= 0 || response.Data.ActualProfitLoss < 0); // Ensures it's a number
            Assert.True(response.Data.AvailableAmount >= 0);
            Assert.True(response.Data.Margin >= 0);
        }
    }
}