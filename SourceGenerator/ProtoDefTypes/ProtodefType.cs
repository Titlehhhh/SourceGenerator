using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefType
	{


		private JToken _value;

		public ProtodefType(JToken token)
		{
			_value = token;
		}
		

		public override string ToString()
		{
			return _value.ToString();
		}
	}

	


}
