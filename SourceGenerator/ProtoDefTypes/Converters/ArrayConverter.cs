using System.Text.Json;
using System.Text.Json.Serialization;

namespace SourceGenerator.ProtoDefTypes.Converters
{
	public sealed class ArrayConverter : JsonConverter<ProtodefArray>
	{
		public override ProtodefArray? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}

		public override void Write(Utf8JsonWriter writer, ProtodefArray value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
