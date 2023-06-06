using System.Diagnostics;
using YiJingFramework.Annotating.Zhouyi;

static void PrintQian(string storeString)
{
    var store = ZhouyiStore.DeserializeFromJsonString(storeString);
    var qian = store?.GetHexagramByName("qian");
    Debug.Assert(qian is not null);

    Console.WriteLine($"{qian.Name}: {qian.Text}");
    Console.WriteLine($"The first line: {qian.FirstLine.LineText}");
    Console.WriteLine();
}

var s = """
{
    "n": "Test Store",
    "t": [
        "It is a test!"
    ],
    "g": [
        {
            "t": "Hexagram Index",
            "e": []
        },
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
        },
        {
            "t": "Line Text",
            "e": [
                {
                    "t": "111111-0",
                    "c": "Line 1 of Qian"
                }
            ]
        }
    ]
}
""";

PrintQian(s);
// Outputs:
// Qian: Text of Qian here
// The first line: Line 1 of Qian

var store = ZhouyiStore.DeserializeFromJsonString(s);
Debug.Assert(store is not null);

var qian = store.GetHexagramByName("qian");
Debug.Assert(qian is not null);

qian.FirstLine.LineText = "QQQQQQQQQQQ";

store.UpdateStore(qian);

var newS = store.SerializeToJsonString();

PrintQian(newS);
// Outputs:
// Qian: Text of Qian here
// The first line: QQQQQQQQQQQ