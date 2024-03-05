using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefType
	{
		public virtual string TypeName { get; };

		private JToken _value;

		public ProtodefType(JToken value)
		{
			
			_value = value;
			if(_value.Type == JTokenType.String)
			{
				TypeName = _value.ToString();
			}
		}

		public override string ToString()
		{
			return _value.ToString();
		}
	}


}
