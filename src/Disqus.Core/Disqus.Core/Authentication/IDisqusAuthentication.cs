namespace Disqus.Core.Authentication
{
	public interface IDisqusAuthentication
	{
		string AuthenticationToken { get; }

		string ApiKey { get; }

		DisqusAuthenticationType AuthType { get; }
	}
}
