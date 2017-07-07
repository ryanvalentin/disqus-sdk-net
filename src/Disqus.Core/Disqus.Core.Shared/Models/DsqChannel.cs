using System;
using System.Collections.Generic;
using Disqus.Core.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Disqus.Core.Models
{
	public class DsqChannel : IDsqIdentifiableModel
	{
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "isCategory")]
		public bool IsCategory { get; set; }

		[JsonProperty(PropertyName = "dateAdded")]
		public DateTime DateAdded { get; set; }

		[JsonProperty(PropertyName = "primaryForum")]
		public DsqForum PrimaryForum { get; set; }

		[JsonProperty(PropertyName = "isAggregation")]
		public bool IsAggregation { get; set; }

		[JsonProperty(PropertyName = "avatar")]
		[JsonConverter(typeof(ProtocolRelativeLinkConverter), "https:")]
		public Uri Avatar { get; set; }

		[JsonProperty(PropertyName = "options")]
		public Dictionary<string, JToken> Options { get; set; }

		[JsonProperty(PropertyName = "hidden")]
		public bool Hidden { get; set; }

		[JsonProperty(PropertyName = "adminOnly")]
		public bool AdminOnly { get; set; }

		[JsonProperty(PropertyName = "bannerColor")]
		public string BannerColor { get; set; }

		[JsonProperty(PropertyName = "enableCuration")]
		public bool EnableCuration { get; set; }

		[JsonProperty(PropertyName = "bannerColorHex")]
		public string BannerColorHex { get; set; }

		[JsonProperty(PropertyName = "banner")]
		[JsonConverter(typeof(ProtocolRelativeLinkConverter), "https:")]
		public Uri Banner { get; set; }

		[JsonProperty(PropertyName = "slug")]
		public string Slug { get; set; }

		[JsonProperty(PropertyName = "ownerId")]
		public string OwnerId { get; set; }
	}
}
