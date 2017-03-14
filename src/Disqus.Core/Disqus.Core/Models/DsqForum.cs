using System;
using Disqus.Core.Helpers;
using Newtonsoft.Json;

namespace Disqus.Core.Models
{
	public class DsqForum : IDsqIdentifiableModel
	{
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "category")]
		public string Category { get; set; }

		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

		[JsonProperty(PropertyName = "founder")]
		public string Founder { get; set; }

		[JsonProperty(PropertyName = "author")]
		[JsonConverter(typeof(DisqusObjectJsonConverter<DsqUser>))]
		public DsqUser Author { get; set; }

		[JsonProperty(PropertyName = "url")]
		[JsonConverter(typeof(ProtocolRelativeLinkConverter), "https:")]
		public Uri Url { get; set; }

		[JsonProperty(PropertyName = "guidelines")]
		public string Guidelines { get; set; }

		[JsonProperty(PropertyName = "raw_guidelines")]
		public string RawGuidelines { get; set; }

		[JsonProperty(PropertyName = "pk")]
		public string Pk { get; set; }

		[JsonProperty(PropertyName = "signedUrl")]
		public string SignedUrl { get; set; }

		[JsonProperty(PropertyName = "raw_description")]
		public string RawDescription { get; set; }

		[JsonProperty(PropertyName = "daysThreadAlive")]
		public int? DaysThreadAlive { get; set; }

		[JsonProperty(PropertyName = "numThreads")]
		public int? NumThreads { get; set; }

		[JsonProperty(PropertyName = "numModerators")]
		public int? NumModerators { get; set; }

		[JsonProperty(PropertyName = "numFollowers")]
		public int? NumFollowers { get; set; }

		[JsonProperty(PropertyName = "channel")]
		[JsonConverter(typeof(DisqusObjectJsonConverter<DsqChannel>))]
		public DsqChannel Channel { get; set; }

		[JsonProperty(PropertyName = "favicon")]
		public DsqImageLinks Favicon { get; set; }

		[JsonProperty(PropertyName = "settings")]
		public ForumSettings Settings { get; set; }

		[JsonProperty(PropertyName = "isFollowing")]
		// Requires attach=followsForum on requests
		public bool? IsFollowing { get; set; }

		public class ForumSettings
		{
			[JsonProperty(PropertyName = "hasCustomAvatar")]
			public bool HasCustomAvatar { get; set; }

			[JsonProperty(PropertyName = "allowAnonPost")]
			public bool AllowAnonPost { get; set; }

			[JsonProperty(PropertyName = "adsBannerEnabled")]
			public bool AdsBannerEnabled { get; set; }

			[JsonProperty(PropertyName = "supportLevel")]
			public int SupportLevel { get; set; }

			[JsonProperty(PropertyName = "allowMedia")]
			public bool AllowMedia { get; set; }

			[JsonProperty(PropertyName = "adsVideoEnabled")]
			public bool AdsVideoEnabled { get; set; }

			[JsonProperty(PropertyName = "disable3rdPartyTrackers")]
			public bool Disable3rdPartyTrackers { get; set; }

			[JsonProperty(PropertyName = "adsDRNativeEnabled")]
			public bool AdsDRNativeEnabled { get; set; }

			[JsonProperty(PropertyName = "allowAnonVotes")]
			public bool AllowAnonVotes { get; set; }

			[JsonProperty(PropertyName = "ssoRequired")]
			public bool SSORequired { get; set; }

			[JsonProperty(PropertyName = "mustVerify")]
			public bool MustVerify { get; set; }

			[JsonProperty(PropertyName = "discoveryLocked")]
			public bool DiscoveryLocked { get; set; }

			[JsonProperty(PropertyName = "adultContent")]
			public bool AdultContent { get; set; }

			[JsonProperty(PropertyName = "isVIP")]
			public bool IsVIP { get; set; }

			[JsonProperty(PropertyName = "mustVerifyEmail")]
			public bool MustVerifyEmail { get; set; }

			[JsonProperty(PropertyName = "unapproveLinks")]
			public bool UnapproveLinks { get; set; }

			[JsonProperty(PropertyName = "validateAllPosts")]
			public bool ValidateAllPosts { get; set; }
		}
	}
}
