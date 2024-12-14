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
            Assert.True(volume.Ask >= 0);
            Assert.True(volume.Bid >= 0);
            Assert.True(volume.High >= 0); 
            Assert.True(volume.Low >= 0);
            Assert.True(volume.Volume >= 0);
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
        Assert.Equal(Ticker.BtcJpy, volume.Symbol);
        Assert.True(volume.Ask >= 0);
        Assert.True(volume.Bid >= 0);
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
        Assert.Equal(Ticker.Btc, response.Data.Symbol);
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
        Assert.Equal(Ticker.Eth, response.Data.Symbol);
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

    [Fact]
    public async Task GetKLinesAsync_WithBtcDaily_ReturnsValidResponse()
    {
        // Arrange
        var api = new GmoCoinPublicApi();
        var year = DateTime.UtcNow.Year.ToString();

        // Act
        var response = await api.GetKLinesAsync(Ticker.Btc, KLineInterval.OneDay, year);

        // Assert
        Assert.Equal(0, response.Status);
        Assert.NotNull(response.Data);
        Assert.NotEmpty(response.Data);

        // Check candlestick data structure
        var firstCandle = response.Data[0];
        Assert.True(firstCandle.Open > 0);
        Assert.True(firstCandle.High >= firstCandle.Open);
        Assert.True(firstCandle.High >= firstCandle.Low);
        Assert.True(firstCandle.Low <= firstCandle.Open);
        Assert.True(firstCandle.Close > 0);
        Assert.True(firstCandle.Volume >= 0);
    }

    [Fact]
    public async Task GetKLinesAsync_WithEthMinute_ReturnsValidResponse()
    {
        // Arrange
        var api = new GmoCoinPublicApi();
        
        // Convert current UTC time to JST (UTC+9)
        var jstNow = DateTime.UtcNow.AddHours(9);
        
        // If it's before 6:00 JST, we need to use the previous day
        var targetDate = jstNow.Hour < 6 
            ? jstNow.AddDays(-1).ToString("yyyyMMdd")
            : jstNow.ToString("yyyyMMdd");

        // Only available after 2021-04-15
        Assert.True(DateTime.Parse("2021-04-15") <= DateTime.UtcNow, 
            "Minute-level data is only available from 2021-04-15 onwards");

        // Act
        var response = await api.GetKLinesAsync(Ticker.Eth, KLineInterval.OneMinute, targetDate);

        // Assert
        Assert.Equal(0, response.Status);
        Assert.NotNull(response.Data);
        Assert.NotEmpty(response.Data);

        // Verify timestamps are in ascending order
        var timestamps = response.Data.Select(k => k.OpenTime).ToList();
        var sortedTimestamps = timestamps.OrderBy(t => t).ToList();
        Assert.Equal(sortedTimestamps, timestamps);
    }

    [Fact]
    public async Task GetKLinesAsync_WithInvalidTicker_ThrowsException()
    {
        // Arrange
        var api = new GmoCoinPublicApi();

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
        {
            await api.GetKLinesAsync((Ticker)999, KLineInterval.OneDay, "20240101");
        });
    }

    [Fact]
    public async Task GetKLinesAsync_WithInvalidInterval_ThrowsException()
    {
        // Arrange
        var api = new GmoCoinPublicApi();

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
        {
            await api.GetKLinesAsync(Ticker.Btc, (KLineInterval)999, "20240101");
        });
    }

    [Fact]
    public async Task GetTradeRulesAsync_ReturnsValidResponse()
    {
        // Arrange
        var api = new GmoCoinPublicApi();

        // Act
        var response = await api.GetTradeRulesAsync();

        // Assert
        Assert.Equal(0, response.Status);
        Assert.NotNull(response.Data);
        Assert.NotEmpty(response.Data);

        // Check BTC spot trading rules
        var btcRule = response.Data.FirstOrDefault(r => r.Symbol == Ticker.Btc);
        Assert.NotNull(btcRule);
        Assert.True(btcRule.MinOrderSize > 0);
        Assert.True(btcRule.MaxOrderSize > btcRule.MinOrderSize);
        Assert.True(btcRule.SizeStep > 0);
        Assert.True(btcRule.TickSize > 0);
        
        // Check BTC_JPY leveraged trading rules
        var btcJpyRule = response.Data.FirstOrDefault(r => r.Symbol == Ticker.BtcJpy);
        Assert.NotNull(btcJpyRule);
        Assert.True(btcJpyRule.MinOrderSize > 0);
        Assert.True(btcJpyRule.MaxOrderSize > btcJpyRule.MinOrderSize);
        Assert.True(btcJpyRule.SizeStep > 0);
        Assert.True(btcJpyRule.TickSize > 0);
    }
}
