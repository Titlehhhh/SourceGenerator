using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefBitFieldNode
	{
		public ProtodefBitFieldNode(JObject obj)
		{
			Name = obj.Value<string>("name");
			Size = obj.Value<int>("size");
			Signed = obj.Value<bool>("signed");
		}
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
