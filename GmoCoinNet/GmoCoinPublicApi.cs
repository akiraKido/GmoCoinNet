using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GmoCoinNet.Network;
using GmoCoinNet.Schema;
#pragma warning disable CS1571 // XML comment has a duplicate param tag

namespace GmoCoinNet
{
    /// <summary>GMO Coin Public API</summary>
    /// <summary xml:lang="ja">GMOコイン公開API</summary>
    public class GmoCoinPublicApi
    {
        /// <summary>The base URL endpoint for GMO Coin's public API.</summary>
        /// <summary xml:lang="ja">GMOコイン公開APIのベースURLエンドポイント</summary>
        public readonly string EndPoint = "https://api.coin.z.com/public";

        /// <summary>Initializes a new instance of the <see cref="GmoCoinPublicApi"/> class.</summary>
        /// <summary xml:lang="ja">GmoCoinPublicApiクラスの新しいインスタンスを初期化します。</summary>
        /// <param name="endPoint">Optional custom API endpoint URL. If null, the default endpoint will be used.</param>
        /// <param name="endPoint" xml:lang="ja">カスタムAPIエンドポイントURL（オプション）。nullの場合、デフォルトのエンドポイントが用されます。</param>
        public GmoCoinPublicApi(string? endPoint = null)
        {
            if (endPoint != null)
            {
                EndPoint = endPoint;
            }
        }

        /// <summary>Gets the current status of the GMO Coin service.</summary>
        /// <summary xml:lang="ja">GMOコインのサービスステータスを取得します。</summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation that returns a <see cref="GmoApiResponse{StatusResponse}"/> containing information about the service status.</returns>
        /// <returns xml:lang="ja">サービスステータス情報を含む<see cref="GmoApiResponse{StatusResponse}"/>を返す非同期操作を表す<see cref="Task"/></returns>
        /// <remarks>This endpoint can be used to check if the GMO Coin service is operating normally.</remarks>
        /// <remarks xml:lang="ja">このエンドポイントを使用して、GMOコインのサービスが正常に動作しているかどうかを確認できます。</remarks>
        public async Task<GmoApiResponse<StatusResponse>> GetStatusAsync()
        {
            string path = "/v1/status";
            var response = await HttpClientProvider.HttpClient.GetAsync(EndPoint + path);
            return await GmoApiResponse<StatusResponse>.FromResponseAsync(response);
        }

        /// <summary>Gets the latest rate for the specified ticker symbol.</summary>
        /// <summary xml:lang="ja">指した銘柄の最新レートを取得します。</summary>
        /// <param name="ticker">The ticker symbol to get rates for. If null, returns rates for all symbols.</param>
        /// <param name="ticker" xml:lang="ja">レートを取得する銘柄。nullの場合、全銘柄のレートを返します。</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation that returns a <see cref="GmoApiResponse{TickerResponse}"/> containing the latest rate information.</returns>
        /// <returns xml:lang="ja">最新のレート情報を含む<see cref="GmoApiResponse{TickerResponse}"/>を返す非同期操作を表す<see cref="Task"/></returns>
        /// <remarks>For getting the latest rates of all ticker symbols at once, it is recommended to call without specifying a ticker parameter.</remarks>
        /// <remarks xml:lang="ja">全銘柄分の最新レートを取得する場合はtickerパラメータ指定無しでの実行をおすすめします。</remarks>
        public async Task<GmoApiResponse<IReadOnlyList<VolumeEntry>>> GetTickerAsync(Ticker? ticker = null)
        {
            string? tickerString = null;
            if (ticker != null)
            {
                tickerString = TickerService.ToString(ticker.Value);
            }
            
            var path = "/v1/ticker";
            if (tickerString != null)
            {
                path += $"?symbol={tickerString}";
            }
            var response = await HttpClientProvider.HttpClient.GetAsync(EndPoint + path);
            return await GmoApiResponse<IReadOnlyList<VolumeEntry>>.FromResponseAsync(response);
        }

