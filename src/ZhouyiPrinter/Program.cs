using System.Diagnostics;
using YiJingFramework.Annotating.Zhouyi;
using YiJingFramework.PrimitiveTypes;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount;

Console.WriteLine("Path of the store file ('./store.json' by default):");
var path = Console.ReadLine();
if (string.IsNullOrEmpty(path))
    path = "./store.json";

var s = File.ReadAllText(path);
var store = ZhouyiStore.DeserializeFromJsonString(s);
Debug.Assert(store is not null);

for (int i = 0; i < 64; i++)
{
    var paintingString = Convert.ToString(i, 2).PadLeft(6, '0');
    var painting = GuaHexagram.Parse(paintingString);
    var hexagram = store.GetHexagram(painting);
    var (upperPainting, lowerPainting) = hexagram.SplitToTrigrams();
    var upper = store.GetTrigram(upperPainting);
    var lower = store.GetTrigram(lowerPainting);

    Console.WriteLine(
        $"第{hexagram.Index}卦 " +
        $"{upper.Nature}{lower.Nature}{hexagram.Name} " +
        $"{upper.Name}上{upper.Name}下");
    Console.WriteLine($"{hexagram.Name}：{hexagram.Text}");
    Console.WriteLine($"象曰：{hexagram.Xiang}");
    Console.WriteLine($"彖曰：{hexagram.Tuan}");

    static string LineTitle(int line, Yinyang yinYang)
    {
        var yinYangString = yinYang.IsYang ? "九" : "六";
        return line switch {
            1 => $"初{yinYangString}",
            2 => $"{yinYangString}二",
            3 => $"{yinYangString}三",
            4 => $"{yinYangString}四",
            5 => $"{yinYangString}五",
            6 => $"上{yinYangString}",
            _ => $"用{yinYangString}"
        };
    }
    foreach (var line in hexagram.EnumerateYaos(false))
    {
        Debug.Assert(line.YinYang.HasValue);
        var title = LineTitle(line.YaoIndex, line.YinYang.Value);
        Console.WriteLine($"{title}：{line.YaoText}");
        Console.WriteLine($"象曰：{line.Xiang}");
    }
    if (hexagram.Yong.YaoText is not null || hexagram.Yong.Xiang is not null)
    {
        var title = LineTitle(0, painting[0]);
        Console.WriteLine($"{title}：{hexagram.Yong.YaoText}");
        Console.WriteLine($"象曰：{hexagram.Yong.Xiang}");
    }
    if (hexagram.Wenyan is not null)
    {
        Console.WriteLine($"文言曰：");
        Console.WriteLine($"{hexagram.Wenyan}");
    }

    Console.WriteLine();
}

var xici = store.GetXici();
Console.WriteLine($"系辭上");
Console.WriteLine($"{xici.PartA}");
Console.WriteLine();
Console.WriteLine($"系辭下");
Console.WriteLine($"{xici.PartB}");
Console.WriteLine();

var shuogua = store.GetShuogua();
Console.WriteLine($"說卦");
Console.WriteLine($"{shuogua.Content}");
Console.WriteLine();

var xugua = store.GetXugua();
Console.WriteLine($"序卦");
Console.WriteLine($"{xugua.Content}");
Console.WriteLine();

var zagua = store.GetZagua();
Console.WriteLine($"雜卦");
Console.WriteLine($"{zagua.Content}");
Console.WriteLine();
