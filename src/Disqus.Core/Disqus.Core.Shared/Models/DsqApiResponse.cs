using Newtonsoft.Json;

namespace Disqus.Core.Models
{
	public class DsqApiResponse<T>
	{
		[JsonProperty(PropertyName = "response")]
		public T Response { get; set; }

		[JsonProperty(PropertyName = "code")]
		public int? Code { get; set; }

		[JsonProperty(PropertyName = "cursor")]
		public DsqCursor Cursor { get; set; }
	}
}
