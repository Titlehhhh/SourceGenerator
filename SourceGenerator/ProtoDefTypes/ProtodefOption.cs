namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefOption : ProtodefType
	{
		public ProtodefType Type { get; }

		public ProtodefOption(ProtodefType type)
		{
			Type = type;
		}
	}
}
