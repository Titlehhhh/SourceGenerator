﻿namespace SourceGenerator.ProtoDefTypes
{
	public sealed class ProtodefCustomType : ProtodefType
	{
		public string Name { get; }

		public ProtodefCustomType(string name)
		{
			Name = name;
		}
	}


}
