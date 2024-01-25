using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Utility.Savegame.Migration
{
    public class JsonMigrationObject
    {
        private class MigrationObjectConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(JsonMigrationObject);
            }

            public override bool CanWrite => false;

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var tokenReader = reader as JTokenReader;
                return new JsonMigrationObject(tokenReader.CurrentToken);
            }
        }

        private readonly JToken _jToken;
        private readonly JsonSerializer _serializer;

        public JsonMigrationObject(string savegameJson) : this(JObject.Parse(savegameJson))
        {
        }

        public JsonMigrationObject() : this(new JObject())
        {
        }


        private JsonMigrationObject(JToken jToken)
        {
            _jToken = jToken;

            _serializer = new JsonSerializer();
            _serializer.Converters.Add(new MigrationObjectConverter());
        }

        public T Get<T>(string propertyName)
        {
            if (CanGet(propertyName))
                return DoGet<T>(propertyName);

            throw new InvalidOperationException($"Can't get property {propertyName}");
        }

        public T Get<T>(string propertyName, T defaultValue)
        {
            return CanGet(propertyName)
                ? DoGet<T>(propertyName)
                : defaultValue;
        }

        public void Set<T>(string propertyName, T value)
        {
            var token = CastToJToken(value);
            Set(propertyName, token);
        }

        public void Set(string propertyName, JsonMigrationObject value)
        {
            Set(propertyName, value._jToken);
        }

        public void Add<T>(string propertyName, T value)
        {
            if (!Exists(propertyName))
                Add(propertyName, CastToJToken(value));
        }

        public void Add(string propertyName, JsonMigrationObject value)
        {
            if (!Exists(propertyName))
                Add(propertyName, value._jToken);
        }

        public void SetOrAdd<T>(string propertyName, T value)
        {
            if (Exists(propertyName))
                Set(propertyName, value);
            else
                Add(propertyName, value);
        }

        public void Remove(string propertyName)
        {
            var token = _jToken[propertyName];
            token.Parent.Remove();
        }

        public bool Exists(string propertyName)
        {
            var token = _jToken[propertyName];
            return (token != null);
        }

        public override string ToString()
        {
            return _jToken.ToString();
        }

        private bool CanGet(string propertyName)
        {
            return _jToken[propertyName] != null;
        }

        private T DoGet<T>(string propertyName)
        {
            return _jToken[propertyName].ToObject<T>(_serializer);
        }

        private void Add(string propertyName, JToken value)
        {
            if (IsObjectType(_jToken) && _jToken[propertyName] == null)
            {
                ((JObject)_jToken).Add(propertyName, value);
            }
            else
            {
                throw new ArgumentException("Either property " + propertyName + " exists already or can't be cast");
            }
        }

        private void Set(string propertyName, JToken value)
        {
            if (_jToken[propertyName] == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            _jToken[propertyName] = value;
        }

        private JToken CastToJToken<T>(T value)
        {
            var cast = JToken.FromObject(value, _serializer);
            if (cast == null)
            {
                throw new ArgumentException("Value type " + typeof(T).FullName + "can not be cast to JToken.");
            }
            return cast;
        }

        private static bool IsObjectType(JToken token)
        {
            return token.Type == JTokenType.Object;
        }
    }
}