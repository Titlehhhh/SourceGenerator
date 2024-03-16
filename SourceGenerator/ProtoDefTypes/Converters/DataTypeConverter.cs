using System.Text.Json;
using System.Text.Json.Serialization;

namespace SourceGenerator.ProtoDefTypes.Converters
{
	public sealed class DataTypeConverter : JsonConverter<ProtodefType>
	{
		public override ProtodefType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String)
			{
				string name = reader.GetString();

				var number = GetNumber(name);

				if (number is null)
				{

					return name switch
					{
						"varint" => new ProtodefVarInt(),
						"varlong" => new ProtodefVarLong(),
						_ => new ProtodefCustomType(name)
					};
				}
				else
				{
					return number;
				}
			}
			else if (reader.TokenType == JsonTokenType.StartArray)
			{
				reader.Read();
				string name = reader.GetString();
				reader.Read();
				ProtodefType result = name switch
				{
					"container" => JsonSerializer.Deserialize<ProtodefContainer>(ref reader, options),
					"bitfield" => JsonSerializer.Deserialize<ProtodefBitField>(ref reader, options),
					"buffer" => JsonSerializer.Deserialize<ProtodefBuffer>(ref reader, options),
					"mapper" => JsonSerializer.Deserialize<ProtodefMapper>(ref reader, options),
					"array" => JsonSerializer.Deserialize<ProtodefArray>(ref reader, options),
					"option" => JsonSerializer.Deserialize<ProtodefOption>(ref reader, options),
					"pstring" => JsonSerializer.Deserialize<ProtodefString>(ref reader, options),
					_ => throw new NotSupportedException($"Unknown type: {name}")
				};
				while (reader.TokenType != JsonTokenType.EndArray)
					reader.Read();

				return result;
			}
			else
			{
				throw new JsonException();
			}
		}

		private ProtodefNumericType? GetNumber(string name)
		{
			return name switch
			{
				"i8" => new ProtodefNumericType("sbyte", true, ByteOrder.BigEndian),
				"u8" => new ProtodefNumericType("byte", false, ByteOrder.BigEndian),
				"i16" => new ProtodefNumericType("short", true, ByteOrder.BigEndian),
				"u16" => new ProtodefNumericType("ushort", false, ByteOrder.BigEndian),
				"li16" => new ProtodefNumericType("short", true, ByteOrder.LittleEndian),
				"lu16" => new ProtodefNumericType("ushort", false, ByteOrder.LittleEndian),
				"i32" => new ProtodefNumericType("int", true, ByteOrder.BigEndian),
				"u32" => new ProtodefNumericType("unit", false, ByteOrder.BigEndian),
				"li32" => new ProtodefNumericType("int", true, ByteOrder.LittleEndian),
				"lu32" => new ProtodefNumericType("uint", false, ByteOrder.LittleEndian),
				"i64" => new ProtodefNumericType("long", true, ByteOrder.BigEndian),
				"u64" => new ProtodefNumericType("ulong", false, ByteOrder.BigEndian),
				"li64" => new ProtodefNumericType("long", true, ByteOrder.LittleEndian),
				"lu64" => new ProtodefNumericType("ulong", false, ByteOrder.LittleEndian),
				"f32" => new ProtodefNumericType("float", true, ByteOrder.BigEndian),
				"lf32" => new ProtodefNumericType("float", true, ByteOrder.LittleEndian),
				"f64" => new ProtodefNumericType("double", true, ByteOrder.BigEndian),
				"lf64" => new ProtodefNumericType("double", true, ByteOrder.LittleEndian),
				_ => null
			};
		}

		public override void Write(Utf8JsonWriter writer, ProtodefType value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