        /// <summary>Gets the order book for the specified ticker symbol</summary>
        /// <summary xml:lang="ja">指定した銘柄の板情報を取得します</summary>
        /// <param name="ticker">The ticker symbol to get the order book for</param>
        /// <param name="ticker" xml:lang="ja">板情報を取得する銘柄</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation that returns a <see cref="GmoApiResponse{OrderBookResponse}"/> containing the order book information</returns>
        /// <returns xml:lang="ja">板情報を含む<see cref="GmoApiResponse{OrderBookResponse}"/>を返す非同期操作を表す<see cref="Task"/></returns>
        public async Task<GmoApiResponse<OrderBookResponse>> GetOrderBooksAsync(Ticker ticker)
        {
            var tickerString = TickerService.ToString(ticker);
            string path = $"/v1/orderbooks?symbol={tickerString}";
            var response = await HttpClientProvider.HttpClient.GetAsync(EndPoint + path);
            return await GmoApiResponse<OrderBookResponse>.FromResponseAsync(response);
        }

        /// <summary>Gets the trade history for the specified ticker symbol</summary>
        /// <summary xml:lang="ja">指定した銘柄の取引履歴を取得します</summary>
        /// <param name="ticker">The ticker symbol to get trades for</param>
        /// <param name="ticker" xml:lang="ja">取引履歴を取得する銘柄</param>
        /// <param name="page">Optional page number (defaults to 1)</param>
        /// <param name="page" xml:lang="ja">取得対象ページ（省略時は1）</param>
        /// <param name="count">Optional number of items per page (defaults to 100)</param>
        /// <param name="count" xml:lang="ja">1ページあたりの取得件数（省略時は100）</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation that returns a <see cref="GmoApiResponse{TradeResponse}"/> containing the trade history</returns>
        /// <returns xml:lang="ja">取引履歴を含む<see cref="GmoApiResponse{TradeResponse}"/>を返す非同期操作を表す<see cref="Task"/></returns>
        public async Task<GmoApiResponse<TradeResponse>> GetTradesAsync(
            Ticker ticker,
            int? page = null,
            int? count = null)
        {
            var tickerString = TickerService.ToString(ticker);
            var pathBuilder = new StringBuilder("/v1/trades?symbol=");
            pathBuilder.Append(tickerString);
            
            if (page.HasValue)
            {
                pathBuilder.Append("&page=").Append(page.Value);
            }
            
            if (count.HasValue)
            {
                pathBuilder.Append("&count=").Append(count.Value); 
            }
            
            var path = pathBuilder.ToString();

            var response = await HttpClientProvider.HttpClient.GetAsync(EndPoint + path);
            return await GmoApiResponse<TradeResponse>.FromResponseAsync(response);
        }

        /// <summary>Gets the candlestick/KLine data for the specified ticker symbol</summary>
        /// <summary xml:lang="ja">指定した銘柄のローソク足データを取得します</summary>
        /// <param name="ticker">The ticker symbol to get KLine data for</param>
        /// <param name="ticker" xml:lang="ja">ローソク足データを取得する銘柄</param>
        /// <param name="interval">The interval for KLine data</param>
        /// <param name="interval" xml:lang="ja">ローソク足の時間間隔</param>
        /// <param name="date">The date to get data for (format: YYYYMMDD or YYYY)</param>
        /// <param name="date" xml:lang="ja">データを取得する日付（形式：YYYYMMDD または YYYY）</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation that returns a <see cref="GmoApiResponse{T}"/> containing the KLine data</returns>
        /// <returns xml:lang="ja">ローソク足データを含む<see cref="GmoApiResponse{T}"/>を返す非同期操作を表す<see cref="Task"/></returns>
        public async Task<GmoApiResponse<IReadOnlyList<KLineEntry>>> GetKLinesAsync(
            Ticker ticker,
            KLineInterval interval,
            string date)
        {
            try
            {
                var tickerString = TickerService.ToString(ticker);
                var intervalString = interval.ToApiString();
                
                var pathBuilder = new StringBuilder("/v1/klines?symbol=")
                    .Append(tickerString)
                    .Append("&interval=")
                    .Append(intervalString)
                    .Append("&date=")
                    .Append(date);

                var response = await HttpClientProvider.HttpClient.GetAsync(EndPoint + pathBuilder);
                return await GmoApiResponse<IReadOnlyList<KLineEntry>>.FromResponseAsync(response);
            }
            catch (Exception ex)
            {
                var tickerString = TickerService.ToString(ticker);
                var intervalString = interval.ToApiString();
                var path = $"/v1/klines?symbol={tickerString}&interval={intervalString}&date={date}";
                Console.WriteLine($"Error occurred with path: {path}\n{ex}");
                throw;
            }
        }
    }
}