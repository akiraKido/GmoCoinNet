using GmoCoinNet.Schema;
using Xunit.Abstractions;

namespace GmoCoinNet.Tests;

public class PublicApiTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public async Task StatusWorks()
    {
        var publicApi = new GmoCoinPublicApi();
        var status = await publicApi.GetStatusAsync();
        testOutputHelper.WriteLine($"status was: {status}");
        Assert.True(status.Data.Status 
            is Status.Open 
            or Status.Maintenance
            or Status.PreOpen);
    }

    [Fact]
    public async Task TickerWorks()
    {
        var publicApi = new GmoCoinPublicApi();
        var volumes = await publicApi.GetTickerAsync();
        testOutputHelper.WriteLine($"Got {volumes.Data.Count} volume entries");
        Assert.NotEmpty(volumes.Data);
        
        foreach (var volume in volumes.Data)
        {
            testOutputHelper.WriteLine($"Volume for {volume.Symbol}: Ask={volume.Ask} Bid={volume.Bid}");
            Assert.True(decimal.Parse(volume.Ask) >= 0);
            Assert.True(decimal.Parse(volume.Bid) >= 0);
            Assert.True(decimal.Parse(volume.High) >= 0); 
            Assert.True(decimal.Parse(volume.Low) >= 0);
            Assert.True(decimal.Parse(volume.Volume) >= 0);
        }
    }

    [Fact]
    public async Task TickerWorksWithSpecificSymbol()
    {
        var publicApi = new GmoCoinPublicApi();
        var volumes = await publicApi.GetTickerAsync(Ticker.BtcJpy);
        Assert.Single(volumes.Data);
        
        var volume = volumes.Data[0];
        testOutputHelper.WriteLine($"BTC/JPY volume: Ask={volume.Ask} Bid={volume.Bid}");
        Assert.Equal(TickerService.ToString(Ticker.BtcJpy), volume.Symbol);
        Assert.True(decimal.Parse(volume.Ask) >= 0);
        Assert.True(decimal.Parse(volume.Bid) >= 0);
    }

    [Fact]
    public async Task GetOrderBooksAsync_WithBtc_ReturnsValidResponse()
    {
        // Arrange
        var api = new GmoCoinPublicApi();

        // Act
        var response = await api.GetOrderBooksAsync(Ticker.Btc);

        // Assert
        Assert.Equal(0, response.Status);
        Assert.NotNull(response.Data);
        Assert.Equal("BTC", response.Data.Symbol);
        Assert.NotEmpty(response.Data.Asks);
        Assert.NotEmpty(response.Data.Bids);

        // Check order book entry structure
        var firstAsk = response.Data.Asks[0];
        Assert.True(firstAsk.Price > 0);
        Assert.True(firstAsk.Size > 0);

        var firstBid = response.Data.Bids[0];
        Assert.True(firstBid.Price > 0);
        Assert.True(firstBid.Size > 0);
    }

    [Fact]
    public async Task GetOrderBooksAsync_WithEth_ReturnsValidResponse()
    {
        // Arrange
        var api = new GmoCoinPublicApi();

        // Act
        var response = await api.GetOrderBooksAsync(Ticker.Eth);

        // Assert
        Assert.Equal(0, response.Status);
        Assert.NotNull(response.Data);
        Assert.Equal("ETH", response.Data.Symbol);
        Assert.NotEmpty(response.Data.Asks);
        Assert.NotEmpty(response.Data.Bids);
    }

    [Fact]
    public async Task GetOrderBooksAsync_WithInvalidTicker_ThrowsException()
    {
        // Arrange
        var api = new GmoCoinPublicApi();

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
        {
            await api.GetOrderBooksAsync((Ticker)999);
        });
    }

    [Fact]
    public async Task GetTradesAsync_WithBtc_ReturnsValidResponse()
    {
        // Arrange
        var api = new GmoCoinPublicApi();

        // Act
        var response = await api.GetTradesAsync(Ticker.Btc);

        // Assert
        Assert.Equal(0, response.Status);
        Assert.NotNull(response.Data);
        Assert.NotNull(response.Data.Pagination);
        Assert.NotEmpty(response.Data.List);

        // Check pagination
        Assert.Equal(1, response.Data.Pagination.CurrentPage);
        Assert.True(response.Data.Pagination.Count > 0);

        // Check trade entry structure
        var firstTrade = response.Data.List[0];
        Assert.True(firstTrade.Price > 0);
        Assert.True(firstTrade.Size > 0);
        Assert.True(firstTrade.Side is TradeSide.Buy or TradeSide.Sell);
        Assert.True(firstTrade.Timestamp > DateTime.MinValue);
    }

    [Fact]
    public async Task GetTradesAsync_WithPagination_ReturnsRequestedPageSize()
    {
        // Arrange
        var api = new GmoCoinPublicApi();
        const int expectedCount = 10;

        // Act
        var response = await api.GetTradesAsync(Ticker.Btc, page: 1, count: expectedCount);

        // Assert
        Assert.Equal(0, response.Status);
        Assert.NotNull(response.Data);
        Assert.Equal(1, response.Data.Pagination.CurrentPage);
        Assert.Equal(expectedCount, response.Data.Pagination.Count);
        Assert.True(response.Data.List.Count <= expectedCount);
    }

    [Fact]
    public async Task GetTradesAsync_WithInvalidTicker_ThrowsException()
    {
        // Arrange
        var api = new GmoCoinPublicApi();

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
        {
            await api.GetTradesAsync((Ticker)999);
        });
    }
}
