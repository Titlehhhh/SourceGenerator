using System.Text.Json;
using System.Text.Json.Serialization;

namespace SourceGenerator.ProtoDefTypes.Converters
{
	public sealed class OptionConverter : JsonConverter<ProtodefOption>
	{
		public override ProtodefOption? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}

		public override void Write(Utf8JsonWriter writer, ProtodefOption value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
