using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefSwitch : ProtodefObject
	{
		//public override string TypeName => "Switch";

		public ProtodefSwitch(JToken value) 
		{
			if (value is JObject obj)
			{
				CompareTo = obj.Value<string>("compareTo");
				CompareToValue = obj.Value<string>("compareToValue");
				Default = obj.Value<string>("default");
				foreach (var item in obj.Value<JObject>("fields"))
				{
					Fields[item.Key] = ProtodefParser.ParseToken(item.Value);
				}
			}
			else
			{
				throw new ArgumentException("value is not JObject");
			}
		}
		//TODO path parser
		public string CompareTo { get; }

		public string? CompareToValue { get; }

		public Dictionary<string, ProtodefObject> Fields { get; } = new();

		public string? Default { get; }
	}


}
