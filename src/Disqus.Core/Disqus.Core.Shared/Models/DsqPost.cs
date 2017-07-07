using System;
using System.ComponentModel;
using Disqus.Core.Helpers;
using Newtonsoft.Json;

namespace Disqus.Core.Models
{
	public class DsqPost : DsqBaseMessageModel, IDsqIdentifiableModel
	{
		[JsonProperty(PropertyName = "isHighlighted")]
		public bool? IsHighlighted { get; set; }

		[JsonProperty(PropertyName = "numReports")]
		public int? NumReports { get; set; }

		[JsonProperty(PropertyName = "parent")]
		public long? Parent { get; set; }

		[JsonProperty(PropertyName = "isApproved", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(true)]
		public bool IsApproved { get; set; }

		[JsonProperty(PropertyName = "isDeletedByAuthor")]
		public bool IsDeletedByAuthor { get; set; }

		[JsonProperty(PropertyName = "isFlagged")]
		public bool IsFlagged { get; set; }

		[JsonProperty(PropertyName = "parent_post")]
		public DsqPost ParentPost { get; set; }

		[JsonProperty(PropertyName = "thread")]
		[JsonConverter(typeof(DisqusObjectJsonConverter<DsqThread>))]
		public DsqThread Thread { get; set; }

		[JsonProperty(PropertyName = "url")]
		[JsonConverter(typeof(ProtocolRelativeLinkConverter), "https:")]
		public Uri Url { get; set; }

		[JsonProperty(PropertyName = "isEdited")]
		public bool IsEdited { get; set; }

		[JsonProperty(PropertyName = "depth")]
		public int? Depth { get; set; }

		[JsonProperty(PropertyName = "points")]
		public int Points { get; set; }

		[JsonProperty(PropertyName = "canVote")]
		public bool CanVote { get; set; }

		[JsonProperty(PropertyName = "hasChildren")]
		public bool? HasChildren { get; set; }
	}
}
