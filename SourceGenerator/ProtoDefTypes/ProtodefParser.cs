using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public static class ProtodefParser
	{
		public static ProtodefType ParseToken(JToken token)
		{
			if (token.Type == JTokenType.String)
			{
				return new ProtodefType(token);
			}
			else if (token.Type == JTokenType.Array)
			{
				string typeName = token.First.ToString();
				JToken last = token.Last;
				return typeName switch
				{
					"container" => new ProtodefContainer(last),
					"switch" => new ProtodefSwitch(last),
					"bitfield" => new ProtodefBitField(last),
					"mapper" => new ProtodefMapper(last),
					"buffer" => new ProtodefBuffer(last),

				};
			}
			else
			{
				throw new NotSupportedException(token.Type.ToString());
			}
		}
	}


}
