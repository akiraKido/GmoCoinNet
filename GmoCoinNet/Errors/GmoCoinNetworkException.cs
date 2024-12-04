using System;
using System.Net;

namespace GmoCoinNet.Errors;

/// <summary>Exception thrown when a GMO Coin API network request fails</summary>
/// <summary xml:lang="ja">GMOコインAPIのネットワークリクエストが失敗した場合にスローされる例外</summary>
public class GmoCoinNetworkException : Exception
{
    private GmoCoinNetworkException(string message) : base(message)
    {
    }

    internal static GmoCoinNetworkException FromHttpStatusCode(HttpStatusCode statusCode)
    {
        var messageFormat = ErrorMessages.ResourceManager.GetString("RequestFailedWithStatusCode", ErrorMessages.Culture);
        if (messageFormat == null)
        {
            throw new InvalidOperationException("Could not find resource string.");
        }
        
        var message = string.Format(messageFormat, statusCode);
        return new GmoCoinNetworkException(message);
    }
}