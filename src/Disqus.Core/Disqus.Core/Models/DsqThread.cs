using System;
using System.Collections.Generic;
using Disqus.Core.Helpers;
using Newtonsoft.Json;

namespace Disqus.Core.Models
{
	public class DsqThread : DsqBaseMessageModel, IDsqIdentifiableModel
	{
		[JsonProperty(PropertyName = "canModerate")]
		public bool CanModerate { get; set; }

		[JsonProperty(PropertyName = "clean_title")]
		public string CleanTitle { get; set; }

		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "highlightedPost")]
		[JsonConverter(typeof(DisqusObjectJsonConverter<DsqPost>))]
		public DsqPost HighlightedPost { get; set; }

		[JsonProperty(PropertyName = "identifiers")]
		public List<string> Identifiers { get; set; }

		[JsonProperty(PropertyName = "slug")]
		public string Slug { get; set; }

		[JsonProperty(PropertyName = "link")]
		[JsonConverter(typeof(ProtocolRelativeLinkConverter), "https:")]
		public Uri Link { get; set; }

		[JsonProperty(PropertyName = "signedLink")]
		[JsonConverter(typeof(ProtocolRelativeLinkConverter), "https:")]
		public Uri SignedLink { get; set; }

		[JsonProperty(PropertyName = "posts")]
		public int? Posts { get; set; }

		[JsonProperty(PropertyName = "isClosed")]
		public bool? IsClosed { get; set; }

		[JsonProperty(PropertyName = "topics")]
		public List<DsqThreadTopic> Topics { get; set; }
	}
}
