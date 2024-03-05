using Newtonsoft.Json.Linq;
using SourceGenerator.ProtoDefTypes;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;



using var sr = new StreamReader("protocol.json");


dynamic json = JObject.Parse(sr.ReadToEnd());


JObject clientPackets = json.play.toServer.types;

List<ProtodefType> packets = new();

foreach(var token in clientPackets)
{
	ProtodefType type = ProtodefParser.ParseToken(token.Value);
	packets.Add(type);
}




Console.ReadLine();