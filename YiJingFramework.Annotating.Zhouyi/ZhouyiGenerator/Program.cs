using System.Text.Encodings.Web;
using System.Text.Json;
using YiJingFramework.Annotating.Zhouyi;
using YiJingFramework.Annotating.Zhouyi.Entities;
using YiJingFramework.PrimitiveTypes;

ZhouyiStore store = new ZhouyiStore(null) {
    Title = "Zhouyi"
};
store.Tags.Add("origin: https://github.com/bollwarm/ZHOUYI.git");

#region trigrams
var qian = new ZhouyiTrigram(new Gua(Yinyang.Yang, Yinyang.Yang, Yinyang.Yang)) {
    Name = "乾",
    Nature = "天"
};
var dui = new ZhouyiTrigram(new Gua(Yinyang.Yang, Yinyang.Yang, Yinyang.Yin)) {
    Name = "兌",
    Nature = "澤"
};
var li = new ZhouyiTrigram(new Gua(Yinyang.Yang, Yinyang.Yin, Yinyang.Yang)) {
    Name = "離",
    Nature = "火"
};
var zhen = new ZhouyiTrigram(new Gua(Yinyang.Yang, Yinyang.Yin, Yinyang.Yin)) {
    Name = "震",
    Nature = "雷"
};
var xun = new ZhouyiTrigram(new Gua(Yinyang.Yin, Yinyang.Yang, Yinyang.Yang)) {
    Name = "巽",
    Nature = "風"
};
var kan = new ZhouyiTrigram(new Gua(Yinyang.Yin, Yinyang.Yang, Yinyang.Yin)) {
    Name = "坎",
    Nature = "水"
};
var gen = new ZhouyiTrigram(new Gua(Yinyang.Yin, Yinyang.Yin, Yinyang.Yang)) {
    Name = "艮",
    Nature = "山"
};
var kun = new ZhouyiTrigram(new Gua(Yinyang.Yin, Yinyang.Yin, Yinyang.Yin)) {
    Name = "坤",
    Nature = "地"
};

store.UpdateStore(qian);
store.UpdateStore(dui);
store.UpdateStore(li);
store.UpdateStore(zhen);
store.UpdateStore(xun);
store.UpdateStore(kan);
store.UpdateStore(gen);
store.UpdateStore(kun);
#endregion

#region hexagramsCommon
var lines = new Queue<string>(File.ReadAllLines("./hexagramsCommon.txt"));
lines.Dequeue();

