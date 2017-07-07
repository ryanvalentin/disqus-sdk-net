using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Disqus.Core.Authentication;
using Disqus.Core.Helpers;
using Disqus.Core.Models;
using Newtonsoft.Json;

namespace Disqus.Core.Services.Api
{
	public class DisqusApiFactory : IDisqusApiFactory, IDisposable
	{
		private readonly HttpClient _client;

		private const string USER_AGENT = "Disqus SDK for .NET";
		private const string API_BASE = "https://disqus.com/api/3.0/";

		private IDisqusAuthentication _authentication;

		internal DisqusApiFactory(IDisqusAuthentication authentication, Uri domain)
		{
			UpdateAuthentication(authentication);

			var handler = new HttpClientHandler();
			if (handler.SupportsAutomaticDecompression)
				handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
			
			_client = new HttpClient(handler);
			_client.DefaultRequestHeaders.Referrer = domain;
			_client.DefaultRequestHeaders.Host = domain.DnsSafeHost;
			_client.DefaultRequestHeaders.UserAgent.ParseAdd(USER_AGENT);
			_client.DefaultRequestHeaders.Add("Origin", domain.OriginalString);
		}

		public void UpdateAuthentication(IDisqusAuthentication authentication)
		{
			_authentication = authentication;
		}

		public async Task<T> GetApiDataAsync<T>(string resource, string endpoint, List<KeyValuePair<string, string>> arguments = null)
		{
			return await SendRequestAsync<T>(method: HttpMethod.Get, resource: resource, endpoint: endpoint, queryParams: arguments);
		}

		public async Task<T> PostApiDataAsync<T>(string resource, string endpoint, List<KeyValuePair<string, string>> arguments = null)
		{
			var content = new FormUrlEncodedContent(arguments);

			return await SendRequestAsync<T>(method: HttpMethod.Post, resource: resource, endpoint: endpoint, content: content);
		}

        private async Task<T> SendRequestAsync<T>(
			HttpMethod method, 
			string resource, 
			string endpoint, 
			List<KeyValuePair<string, string>> queryParams = null, 
			FormUrlEncodedContent content = null
		)
		{
			string query = $"api_key={_authentication.ApiKey}&{_authentication.AuthType.ToArgument()}={_authentication.AuthenticationToken}";
			if (queryParams != null)
				query += "&" + String.Join("&", queryParams.Select(kv => $"{kv.Key}={WebUtility.UrlEncode(kv.Value)}"));

			Uri url = new Uri($"{API_BASE}{resource}/{endpoint}.json?{query}");

			var response = await _client.SendAsync(new HttpRequestMessage()
			{
				RequestUri = url,
				Content = content,
				Method = method
			});

			if (!response.IsSuccessStatusCode)
				throw await GetExceptionResponseAsync(response);

			return await DeserializeResponseMessageAsync<T>(response);
		}

		private async Task<DisqusApiException> GetExceptionResponseAsync(HttpResponseMessage message)
		{
			try
			{
				var errorResponse = await DeserializeResponseMessageAsync<DsqApiResponse<string>>(message);

				return new DisqusApiException(errorResponse.Code, errorResponse.Response, (int)(message.StatusCode));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private async Task<T> DeserializeResponseMessageAsync<T>(HttpResponseMessage message)
		{
			using (var messageStream = await message.Content.ReadAsStreamAsync())
			{
				return await DeserializeStreamAsync<T>(messageStream);
			}
		}

		private async Task<T> DeserializeStreamAsync<T>(Stream stream)
		{
			return await Task.Run(() =>
			{
				using (StreamReader sr = new StreamReader(stream))
				{
					using (JsonReader reader = new JsonTextReader(sr))
					{
						JsonSerializer serializer = new JsonSerializer();

						try
						{
							return serializer.Deserialize<T>(reader);
						}
						catch (JsonReaderException ex)
						{
							throw ex;
						}
					}
				}
			});
		}

		public void Dispose()
		{
			_client.Dispose();
		}
	}
}
