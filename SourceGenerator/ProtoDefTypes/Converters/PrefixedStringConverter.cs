using System.Text.Json;
using System.Text.Json.Serialization;

namespace SourceGenerator.ProtoDefTypes.Converters
{
	public sealed class PrefixedStringConverter : JsonConverter<ProtodefString>
	{
		public override ProtodefString? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.StartObject)
			{
				ProtodefType? countType = null;
				while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
				{
					if (reader.TokenType == JsonTokenType.PropertyName)
					{
						string propName = reader.GetString();


						if (propName == "countType")
						{
							countType = JsonSerializer.Deserialize<ProtodefType>(ref reader, options);
						}
					}
				}
				reader.Read();
				if (countType is null)
					throw new JsonException();

				return new ProtodefString(countType);
			}
			else
			{
				throw new JsonException();
			}
		}

		public override void Write(Utf8JsonWriter writer, ProtodefString value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