for (; lines.Count > 0;)
{
    ZhouyiHexagram hexagram;
    {
        // <White Line>
        var line = lines.Dequeue();
        if (!string.IsNullOrWhiteSpace(line))
            throw new Exception();
    }
    {
        // 《易經》第一卦乾乾為天乾上乾下
        var line = lines.Dequeue();
        var s = line.Split("卦");
        if (s.Length is not 2)
            throw new Exception();

        ZhouyiTrigram upper;
        ZhouyiTrigram lower;
        if (s[1].Length == "乾乾為天乾上乾下".Length)
        {
            upper = store.GetTrigramByName(s[1][4].ToString())!;
            lower = store.GetTrigramByName(s[1][6].ToString())!;
            hexagram = new ZhouyiHexagram(new Gua(lower.Painting.Concat(upper.Painting))) {
                Name = s[1][0].ToString()
            };
        }
        else
        {
            if (s[1].Length != "乾乾乾乾為天乾上乾下".Length)
                throw new Exception();
            upper = store.GetTrigramByName(s[1][6].ToString())!;
            lower = store.GetTrigramByName(s[1][8].ToString())!;
            hexagram = new ZhouyiHexagram(new Gua(lower.Painting.Concat(upper.Painting))) {
                Name = s[1][0..2]
            };
        }
        hexagram.Index = s[0][(s[0].LastIndexOf('第') + 1)..];
        if (upper.Painting == lower.Painting)
        {
            var r =
                $"《易經》" +
                $"第{hexagram.Index}卦{hexagram.Name}" +
                $"{hexagram.Name}為{upper.Nature}" +
                $"{upper.Name}上{lower.Name}下";
            if (r != line)
                throw new Exception();
        }
        else
        {
            var r =
                $"《易經》" +
                $"第{hexagram.Index}卦{hexagram.Name}" +
                $"{upper.Nature}{lower.Nature}{hexagram.Name}" +
                $"{upper.Name}上{lower.Name}下";
            if (r != line)
                throw new Exception();
        }
    }
    {
        // <White Line>
        var line = lines.Dequeue();
        if (!string.IsNullOrWhiteSpace(line))
            throw new Exception();
    }
    {
        // 乾，元亨，利貞。
        var line = lines.Dequeue();
        if (!line.StartsWith($"{hexagram.Name}，"))
            throw new Exception();
        hexagram.Text = line[$"{hexagram.Name}，".Length..];
    }
    {
        // 《彖》曰：大哉乾元，萬物資始，乃統天。云行雨施，品物流形。大明始終，六位時成，時乘六龍以御天。乾道變化，各正性命，保合大和，乃利貞。首出庶物，萬國咸寧。
        var line = lines.Dequeue();
        if (!line.StartsWith("《彖》曰："))
            throw new Exception();
        hexagram.Tuan = line["《彖》曰：".Length..];
    }
    {
        // 《象》曰：天行健，君子以自強不息。
        var line = lines.Dequeue();
        if (!line.StartsWith("《象》曰："))
            throw new Exception();
        hexagram.Xiang = line["《象》曰：".Length..];
    }
    {
        // 初九：潛龍勿用。
        var line = lines.Dequeue();
        var starting = "初" + (hexagram.Painting[0].IsYang ? "九" : "六") + "：";
        if (!line.StartsWith(starting))
            throw new Exception();
        hexagram.FirstLine.LineText = line[starting.Length..];
    }
    {
        // 《象》曰：潛龍勿用，陽在下也。
        var line = lines.Dequeue();
        var starting = "《象》曰：";
        if (!line.StartsWith(starting))
            throw new Exception();
        hexagram.FirstLine.Xiang = line[starting.Length..];
    }
    {
        var line = lines.Dequeue();
        var starting = (hexagram.Painting[1].IsYang ? "九" : "六") + "二：";
        if (!line.StartsWith(starting))
            throw new Exception();
        hexagram.SecondLine.LineText = line[starting.Length..];
    }
    {
        var line = lines.Dequeue();
        var starting = "《象》曰：";
        if (!line.StartsWith(starting))
            throw new Exception();
        hexagram.SecondLine.Xiang = line[starting.Length..];
    }
    {
        var line = lines.Dequeue();
        var starting = (hexagram.Painting[2].IsYang ? "九" : "六") + "三：";
        if (!line.StartsWith(starting))
            throw new Exception();
        hexagram.ThirdLine.LineText = line[starting.Length..];
    }
    {
        var line = lines.Dequeue();
        var starting = "《象》曰：";
        if (!line.StartsWith(starting))
            throw new Exception();
        hexagram.ThirdLine.Xiang = line[starting.Length..];
    }
    {
        var line = lines.Dequeue();
        var starting = (hexagram.Painting[3].IsYang ? "九" : "六") + "四：";
        if (!line.StartsWith(starting))
            throw new Exception();
        hexagram.FourthLine.LineText = line[starting.Length..];
    }
    {
        var line = lines.Dequeue();
        var starting = "《象》曰：";
        if (!line.StartsWith(starting))
            throw new Exception();
        hexagram.FourthLine.Xiang = line[starting.Length..];
    }
    {
        var line = lines.Dequeue();
        var starting = (hexagram.Painting[4].IsYang ? "九" : "六") + "五：";
        if (!line.StartsWith(starting))
            throw new Exception();
        hexagram.FifthLine.LineText = line[starting.Length..];
    }
    {
        var line = lines.Dequeue();
        var starting = "《象》曰：";
        if (!line.StartsWith(starting))
            throw new Exception();
        hexagram.FifthLine.Xiang = line[starting.Length..];
    }
    {
        var line = lines.Dequeue();
        var starting = "上" + (hexagram.Painting[5].IsYang ? "九" : "六") + "：";
        if (!line.StartsWith(starting))
            throw new Exception();
        hexagram.SixthLine.LineText = line[starting.Length..];
    }
    {
        var line = lines.Dequeue();
        var starting = "《象》曰：";
        if (!line.StartsWith(starting))
            throw new Exception();
        hexagram.SixthLine.Xiang = line[starting.Length..];
    }
    store.UpdateStore(hexagram);
}
#endregion

var sLength = "取自 https://github.com/bollwarm/ZHOUYI.git".Length;

#region qiankun
ZhouyiHexagram qian6 = store.GetHexagramByName("乾")!;
qian6.Yong.LineText = "見群龍無首，吉。";
qian6.Yong.Xiang = "用九，天德不可為首也。";
qian6.Wenyan = File.ReadAllText("wenyanQian.txt")[sLength..];
store.UpdateStore(qian6);

ZhouyiHexagram kun6 = store.GetHexagramByName("坤")!;
kun6.Yong.LineText = "利永貞。";
kun6.Yong.Xiang = "用六永貞，以大終也。";
kun6.Wenyan = File.ReadAllText("wenyanKun.txt")[sLength..];
store.UpdateStore(kun6);
#endregion

#region others
store.UpdateStore(new Shuogua() {
    Content = File.ReadAllText("shuogua.txt")[sLength..]
});
store.UpdateStore(new Xugua() {
    Content = File.ReadAllText("xugua.txt")[sLength..]
});
store.UpdateStore(new Zagua() {
    Content = File.ReadAllText("zagua.txt")[sLength..]
});
store.UpdateStore(new Xici() {
    PartA = File.ReadAllText("xiciA.txt")[sLength..],
    PartB = File.ReadAllText("xiciB.txt")[sLength..]
});
#endregion

File.WriteAllText("out.json", store.SerializeToJsonString());
File.WriteAllText("out2.json", store.SerializeToJsonString(new JsonSerializerOptions() {
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    WriteIndented = true
}));
