using System;
using Disqus.Core.Helpers;
using Newtonsoft.Json;

namespace Disqus.Core.Models
{
	public class DsqUser : IDsqIdentifiableModel
	{
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "username")]
		public string Username { get; set; }

		[JsonProperty(PropertyName = "about")]
		public string About { get; set; }

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "email")]
		public string Email { get; set; }

		[JsonProperty(PropertyName = "url")]
		[JsonConverter(typeof(ProtocolRelativeLinkConverter), "https:")]
		public Uri Url { get; set; }

		[JsonProperty(PropertyName = "signedUrl")]
		[JsonConverter(typeof(ProtocolRelativeLinkConverter), "https:")]
		public Uri SignedUrl { get; set; }

		[JsonProperty(PropertyName = "profileUrl")]
		[JsonConverter(typeof(ProtocolRelativeLinkConverter), "https:")]
		public Uri ProfileUrl { get; set; }

		[JsonProperty(PropertyName = "location")]
		public string Location { get; set; }

		[JsonProperty(PropertyName = "isAnonymous")]
		public bool IsAnonymous { get; set; }

		[JsonProperty(PropertyName = "isPowerContributor")]
		public bool? IsPowerContributor { get; set; }

		[JsonProperty(PropertyName = "isFollowing")]
		public bool? IsFollowing { get; set; }

		[JsonProperty(PropertyName = "isFollowedBy")]
		public bool? IsFollowedBy { get; set; }

		[JsonProperty(PropertyName = "reputation")]
		public double Reputation { get; set; }

		[JsonProperty(PropertyName = "isPrivate")]
		public bool IsPrivate { get; set; }

		[JsonProperty(PropertyName = "isBlocked")]
		public bool? IsBlocked { get; set; }

		[JsonProperty(PropertyName = "isVerified")]
		public bool? IsVerified { get; set; }

		[JsonProperty(PropertyName = "joinedAt")]
		public DateTime? JoinedAt { get; set; }

		[JsonProperty(PropertyName = "avatar")]
		public DsqImageLinks Avatars { get; set; }

		[JsonProperty(PropertyName = "numPosts")]
		public int? NumPosts { get; set; }

		[JsonProperty(PropertyName = "numLikesReceived")]
		public int? NumLikesReceived { get; set; }

		[JsonProperty(PropertyName = "numFollowers")]
		public int? NumFollowers { get; set; }

		[JsonProperty(PropertyName = "numFollowing")]
		public int? NumFollowing { get; set; }

		[JsonProperty(PropertyName = "numForumsFollowing")]
		public int? NumForumsFollowing { get; set; }

		[JsonProperty(PropertyName = "disable3rdPartyTrackers")]
		public bool Disable3rdPartyTrackers { get; set; }

		[JsonProperty(PropertyName = "homeFeedOnboardingComplete")]
		public bool HomeFeedOnboardingComplete { get; set; }
	}
}
