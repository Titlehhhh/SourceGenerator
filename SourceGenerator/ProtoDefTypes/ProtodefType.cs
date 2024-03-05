using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefType
	{
		public virtual string TypeName { get; } = "String";

		private JToken _value;

		public ProtodefType(JToken value)
		{
			_value = value;
			
		}

		public override string ToString()
		{
			return _value.ToString();
		}
	}


}
