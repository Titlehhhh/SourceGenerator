namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefContainer : ProtodefType
	{
		private readonly List<ProtodefContainerField> fields = new();


		public ProtodefContainer(List<ProtodefContainerField> fields)
		{
			this.fields = fields;
		}
	}


}
