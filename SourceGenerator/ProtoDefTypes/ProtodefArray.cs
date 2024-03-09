﻿using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefArray : ProtodefType
	{
		public ProtodefArray(ProtodefType type, ProtodefType countType, string? count, JToken token) : base(token)
		{
			Type = type;
			CountType = countType;
			Count = count;
		}

		public ProtodefType Type { get; }
		public ProtodefType CountType { get; }
		public string? Count { get; }
	}


}
