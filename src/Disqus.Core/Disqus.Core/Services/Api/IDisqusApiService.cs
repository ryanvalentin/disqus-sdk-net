using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Disqus.Core.Models;

namespace Disqus.Core.Services.Api
{
	public interface IDisqusApiService
	{
		/// <summary>
		/// Returns thread details.
		/// </summary>
		/// <returns><see cref="Task{DsqApiResponse{DsqThread}}"/></returns>
		/// <param name="thread">Looks up a thread by ID</param>
		/// <param name="includeForum">If set to <c>true</c> include forum details.</param>
		/// <param name="includeAuthor">If set to <c>true</c> include author details.</param>
		/// <param name="forum">Looks up a forum by ID (aka short name).</param>
		/// <param name="attachTopics">If set to <c>true</c> attach topics.</param>
		/// <exception cref="DisqusApiException">Thrown when the Disqus API responds with an error.</exception>
		Task<DsqApiResponse<DsqThread>> ThreadsDetailsAsync(
			string thread,
			bool includeForum = false,
			bool includeAuthor = false,
			string forum = "",
			bool attachTopics = false
		);

		/// <summary>
		/// Returns thread details.
		/// </summary>
		/// <returns><see cref="Task{DsqApiResponse{DsqThread}}"/></returns>
		/// <param name="threadUri">The URL associated with the thread.</param>
		/// <param name="forum">Looks up a forum by ID (aka short name).</param>
		/// <param name="includeForum">If set to <c>true</c> include forum details.</param>
		/// <param name="includeAuthor">If set to <c>true</c> include author details.</param>
		/// <exception cref="DisqusApiException">Thrown when the Disqus API responds with an error.</exception>
		Task<DsqApiResponse<DsqThread>> ThreadsDetailsAsync(Uri threadUri, string forum, bool includeForum = false, bool includeAuthor = false);

		/// <summary>
		/// Returns thread details.
		/// </summary>
		/// <returns><see cref="Task{DsqApiResponse{DsqThread}}"/></returns>
		/// <param name="disqusIdentifier">The custom disqus_identifier associated with the thread.</param>
		/// <param name="forum">Looks up a forum by ID (aka short name).</param>
		/// <param name="includeForum">If set to <c>true</c> include forum details.</param>
		/// <param name="includeAuthor">If set to <c>true</c> include author details.</param>
		/// <exception cref="DisqusApiException">Thrown when the Disqus API responds with an error.</exception>
		Task<DsqApiResponse<DsqThread>> ThreadsDetailsAsync(string disqusIdentifier, string forum, bool includeForum = false, bool includeAuthor = false);

		/// <summary>
		/// Returns a list of posts within a thread.
		/// </summary>
		/// <returns><see cref="Task{DsqApiResponse{List{DsqPost}}}"/></returns>
		/// <param name="thread">Looks up a thread by ID.</param>
		/// <param name="cursor">The pagination ID.</param>
		/// <param name="limit">The number of posts returned in each call.</param>
		/// <param name="include">Specify which post states to include.</param>
		/// <param name="order">The sort order of returned posts.</param>
		/// <param name="forum">Looks up a forum by ID (aka short name).</param>
		/// <param name="attach">Specifies metadata attachments on returned posts.</param>
		/// <param name="query">The search query to filter posts by.</param>
		/// <exception cref="DisqusApiException">Thrown when the Disqus API responds with an error.</exception>
		Task<DsqApiResponse<List<DsqPost>>> ThreadsListPostsAsync(
			string thread,
			string cursor = "",
			DsqLimit? limit = null,
			DsqIncludePost include = DsqIncludePost.Approved | DsqIncludePost.Highlighted,
			DsqSortOrder order = DsqSortOrder.Newest,
			string forum = "",
			PostAttachments attach = PostAttachments.None,
			string query = ""
		);

		/// <summary>
		/// Returns details of the authenticated user.
		/// </summary>
		/// <returns><see cref="Task{DsqApiResponse{DsqUser}}"/></returns>
		/// <exception cref="DisqusApiException">Thrown when the Disqus API responds with an error.</exception>
		Task<DsqApiResponse<DsqUser>> UsersDetailsAsync();

		/// <summary>
		/// Userses the details async.
		/// </summary>
		/// <returns><see cref="Task{DsqApiResponse{DsqUser}}"/></returns>
		/// <param name="user">Looks up a user by ID, username, or email. <see cref="DsqUserArgument"/></param>
		/// <exception cref="DisqusApiException">Thrown when the Disqus API responds with an error.</exception>
		Task<DsqApiResponse<DsqUser>> UsersDetailsAsync(DsqUserArgument user);

		/// <summary>
		/// Returns forum details.
		/// </summary>
		/// <returns><see cref="Task{DsqApiResponse{DsqForum}}"/></returns>
		/// <param name="forum">Looks up a forum by ID (aka short name)</param>
		/// <param name="includeAuthor">If set to <c>true</c> include author details <see cref="DsqUser"/>.</param>
		/// <param name="attach">Specifies metadata attachments on returned forum.</param>
		/// <exception cref="DisqusApiException">Thrown when the Disqus API responds with an error.</exception>
		Task<DsqApiResponse<DsqForum>> ForumsDetailsAsync(
			string forum,
			bool includeAuthor = false,
			ForumAttachments attach = ForumAttachments.None
		);
	}
}
