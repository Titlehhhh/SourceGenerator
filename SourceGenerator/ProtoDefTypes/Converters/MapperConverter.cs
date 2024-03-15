using System.Text.Json;
using System.Text.Json.Serialization;

namespace SourceGenerator.ProtoDefTypes.Converters
{
	public sealed class MapperConverter : JsonConverter<ProtodefMapper>
	{
		public override ProtodefMapper? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}

		public override void Write(Utf8JsonWriter writer, ProtodefMapper value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
