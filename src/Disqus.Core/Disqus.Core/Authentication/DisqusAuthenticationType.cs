using Disqus.Core.Helpers;

namespace Disqus.Core.Authentication
{
	public enum DisqusAuthenticationType
	{
		[ArgumentValue("access_token")]
		OAuth,

		[ArgumentValue("remote_auth")]
		RemoteAuth,
	}
}