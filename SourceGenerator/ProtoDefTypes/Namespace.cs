using System.Collections;

namespace SourceGenerator.ProtoDefTypes
{
	public class Namespace : ProtodefType, IEnumerable<ProtodefType>
	{
		private IEnumerable<ProtodefType> _types;
		public Namespace(ProtodefType type)
		{
			_types = new[] { type };
		}
		public Namespace(IEnumerable<ProtodefType> types)
		{
			_types = types;
		}
		public IEnumerator<ProtodefType> GetEnumerator()
		{
			return _types.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _types.GetEnumerator();
		}
	}
}
