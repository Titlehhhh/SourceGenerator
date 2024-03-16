using SourceGenerator.ProtoDefTypes.Converters;
using System.Text.Json;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefParser
	{
		private readonly string json;

		public ProtodefParser(string json)
		{
			this.json = json;
		}

		public Protocol Parse()
		{
			JsonSerializerOptions options = new();

			options.Converters.Add(new ProtocolConverter());
			options.Converters.Add(new ArrayConverter());
			options.Converters.Add(new BitFieldConverter());
			options.Converters.Add(new BufferConverter());
			options.Converters.Add(new ContainerConverter());
			options.Converters.Add(new MapperConverter());
			options.Converters.Add(new OptionConverter());
			options.Converters.Add(new PrefixedStringConverter());
			options.Converters.Add(new SwitchConverter());
			options.Converters.Add(new NamespaceConverter());
			options.Converters.Add(new DataTypeConverter());
			options.Converters.Add(new ContainerFieldConverter());

			JsonSerializer.Deserialize<Protocol>(json, options);

			return null;
		}
	}
}
