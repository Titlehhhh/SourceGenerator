﻿using System.Text.Json.Serialization;

namespace SourceGenerator.ProtoDefTypes
{
	public sealed class ProtodefArray : ProtodefType
	{
		[JsonConstructor]
		public ProtodefArray(ProtodefType type, ProtodefType countType, string? count)
		{
			Type = type;
			CountType = countType;
			Count = count;
		}
		[JsonPropertyName("type")]
		public ProtodefType Type { get; }
		[JsonPropertyName("countType")]
		public ProtodefType CountType { get; }

		[JsonPropertyName("count")]
		public string? Count { get; }
	}


}
