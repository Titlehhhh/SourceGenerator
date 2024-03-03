using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;



 string ToPascalCase(string original)
{
	Regex invalidCharsRgx = new Regex("[^_a-zA-Z0-9]");
	Regex whiteSpace = new Regex(@"(?<=\s)");
	Regex startsWithLowerCaseChar = new Regex("^[a-z]");
	Regex firstCharFollowedByUpperCasesOnly = new Regex("(?<=[A-Z])[A-Z0-9]+$");
	Regex lowerCaseNextToNumber = new Regex("(?<=[0-9])[a-z]");
	Regex upperCaseInside = new Regex("(?<=[A-Z])[A-Z]+?((?=[A-Z][a-z])|(?=[0-9]))");

	// replace white spaces with undescore, then replace all invalid chars with empty string
	var pascalCase = invalidCharsRgx.Replace(whiteSpace.Replace(original, "_"), string.Empty)
		// split by underscores
		.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)
		// set first letter to uppercase
		.Select(w => startsWithLowerCaseChar.Replace(w, m => m.Value.ToUpper()))
		// replace second and all following upper case letters to lower if there is no next lower (ABC -> Abc)
		.Select(w => firstCharFollowedByUpperCasesOnly.Replace(w, m => m.Value.ToLower()))
		// set upper case the first lower case following a number (Ab9cd -> Ab9Cd)
		.Select(w => lowerCaseNextToNumber.Replace(w, m => m.Value.ToUpper()))
		// lower second and next upper case letters except the last if it follows by any lower (ABcDEf -> AbcDef)
		.Select(w => upperCaseInside.Replace(w, m => m.Value.ToLower()));

	return string.Concat(pascalCase);
}

using var sr = new StreamReader("protocol.json");


dynamic json = JObject.Parse(sr.ReadToEnd());


JObject clientPackets = json.play.toServer.types;


string[] supportedTypes =
{
	"varint",
	"varlong",
    //"optvarint": 
   // "pstring": 
    "u16",
	"u8",
	"i64",
	//"buffer",
	"i32",
	"i8",
	"bool",
	"i16",
	"f32",
	"f64",
};


List<Packet> packets = new();

foreach (var packet in clientPackets)
{
	if (packet.Key.StartsWith("packet_"))
	{
		JArray packetWithFields = (JArray)packet.Value.Last;



		bool ok = true;

		Packet pack = new();

		pack.Name = "Client" + ToPascalCase(packet.Key.Substring(7)) + "Packet";

		foreach (var field in packetWithFields)
		{
			string name = ToPascalCase(field.Value<string>("name"));
			var type = field.Value<JToken>("type");

			if (type.Type != JTokenType.String)
			{

				ok = false;
				continue;
			}
			if (!supportedTypes.Contains(type.ToString()))
			{
				ok = false;
				continue;
			}

			pack.Fields.Add(name, type.ToString());
		}

		if (ok)
		{
			packets.Add(pack);
		}
	}
}


Dictionary<string, TypeMap> typeToType = new Dictionary<string, TypeMap>
{
	{"varint", new("int", "ReadVarInt","WriteVarInt")},
	{"varlong", new("long", "ReadVarLong","WriteVarLong") },
	{"u16", new("ushort", "ReadUnsignedShort","WriteUnsignedShort") },
	{"u8",  new("byte", "ReadUnsignedByte","WriteUnsignedByte") },
	{"i64", new("long", "ReadLong","WriteLong") },
	{"i32", new("int", "ReadInt","WriteInt") },
	{"i8",  new("sbyte", "ReadByte","WriteByte") },
	{"bool",new("bool", "ReadBoolean","WriteBoolean") },
	{"i16", new("short", "ReadShort","WriteShort") },
	{"f32",new("float", "ReadFloat","WriteFloat") },
	{"f64", new("double" , "ReadDouble","WriteDouble")},
};


string template = await File.ReadAllTextAsync("PacketTemplate.cs");


if (Directory.Exists("outputPackets"))
{
	Directory.Delete("outputPackets", true);
	Directory.CreateDirectory("outputPackets");
}
else
{
	Directory.CreateDirectory("outputPackets");
}

List<Task> tasks = new();

foreach (var packet in packets)
{



	StringBuilder propsBuilder = new();
	StringBuilder readBuilder = new();
	StringBuilder writeBuilder = new();

	foreach (var field in packet.Fields)
	{
		TypeMap map = typeToType[field.Value];

		string prop = $"\t\tpublic {map.NET_Name} {field.Key} {{ get; set; }}";

		string read = $"\t\t\t{field.Key} = reader.{map.ReadMethod}();";

		string write = $"\t\t\twriter.{map.WriteMethod}({field.Key});";

		propsBuilder.AppendLine(prop);
		readBuilder.AppendLine(read);
		writeBuilder.AppendLine(write);

	}


	string source = string.Format(
		template,
		packet.Name,
		readBuilder.ToString().Trim(),
		writeBuilder.ToString().Trim(),
		propsBuilder.ToString().Trim());


	var t = File.WriteAllTextAsync(Path.Combine("outputPackets", $"{packet.Name}.cs"), source);
	tasks.Add(t);

}

await Task.WhenAll(tasks);


public class TypeMap
{
	public string NET_Name { get; }

	public string ReadMethod { get; }
	public string WriteMethod { get; }

	public TypeMap(string nET_Name, string readMethod, string writeMethod)
	{
		NET_Name = nET_Name;
		ReadMethod = readMethod;
		WriteMethod = writeMethod;
	}
}

public class Packet
{
	public int Id { get; set; }
	public string Name { get; set; }

	public override string ToString()
	{
		return Name;
	}

	public Dictionary<string, string> Fields { get; } = new();
}
