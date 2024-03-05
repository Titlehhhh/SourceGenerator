using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefBuffer : ProtodefObject
	{
		//public override string TypeName => "Cuffer";
		public ProtodefBuffer(JToken value)
		{
			if (value is JObject obj)
			{
				CountType = ProtodefParser.ParseToken(obj["countType"]);
				Count = obj.Value<string>("count");
				Rest = obj.Value<bool>("rest");
			}
			else
			{
				throw new ArgumentException("no object");
			}
		}

		public ProtodefObject CountType { get; }
		public string? Count { get; }
		public bool? Rest { get; }
	}


}
