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
using System.IO;
using NUnit.Framework;

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
			if (_throwErrors)
				throw new Exception("Test Exception");

			string argKey = $"api_key=public&access_token={AuthMode.ToString().ToLowerInvariant()}";
			if (arguments != null)
			{
				arguments.Sort((x, y) => String.Compare(x.Key, y.Key, StringComparison.InvariantCulture));
				argKey += "&" + String.Join("&", arguments.Select(kv => $"{kv.Key}={WebUtility.UrlEncode(kv.Value)}"));
			}

			string path = Path.Combine(TestContext.CurrentContext.TestDirectory, $"Fixtures/Data/{resource}/{endpoint}.json");
			string jsonStr = File.ReadAllText(path);

			Console.WriteLine($"Looking up '{resource}' with endpoint '{endpoint} and key '{argKey}'");

			var json = JObject.Parse(jsonStr);

			if ((int)json[argKey]["code"] > 0)
			{
				var errorJson = json[argKey].ToObject<DsqApiResponse<string>>();
				throw new DisqusApiException(errorJson.Code, errorJson.Response, 400);
			}

			var deserialized = json[argKey].ToObject<T>();

			return Task.FromResult(deserialized);
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

		public void StartThrowingErrors()
		{
			_throwErrors = true;
		}

		public void StopThrowingErrors()
		{
			_throwErrors = false;
		}
	}
}
