using System;
using Disqus.Core.Helpers;

namespace Disqus.Core.Services.Api
{
	public enum OAuthGrantType
	{
		[ArgumentValue("password")]
		Password,

		[ArgumentValue("refresh_token")]
		Refresh,

		[ArgumentValue("urn:disqus:params:oauth:grant-type:facebook-login")]
		Facebook,

		[ArgumentValue("urn:disqus:params:oauth:grant-type:twitter-login")]
		Twitter,

		[ArgumentValue("urn:disqus:params:oauth:grant-type:google-login")]
		Google,
	}

	[Flags]
	public enum OAuthScopes
	{
		[ArgumentValue("read")]
		Read = 0x01,

		[ArgumentValue("write")]
		Write = 0x02,

		[ArgumentValue("email")]
		Email = 0x04,

		[ArgumentValue("admin")]
		Admin = 0x08,
	}

	public enum DsqSortOrder
	{
		[ArgumentValue("asc")]
		Oldest,

		[ArgumentValue("desc")]
		Newest
	}

	public enum DsqThreadedPostSortOrder
	{
		[ArgumentValue("popular")]
		Best,

		[ArgumentValue("desc")]
		Newest,

		[ArgumentValue("asc")]
		Oldest
	}

	public enum DsqVoteType
	{
		[ArgumentValue("1")]
		Upvote = 1,

		[ArgumentValue("0")]
		Neutralvote = 0,

		[ArgumentValue("-1")]
		Downvote = -1
	}

	public enum DsqFavoriteType
	{
		[ArgumentValue("1")]
		Favorite = 1,

		[ArgumentValue("0")]
		Unfavorite = 0
	}

	[Flags]
	public enum DsqIncludeThread
	{
		[ArgumentValue("open")]
		Open = 0x01,

		[ArgumentValue("closed")]
		Closed = 0x02,

		[ArgumentValue("killed")]
		Deleted = 0x04
	}

	[Flags]
	public enum DsqIncludePost
	{
		[ArgumentValue("approved")]
		Approved = 0x01,

		[ArgumentValue("unapproved")]
		Unapproved = 0x02,

		[ArgumentValue("spam")]
		Spam = 0x04,

		[ArgumentValue("deleted")]
		Deleted = 0x08,

		[ArgumentValue("flagged")]
		Flagged = 0x016,

		[ArgumentValue("highlighted")]
		Highlighted = 0x032
	}

	public enum DsqChannelsSortField
	{
		[ArgumentValue("id")]
		Id,

		[ArgumentValue("slug")]
		Slug,

		[ArgumentValue("name")]
		Name,

		[ArgumentValue("date_added")]
		DateAdded
	}

	[Flags]
	public enum ForumAttachments
	{
		[ArgumentValue("")]
		None = 0x01,

		[ArgumentValue("followsForum")]
		FollowsForum = 0x02,

		[ArgumentValue("counters")]
		Counters = 0x04,

		[ArgumentValue("forumIntegration")]
		ForumIntegration = 0x08,

		[ArgumentValue("forumDaysAlive")]
		ForumDaysAlive = 0x016,

		[ArgumentValue("forumForumCategory")]
		ForumForumCategory = 0x032,

		[ArgumentValue("channel_categories")]
		ChannelCategories = 0x064 // Only works with Channel list API calls
	}

	public enum DsqUserArgumentType
	{
		[ArgumentValue("username:")]
		Username,

		[ArgumentValue("email:")]
		Email,

		[ArgumentValue("")]
		UserId
	}
}