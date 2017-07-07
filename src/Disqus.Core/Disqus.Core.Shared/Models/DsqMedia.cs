using System;
using Disqus.Core.Helpers;
using Newtonsoft.Json;

namespace Disqus.Core.Models
{
	public class DsqMedia
	{
		[JsonProperty(PropertyName = "thumbnailUrl")]
		[JsonConverter(typeof(ProtocolRelativeLinkConverter), "https:")]
		public Uri ThumbnailUrl { get; set; }

		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

		[JsonProperty(PropertyName = "mediaType")]
		public string MediaType { get; set; }

		[JsonProperty(PropertyName = "url")]
		[JsonConverter(typeof(ProtocolRelativeLinkConverter), "https:")]
		public Uri Url { get; set; }

		[JsonProperty(PropertyName = "resolvedUrl")]
		[JsonConverter(typeof(ProtocolRelativeLinkConverter), "https:")]
		public Uri ResolvedUrl { get; set; }

		[JsonProperty(PropertyName = "location")]
		[JsonConverter(typeof(ProtocolRelativeLinkConverter), "https:")]
		public Uri Location { get; set; }

		[JsonProperty(PropertyName = "html")]
		public string Html { get; set; }

		[JsonProperty(PropertyName = "htmlHeight")]
		public int? HtmlHeight { get; set; }

		[JsonProperty(PropertyName = "htmlWidth")]
		public int? HtmlWidth { get; set; }

		[JsonProperty(PropertyName = "providerName")]
		public string ProviderName { get; set; }
	}
}
