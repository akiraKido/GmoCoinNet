using System;
using Newtonsoft.Json;

namespace GmoCoinNet.Schema;

/// <summary>Response containing GMO Coin service status information</summary>
/// <summary xml:lang="ja">GMOコインのサービスステータス情報を含むレスポンス</summary>
public class StatusResponse(Status status)
{
    /// <summary>Current status of the GMO Coin service</summary>
    /// <summary xml:lang="ja">GMOコインサービスの現在のステータス</summary>
    [JsonProperty("status")] public readonly Status Status = status;

    /// <summary>Returns the string representation of the service status</summary>
    /// <summary xml:lang="ja">サービスステータスの文字列表現を返す</summary>
    public override string ToString() => StatusService.ToString(Status);
}

/// <summary>GMO Coin service status values</summary>
/// <summary xml:lang="ja">GMOコインサービスのステータス値</summary>
public enum Status
{
    /// <summary>Service is under maintenance</summary>
    /// <summary xml:lang="ja">サービスはメンテナンス中</summary>
    Maintenance,

    /// <summary>Service is preparing to open</summary>
    /// <summary xml:lang="ja">サービスは開始準備中</summary>
    PreOpen,

    /// <summary>Service is operating normally</summary>
    /// <summary xml:lang="ja">サービスは正常に稼働中</summary>
    Open
}

internal static class StatusService
{
    private const string Maintenance = "MAINTENANCE";
    private const string PreOpen = "PREOPEN";
    private const string Open = "OPEN";

    public static Status FromString(string value) => value switch
    {
        Maintenance => Status.Maintenance,
        PreOpen => Status.PreOpen,
        Open => Status.Open,
        _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Invalid status")
    };

    public static string ToString(Status value) => value switch
    {
        Status.Maintenance => Maintenance,
        Status.PreOpen => PreOpen,
        Status.Open => Open,
        _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Invalid status")
    };
    
    internal class StatusJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(Status);

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var status = (Status)(value ?? throw new ArgumentNullException(nameof(value)));
            writer.WriteValue(StatusService.ToString(status));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
            JsonSerializer serializer)
        {
            var stringValue = (string)(reader.Value ?? throw new ArgumentNullException(nameof(reader.Value)));
            return FromString(stringValue);
        }
    }
}