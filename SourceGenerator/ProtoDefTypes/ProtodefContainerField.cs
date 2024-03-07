namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefContainerField
	{


		public ProtodefContainerField(bool? anon, string? name, ProtodefType type)
		{
			Anon = anon;
			Name = name;
			Type = type;
		}

		//public bool IsAnon => Anon;

		public bool? Anon { get; }
		public string? Name { get; }
		public ProtodefType Type { get; }
	}


}
