namespace SourceGenerator.ProtoDefTypes
{
	public class Numeric
	{
		public int Size { get; }
		public bool Signed { get; }
		public string NET_Name { get; }
		public string Name { get; }

		public Numeric(string name, string dotnet_name, int size, bool signed)
		{
			Size = size;
			Signed = signed;
			NET_Name = dotnet_name;
			Name = name;
		}


	}


}
