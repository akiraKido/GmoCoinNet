using System;
using System.Net.Http;
using System.Threading.Tasks;
using GmoCoinNet.Errors;
using Newtonsoft.Json;
#pragma warning disable CS1710 // XML comment has a duplicate typeparam tag

namespace GmoCoinNet.Schema;

/// <summary>Generic response wrapper for GMO Coin API responses</summary>
/// <summary xml:lang="ja">GMOコインAPIレスポンスの汎用ラッパー</summary>
/// <typeparam name="T">Type of the response data</typeparam>
/// <typeparam name="T" xml:lang="ja">レスポンスデータの型</typeparam>
public record GmoApiResponse<T>(int Status, T Data, DateTime ResponseTime)
{
    /// <summary>HTTP status code of the response</summary>
    /// <summary xml:lang="ja">レスポンスのHTTPステータスコード</summary>
    [JsonProperty("status")] public readonly int Status = Status;

    /// <summary>Response data</summary>
    /// <summary xml:lang="ja">レスポンスデータ</summary>
    [JsonProperty("data")] public readonly T Data = Data;

    /// <summary>Server response timestamp</summary>
    /// <summary xml:lang="ja">サーバーレスポンスのタイムスタンプ</summary>
    [JsonProperty("responsetime")] public readonly DateTime ResponseTime = ResponseTime;

    internal static async Task<GmoApiResponse<T>> FromResponseAsync(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            throw ErrorHelper.NetworkException(statusCode: response.StatusCode);
        }

        try
        {
            var data = JsonConvert.DeserializeObject<GmoApiResponse<T>>(content,
                new StatusService.StatusJsonConverter(),
                new TickerService.TickerConverter()
            );
            if (data == null)
            {
                throw ErrorHelper.SerializationException(reason: content);
            }

            return data;
        }
        catch (Exception e)
        {
            throw ErrorHelper.SerializationException(reason: $"{content}\nException: {e}");
        }
    }
}