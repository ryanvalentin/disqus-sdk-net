using System;

namespace Disqus.Core.Services.Api
{
	public class DisqusApiException : Exception
	{
		public DisqusApiException(int? code, string message, int statusCode = 400)
            : base(message)
        {
			Code = code;
			StatusCode = statusCode;
		}

		public int? Code { get; private set; }

		public int StatusCode { get; private set; }
	}
}
