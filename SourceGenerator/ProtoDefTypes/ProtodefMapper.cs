using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefMapper : ProtodefObject
	{
		//public override string TypeName => "Mapper";
		public ProtodefMapper(JToken value) 
		{
			if (value is JObject obj)
			{
				//Type = new ProtodefType(obj["type"]);

				foreach (var item in obj.Value<JObject>("mappings"))
				{
					Mappings[item.Key] = ProtodefParser.ParseToken(item.Value);
				}

			}
			else
			{
				throw new ArgumentException("no object");
			}
		}

		public ProtodefObject Type { get; private set; }
		public Dictionary<string, ProtodefObject> Mappings { get; private set; } = new();
	}


}
