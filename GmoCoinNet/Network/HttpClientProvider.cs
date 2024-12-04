using System.Net.Http;

namespace GmoCoinNet.Network;

internal static class HttpClientProvider
{
    public static HttpClient HttpClient { get; } = new();
}
