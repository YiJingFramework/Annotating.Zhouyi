using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using YiJingFramework.Annotating.Zhouyi.Entities;
using YiJingFramework.Annotating.Zhouyi.InternalEntities;
using YiJingFramework.PrimitiveTypes;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount;

namespace YiJingFramework.Annotating.Zhouyi;

public sealed partial class ZhouyiStore
{
    #region painting
    private static string? FindContent(AnnotationGroup group, string target)
    {
        return group.GetEntry(target)?.Content;
    }

    private static bool FindEntry<TGua>(
        AnnotationGroup group,
        string? content,
        StringComparison comparisonType,
        [MaybeNullWhen(false)] out AnnotationEntry resultEntry,
        [MaybeNullWhen(false)] out TGua resultGua)
        where TGua : IGuaWithFixedCount<TGua>
    {
        foreach (var e in group.Entries)
        {
            if (string.Equals(e.Content, content, comparisonType))
            {
                if (Gua.TryParse(e.Target, out var gua) && TGua.TryFromGua(gua, out resultGua))
                {
                    resultEntry = e;
                    return true;
                }
            }
        }
        resultEntry = null;
        resultGua = default;
        return false;
    }

    private static string?[] FindSixContents(AnnotationGroup group, GuaHexagram target)
    {
        var result = new string?[6];
        var foundRecord = new bool[6];
        int foundCount = 0;

        foreach (var entry in group.Entries)
        {
            if (!HexagramLine.CheckAndParse(entry.Target, out var guaLine))
                continue;
            if (guaLine.Gua == target)
            {
                if (foundRecord[guaLine.LineIndex] is true)
                    continue;

                result[guaLine.LineIndex] = entry.Content;
                foundRecord[guaLine.LineIndex] = true;
                if (foundCount is 5)
                    break;
                foundCount++;
            }
        }
        return result;
    }

    /// <summary>
    /// 通过卦画获取三爻卦。
    /// Get a trigram with its painting.
    /// 获取有性能损耗，应当多复用得到的结果。
    /// The result should be reused; otherwise might cause performance loss.
    /// 得到的卦和本实例没有绑定关系。
    /// 如要修改此仓库的内容，需再调用 <seealso cref="UpdateStore(ZhouyiTrigram)"/> 。
    /// The result is not bound to this instance.
    /// Use <seealso cref="UpdateStore(ZhouyiTrigram)"/> if you want to edit the store.
    /// </summary>
    /// <param name="painting">
    /// 卦画。
    /// The painting.
    /// </param>
    /// <returns>
    /// 结果。
    /// The result.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="painting"/> 是 <c>null</c> 。
    /// <paramref name="painting"/> is <c>null</c>.
    /// </exception>
    public ZhouyiTrigram GetTrigram(GuaTrigram painting)
    {
        ArgumentNullException.ThrowIfNull(painting);

        var paintingString = painting.ToString();
        return new ZhouyiTrigram(painting) {
            Name = FindContent(this.Groups.TrigramNameGroup, paintingString),
            Nature = FindContent(this.Groups.TrigramNatureGroup, paintingString)
        };
    }

    /// <summary>
    /// 通过卦名获取三爻卦。
    /// Get a trigram with its name.
    /// 获取有性能损耗，应当多复用得到的结果。
    /// The result should be reused; otherwise might cause performance loss.
    /// 得到的卦和本实例没有绑定关系。
    /// 如要修改此仓库的内容，需再调用 <seealso cref="UpdateStore(ZhouyiTrigram)"/> 。
    /// The result is not bound to this instance.
    /// Use <seealso cref="UpdateStore(ZhouyiTrigram)"/> if you want to edit the store.
    /// </summary>
    /// <param name="name">
    /// 卦名。
    /// The name.
    /// </param>
    /// <param name="comparisonType">
    /// 字符串比较方式。
    /// String comparison type.
    /// </param>
    /// <returns>
    /// 结果。
    /// The result.
    /// </returns>
    public ZhouyiTrigram? GetTrigramByName(
        string? name,
        StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase)
    {
        if (!FindEntry<GuaTrigram>(this.Groups.TrigramNameGroup,
            name, comparisonType, out var entry, out var gua))
        {
            return null;
        }

        return new ZhouyiTrigram(gua) {
            Name = entry.Content,
            Nature = FindContent(this.Groups.TrigramNatureGroup, gua.ToString())
        };
    }

