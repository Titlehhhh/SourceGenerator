namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefSwitch : ProtodefObject
	{
		//public override string TypeName => "Switch";



		public ProtodefSwitch(string compareTo, string? compareToValue, Dictionary<string, ProtodefObject> fields, string? @default)
		{
			CompareTo = compareTo;
			CompareToValue = compareToValue;
			Fields = fields;
			Default = @default;
		}

		//TODO path parser
		public string CompareTo { get; }

		public string? CompareToValue { get; }

		public Dictionary<string, ProtodefObject> Fields { get; } = new();

		public string? Default { get; }
	}


}
