using YiJingFramework.Annotating.Zhouyi;
using YiJingFramework.Annotating.Zhouyi.Entities;
using YiJingFramework.Core;

var store = new ZhouyiStore(null) {
    Title = "Test Store"
};
store.Tags.Add("It is a test!");

var qianPainting = new Painting(Enumerable.Repeat(YinYang.Yang, 6));
var qian = new ZhouyiHexagram(qianPainting);
qian.Name = "Qian";
qian.Text = "Text of Qian here";
qian.FirstLine.LineText = "Line 1 of Qian";

store.UpdateStore(qian);

var s = store.SerializeToJsonString();
Console.WriteLine(s);
Console.WriteLine();

// Ouput: {"n":"Test Store","t":["It is a test!"],"gs":[],"gp":[{"t":"Hexagram Index","e":[]},{"t":"Hexagram Name","e":[{"t":"111111","c":"Qian"}]},{"t":"Hexagram Text","e":[{"t":"111111","c":"Text of Qian here"}]},{"t":"Xiang (Hexagram)","e":[]},{"t":"Tuan","e":[]},{"t":"Wenyan","e":[]},{"t":"Hexagram Yong Text","e":[]},{"t":"Xiang (Yong)","e":[]}],"gl":[{"t":"Line Text","e":[{"t":"111111100000","c":"Line 1 of Qian"}]},{"t":"Xiang (Line)","e":[]}]}