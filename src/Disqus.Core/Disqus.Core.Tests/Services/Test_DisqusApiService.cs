using System;
using System.Linq;
using System.Threading.Tasks;
using Disqus.Core.Authentication;
using Disqus.Core.Services.Api;
using NUnit.Framework;

namespace Disqus.Core.Tests.Services
{
	[TestFixture]
	public class Test_DisqusApiService
	{
		private const string TEST_API_KEY = "hDuMtiXLQn5TarhIlbB9Q8hpYYvDRS2QPa64U31QIi1DVu5pB4epANLFQeey4HIB";
		private const string TEST_ACCESS_TOKEN = "5662bb7b415e4cd0bc05b26c40e23e97";

		private FixturesFactory _factory;
		private DisqusApiService _apiService;

		public Test_DisqusApiService()
		{
			_factory = new FixturesFactory();
		}

		[SetUp]
		public void SetupApiService()
		{
			// Add anything that should happen before each test
			var auth = new DisqusOAuthAuthentication(TEST_API_KEY);

			_apiService = new DisqusApiService(auth, new Uri("https://disqus.com/"), _factory);
		}

		[TearDown]
		public void ResetApiService()
		{
			// Add anything that should happen after each test

			_apiService.UpdateAuthentication(new DisqusOAuthAuthentication(TEST_API_KEY));

			_factory.StopThrowingErrors();
		}

		[TestCase]
		public void Test_HandledError()
		{
			DisqusApiException ex = Assert.ThrowsAsync<DisqusApiException>(_apiService.UsersDetailsAsync);
			Assert.AreEqual("You must either provide a user or authenticate the user.", ex.Message);
		}

		[TestCase]
		public void Test_UnhandledError()
		{
			_factory.StartThrowingErrors();

			Exception ex = Assert.ThrowsAsync<Exception>(_apiService.UsersDetailsAsync);
			Assert.AreEqual("Test Exception", ex.Message);
		}

		[TestCase]
		public async Task Test_UsersDetails_NoAuth()
		{
			var data = await _apiService.UsersDetailsAsync(new DsqUserArgument("117139345", DsqUserArgumentType.UserId));

			Assert.AreEqual(0, data.Code);
			Assert.AreEqual("117139345", data.Response.Id);
			Assert.AreEqual("disquswindows", data.Response.Username);
			Assert.AreEqual("https://disqus.com/by/disquswindows/", data.Response.ProfileUrl.OriginalString);
			Assert.IsFalse(data.Response.IsAnonymous);
			Assert.IsTrue(String.IsNullOrEmpty(data.Response.Url?.OriginalString));
			Assert.IsFalse(String.IsNullOrEmpty(data.Response.Avatars?.Permalink.OriginalString));
			Assert.IsNull(data.Response.IsFollowing);
			Assert.IsNull(data.Response.IsFollowedBy);
			Assert.IsNull(data.Response.IsBlocked);
			Assert.IsNull(data.Response.IsVerified);
		}

		[TestCase]
		public async Task Test_UsersDetails_Auth()
		{
			_apiService.UpdateAuthentication(new DisqusOAuthAuthentication(TEST_API_KEY, TEST_ACCESS_TOKEN));

			var data = await _apiService.UsersDetailsAsync(new DsqUserArgument("117139345", DsqUserArgumentType.UserId));

			Assert.IsNotNull(data.Response.IsFollowing);
			Assert.IsNotNull(data.Response.IsFollowedBy);
			Assert.IsNotNull(data.Response.IsBlocked);
			Assert.IsNull(data.Response.IsVerified);
		}

		[TestCase]
		public async Task Test_UsersDetails_Self()
		{
			_apiService.UpdateAuthentication(new DisqusOAuthAuthentication(TEST_API_KEY, "self"));

			var data = await _apiService.UsersDetailsAsync();

			Assert.IsNotNull(data.Response.IsVerified);
			Assert.IsFalse(String.IsNullOrEmpty(data.Response.Email));
		}

		[TestCase]
		public async Task Test_ForumsDetails_Basic()
		{
			var data = await _apiService.ForumsDetailsAsync("disqus");

			Assert.AreEqual("disqus", data.Response.Id);
			Assert.AreEqual("Tech", data.Response.Category);
			Assert.AreEqual("11", data.Response.Pk);
			Assert.AreEqual("http://blog.disqus.com/", data.Response.Url?.OriginalString);
			Assert.AreEqual("http://disqus.com/api/forums/favicons/disqus.jpg", data.Response.Favicon.Permalink?.OriginalString);
			Assert.AreEqual("3", data.Response.Founder);
			Assert.AreEqual(298, data.Response.OrganizationId);

			Assert.IsNull(data.Response.Author);
			Assert.IsNull(data.Response.NumThreads);
			Assert.IsNull(data.Response.NumFollowers);
			Assert.IsNull(data.Response.IsFollowing);
			Assert.IsNull(data.Response.Channel);
		}

		[TestCase]
		public async Task Test_ForumsDetails_RelatedAuthor()
		{
			var data = await _apiService.ForumsDetailsAsync("disqus", true);

			Assert.AreEqual("danielha", data.Response.Author.Username);
			Assert.AreEqual("obscurelyfamous", data.Response.Author.Name);
		}

		[TestCase]
		public async Task Test_ForumsDetails_Channel()
		{
			var data = await _apiService.ForumsDetailsAsync("channel-discussdisqus");

			Assert.IsNotNull(data.Response.Channel);
			Assert.AreEqual("Discuss Disqus", data.Response.Channel.Name);
		}

		[TestCase]
		public async Task Test_ThreadsDetails_Basic()
		{
			var data = await _apiService.ThreadsDetailsAsync("5589703545");

			Assert.AreEqual("5589703545", data.Response.Id);
			Assert.IsTrue(data.Response.Link.IsWellFormedOriginalString());
			Assert.AreEqual("4889630000", data.Response.Identifiers.FirstOrDefault());
			Assert.AreEqual("disqus", data.Response.Forum.Id);
		}

		[TestCase]
		public async Task Test_ThreadsDetails_ByIdentifier()
		{
			var data = await _apiService.ThreadsDetailsAsync(disqusIdentifier: "4889630000", forum: "disqus");

			Assert.AreEqual("5589703545", data.Response.Id);
		}

		[TestCase]
		public async Task Test_ThreadsDetails_ByUri()
		{
			var data = await _apiService.ThreadsDetailsAsync(threadUri: new Uri("https://blog.disqus.com/protecting-users-privacy-on-disqus"), forum: "disqus");

			Assert.AreEqual("5589703545", data.Response.Id);
		}

		[TestCase]
		public async Task Test_ThreadsListPosts_Basic()
		{
			var data = await _apiService.ThreadsListPostsAsync("5589703545");

			Assert.IsTrue(data.Response.Count > 0);
		}

		[TestCase]
		public async Task Test_PostsListChildren_AttachHasChildren()
		{
			var data = await _apiService.PostsListChildrenAsync(
				parentPost: "3204697376", 
				forum: "destructoid", 
				order: DsqSortOrder.Newest, 
				attach: PostAttachments.HasChildren
			);

			Assert.IsNotNull(data.Response.FirstOrDefault()?.HasChildren);

		}
	}
}
