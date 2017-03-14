using System;

namespace Disqus.Core.Helpers
{
	public class ArgumentValue : Attribute
	{
		private readonly string _value;

		public ArgumentValue(string value)
		{
			_value = value;
		}

		public string Value
		{
			get { return _value; }
		}
	}
}
