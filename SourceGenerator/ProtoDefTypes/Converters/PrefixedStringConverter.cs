using System.Text.Json;
using System.Text.Json.Serialization;

namespace SourceGenerator.ProtoDefTypes.Converters
{
	public sealed class PrefixedStringConverter : JsonConverter<ProtodefString>
	{
		public override ProtodefString? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}

		public override void Write(Utf8JsonWriter writer, ProtodefString value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
