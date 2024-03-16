namespace SourceGenerator.ProtoDefTypes
{
	public sealed class ProtodefBuffer : ProtodefType
	{

		public ProtodefBuffer(ProtodefType countType, string? count, bool? rest)
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
