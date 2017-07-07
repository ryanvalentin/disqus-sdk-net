using System;
using Disqus.Core.Helpers;
using Newtonsoft.Json;

namespace Disqus.Core
{
	public class DsqImageLinks
	{
		[JsonProperty(PropertyName = "permalink")]
		[JsonConverter(typeof(ProtocolRelativeLinkConverter), "https:")]
		public Uri Permalink { get; set; }

		[JsonProperty(PropertyName = "cache")]
		[JsonConverter(typeof(ProtocolRelativeLinkConverter), "https:")]
		public Uri Cache { get; set; }

		[JsonProperty(PropertyName = "large")]
		public DsqImageLinks Large { get; set; }

		[JsonProperty(PropertyName = "small")]
		public DsqImageLinks Small { get; set; }

		[JsonProperty(PropertyName = "isCustom")]
		public bool? IsCustom { get; set; }
	}
}
