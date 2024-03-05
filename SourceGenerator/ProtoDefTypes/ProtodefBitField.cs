using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceGenerator.ProtoDefTypes
{

	public class ProtodefBitField : ProtodefObject, IEnumerable<ProtodefBitFieldNode>, IEnumerable
	{
		//public override string TypeName => "Bitfield";
		private List<ProtodefBitFieldNode> nodes = new();
		public ProtodefBitField(JToken value) 
		{
			if(value is JArray array)
			{
				foreach(JObject obj in array)
				{
					nodes.Add(new ProtodefBitFieldNode(obj));
				}
			}
			else
			{
				throw new ArgumentException("no array");
			}
		}

		public IEnumerator<ProtodefBitFieldNode> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}


}
