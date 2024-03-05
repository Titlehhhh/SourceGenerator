using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefObject
	{
		

		private JToken _value;

		

		public override string ToString()
		{
			return _value.ToString();
		}
	}

	public class ProtodefType
	{
		public bool IsNative { get; }

	}


}
