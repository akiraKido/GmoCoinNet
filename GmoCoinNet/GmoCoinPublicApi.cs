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
        /// <param name="endPoint" xml:lang="ja">カスタムAPIエンドポイントURL（オプション）。nullの場合、デフォルトのエンドポイントが使用されます。</param>
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
        public async Task<GmoApiResponse<StatusResponse>> StatusAsync()
        {
            string path = "/v1/status";
            var response = await HttpClientProvider.HttpClient.GetAsync(EndPoint + path);
            return await GmoApiResponse<StatusResponse>.FromResponseAsync(response);
        }
    }
}