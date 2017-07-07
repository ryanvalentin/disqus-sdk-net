using System;
using Disqus.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Disqus.Core
{
	public class DisqusObjectJsonConverter<T> : JsonConverter where T : class, IDsqIdentifiableModel, new()
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null || reader.TokenType == JsonToken.None)
				return null;

			if (reader.ValueType == typeof(string))
			{
				string id = reader.Value as string;

				// Clean the ID if it's a key
				if (id.Contains("="))
					id = id.Split('=')[1];

				T newObject = new T() { Id = id };

				return newObject;
			}
			else
			{
				JObject parsedObject = JObject.Load(reader);

				T fullObject = parsedObject.ToObject<T>();
				if (String.IsNullOrEmpty(fullObject.Id))
					fullObject.Id = Guid.NewGuid().ToString();

				return fullObject;
			}
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(T) || objectType == typeof(string);
		}
	}
}
