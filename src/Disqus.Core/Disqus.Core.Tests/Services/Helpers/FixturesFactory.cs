using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Disqus.Core.Authentication;
using Disqus.Core.Models;
using Disqus.Core.Services.Api;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net;

namespace Disqus.Core.Tests.Services
{
	public enum AuthenticationMode
	{
		None,
		Authenticated,
		Self,
		Moderator
	}

	public class FixturesFactory : IDisqusApiFactory
	{
		private bool _throwErrors = false;
		private bool _useDisqusApiExceptions = false;
		private ApiFixtures _fixtures = new ApiFixtures();

		public FixturesFactory()
		{
		}

		public AuthenticationMode AuthMode { get; set; }

		public Task<T> GetApiDataAsync<T>(string resource, string endpoint, List<KeyValuePair<string, string>> arguments = null)
		{
			return GetResponse<T>(resource, endpoint, arguments);
		}

		public Task<T> PostApiDataAsync<T>(string resource, string endpoint, List<KeyValuePair<string, string>> arguments = null)
		{
			return GetResponse<T>(resource, endpoint, arguments);
		}

		private Task<T> GetResponse<T>(string resource, string endpoint, List<KeyValuePair<string, string>> arguments = null)
		{
			ThrowErrorIfSet();

			string concatArgs = "";
			if (arguments != null)
			{
				arguments.Sort((x, y) => String.Compare(x.Key, y.Key, StringComparison.InvariantCulture));
				concatArgs = "?" + String.Join("&", arguments.Select(kv => $"{kv.Key}={WebUtility.UrlEncode(kv.Value)}"));
			}

			string json = "{}";

			Console.WriteLine($"Looking up '{resource}' with endpoint '{endpoint}{concatArgs}'");

			switch (AuthMode)
			{
				case AuthenticationMode.None:
					json = _fixtures.NoAuthResources[resource][$"{endpoint}{concatArgs}"];
					break;
				case AuthenticationMode.Authenticated:
					json = _fixtures.AuthResources[resource][$"{endpoint}{concatArgs}"];
					break;
				case AuthenticationMode.Self:
					json = _fixtures.SelfResources[resource][$"{endpoint}{concatArgs}"];
					break;
				default: // case AuthenticationMode.Moderator:
					return Task.FromResult(default(T));
			}

			return Task.FromResult(JsonConvert.DeserializeObject<T>(json));
		}

		public void UpdateAuthentication(IDisqusAuthentication authentication)
		{
			if (String.IsNullOrEmpty(authentication.AuthenticationToken))
				AuthMode = AuthenticationMode.None;
			else if (authentication.AuthenticationToken == "self")
				AuthMode = AuthenticationMode.Self;
			else if (authentication.AuthenticationToken == "mod")
				AuthMode = AuthenticationMode.Moderator;
			else
				AuthMode = AuthenticationMode.Authenticated;
		}

		public void StartThrowingErrors(bool isUnhandled)
		{
			_throwErrors = true;
			_useDisqusApiExceptions = !isUnhandled;
		}

		public void StopThrowingErrors()
		{
			_throwErrors = false;
			_useDisqusApiExceptions = false;
		}

		private void ThrowErrorIfSet()
		{
			if (!_throwErrors)
				return;

			if (_useDisqusApiExceptions)
			{
				var data = JsonConvert.DeserializeObject<DsqApiResponse<string>>(ApiFixtures.API_ERROR);
				throw new DisqusApiException(data.Code, data.Response, 400);
			}
			else
			{
				throw new Exception("Test Exception");
			}
		}
	}
}
