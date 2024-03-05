using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefField
	{
		public ProtodefField(JObject obj)
		{
			Name = obj.Value<string>("name");
			Type = ProtodefParser.ParseToken(obj["type"]);
		}

		public string Name { get; }
		public ProtodefType Type { get; }
	}


}
