namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefMapper : ProtodefObject
	{
		public ProtodefMapper(ProtodefObject type, Dictionary<string, ProtodefObject> mappings)
		{
			Type = type;
			Mappings = mappings;
		}

		public ProtodefObject Type { get; private set; }
		public Dictionary<string, ProtodefObject> Mappings { get; private set; } = new();
	}


}
