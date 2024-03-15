using System.Text.Json;
using System.Text.Json.Serialization;

namespace SourceGenerator.ProtoDefTypes.Converters
{
	public sealed class ProtocolConverter : JsonConverter<Protocol>
	{
		public override Protocol? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}

		public override void Write(Utf8JsonWriter writer, Protocol value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
