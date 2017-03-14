using System;
using Disqus.Core.Authentication;
using NUnit.Framework;

namespace Disqus.Core.Tests.Authentication
{
	[TestFixture]
	public class Test_DisqusAuthentication
	{
		private const string TEST_API_KEY = "hDuMtiXLQn5TarhIlbB9Q8hpYYvDRS2QPa64U31QIi1DVu5pB4epANLFQeey4HIB";
		private const string TEST_API_SECRET = "5fsz7mcyMYmYofgtOhKzsPMWdsExXLdE11JDwmQj0ZZCxSRBaWTjQd2snI65e3Nq";

		private readonly TimeSpan _timestampStatic;
		private readonly string _timestampStaticValue;

		public Test_DisqusAuthentication()
		{
			_timestampStatic = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
			_timestampStaticValue = Convert.ToInt32(_timestampStatic.TotalSeconds).ToString();
		}

		[TestCase]
		public void Test_DisqusOAuthAuthentication_LoggedIn()
		{
			const string accessToken = "5662bb7b415e4cd0bc05b26c40e23e97";
			var auth = new DisqusOAuthAuthentication(apiKey: TEST_API_KEY, accessToken: accessToken);

			Assert.AreEqual(TEST_API_KEY, auth.ApiKey);
			Assert.AreEqual(DisqusAuthenticationType.OAuth, auth.AuthType);
			Assert.AreEqual(accessToken, auth.AuthenticationToken);
		}

		[TestCase]
		public void Test_DisqusOAuthAuthentication_LoggedOut()
		{
			var auth = new DisqusOAuthAuthentication(apiKey: TEST_API_KEY);

			Assert.AreEqual(null, auth.AuthenticationToken);
			Assert.AreEqual(DisqusAuthenticationType.OAuth, auth.AuthType);
		}

		[TestCase]
		public void Test_DisqusRemoteAuthentication_LoggedIn_Basic()
		{
			string expectedMessage = "eyJpZCI6IjEiLCJ1c2VybmFtZSI6InRlc3R1c2VyIiwiZW1haWwiOiJ0ZXN0dXNlckBleGFtcGxlLmNvbSJ9";

			var remoteUser = new DisqusRemoteUser(
				id: "1",
				name: "testuser",
				email: "testuser@example.com"
			);

			var auth = new DisqusRemoteAuthentication(
				apiKey: TEST_API_KEY,
				apiSecret: TEST_API_SECRET,
				remoteUser: remoteUser,
				timestamp: _timestampStatic
			);

			Assert.AreEqual(TEST_API_KEY, auth.ApiKey);
			Assert.AreEqual(auth.AuthenticationToken.Length, 136);
			Assert.IsTrue(auth.AuthenticationToken.StartsWith(expectedMessage, StringComparison.InvariantCulture));
			Assert.IsTrue(auth.AuthenticationToken.EndsWith(_timestampStaticValue, StringComparison.InvariantCulture));
		}

		[TestCase]
		public void Test_DisqusRemoteAuthentication_LoggedIn_Full()
		{
			string expectedMessage = "eyJpZCI6IjEiLCJ1c2VybmFtZSI6InRlc3R1c2VyIiwiZW1haWwiOiJ0ZXN0dXNlckBleGFtcGxlLmNvbSIsImF2YXRhciI6Imh0dHBzOi8vZGlzcXVzLmNvbS9hdmF0YXIvc29tZXRoaW5nLmpwZyIsInVybCI6Imh0dHBzOi8vZGlzcXVzLmNvbS8ifQ==";

			var remoteUser = new DisqusRemoteUser(
				id: "1",
				name: "testuser",
				email: "testuser@example.com",
				avatar: new Uri("https://disqus.com/avatar/something.jpg"),
				url: new Uri("https://disqus.com/")
			);

			var auth = new DisqusRemoteAuthentication(
				apiKey: TEST_API_KEY,
				apiSecret: TEST_API_SECRET,
				remoteUser: remoteUser
			);

			Assert.IsTrue(
				auth.AuthenticationToken.StartsWith(expectedMessage, StringComparison.InvariantCulture),
				$"Message did not match, was {auth.AuthenticationToken.Split(' ')[0]}"
			);
		}

		[TestCase]
		public void Test_DisqusRemoteAuthentication_LoggedOut()
		{
			string expectedMessage = "e30=";

			var remoteUser = new DisqusRemoteUser(
				id: null,
				name: null,
				email: null
			);

			var auth = new DisqusRemoteAuthentication(
				apiKey: TEST_API_KEY,
				apiSecret: TEST_API_SECRET,
				remoteUser: remoteUser
			);

			Assert.IsTrue(auth.AuthenticationToken.StartsWith(expectedMessage, StringComparison.InvariantCulture));
		}
	}
}
