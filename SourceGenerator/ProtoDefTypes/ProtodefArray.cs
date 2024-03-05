using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefArray : ProtodefType
	{
		public ProtodefArray(JToken value) : base(value)
		{
			if(value is JObject obj)
			{
				Type = new ProtodefType(obj["type"]);
				CountType = new ProtodefType(obj["countType"]);
				Count = obj.Value<string>("count");
			}
			else
			{
				throw new ArgumentException("no object");
			}
		}


		public ProtodefType Type { get; }
		public ProtodefType CountType { get; }
		public string? Count { get; }
	}


}
