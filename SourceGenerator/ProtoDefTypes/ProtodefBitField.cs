﻿using Newtonsoft.Json.Linq;
using System.Collections;

namespace SourceGenerator.ProtoDefTypes
{

	public class ProtodefBitField : ProtodefType, IEnumerable<ProtodefBitFieldNode>, IEnumerable
	{
		//public override string TypeName => "Bitfield";
		private List<ProtodefBitFieldNode> nodes = new();

		public ProtodefBitField(List<ProtodefBitFieldNode> nodes, JToken token) : base(token)
		{
			this.nodes = nodes;
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
