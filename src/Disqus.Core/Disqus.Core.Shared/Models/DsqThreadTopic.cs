using Newtonsoft.Json;

namespace Disqus.Core
{
	public class DsqThreadTopic
	{
		[JsonProperty(PropertyName = "identifier")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "displayName")]
		public string DisplayName { get; set; }
	}
}
