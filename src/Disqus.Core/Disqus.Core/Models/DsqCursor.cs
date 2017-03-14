using Newtonsoft.Json;

namespace Disqus.Core.Models
{
	public class DsqCursor
	{
		[JsonProperty(PropertyName = "hasNext")]
		public bool? HasNext { get; set; }

		[JsonProperty(PropertyName = "hasPrev")]
		public bool? HasPrev { get; set; }

		[JsonProperty(PropertyName = "next")]
		public string Next { get; set; }

		[JsonProperty(PropertyName = "prev")]
		public string Prev { get; set; }
	}
}
