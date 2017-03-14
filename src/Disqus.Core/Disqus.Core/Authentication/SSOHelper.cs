using System;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Disqus.Core.Helpers;

namespace Disqus.Core.Authentication
{
	internal static class SSOHelper
	{
		internal static string GeneratePayload(DisqusRemoteUser remoteUser, string apiSecret, TimeSpan? timestamp = null)
		{
			string userData = JsonConvert.SerializeObject(remoteUser);

			byte[] userDataAsBytes = userData.ToAsciiBytes();;

			// Base64 Encode the message
			string Message = Convert.ToBase64String(userDataAsBytes);

			// Get the proper timestamp
			TimeSpan ts = timestamp ?? DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
			
			string timestampStr = Convert.ToInt32(ts.TotalSeconds).ToString();

			// Convert the message + timestamp to bytes
			byte[] messageAndTimestampBytes = $"{Message} {timestampStr}".ToAsciiBytes();

			// Convert Disqus API key to HMAC-SHA1 signature
			byte[] apiBytes = apiSecret.ToAsciiBytes();
			HMACSHA1 hmac = new HMACSHA1(apiBytes);
			byte[] hashedMessage = hmac.ComputeHash(messageAndTimestampBytes);

			// Put it all together into the final payload
			return $"{Message} {hashedMessage.ToHexString().ToLowerInvariant()} {timestampStr}";
		}
	}
}
