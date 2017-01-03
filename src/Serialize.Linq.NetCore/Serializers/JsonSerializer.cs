#region Copyright
//  Copyright, Sascha Kiefer (esskar)
//  Released under LGPL License.
//  
//  License: https://raw.github.com/esskar/Serialize.Linq/master/LICENSE
//  Contributing: https://github.com/esskar/Serialize.Linq
#endregion

using System;
using System.IO;
using Serialize.Linq.Interfaces;
using Serialize.Linq.Nodes;

namespace Serialize.Linq.Serializers
{
    public class JsonSerializer : ISerializer
    {
        public void Serialize<T>(Stream stream, T obj) where T : Nodes.Node
        {
            var json = Serialize(obj);
            var writer = new StreamWriter(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            writer.Write(json);
            writer.Flush();
        }

        public string Serialize<T>(T obj) where T : Node
        {
            try
            {
                var jsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings()
                {
                    TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
                };

                return Newtonsoft.Json.JsonConvert.SerializeObject(obj, jsonSerializerSettings);
            }
            catch (Exception ex)
            {
                throw new System.Runtime.Serialization.SerializationException("Error converting type: " + ex.Message, ex);
            }
        }

        public T Deserialize<T>(Stream stream) where T : Nodes.Node
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            var reader = new StreamReader(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            return Deserialize<T>(reader.ReadToEnd());
        }



        public T Deserialize<T>(string text) where T : Node
        {
            try
            {
                var jsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings()
                {
                    TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
                };

                return (T)Newtonsoft.Json.JsonConvert.DeserializeObject(text, jsonSerializerSettings);
            }
            catch (Exception ex)
            {
                throw new System.Runtime.Serialization.SerializationException("Error converting type: " + ex.Message, ex);
            }
        }
    }
}