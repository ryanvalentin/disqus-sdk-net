using System;

namespace Disqus.Core.Authentication
{
	public class DisqusRemoteAuthentication : IDisqusAuthentication
	{
		private readonly string _apiKey;

		public readonly DisqusRemoteUser RemoteUser;

		public DisqusRemoteAuthentication(string apiKey, string apiSecret, DisqusRemoteUser remoteUser, TimeSpan? timestamp = null)
		{
			_apiKey = apiKey;

			RemoteUser = remoteUser;

			AuthenticationToken = SSOHelper.GeneratePayload(remoteUser, apiSecret, timestamp);
		}

		public string AuthenticationToken
		{
			get;
			private set;
		}

		public string ApiKey
		{
			get
			{
				return _apiKey;
			}
		}

		public DisqusAuthenticationType AuthType
		{
			get
			{
				return DisqusAuthenticationType.RemoteAuth;
			}
		}
	}
}
