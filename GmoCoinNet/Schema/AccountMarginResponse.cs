using System;
using Newtonsoft.Json;

namespace GmoCoinNet.Schema;

/// <summary>Response containing margin account information</summary>
/// <summary xml:lang="ja">証拠金口座情報を含むレスポンス</summary>
public class AccountMarginResponse
{
    /// <summary>Total valuation at market price</summary>
    /// <summary xml:lang="ja">時価評価総額</summary>
    [JsonProperty("actualProfitLoss")] public readonly decimal ActualProfitLoss;

    /// <summary>Available trading amount</summary>
    /// <summary xml:lang="ja">取引余力</summary>
    [JsonProperty("availableAmount")] public readonly decimal AvailableAmount;

    /// <summary>Required margin</summary>
    /// <summary xml:lang="ja">拘束証拠金</summary>
    [JsonProperty("margin")] public readonly decimal Margin;

    /// <summary>Margin call status</summary>
    /// <summary xml:lang="ja">追証ステータス</summary>
    [JsonProperty("marginCallStatus")] public readonly MarginCallStatus MarginCallStatus;

    /// <summary>Margin maintenance ratio</summary>
    /// <summary xml:lang="ja">証拠金維持率</summary>
    [JsonProperty("marginRatio")] public readonly decimal? MarginRatio;

    /// <summary>Unrealized profit/loss</summary>
    /// <summary xml:lang="ja">評価損益</summary>
    [JsonProperty("profitLoss")] public readonly decimal ProfitLoss;

    /// <summary>Transferable amount</summary>
    /// <summary xml:lang="ja">振替可能額</summary>
    [JsonProperty("transferableAmount")] public readonly decimal TransferableAmount;

    [JsonConstructor]
    internal AccountMarginResponse(
        [JsonProperty("actualProfitLoss")] string actualProfitLoss,
        [JsonProperty("availableAmount")] string availableAmount,
        [JsonProperty("margin")] string margin,
        [JsonProperty("marginCallStatus")] string marginCallStatus,
        [JsonProperty("marginRatio")] string? marginRatio,
        [JsonProperty("profitLoss")] string profitLoss,
        [JsonProperty("transferableAmount")] string transferableAmount)
    {
        ActualProfitLoss = decimal.Parse(actualProfitLoss);
        AvailableAmount = decimal.Parse(availableAmount);
        Margin = decimal.Parse(margin);
        MarginCallStatus = marginCallStatus switch
        {
            "NORMAL" => MarginCallStatus.Normal,
            "MARGIN_CALL" => MarginCallStatus.MarginCall,
            "LOSSCUT" => MarginCallStatus.LossCut,
            _ => throw new ArgumentException($"Invalid margin call status: {marginCallStatus}")
        };
        if (marginRatio is not null)
        {
            MarginRatio = decimal.Parse(marginRatio);
        }
        ProfitLoss = decimal.Parse(profitLoss);
        TransferableAmount = decimal.Parse(transferableAmount);
    }
}

/// <summary>Margin call status values</summary>
/// <summary xml:lang="ja">追証ステータスの値</summary>
public enum MarginCallStatus
{
    /// <summary>Normal status</summary>
    /// <summary xml:lang="ja">正常状態</summary>
    Normal,

    /// <summary>Margin call status</summary>
    /// <summary xml:lang="ja">追証発生状態</summary>
    MarginCall,

    /// <summary>Loss cut status</summary>
    /// <summary xml:lang="ja">ロスカット状態</summary>
    LossCut
} 