using System.Text.Json;
using System.Text.Json.Serialization;

namespace SourceGenerator.ProtoDefTypes.Converters
{

	public sealed class SwitchConverter : JsonConverter<ProtodefSwitch>
	{
		public override ProtodefSwitch? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}

		public override void Write(Utf8JsonWriter writer, ProtodefSwitch value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
