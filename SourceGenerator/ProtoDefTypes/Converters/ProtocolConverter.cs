using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SourceGenerator.ProtoDefTypes.Converters
{

	public sealed class ProtocolConverter : JsonConverter<Protocol>
	{
		public override Protocol? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.StartObject)
			{
				Dictionary<string, ProtodefType> types = new();
				Dictionary<string, Namespace> namespaces = new();
				while (reader.Read())
				{

					if (reader.TokenType == JsonTokenType.PropertyName)
					{
						string name = reader.GetString();
						if (name == "types")
						{
							if (reader.TokenType == JsonTokenType.PropertyName)
							{
								if (reader.Read())
								{


									if (reader.TokenType == JsonTokenType.StartObject)
									{



										types = JsonSerializer.Deserialize<Dictionary<string, ProtodefType>>(ref reader, options);

									}
								}


							}
						}
						else
						{
							//namespaces[name] = JsonSerializer.Deserialize<Namespace>(ref reader, options);
						}
					}

				}
				return new Protocol(namespaces, types);
			}
			else
			{
				throw new JsonException();
			}

		}


		public override void Write(Utf8JsonWriter writer, Protocol value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
