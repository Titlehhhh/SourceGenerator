using Newtonsoft.Json.Linq;
using SourceGenerator.ProtoDefTypes;


internal class Program
{
	private static void Main(string[] args)
	{


		using var sr = new StreamReader("protocol.json");


		dynamic json = JObject.Parse(sr.ReadToEnd());


		List<string> nativeTypes = new();

		foreach (JProperty item in json.types)
		{
			if (item.Value.ToString() == "native")
			{
				nativeTypes.Add(item.Name);
			}
		}

		JObject clientPackets = json.play.toClient.types;

		List<ProtodefType> packets = new();

		foreach (var token in clientPackets)
		{
			//ProtodefObject type = ProtodefParser.ParseToken(token.Value);
			//packets.Add(type);
		}




		Console.ReadLine();
	}
}