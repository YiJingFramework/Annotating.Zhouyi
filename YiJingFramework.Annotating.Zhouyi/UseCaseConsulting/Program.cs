using System.Diagnostics;
using YiJingFramework.Annotating.Zhouyi;

var s = """
{
  "n": "Test Store",
  "t": [
    "It is a test!"
  ],
  "gp": [
    {
      "t": "Hexagram Name",
      "e": [
        {
          "t": "111111",
          "c": "Qian"
        }
      ]
    },
    {
      "t": "Hexagram Text",
      "e": [
        {
          "t": "111111",
          "c": "Text of Qian here"
        }
      ]
    }
  ],
  "gl": [
    {
      "t": "Line Text",
      "e": [
        {
          "t": "111111100000",
          "c": "Line 1 of Qian"
        }
      ]
    }
  ]
}
""";

var store = ZhouyiStore.DeserializeFromJsonString(s);
Debug.Assert(store is not null);
// DeserializeFromJsonString will throw exceptions when failing.
// But 'store' might be null when 's' is just the string "null".

Console.WriteLine(store.Title);

var qian = store.GetHexagramByName("qian");
// case-insensitive by default
Debug.Assert(qian is not null);
// GetHexagramByName won't throw any exception but just return null.

Console.WriteLine($"{qian.Name}: {qian.Text}");
Console.WriteLine($"The first line: {qian.FirstLine.LineText}");
Console.WriteLine();

/* 
 * Outputs:
 * Test Store
 * Qian: Text of Qian here
 * The first line: Line 1 of Qian
 */