    /// <summary>
    /// 通过对应的自然事物获取三爻卦。
    /// Get a trigram with its corresponding nature.
    /// 获取有性能损耗，应当多复用得到的结果。
    /// The result should be reused; otherwise might cause performance loss.
    /// 得到的卦和本实例没有绑定关系。
    /// 如要修改此仓库的内容，需再调用 <seealso cref="UpdateStore(ZhouyiTrigram)"/> 。
    /// The result is not bound to this instance.
    /// Use <seealso cref="UpdateStore(ZhouyiTrigram)"/> if you want to edit the store.
    /// </summary>
    /// <param name="nature">
    /// 自然事物。
    /// The nature.
    /// </param>
    /// <param name="comparisonType">
    /// 字符串比较方式。
    /// String comparison type.
    /// </param>
    /// <returns>
    /// 结果。
    /// The result.
    /// </returns>
    public ZhouyiTrigram? GetTrigramByNature(
        string? nature,
        StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase)
    {
        if (!FindEntry<GuaTrigram>(this.Groups.TrigramNatureGroup,
            nature, comparisonType, out var entry, out var gua))
        {
            return null;
        }
        return new ZhouyiTrigram(gua) {
            Name = FindContent(this.Groups.TrigramNameGroup, gua.ToString()),
            Nature = entry.Content
        };
    }

    private void FillNoFindingProperties(ZhouyiHexagram hexagram)
    {
        var paintingString = hexagram.Painting.ToString();
        hexagram.Text = FindContent(this.Groups.HexagramTextGroup, paintingString);

        hexagram.Xiang = FindContent(this.Groups.XiangHexagramGroup, paintingString);
        hexagram.Tuan = FindContent(this.Groups.TuanGroup, paintingString);
        hexagram.Wenyan = FindContent(this.Groups.WenyanGroup, paintingString);

        var linesText = FindSixContents(this.Groups.LineTextGroup, hexagram.Painting);
        var linesXiang = FindSixContents(this.Groups.XiangLineGroup, hexagram.Painting);

        var line = hexagram.FirstLine;
        line.LineText = linesText[0];
        line.Xiang = linesXiang[0];

        line = hexagram.SecondLine;
        line.LineText = linesText[1];
        line.Xiang = linesXiang[1];

        line = hexagram.ThirdLine;
        line.LineText = linesText[2];
        line.Xiang = linesXiang[2];

        line = hexagram.FourthLine;
        line.LineText = linesText[3];
        line.Xiang = linesXiang[3];

        line = hexagram.FifthLine;
        line.LineText = linesText[4];
        line.Xiang = linesXiang[4];

        line = hexagram.SixthLine;
        line.LineText = linesText[5];
        line.Xiang = linesXiang[5];

        line = hexagram.Yong;
        line.LineText = FindContent(this.Groups.HexagramYongTextGroup, paintingString);
        line.Xiang = FindContent(this.Groups.XiangYongGroup, paintingString);
    }

    /// <summary>
    /// 通过卦画获取六爻卦。
    /// Get a hexagram with its painting.
    /// 获取有性能损耗，应当多复用得到的结果。
    /// The result should be reused; otherwise might cause performance loss.
    /// 得到的卦和本实例没有绑定关系。
    /// 如要修改此仓库的内容，需再调用 <seealso cref="UpdateStore(ZhouyiHexagram)"/> 。
    /// The result is not bound to this instance.
    /// Use <seealso cref="UpdateStore(ZhouyiHexagram)"/> if you want to edit the store.
    /// </summary>
    /// <param name="painting">
    /// 卦画。
    /// The painting.
    /// </param>
    /// <returns>
    /// 结果。
    /// The result.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="painting"/> 是 <c>null</c> 。
    /// <paramref name="painting"/> is <c>null</c>.
    /// </exception>
    public ZhouyiHexagram GetHexagram(GuaHexagram painting)
    {
        ArgumentNullException.ThrowIfNull(painting);

        var paintingString = painting.ToString();
        var result = new ZhouyiHexagram(painting) {
            Name = FindContent(this.Groups.HexagramNameGroup, paintingString),
            Index = FindContent(this.Groups.HexagramIndexGroup, paintingString)
        };
        this.FillNoFindingProperties(result);
        return result;
    }

    /// <summary>
    /// 通过卦名获取六爻卦。
    /// Get a hexagram with its name.
    /// 获取有性能损耗，应当多复用得到的结果。
    /// The result should be reused; otherwise might cause performance loss.
    /// 得到的卦和本实例没有绑定关系。
    /// 如要修改此仓库的内容，需再调用 <seealso cref="UpdateStore(ZhouyiHexagram)"/> 。
    /// The result is not bound to this instance.
    /// Use <seealso cref="UpdateStore(ZhouyiHexagram)"/> if you want to edit the store.
    /// </summary>
    /// <param name="name">
    /// 卦名。
    /// The name.
    /// </param>
    /// <param name="comparisonType">
    /// 字符串比较方式。
    /// String comparison type.
    /// </param>
    /// <returns>
    /// 结果。
    /// The result.
    /// </returns>
    public ZhouyiHexagram? GetHexagramByName(
        string? name,
        StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase)
    {
        if (!FindEntry<GuaHexagram>(this.Groups.HexagramNameGroup,
            name, comparisonType, out var entry, out var gua))
        {
            return null;
        }
        var result = new ZhouyiHexagram(gua) {
            Name = entry.Content,
            Index = FindContent(this.Groups.HexagramIndexGroup, gua.ToString())
        };
        this.FillNoFindingProperties(result);
        return result;
    }

