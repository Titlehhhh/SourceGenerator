using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefBuffer : ProtodefType
	{
		public ProtodefBuffer(JToken value) : base(value)
		{
		}

		public ProtodefType CountType { get; }
		public string? Count { get; }
		public bool? Rest { get; }
	}


}
