using System.Text.Json;
using System.Text.Json.Serialization;

namespace SourceGenerator.ProtoDefTypes.Converters
{
	public sealed class ContainerFieldConverter : JsonConverter<ProtodefContainerField>
	{
		public override ProtodefContainerField? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.StartObject)
			{
				string? name = null;
				ProtodefType type = null;
				bool isAnon = false;

				while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
				{
					if (reader.TokenType == JsonTokenType.PropertyName)
					{
						string propName = reader.GetString();

						switch (name)
						{
							case "name":
								name = reader.GetString();
								break;
							case "type":
								type = JsonSerializer.Deserialize<ProtodefType>(ref reader, options);
								break;
							case "anon":
								isAnon = reader.GetBoolean();
								break;
						}
					}
					else
					{
						throw new JsonException();
					}
				}
				if (type is null)
					throw new JsonException();
				if (!string.IsNullOrEmpty(name) && isAnon)
					throw new JsonException();

				return new ProtodefContainerField(isAnon, name, type);
			}
			else
			{
				throw new JsonException();
			}
		}

		public override void Write(Utf8JsonWriter writer, ProtodefContainerField value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
	public sealed class ContainerConverter : JsonConverter<ProtodefContainer>
	{
		public override ProtodefContainer? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.StartArray)
			{
				List<ProtodefContainerField> fields = new();

				while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
				{
					fields.Add(JsonSerializer.Deserialize<ProtodefContainerField>(ref reader, options));
				}


				return new ProtodefContainer(fields);
			}
			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, ProtodefContainer value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
