using Newtonsoft.Json.Linq;

namespace SourceGenerator.ProtoDefTypes
{
	public class ProtodefParser
	{
		public List<string> nativeTypes = new();

		public Dictionary<string, ProtodefDefiniteType> Types { get; } = new ();

		public Dictionary<string, ProtodefType> ToClientPackets { get; } = new();

		public Dictionary<string, ProtodefType> ToServerPackets { get; } = new();

		private readonly JObject _jobj;

		public ProtodefParser(string protocol_json)
		{
			_jobj = JObject.Parse(protocol_json);

			foreach(var item in _jobj.Value<JObject>("types"))
			{
				try
				{
					Types[item.Key] = Parse(item.Value);
				} 
				catch(Exception ex)
				{
					throw new Exception($"Parse types error. Type: {item.Key}", ex);
				}
				
			}

			foreach(var item in (JObject)_jobj.SelectToken("play.toClient.types"))
			{
				if(item.Key.StartsWith("packet_"))
				{
					try
					{
						ToClientPackets[item.Key] = Parse(item.Value);
					}
					catch(Exception ex)
					{
						throw new Exception($"Parse toClient packets error. Packet: {item.Key}", ex);
					}
					
				}
				
			}
			foreach(var item in (JObject)_jobj.SelectToken("play.toServer.types"))
			{
				if(item.Key.StartsWith("packet_"))
				{
					try
					{
						ToServerPackets[item.Key] = Parse(item.Value);
					}
					catch(Exception ex)
					{
						throw new Exception($"Parse toServer packets error. Packet: {item.Key}", ex);
					}
				}
				
			}

		}

		private ProtodefType GetType(string name)
		{
			if()
		}

		private ProtodefType Parse(JToken token)
		{
			if (token.Type == JTokenType.String)
			{
				if(Types.TryGetValue(token.ToString(), out var result))
				{
					return result;
				}
				return new ProtodefType(token);
			}
			else if (token.Type == JTokenType.Array)
			{
				string typeName = token.First.ToString();
				JToken last = token.Last;


				return typeName switch
				{
					"container" => ParseContainer(last),
					"switch" => ParseSwitch(last),
					"bitfield" => ParseBitfield(last),
					"mapper" => ParseMapper(last),
					"buffer" => ParseBuffer(last),
					"array" => ParseArray(last),
					"option" => ParseOption(last),
					"pstring" => ParseString(last),
					_ => ParseOther(typeName, last)

				};
			}
			else
			{
				throw new NotSupportedException(token.Type.ToString());
			}
		}

		private ProtodefOption ParseOption(JToken token)
		{
			return new ProtodefOption(Parse(token), token);
		}

		private ProtodefType ParseOther(string name, JToken token)
		{
			throw new Exception("unknown type: "+name);
			
		}
		private ProtodefString ParseString(JToken token)
		{
			return new ProtodefString(token);
		}

		private ProtodefContainer ParseContainer(JToken token)
		{
			if (token is JArray array)
			{
				List<ProtodefContainerField> fields = new();
				foreach (JObject item in array)
				{
					var type = Parse(item.Value<JToken>("type"));
					var anon = item.Value<bool>("anon");
					var name = item.Value<string>("name");
					var field = new ProtodefContainerField(anon, name, type);

					fields.Add(field);
				}
				return new ProtodefContainer(fields, token);
			}
			else
			{
				throw new ArgumentException("no array");
			}
		}

		private ProtodefSwitch ParseSwitch(JToken token)
		{
			if (token is JObject obj)
			{
				
				var compareTo = obj.Value<string>("compareTo");
				var compareToValue = obj.Value<string>("compareToValue");
				var @default = obj.Value<string>("default");

				var fields = new Dictionary<string, ProtodefType>();
				foreach (var item in obj.Value<JObject>("fields"))
				{
					fields[item.Key] = this.Parse(item.Value);
				}

				return new ProtodefSwitch(compareTo, compareToValue, fields, @default, token);
				

				
			}
			else
			{
				throw new ArgumentException("value is not JObject");
			}
		}


		private ProtodefBitField ParseBitfield(JToken token)
		{
			if (token is JArray array)
			{
				var nodes = new List<ProtodefBitFieldNode>();
				foreach (JObject obj in array)
				{
					nodes.Add(new ProtodefBitFieldNode(obj));
				}
				return new ProtodefBitField(nodes, token);
			}
			else
			{
				throw new ArgumentException("no array");
			}
		}

		private ProtodefMapper ParseMapper(JToken token)
		{
			if (token is JObject obj)
			{
				var type = Parse(obj.Value<JToken>("type"));
				var mappings = new Dictionary<string, ProtodefType>();
				foreach (var item in obj.Value<JObject>("mappings"))
				{
					mappings[item.Key] = Parse(item.Value);
				}

				return new ProtodefMapper(type, mappings, token);

			}
			else
			{
				throw new ArgumentException("no object");
			}
		}

		private ProtodefBuffer ParseBuffer(JToken token)
		{
			if (token is JObject obj)
			{
				var countType = Parse(obj.Value<JToken>("countType"));
				var count = obj.Value<string>("count");
				var rest = obj.Value<bool>("rest");

				return new ProtodefBuffer(countType, count, rest, token);
			}
			else
			{
				throw new ArgumentException("no object");
			}
		}


		private ProtodefArray ParseArray(JToken token)
		{
			if (token is JObject obj)
			{
				var type = Parse(obj["type"]);
				var countType = Parse(obj["countType"]);
				var count = obj.Value<string>("count");

				return new ProtodefArray(type, countType, count, token);
			}
			else
			{
				throw new ArgumentException("no object");
			}
		}
	}
}
