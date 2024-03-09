using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefOption : ProtodefType
	{
		public ProtodefType Type { get; }

		public ProtodefOption(ProtodefType type, JToken token) : base(token)
		{
			Type = type;
		}
	}
}
