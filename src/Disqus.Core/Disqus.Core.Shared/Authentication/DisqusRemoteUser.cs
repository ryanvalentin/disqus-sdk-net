using System;
using Newtonsoft.Json;

namespace Disqus.Core.Authentication
{
	public class DisqusRemoteUser
	{
		public DisqusRemoteUser(string id, string name, string email, Uri avatar = null, Uri url = null)
		{
			Id = id;
			Name = name;
			Email = email;
			Avatar = avatar;
			Url = url;
		}

		[JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
		public string Id { get; set; }

		[JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
		public string Name { get; set; }

		[JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
		public string Email { get; set; }

		[JsonProperty("avatar", NullValueHandling = NullValueHandling.Ignore)]
		public Uri Avatar { get; set; }

		[JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
		public Uri Url { get; set; }
	}
}
