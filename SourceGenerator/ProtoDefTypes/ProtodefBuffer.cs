using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefBuffer : ProtodefObject
	{
		
		

		public ProtodefBuffer(ProtodefObject countType, string? count, bool? rest)
		{
			CountType = countType;
			Count = count;
			Rest = rest;
		}

		public ProtodefObject CountType { get; }
		public string? Count { get; }
		public bool? Rest { get; }
	}


}
