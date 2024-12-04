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
}
