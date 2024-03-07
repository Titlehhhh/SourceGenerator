namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefMapper : ProtodefType
	{
		public ProtodefMapper(ProtodefType type, Dictionary<string, ProtodefType> mappings)
		{
			Type = type;
			Mappings = mappings;
		}

		public ProtodefType Type { get; private set; }
		public Dictionary<string, ProtodefType> Mappings { get; private set; } = new();
	}


}
