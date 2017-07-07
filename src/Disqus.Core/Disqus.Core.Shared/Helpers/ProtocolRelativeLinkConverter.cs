using System;
using Newtonsoft.Json;

namespace Disqus.Core.Helpers
{
	public class ProtocolRelativeLinkConverter : JsonConverter
	{
		private string _protocol { get; set; }

		public ProtocolRelativeLinkConverter(string protocol)
		{
			_protocol = protocol;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null || reader.TokenType == JsonToken.None)
				return null;

			if (reader.ValueType == typeof(string))
			{
				string link = reader.Value as string;

				if (link.StartsWith("//", StringComparison.Ordinal))
					link = _protocol + link;

				Uri returnUrl;
				if (Uri.TryCreate(link, UriKind.RelativeOrAbsolute, out returnUrl))
					return returnUrl;
			}

			return null;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(string);
		}
	}
}
