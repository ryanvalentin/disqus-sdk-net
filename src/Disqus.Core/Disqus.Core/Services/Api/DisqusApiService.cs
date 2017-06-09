using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disqus.Core.Authentication;
using Disqus.Core.Helpers;
using Disqus.Core.Models;

namespace Disqus.Core.Services.Api
{
	public class DisqusApiService : IDisqusApiService
	{
		private readonly IDisqusApiFactory _factory;

		public DisqusApiService(IDisqusAuthentication authentication, Uri domain, IDisqusApiFactory factory = null)
		{
			_factory = factory ?? new DisqusApiFactory(authentication: authentication, domain: domain);
		}

		public void UpdateAuthentication(IDisqusAuthentication authentication)
		{
			_factory.UpdateAuthentication(authentication);
		}

		#region Threads

		public async Task<DsqApiResponse<DsqThread>> ThreadsDetailsAsync(
			string thread,
			bool includeForum = false,
			bool includeAuthor = false,
			string forum = "",
			bool attachTopics = false
			)
		{
			var arguments = new List<KeyValuePair<string, string>>()
			{
				{ new KeyValuePair<string, string>("thread", thread) },
			};

			if (includeForum)
				arguments.Add(new KeyValuePair<string, string>("related", "forum"));

			if (includeAuthor)
				arguments.Add(new KeyValuePair<string, string>("related", "author"));

			if (!String.IsNullOrEmpty(forum))
				arguments.Add(new KeyValuePair<string, string>("forum", forum));

			if (attachTopics)
				arguments.Add(new KeyValuePair<string, string>("attach", "topics"));

			return await _factory.GetApiDataAsync<DsqApiResponse<DsqThread>>("threads", "details", arguments);
		}

		public async Task<DsqApiResponse<DsqThread>> ThreadsDetailsAsync(Uri threadUri, string forum, bool includeForum = false, bool includeAuthor = false)
		{
			return await ThreadsDetailsAsync($"link:{threadUri.OriginalString}", includeForum, includeAuthor, forum);
		}

		public async Task<DsqApiResponse<DsqThread>> ThreadsDetailsAsync(string disqusIdentifier, string forum, bool includeForum = false, bool includeAuthor = false)
		{
			return await ThreadsDetailsAsync($"ident:{disqusIdentifier}", includeForum, includeAuthor, forum);
		}

		public async Task<DsqApiResponse<List<DsqThread>>> ThreadsSetAsync(
			IEnumerable<string> thread,
			bool includeForum = false,
			bool includeAuthor = false,
			string forum = ""
		    )
        {
            var arguments = new List<KeyValuePair<string, string>>(thread.Select(t => new KeyValuePair<string, string>("thread", t)));

			if (includeForum)
				arguments.Add(new KeyValuePair<string, string>("related", "forum"));

			if (includeAuthor)
				arguments.Add(new KeyValuePair<string, string>("related", "author"));

			if (!String.IsNullOrEmpty(forum))
				arguments.Add(new KeyValuePair<string, string>("forum", forum));

            return await _factory.GetApiDataAsync<DsqApiResponse<List<DsqThread>>>("threads", "set", arguments);
        }

		public async Task<DsqApiResponse<List<DsqThread>>> ThreadsSetAsync(IEnumerable<Uri> threadUri, string forum, bool includeForum = false, bool includeAuthor = false)
        {
            return await ThreadsSetAsync(threadUri.Select(t => $"link:{t.OriginalString}"), includeForum, includeAuthor, forum);
        }

        public async Task<DsqApiResponse<List<DsqThread>>> ThreadsSetAsync(IEnumerable<string> disqusIdentifier, string forum, bool includeForum = false, bool includeAuthor = false)
        {
            return await ThreadsSetAsync(disqusIdentifier.Select(t => $"ident:{t}"), includeForum, includeAuthor, forum);
        }

