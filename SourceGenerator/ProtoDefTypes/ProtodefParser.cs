﻿using System.Text.Json;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefParser
	{
		private readonly string json;

		public ProtodefParser(string json)
		{
			this.json = json;
		}

		public Protocol Parse()
		{
			JsonSerializerOptions options = new();

			options.Converters.Add(new TestConverter());

			JsonSerializer.Deserialize<Protocol>(json, options);

			return null;
		}
	}
}
