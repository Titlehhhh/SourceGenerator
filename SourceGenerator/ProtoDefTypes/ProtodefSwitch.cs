namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefSwitch : ProtodefType
	{
		

		public ProtodefSwitch(string compareTo, string? compareToValue, Dictionary<string, ProtodefType> fields, string? @default)
		{
			CompareTo = compareTo;
			CompareToValue = compareToValue;
			Fields = fields;
			Default = @default;
		}

		//TODO path parser
		public string CompareTo { get; }

		public string? CompareToValue { get; }

		public Dictionary<string, ProtodefType> Fields { get; } = new();

		public string? Default { get; }
	}


}
