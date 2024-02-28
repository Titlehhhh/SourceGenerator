
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Linq;

static string ConvertToPascalCase(string str)
{
	TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
	return textInfo.ToTitleCase(str.Replace("_", " ")).Replace(" ", "");
}

using var sr = new StreamReader("protocol.json");

dynamic json = JObject.Parse(sr.ReadToEnd());


JObject clientPackets = json.play.toClient.types;



foreach (var packet in clientPackets)
{
	if (packet.Key.StartsWith("packet_"))
	{
		JArray packetWithFields = (JArray)packet.Value.Last;
		
	}
}

Console.ReadLine();

