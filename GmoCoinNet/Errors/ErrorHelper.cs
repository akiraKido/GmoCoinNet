using System;
using System.Net;

namespace GmoCoinNet.Errors;

internal static class ErrorHelper
{
    public static GmoCoinNetworkException NetworkException(HttpStatusCode statusCode)
        => GmoCoinNetworkException.FromHttpStatusCode(statusCode);

    public static GmoCoinSerializationException SerializationException(string reason)
    {
        var messageFormat = ErrorMessages.ResourceManager.GetString("DeserializationFailed", ErrorMessages.Culture);
        if (messageFormat == null)
        {
            throw new InvalidOperationException("Could not find resource string for key 'DeserializationFailed'.");
        }
        
        var messageWithArgs = string.Format(messageFormat, reason);
        return new GmoCoinSerializationException(messageWithArgs);
    }
}