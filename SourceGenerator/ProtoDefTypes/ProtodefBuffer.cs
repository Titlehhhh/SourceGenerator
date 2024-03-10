using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefBuffer : ProtodefType
	{



		public ProtodefBuffer(ProtodefType countType, string? count, bool? rest, JToken token) : base(token)
		{
			CountType = countType;
			Count = count;
			Rest = rest;
		}

		public ProtodefType CountType { get; }
		public string? Count { get; }
		public bool? Rest { get; }
	}


}
