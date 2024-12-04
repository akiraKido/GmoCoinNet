using System;

namespace GmoCoinNet.Errors;

/// <summary>Exception thrown when GMO Coin API response deserialization fails</summary>
/// <summary xml:lang="ja">GMOコインAPIのレスポンスのデシリアライズに失敗した場合にスローされる例外</summary>
public class GmoCoinSerializationException : Exception
{
    internal GmoCoinSerializationException(string message) : base(message)
    {
    }
}