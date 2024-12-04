using Xunit.Abstractions;

namespace GmoCoinNet.Tests;

public class PublicApiTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public async Task StatusWorks()
    {
        var publicApi = new GmoCoinPublicApi();
        var status = await publicApi.StatusAsync();
        testOutputHelper.WriteLine($"status was: {status}");
        Assert.True(status.Data.Status 
            is Schema.Status.Open 
            or Schema.Status.Maintenance
            or Schema.Status.PreOpen);
    }
}