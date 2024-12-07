using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GmoCoinNet.Network;
using GmoCoinNet.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GmoCoinNet
{
    public class GmoCoinPrivateApi
    {
        public static readonly string Endpoint = "https://api.coin.z.com/private";
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _secretKey;

        public GmoCoinPrivateApi(string apiKey, string secretKey, HttpClient? httpClient = null)
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _secretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey));
            _httpClient = httpClient ?? HttpClientProvider.HttpClient;
        }

        public async Task<GmoApiResponse<AccountMarginResponse>> GetAccountMarginAsync() 
            => await GetAsync<AccountMarginResponse>("/v1/account/margin");

        private async Task<GmoApiResponse<T>> GetAsync<T>(string path)
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            const string method = "GET";
            
            using var request = new HttpRequestMessage(new HttpMethod(method), Endpoint + path);
            var text = timestamp + method + path;
            var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey));
            var hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(text));
            var sign = ToHexString(hash);

            request.Headers.Add("API-KEY", _apiKey);
            request.Headers.Add("API-TIMESTAMP", timestamp);
            request.Headers.Add("API-SIGN", sign);

            var response = await _httpClient.SendAsync(request);
            return await GmoApiResponse<T>.FromResponseAsync(response);
        }

        private async Task<GmoApiResponse<T>> PostAsync<T>(string path, JObject body)
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            const string method = "POST";
            
            var bodyString = body.ToString(Formatting.None);
            
            using var request = new HttpRequestMessage(new HttpMethod(method), Endpoint + path);
            var text = timestamp + method + path + bodyString;
            var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey));
            var hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(text));
            var sign = ToHexString(hash);

            request.Headers.Add("API-KEY", _apiKey);
            request.Headers.Add("API-TIMESTAMP", timestamp);
            request.Headers.Add("API-SIGN", sign);
            request.Content = new StringContent(bodyString, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            return await GmoApiResponse<T>.FromResponseAsync(response);
        }
        
        private static string ToHexString(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
        }
    }
}