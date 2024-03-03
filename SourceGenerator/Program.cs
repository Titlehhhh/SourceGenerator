using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;



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

		//pack.Name = "Client" + ToPascalCase(packet.Key.Substring(7)) + "Packet";

		foreach (var field in packetWithFields)
		{
			string name = field.Value<string>("name");
			var type = field.Value<JToken>("type");



			
		}

	}
}



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
