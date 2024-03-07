using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefType
	{


		private JToken _value;

		
		

		public override string ToString()
		{
			return _value.ToString();
		}
	}

	


}
