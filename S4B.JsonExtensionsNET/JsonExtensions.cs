using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("S4B.JsonExtensionsNET.Test")]

namespace S4B.JsonExtensionsNET
{
    public static class JsonExtensions
    {
        public static IEnumerable<(string key, string value)> Flatten<T>(T obj, char separator = '.') where T : new()
        {
            var token = JToken.FromObject(obj);
            return Flatten(token, separator);
        }

        public static T Unflatten<T>((string key, string value)[] data, char separator = '.')
        {
            var setting = new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Merge
            };
            var res = UnflattenRow(data[0].key, data[0].value, separator);
            for (int i = 1; i < data.Length; i++)
            {
                var (key, value) = data[i];
                res.Merge(UnflattenRow(key, value, separator), setting);
            }
            return res.ToObject<T>();
        }
        internal static JContainer UnflattenRow(string path, string value, char separator)
        {
            JContainer lastItem = null;
            var segments = path.Split(new char[] { separator, '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = segments.Length - 1; i >= 0; i--)
            {
                var segment = segments[i];
                if (int.TryParse(segment, out var index))
                {
                    var array = new JArray();
                    for (int j = 0; j < index; j++)
                        array.Add(null);
                    array.Add(lastItem ?? (JToken)value);
                    lastItem = array;
                }
                else
                {
                    var obj = new JObject
                    {
                        { segment, lastItem ?? (JToken)value }
                    };
                    lastItem = obj;

                }
            }
            return lastItem;
        }

        internal static IEnumerable<(string key, string value)> Flatten(JToken token, char separator = '.')
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    foreach (var item in token.Children<JProperty>())
                    {
                        var res = Flatten(item);
                        foreach (var r in res)
                            yield return r;
                    }
                    break;
                case JTokenType.Array:
                    foreach (var item in token.Children())
                    {
                        var res = Flatten(item);
                        foreach (var r in res)
                            yield return r;
                    }
                    break;
                case JTokenType.Property:
                    foreach (var item in ((JProperty)token).Children())
                    {
                        var res = Flatten(item);
                        foreach (var r in res)
                            yield return r;
                    }
                    break;
                default:
                    yield return (separator == '.' ? token.Path : token.Path.Replace('.', separator), ((JValue)token).Value?.ToString());
                    break;
            }
        }

    }
}
