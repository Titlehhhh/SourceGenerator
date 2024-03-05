using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefArray : ProtodefObject
	{
		//public override string TypeName => "Array";

		public ProtodefArray(JToken value) 
		{
			if(value is JObject obj)
			{
				//Type = new ProtodefType(obj["type"]);
				//CountType = new ProtodefType(obj["countType"]);
				//Count = obj.Value<string>("count");
			}
			else
			{
				throw new ArgumentException("no object");
			}
		}


		public ProtodefObject Type { get; }
		public ProtodefObject CountType { get; }
		public string? Count { get; }
	}


}
