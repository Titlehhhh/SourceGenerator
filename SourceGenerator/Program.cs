using Newtonsoft.Json.Linq;
using SourceGenerator.ProtoDefTypes;


internal class Program
{
	private static void Main(string[] args)
	{


		using var sr = new StreamReader("protocol.json");


		string json = sr.ReadToEnd();


		ProtodefParser parser = new(json);

		



		//Console.ReadLine();
	}
}