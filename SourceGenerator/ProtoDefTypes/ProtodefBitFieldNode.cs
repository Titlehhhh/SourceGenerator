namespace SourceGenerator.ProtoDefTypes
{
	public sealed class ProtodefBitFieldNode
	{
		public ProtodefBitFieldNode(string name, int size, bool signed)
		{
			Name = name;
			Size = size;
			Signed = signed;
		}

		public string Name { get; }
		public int Size { get; }
		public bool Signed { get; }
	}


}
