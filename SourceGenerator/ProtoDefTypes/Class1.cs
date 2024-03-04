using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
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

	public class ProtodefType
	{
		private JToken _value;

		public ProtodefType(JToken value)
		{
			_value = value;
		}

		public override string ToString()
		{
			return _value.ToString();
		}
	}

	public class ProtodefSwitch : ProtodefType
	{
		public ProtodefSwitch(JToken value) : base(value)
		{
			if(value is JObject obj)
			{
				CompareTo = obj.Value<string>("compareTo");
				CompareToValue = obj.Value<string>("compareToValue");
				Default = obj.Value<string>("default");
				Fields = obj.ToObject<Dictionary<string, object>>();
			}
			else
			{
				throw new ArgumentException("value is not JObject");
			}
		}
		//TODO path parser
		public string CompareTo { get; }

		public string? CompareToValue { get; }

		public Dictionary<string, object> Fields { get; }

		public string? Default { get; }
	}

	public class ProtodefContainer : ReadOnlyCollection<ProtodefField>, IEnumerable<KeyValuePair<string, ProtodefType?>>, IEnumerable
	{
		public ProtodefContainer(IList<ProtodefField> list) : base(list)
		{
		}

		IEnumerator<KeyValuePair<string, ProtodefType?>> IEnumerable<KeyValuePair<string, ProtodefType?>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}

	public class ProtodefArray : ProtodefType
	{
		public ProtodefArray(JToken value) : base(value)
		{
		}


		public ProtodefType Type { get; }
		public ProtodefType CountType { get; }
		public string? Count { get; }
	}

	public class ProtodefField
	{
		public string Name { get; set; }
		public ProtodefType Type { get; set; }
	}

	public class ProtodefBuffer : ProtodefType
	{
		public ProtodefBuffer(JToken value) : base(value)
		{
		}

		public ProtodefType CountType { get; }
		public string? Count { get; }
		public bool? Rest { get; }
	}

	public class ProtodefBitField : ProtodefType, IEnumerable<ProtodefBitFieldNode>, IEnumerable
	{
		public ProtodefBitField(JToken value) : base(value)
		{
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
	public class ProtodefBitFieldNode
	{
		public ProtodefBitFieldNode(string name, int size, bool signed)
		{
			Name = name;
			Size = size;
			Signed = signed;
		}

		public string Name { get; }
		public int Size { get; }
		public bool Signed { get; }
	}
	public class ProtodefMapper : ProtodefType
	{
		public ProtodefMapper(JToken value) : base(value)
		{
		}
	}


	public class ProtodefString
	{

	}


}
