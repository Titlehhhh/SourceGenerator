using System.Text.Json;
using System.Text.Json.Serialization;

namespace SourceGenerator.ProtoDefTypes.Converters
{
	public sealed class DataTypeConverter : JsonConverter<ProtodefType>
	{
		public override ProtodefType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}

		public override void Write(Utf8JsonWriter writer, ProtodefType value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
