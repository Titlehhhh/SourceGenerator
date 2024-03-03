using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceGenerator.ProtoDefTypes
{
	public class Numeric
	{
		public int Size { get; }
		public bool Signed { get; }
		public string NET_Name { get; }
		public string Name { get; }

		public Numeric(string name, string dotnet_name, int size, bool signed)
		{
			Size = size;
			Signed = signed;
			NET_Name = dotnet_name;
			Name = name;
		}


	}
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

	public class PSwitch
	{
		public string CompareTo { get; set; }
		public string? CompareToValue { get; set; }

		public IEnumerable<Field> Fields { get; set; }

		public string? Default { get; set; }
	}
	public class Option
	{
		public PType Type { get; set; }
	}
	public class PContainer : Collection<Field>
	{
		
	}

	public class PArray
	{
		public PType Type { get; set; }
		public PType CountType { get; set; }

		public string? Count { get; set; }
	}
	public class PType
	{

	}
	public class Field
	{
		public string Name { get; set; }
		public PType Type { get; set; }
	}

	public class PBuffer
	{
		public PType CountType { get; set; }
		public string? Count { get; set; }
		public bool? Rest { get; set; }
	}

	public class BitField : Collection<BitFieldNode>
	{
	}
	public class BitFieldNode
	{
		public string Name { get; set; }
		public int Size { get; set; }
		public bool Signed { get; set; }
	}
	public class Mapper
	{

	}
	public class PString
	{

	}


}
