using System.Text.Encodings.Web;
using YiJingFramework.Core;
using YiJingFramework.References.Zhouyi.Zhuan;
using A = YiJingFramework.Annotating.Zhouyi;
using R = YiJingFramework.References.Zhouyi;

Console.WriteLine("Please enter the path of the old translation file (jing):");
var jingPath = Console.ReadLine();

Console.WriteLine("Please enter the path of the old translation file (xiang):");
var xiangPath = Console.ReadLine();

Console.WriteLine("Please enter the path of the old translation file (tuan):");
var tuanPath = Console.ReadLine();

var store = new A.ZhouyiStore(null);
if (!string.IsNullOrEmpty(jingPath))
{
    var zhouyi = new R.Zhouyi(jingPath);
    for (int i = 0; i < 8; i++)
    {
        var painting = Painting.Parse(Convert.ToString(i, 2).PadLeft(3, '0'));
        var trigram = zhouyi.GetTrigram(painting);
        var newTrigram = store.GetTrigram(trigram.GetPainting());

        newTrigram.Nature = trigram.Nature;
        newTrigram.Name = trigram.Name;

        store.UpdateStore(newTrigram);
    }
    for (int i = 1; i <= 64; i++)
    {
        var hexagram = zhouyi.GetHexagram(i);
        var newHexagram = store.GetHexagram(hexagram.GetPainting());

        newHexagram.Index = hexagram.Index.ToString();
        newHexagram.Name = hexagram.Name;
        newHexagram.Text = hexagram.Text;

        newHexagram.FirstLine.LineText = hexagram.FirstLine.LineText;
        newHexagram.SecondLine.LineText = hexagram.SecondLine.LineText;
        newHexagram.ThirdLine.LineText = hexagram.ThirdLine.LineText;
        newHexagram.FourthLine.LineText = hexagram.FourthLine.LineText;
        newHexagram.FifthLine.LineText = hexagram.FifthLine.LineText;
        newHexagram.SixthLine.LineText = hexagram.SixthLine.LineText;
        newHexagram.Yong.LineText = hexagram.ApplyNinesOrApplySixes?.LineText;

        store.UpdateStore(newHexagram);
    }
}

if (!string.IsNullOrEmpty(xiangPath))
{
    var zhouyi = new R.Zhouyi();
    var xiang = new XiangZhuan(xiangPath);
    for (int i = 1; i <= 64; i++)
    {
        var hexagram = zhouyi.GetHexagram(i);
        var newHexagram = store.GetHexagram(hexagram.GetPainting());

        newHexagram.Xiang = xiang[hexagram];

        newHexagram.FirstLine.Xiang = xiang[hexagram.FirstLine];
        newHexagram.SecondLine.Xiang = xiang[hexagram.SecondLine];
        newHexagram.ThirdLine.Xiang = xiang[hexagram.ThirdLine];
        newHexagram.FourthLine.Xiang = xiang[hexagram.FourthLine];
        newHexagram.FifthLine.Xiang = xiang[hexagram.FifthLine];
        newHexagram.SixthLine.Xiang = xiang[hexagram.SixthLine];
        if (hexagram.ApplyNinesOrApplySixes is not null)
            newHexagram.Yong.Xiang = xiang[hexagram.ApplyNinesOrApplySixes];

        store.UpdateStore(newHexagram);
    }
}

if (!string.IsNullOrEmpty(tuanPath))
{
    var zhouyi = new R.Zhouyi();
    var tuan = new Tuanzhuan(tuanPath);
    for (int i = 1; i <= 64; i++)
    {
        var hexagram = zhouyi.GetHexagram(i);
        var newHexagram = store.GetHexagram(hexagram.GetPainting());

        newHexagram.Tuan = tuan[hexagram];

        store.UpdateStore(newHexagram);
    }
}

Console.WriteLine(store.SerializeToJsonString(new() {
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
}));

Console.WriteLine("Please enter the path of the new store file:");
var newPath = Console.ReadLine();
if (!string.IsNullOrEmpty(newPath))
    File.WriteAllText(newPath, store.SerializeToJsonString());
