using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefParser
	{
		public List<string> nativeTypes = new();

		public ProtodefObject Parse(JToken token)
		{
			if (token.Type == JTokenType.String)
			{
				throw new NotImplementedException();
				//return new ProtodefType();
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
					"array" => new ProtodefArray(last),
					_ => throw new Exception($"Unknow type - [{typeName}]")

				};
			}
			else
			{
				throw new NotSupportedException(token.Type.ToString());
			}
		}


		private ProtodefContainer ParseContainer(JToken token)
		{
			return null;
		}

		public static ProtodefObject ParseToken(JToken token)
		{
			if (token.Type == JTokenType.String)
			{
				return null;
				//return new ProtodefType(token);
			}
			else if (token.Type == JTokenType.Array)
			{
				string typeName = token.First.ToString();
				JToken last = token.Last;
				Console.WriteLine(typeName);
				return typeName switch
				{
					"container" => new ProtodefContainer(last),
					"switch" => new ProtodefSwitch(last),
					"bitfield" => new ProtodefBitField(last),
					"mapper" => new ProtodefMapper(last),
					"buffer" => new ProtodefBuffer(last),
					"array" => new ProtodefArray(last),
					_ => throw new Exception($"Unknow type - [{typeName}]")

				};
			}
			else
			{
				throw new NotSupportedException(token.Type.ToString());
			}
		}
	}


}
