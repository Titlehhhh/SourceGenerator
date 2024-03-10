using SourceGenerator;
using SourceGenerator.ProtoDefTypes;


internal class Program
{
	private static void Main(string[] args)
	{


		using var sr = new StreamReader("protocol.json");


		string json = sr.ReadToEnd();


		var prot = RustToCsharpConverter.ReadProtocol(json);



		Console.WriteLine();

		//Console.ReadLine();
	}
}