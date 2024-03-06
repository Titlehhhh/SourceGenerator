using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefArray : ProtodefObject
	{
		public ProtodefArray(ProtodefObject type, ProtodefObject countType, string? count)
		{
			Type = type;
			CountType = countType;
			Count = count;
		}

		public ProtodefObject Type { get; }
		public ProtodefObject CountType { get; }
		public string? Count { get; }
	}


}