		public async Task<DsqApiResponse<List<DsqPost>>> ThreadsListPostsAsync(
			string thread,
			string cursor = "",
			DsqLimit? limit = null,
			DsqIncludePost include = DsqIncludePost.Approved | DsqIncludePost.Unapproved | DsqIncludePost.Highlighted,
			DsqSortOrder order = DsqSortOrder.Newest,
			string forum = "",
			PostAttachments attach = PostAttachments.None,
			string query = ""
			)
		{
			var arguments = new List<KeyValuePair<string, string>>()
			{
				{ new KeyValuePair<string, string>("thread", thread) },
				{ new KeyValuePair<string, string>("limit", (limit ?? new DsqLimit(25)).ToString()) },
				{ new KeyValuePair<string, string>("order", order.ToArgument()) },
				{ new KeyValuePair<string, string>("cursor", cursor) },
			};

			foreach (var a in attach.GetFlags().Where(f => f.ToArgument() != ""))
				if (attach.HasFlag(a))
					arguments.Add(new KeyValuePair<string, string>("attach", a.ToArgument()));

			foreach (var f in include.GetFlags())
				if (include.HasFlag(f))
					arguments.Add(new KeyValuePair<string, string>("include", f.ToArgument()));

			if (!String.IsNullOrWhiteSpace(forum))
				arguments.Add(new KeyValuePair<string, string>("forum", forum));

			if (!String.IsNullOrWhiteSpace(query))
				arguments.Add(new KeyValuePair<string, string>("query", query));

			return await _factory.GetApiDataAsync<DsqApiResponse<List<DsqPost>>>(resource: "threads", endpoint: "listPosts", arguments: arguments);
		}

		#endregion

		#region Users

		public async Task<DsqApiResponse<DsqUser>> UsersDetailsAsync()
		{
			return await _factory.GetApiDataAsync<DsqApiResponse<DsqUser>>("users", "details");
		}

		public async Task<DsqApiResponse<DsqUser>> UsersDetailsAsync(DsqUserArgument user)
		{
			var arguments = new List<KeyValuePair<string, string>>()
			{
				{ new KeyValuePair<string, string>("user", user.ToString()) },
			};

			return await _factory.GetApiDataAsync<DsqApiResponse<DsqUser>>("users", "details", arguments);
		}

		#endregion

		#region Forums

		public async Task<DsqApiResponse<DsqForum>> ForumsDetailsAsync(
			string forum,
			bool includeAuthor = false,
			ForumAttachments attach = ForumAttachments.None
			)
		{
			var arguments = new List<KeyValuePair<string, string>>()
			{
				{ new KeyValuePair<string, string>("forum", forum) },
			};

			if (includeAuthor)
				arguments.Add(new KeyValuePair<string, string>("related", "author"));

			foreach (var a in attach.GetFlags().Where(f => f.ToArgument() != ""))
			{
				if (attach.HasFlag(a))
					arguments.Add(new KeyValuePair<string, string>("attach", a.ToArgument()));
			}

			return await _factory.GetApiDataAsync<DsqApiResponse<DsqForum>>("forums", "details", arguments);
		}

		#endregion

		#region Posts

		public async Task<DsqApiResponse<List<DsqPost>>> PostsListChildrenAsync(
			string parentPost,
			string forum,
			DsqSortOrder order = DsqSortOrder.Oldest,
			DsqLimit? limit = null,
			bool includeForum = false,
			bool includeThread = false,
			DsqIncludePost include = DsqIncludePost.Approved | DsqIncludePost.Highlighted,
			PostAttachments attach = PostAttachments.None
			)
		{
			var arguments = new List<KeyValuePair<string, string>>()
			{
				{ new KeyValuePair<string, string>("parent_post", parentPost) },
				{ new KeyValuePair<string, string>("forum", forum) },
				{ new KeyValuePair<string, string>("limit", limit.ToString()) },
			 	{ new KeyValuePair<string, string>("order", order.ToArgument()) },
			};

			if (includeForum)
				arguments.Add(new KeyValuePair<string, string>("related", "forum"));

			if (includeThread)
				arguments.Add(new KeyValuePair<string, string>("related", "thread"));

			foreach (var a in attach.GetFlags().Where(f => f.ToArgument() != ""))
			{
				if (attach.HasFlag(a))
					arguments.Add(new KeyValuePair<string, string>("attach", a.ToArgument()));
			}

			return await _factory.GetApiDataAsync<DsqApiResponse<List<DsqPost>>>("posts", "list", arguments);
		}

		#endregion
	}
}
