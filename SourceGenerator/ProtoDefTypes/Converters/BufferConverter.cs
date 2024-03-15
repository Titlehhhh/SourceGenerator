using System.Text.Json;
using System.Text.Json.Serialization;

namespace SourceGenerator.ProtoDefTypes.Converters
{
	public sealed class BufferConverter : JsonConverter<ProtodefBuffer>
	{
		public override ProtodefBuffer? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}

		public override void Write(Utf8JsonWriter writer, ProtodefBuffer value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
