using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceGenerator
{
	using System.Collections.Generic;
	using System.IO;
	using Newtonsoft.Json;

	public class Protocol
	{
		public Dictionary<string, DataType> types { get; set; }
		public Dictionary<string, Namespace> namespaces { get; set; }
	}

	public class Namespace
	{
		[JsonProperty("Map")]
		public Dictionary<string, Namespace> Map { get; set; }
		public DataType DataType { get; set; }
	}

	public class DataType
	{
		public Conditional Conditional { get; set; }
		public Numeric Numeric { get; set; }
		public Primitive Primitive { get; set; }
		public Structure Structure { get; set; }
		public Util Util { get; set; }
		public string Custom { get; set; }
	}

	public class Conditional
	{
		public Switch Switch { get; set; }
		public DataType Option { get; set; }
	}

	public class Switch
	{
		public string name { get; set; }
		[JsonProperty("compareTo")]
		public string compare_to { get; set; }
		public Dictionary<string, DataType> fields { get; set; }
		public DataType @default { get; set; }
	}

	public class Numeric
	{
		public Byte Byte { get; set; }
		public Short Short { get; set; }
		public Int Int { get; set; }
		public Long Long { get; set; }
		public Float Float { get; set; }
		public Double Double { get; set; }
		public string VarInt { get; set; }
	}

	public class Byte
	{
		public bool signed { get; set; }
	}

	public class Short
	{
		public bool signed { get; set; }
		public ByteOrder byte_order { get; set; }
	}

	public class Int
	{
		public bool signed { get; set; }
		public ByteOrder byte_order { get; set; }
	}

	public class Long
	{
		public bool signed { get; set; }
		public ByteOrder byte_order { get; set; }
	}

	public class Float
	{
		public ByteOrder byte_order { get; set; }
	}

	public class Double
	{
		public ByteOrder byte_order { get; set; }
	}

	public enum ByteOrder
	{
		BigEndian,
		LittleEndian
	}

	public enum Primitive
	{
		[JsonProperty("bool")]
		Boolean,
		[JsonProperty("cstring")]
		String,
		Void
	}

	public class Structure
	{
		public Array Array { get; set; }
		public List<Field> Container { get; set; }
		public Count Count { get; set; }
	}

	public class Array
	{
		[JsonProperty("countType")]
		public DataType count_type { get; set; }
		public ArrayCount count { get; set; }
		[JsonProperty("type")]
		public DataType elements_type { get; set; }
	}

	public class ArrayCount
	{
		[JsonProperty("FieldReference")]
		public string FieldReference { get; set; }
		[JsonProperty("FixedLength")]
		public int FixedLength { get; set; }
	}

	public class Field
	{
		public string name { get; set; }
		[JsonProperty("type")]
		public DataType field_type { get; set; }
		[JsonProperty("anon")]
		public bool? anonymous { get; set; }
	}

	public class Count
	{
		[JsonProperty("type")]
		public DataType count_type { get; set; }
		[JsonProperty("countFor")]
		public string count_for { get; set; }
	}

	public class Util
	{
		public Buffer Buffer { get; set; }
		public Mapper Mapper { get; set; }
		public List<BitField> Bitfield { get; set; }
		public PrefixedString PrefixedString { get; set; }
		public Loop Loop { get; set; }
		public TopBitSetTerminatedArray TopBitSetTerminatedArray { get; set; }
	}

	public class Buffer
	{
		[JsonProperty("countType")]
		public DataType count_type { get; set; }
		public ArrayCount count { get; set; }
		public bool? rest { get; set; }
	}

	public class Mapper
	{
		[JsonProperty("type")]
		public string mappings_type { get; set; }
		public Dictionary<string, string> mappings { get; set; }
	}

	public class BitField
	{
		public string name { get; set; }
		public int size { get; set; }
		public bool signed { get; set; }
	}

	public class PrefixedString
	{
		[JsonProperty("countType")]
		public DataType count_type { get; set; }
	}

	public class Loop
	{
		[JsonProperty("endVal")]
		public int end_val { get; set; }
		[JsonProperty("type")]
		public DataType data_type { get; set; }
	}

	public class TopBitSetTerminatedArray
	{
		public Structure type { get; set; }
	}

	public class RustToCsharpConverter
	{
		public static Protocol ReadProtocol(string json)
		{

			return JsonConvert.DeserializeObject<Protocol>(json);

		}
	}



}
