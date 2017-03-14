using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Disqus.Core.Models
{
	public class DsqBaseMessageModel
	{
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "createdAt")]
		public DateTime? CreatedAt { get; set; }

		[JsonProperty(PropertyName = "userScore")]
		public int? UserScore { get; set; }

		[JsonProperty(PropertyName = "author")]
		[JsonConverter(typeof(DisqusObjectJsonConverter<DsqUser>))]
		public DsqUser Author { get; set; }

		[JsonProperty(PropertyName = "isSpam")]
		public bool IsSpam { get; set; }

		[JsonProperty(PropertyName = "isDeleted")]
		public bool IsDeleted { get; set; }

		[JsonProperty(PropertyName = "media")]
		public List<DsqMedia> Media { get; set; }

		[JsonProperty(PropertyName = "message")]
		public string Message { get; set; }

		[JsonProperty(PropertyName = "raw_message")]
		public string RawMessage { get; set; }

		[JsonProperty(PropertyName = "forum")]
		[JsonConverter(typeof(DisqusObjectJsonConverter<DsqForum>))]
		public DsqForum Forum { get; set; }

		[JsonProperty(PropertyName = "likes")]
		public int? Likes { get; set; }

		[JsonProperty(PropertyName = "dislikes")]
		public int? Dislikes { get; set; }
	}
}
