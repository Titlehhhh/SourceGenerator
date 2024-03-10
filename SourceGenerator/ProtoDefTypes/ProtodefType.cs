using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefType
	{


		public JToken Token { get; }

		public ProtodefType(JToken token)
		{
			Token = token;
		}


		public override string ToString()
		{
			return Token.ToString();
		}
	}

	public class ProtodefDefiniteType : ProtodefType
	{
		public bool IsNative => Value == "native";

		public string Name { get; }
		public object Value { get; }

		public ProtodefDefiniteType(string name, object value, JToken token) : base(token)
		{
			Name = name;
			Value = value;
		}
	}




}
