namespace Disqus.Core.Authentication
{
	public class DisqusOAuthAuthentication : IDisqusAuthentication
	{
		private readonly string _apiKey;

		public DisqusOAuthAuthentication(string apiKey, string accessToken = null)
		{
			_apiKey = apiKey;
			AuthenticationToken = accessToken;
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
				return DisqusAuthenticationType.OAuth;
			}
		}
	}
}
