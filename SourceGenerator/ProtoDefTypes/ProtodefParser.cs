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
			
			
			
			options.Converters.Add(new NamespaceConverter());
			options.Converters.Add(new DataTypeConverter());


			return JsonSerializer.Deserialize<Protocol>(json, options);
		}
	}
}
