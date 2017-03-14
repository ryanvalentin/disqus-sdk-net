using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Disqus.Core.Models;

namespace Disqus.Core.Services.Api
{
	public interface IDisqusApiService
	{
		Task<DsqApiResponse<DsqThread>> ThreadsDetailsAsync(
			string thread,
			bool includeForum = false,
			bool includeAuthor = false,
			string forum = "",
			bool attachTopics = false
		);

		Task<DsqApiResponse<DsqThread>> ThreadsDetailsAsync(Uri threadUri, string forum, bool includeForum = false, bool includeAuthor = false);

		Task<DsqApiResponse<DsqThread>> ThreadsDetailsAsync(string disqusIdentifier, string forum, bool includeForum = false, bool includeAuthor = false);

		Task<DsqApiResponse<List<DsqPost>>> ThreadsListPostsAsync(
			string thread,
			string cursor = "",
			DsqLimit? limit = null,
			DsqIncludePost include = DsqIncludePost.Approved | DsqIncludePost.Highlighted,
			DsqSortOrder order = DsqSortOrder.Newest,
			string forum = "",
			string query = ""
		);

		Task<DsqApiResponse<DsqUser>> UsersDetailsAsync();

		Task<DsqApiResponse<DsqUser>> UsersDetailsAsync(DsqUserArgument user);

		Task<DsqApiResponse<DsqForum>> ForumsDetailsAsync(
			string forum,
			bool includeAuthor = false,
			ForumAttachments attach = ForumAttachments.None
		);
	}
}
