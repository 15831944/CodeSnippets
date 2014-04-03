using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataObjects
{
    /// <summary>
    /// Custom dictionary, used for serialization or deserialization of JSON object into key/value pair structure
    /// </summary>
    [Serializable]
    public class JsonDictionary : ISerializable
    {
        private Dictionary<string, object> m_entries;

        public JsonDictionary()
        {
            m_entries = new Dictionary<string, object>();
        }

        public IEnumerable<KeyValuePair<string, object>> Entries
        {
            get { return m_entries; }
        }

        public void Add(string key, object value)
        {
            m_entries.Add(key, value);
        }

        public object this[string key]
        {
            get { return m_entries[key]; }
            set { m_entries[key] = value; }
        }

        public bool HasKey(string key)
        {
            return m_entries.ContainsKey(key);
        }

        protected JsonDictionary(SerializationInfo info, StreamingContext context)
        {
            m_entries = new Dictionary<string, object>();
            foreach (var entry in info)
            {
                object value = entry.Value;
                if (value.GetType() == typeof(JValue))
                {
                    JValue valueJson = value as JValue;
                    switch (valueJson.Type)
                    {
                        case JTokenType.Array:
                            break;
                        case JTokenType.Boolean:
                            value = valueJson.Value<bool>();
                            break;
                        case JTokenType.Object:
                            break;
                        case JTokenType.String:
                            value = valueJson.Value<string>();
                            break;
                        case JTokenType.Integer:
                            value = valueJson.Value<int>();
                            break;
                        default:
                            break;
                    }
                }
                m_entries.Add(entry.Name, value);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            foreach (var entry in m_entries)
            {
                info.AddValue(entry.Key, entry.Value);
            }
        }
    }
}
