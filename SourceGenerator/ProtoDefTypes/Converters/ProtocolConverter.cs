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
								while (reader.Read() && (reader.TokenType != JsonTokenType.EndObject))
								{
									if (reader.TokenType == JsonTokenType.StartObject)
										reader.Read();

									string typeName = reader.GetString();

									

									types[typeName] = JsonSerializer.Deserialize<ProtodefType>(ref reader, options);
								}

							}
							else
							{
								Console.WriteLine("GG");
							}
						}
						else
						{
							//namespaces[name] = JsonSerializer.Deserialize<Namespace>(ref reader, options);
						}
					}

				}
				return new Protocol();
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
