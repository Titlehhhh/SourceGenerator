using System.Text.Json;
using System.Text.Json.Serialization;

namespace SourceGenerator.ProtoDefTypes.Converters
{
	public sealed class NamespaceConverter : JsonConverter<Namespace>
	{
		public override Namespace? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}

		public override void Write(Utf8JsonWriter writer, Namespace value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
