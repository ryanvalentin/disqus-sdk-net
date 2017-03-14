using System;
using Disqus.Core.Helpers;

namespace Disqus.Core.Services.Api
{
	public struct DsqUserArgument
	{
		public DsqUserArgument(string id, DsqUserArgumentType type = DsqUserArgumentType.UserId)
		{
			Id = id;
			Type = type;
		}

		public string Id { get; set; }

		public DsqUserArgumentType Type { get; set; }

		public override string ToString()
		{
			return $"{Type.ToArgument()}{Id}";
		}
	}
}
