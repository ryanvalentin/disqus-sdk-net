using System.Collections.Generic;
using System.Threading.Tasks;
using Disqus.Core.Authentication;

namespace Disqus.Core.Services.Api
{
	public interface IDisqusApiFactory
	{
		Task<T> GetApiDataAsync<T>(string resource, string endpoint, List<KeyValuePair<string, string>> arguments = null);

		Task<T> PostApiDataAsync<T>(string resource, string endpoint, List<KeyValuePair<string, string>> arguments = null);

		void UpdateAuthentication(IDisqusAuthentication authentication);
	}
}
