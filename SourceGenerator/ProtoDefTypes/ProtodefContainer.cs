namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefContainer : ProtodefObject
	{
		//public override string TypeName => "Container";

		private readonly List<ProtodefContainerField> fields = new();


		public ProtodefContainer(List<ProtodefContainerField> fields)
		{
			this.fields = fields;
		}
	}


}
