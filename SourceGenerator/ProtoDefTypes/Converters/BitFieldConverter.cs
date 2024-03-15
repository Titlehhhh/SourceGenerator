using System.Text.Json;
using System.Text.Json.Serialization;

namespace SourceGenerator.ProtoDefTypes.Converters
{
	public sealed class BitFieldConverter : JsonConverter<ProtodefBitField>
	{
		public override ProtodefBitField? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}

		public override void Write(Utf8JsonWriter writer, ProtodefBitField value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
