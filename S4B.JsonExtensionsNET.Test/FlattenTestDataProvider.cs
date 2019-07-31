using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace S4B.JsonExtensionsNET.Test
{
    public class FlattenTestDataProvider : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new Model(),
                new (string key, string value)[] {
                    ("IntVal", "0"),
                    ("StringVal", null),
                    ("Dict.a", "b"),
                    ("Dict.key", null),
                    ("Dict.key1", ""),
                    ("SubModel.Hs[0]", "1"),
                    ("SubModel.Hs[1]", "2"),
                    ("SubModel.Hs[2]", "6"),
                    ("DecimalArray[0]", "1"),
                    ("DecimalArray[1]", "4"),
                    ("DecimalArray[2]", "7"),
                    ("DecimalArray[3]", "9"),
                    ("DecimalArray[4]", "11.4"),
                    ("DecimalArray[5]", "79228162514264337593543950335"),
                    ("NullArray", null)
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class Model
    {
        public int IntVal { get; set; }
        public string StringVal { get; set; }
        public Dictionary<string, string> Dict { get; set; } = new Dictionary<string, string>
        {
            {"a", "b" },
            {"key", null },
            {"key1", "" },
        };
        public Model1 SubModel { get; set; } = new Model1();
        public decimal[] DecimalArray { get; set; } = new decimal[] { 1m, 4, 7, 9, 11.4m, decimal.MaxValue };
        public float[] NullArray { get; set; }
    }

    public class Model1
    {
        public HashSet<int> Hs { get; set; } = new HashSet<int> { 1, 2, 6 };
    }
}
