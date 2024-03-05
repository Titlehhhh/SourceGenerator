namespace SourceGenerator.ProtoDefTypes
{
	public static class Numerics
	{
		public static Dictionary<string, Numeric> All = new Dictionary<string, Numeric>
		{
			{"i8", new("i8", "sbyte", 1, true) },
			{"u8", new("u8", "byte", 1, false) },
			{"i16", new("i16", "sbyte", 2, true) },
			{"u16", new("u16", "ushort", 2, true) },
			{"i32", new("i32", "sbyte", 4, true) },
			{"u32", new("u32", "uint", 4, false) },
			{"f32", new("f32", "float", 4, true) },
			{"f64", new("f64", "double", 8, true) },
			{"i64", new("i64", "long", 8, true) },
			{"u64", new("u64", "ulong", 8, false) },
		};
	}


}
