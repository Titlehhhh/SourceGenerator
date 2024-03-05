using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.ObjectModel;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefContainer : ProtodefObject
	{
		//public override string TypeName => "Container";

		private readonly List<ProtodefField> fields = new();
		public ProtodefContainer(JToken value) 
		{
			if(value is JArray array)
			{
				foreach(JObject item in array)
				{
					fields.Add(new ProtodefField(item));
				}
			}
			else
			{
				throw new ArgumentException("no array");
			}
		}

		public ProtodefContainer(List<ProtodefField> fields)
		{
			this.fields = fields;
		}
	}


}
