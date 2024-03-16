namespace SourceGenerator.ProtoDefTypes
{
	public sealed class ProtodefString : ProtodefType
	{
		public ProtodefType CountType { get; }

		public ProtodefString(ProtodefType countType)
		{
			CountType = countType;
		}
	}


}
