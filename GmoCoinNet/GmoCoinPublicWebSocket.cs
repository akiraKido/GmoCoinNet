using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GmoCoinNet.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GmoCoinNet;

/// <summary>Client for GMO Coin's public WebSocket API</summary>
/// <summary xml:lang="ja">GMOコインの公開WebSocket APIクライアント</summary>
public class GmoCoinPublicWebSocket
{
    private const string DefaultEndpoint = "wss://api.coin.z.com/ws/public/v1";

    /// <summary>Subscribes to real-time ticker updates for the specified symbol</summary>
    /// <summary xml:lang="ja">指定した銘柄のリアルタイムティッカー更新を購読します</summary>
    /// <param name="ticker">The ticker symbol to subscribe to</param>
    /// <param name="ticker" xml:lang="ja">購読する銘柄</param>
    /// <param name="cancellationToken">Token to cancel the subscription</param>
    /// <param name="cancellationToken" xml:lang="ja">購読をキャンセルするためのトークン</param>
    /// <returns>An async stream of ticker updates</returns>
    /// <returns xml:lang="ja">ティッカー更新の非同期ストリーム</returns>
    public IAsyncEnumerable<TickerEntry> SubscribeToTickerStream(Ticker ticker, CancellationToken cancellationToken = default)
    {
        return SubscribeToStream<TickerEntry>("ticker", ticker, cancellationToken);
    }

    /// <summary>Subscribes to real-time order book updates for the specified symbol</summary>
    /// <summary xml:lang="ja">指定した銘柄のリアルタイム板情報更新を購読します</summary>
    /// <param name="ticker">The ticker symbol to subscribe to</param>
    /// <param name="ticker" xml:lang="ja">購読する銘柄</param>
    /// <param name="cancellationToken">Token to cancel the subscription</param>
    /// <param name="cancellationToken" xml:lang="ja">購読をキャンセルするためのトークン</param>
    /// <returns>An async stream of order book updates</returns>
    /// <returns xml:lang="ja">板情報更新の非同期ストリーム</returns>
    public IAsyncEnumerable<OrderBookStreamEntry> SubscribeToOrderBooks(Ticker ticker, CancellationToken cancellationToken = default)
    {
        return SubscribeToStream<OrderBookStreamEntry>("orderbooks", ticker, cancellationToken);
    }

    private static async IAsyncEnumerable<T> SubscribeToStream<T>(
        string channel,
        Ticker ticker,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var symbolString = TickerService.ToString(ticker);
        using var client = new ClientWebSocket();
        await client.ConnectAsync(new Uri(DefaultEndpoint), cancellationToken);

        static async Task SendCommandAsync(
            string command, ClientWebSocket client, string channel, string symbol, CancellationToken cancellationToken)
        {
            var message = new JObject
            {
                ["command"] = command,
                ["channel"] = channel,
                ["symbol"] = symbol
            }.ToString();

            var messageBytes = Encoding.UTF8.GetBytes(message);
            await client.SendAsync(
                new ArraySegment<byte>(messageBytes),
                WebSocketMessageType.Text,
                true,
                cancellationToken);
        }

        await SendCommandAsync("subscribe", client, channel, symbolString, cancellationToken);

        try
        {
            var buffer = new byte[10 * 1024 * 1024]; // 10 MB
            while (client.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
            {
                var result = await client.ReceiveAsync(
                    new ArraySegment<byte>(buffer),
                    cancellationToken);
                
                if (result.MessageType == WebSocketMessageType.Close || cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                var json = Encoding.UTF8.GetString(buffer, 0, result.Count);
                var data = JsonConvert.DeserializeObject<T>(json);
                if (data != null)
                {
                    yield return data;
                }
            }
        }
        finally
        {
            if (client.State == WebSocketState.Open)
            {
                await SendCommandAsync("unsubscribe", client, channel, symbolString, cancellationToken);
                await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Subscription ended", CancellationToken.None);
            }
        }
    }
}