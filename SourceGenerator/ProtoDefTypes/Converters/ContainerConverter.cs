using System.Text.Json;
using System.Text.Json.Serialization;

namespace SourceGenerator.ProtoDefTypes.Converters
{
	public sealed class ContainerConverter : JsonConverter<ProtodefContainer>
	{
		public override ProtodefContainer? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}

		public override void Write(Utf8JsonWriter writer, ProtodefContainer value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