    /// <summary>
    /// 通过卦序获取六爻卦。
    /// Get a hexagram with its index.
    /// 获取有性能损耗，应当多复用得到的结果。
    /// The result should be reused; otherwise might cause performance loss.
    /// 得到的卦和本实例没有绑定关系。
    /// 如要修改此仓库的内容，需再调用 <seealso cref="UpdateStore(ZhouyiHexagram)"/> 。
    /// The result is not bound to this instance.
    /// Use <seealso cref="UpdateStore(ZhouyiHexagram)"/> if you want to edit the store.
    /// </summary>
    /// <param name="index">
    /// 卦序。
    /// The index.
    /// </param>
    /// <param name="comparisonType">
    /// 字符串比较方式。
    /// String comparison type.
    /// </param>
    /// <returns>
    /// 结果。
    /// The result.
    /// </returns>
    public ZhouyiHexagram? GetHexagramByIndex(
        string? index,
        StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase)
    {
        if (!FindEntry<GuaHexagram>(this.Groups.HexagramIndexGroup,
            index, comparisonType, out var entry, out var gua))
        {
            return null;
        }
        var result = new ZhouyiHexagram(gua) {
            Name = FindContent(this.Groups.HexagramNameGroup, gua.ToString()),
            Index = entry.Content
        };
        this.FillNoFindingProperties(result);
        return result;
    }
    #endregion

    #region string
    /// <summary>
    /// 获取《系辞》。
    /// Get Xici.
    /// 得到的结果和本实例没有绑定关系。
    /// 如要修改此仓库的内容，需再调用 <seealso cref="UpdateStore(Xici)"/> 。
    /// The result is not bound to this instance.
    /// Use <seealso cref="UpdateStore(Xici)"/> if you want to edit the store.
    /// </summary>
    /// <returns>
    /// 结果。
    /// The result.
    /// </returns>
    public Xici GetXici()
    {
        var group = this.Groups.TheRestOfYizhuanGroup;
        return new Xici() {
            PartA = FindContent(group, ZhouyiGroups.TARGET_XICI_A),
            PartB = FindContent(group, ZhouyiGroups.TARGET_XICI_B)
        };
    }

    /// <summary>
    /// 获取《说卦》。
    /// Get Shuogua.
    /// 得到的结果和本实例没有绑定关系。
    /// 如要修改此仓库的内容，需再调用 <seealso cref="UpdateStore(Shuogua)"/> 。
    /// The result is not bound to this instance.
    /// Use <seealso cref="UpdateStore(Shuogua)"/> if you want to edit the store.
    /// </summary>
    /// <returns>
    /// 结果。
    /// The result.
    /// </returns>
    public Shuogua GetShuogua()
    {
        return new Shuogua() {
            Content = FindContent(this.Groups.TheRestOfYizhuanGroup, ZhouyiGroups.TARGET_SHUOGUA)
        };
    }

    /// <summary>
    /// 获取《序卦》。
    /// Get Xugua.
    /// 得到的结果和本实例没有绑定关系。
    /// 如要修改此仓库的内容，需再调用 <seealso cref="UpdateStore(Xugua)"/> 。
    /// The result is not bound to this instance.
    /// Use <seealso cref="UpdateStore(Xugua)"/> if you want to edit the store.
    /// </summary>
    /// <returns>
    /// 结果。
    /// The result.
    /// </returns>
    public Xugua GetXugua()
    {
        return new Xugua() {
            Content = FindContent(this.Groups.TheRestOfYizhuanGroup, ZhouyiGroups.TARGET_XUGUA)
        };
    }

    /// <summary>
    /// 获取《杂卦》。
    /// Get Zagua.
    /// 得到的结果和本实例没有绑定关系。
    /// 如要修改此仓库的内容，需再调用 <seealso cref="UpdateStore(Zagua)"/> 。
    /// The result is not bound to this instance.
    /// Use <seealso cref="UpdateStore(Zagua)"/> if you want to edit the store.
    /// </summary>
    /// <returns>
    /// 结果。
    /// The result.
    /// </returns>
    public Zagua GetZagua()
    {
        return new Zagua() {
            Content = FindContent(this.Groups.TheRestOfYizhuanGroup, ZhouyiGroups.TARGET_ZAGUA)
        };
    }
    #endregion
}