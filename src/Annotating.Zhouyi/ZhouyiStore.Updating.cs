using System.Diagnostics;
using YiJingFramework.Annotating.Zhouyi.Entities;
using YiJingFramework.Annotating.Zhouyi.InternalEntities;
using YiJingFramework.PrimitiveTypes;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount;

namespace YiJingFramework.Annotating.Zhouyi;

public sealed partial class ZhouyiStore
{
    private static void UpdateEntry(AnnotationGroup group, string target, string? content)
    {
        foreach (var e in group.Entries)
        {
            if (e.Target == target)
            {
                e.Content = content;
                return;
            }
        }
        if (content is not null)
            _ = group.AddEntry(target, content);
    }

    private static void UpdateSixContents(
        AnnotationGroup group, GuaHexagram target, params string?[] contents)
    {
        Debug.Assert(contents.Length is 6);

        var foundRecord = new bool[6];
        int foundCount = 0;
        foreach (var entry in group.Entries)
        {
            if (!HexagramYao.CheckAndParse(entry.Target, out var guaYao))
                continue;

            if (guaYao.Gua == target)
            {
                if (foundRecord[guaYao.YaoIndex] is true)
                    continue;

                entry.Content = contents[guaYao.YaoIndex];
                foundRecord[guaYao.YaoIndex] = true;
                if (foundCount is 5)
                    return;
                foundCount++;
            }
        }

        for (int i = 0; i < 6; i++)
        {
            if (!foundRecord[i])
            {
                var content = contents[i];
                if (content is not null)
                    _ = group.AddEntry(new HexagramYao(target, i).ToString(), content);
            }
        }
    }

    /// <summary>
    /// 更新仓库。
    /// Update the store.
    /// 此操作不是线程安全的。
    /// The operation is not thread safe.
    /// </summary>
    /// <param name="trigram">
    /// 要更新的内容。
    /// Things to update.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="trigram"/> 是 <c>null</c> 。
    /// <paramref name="trigram"/> is <c>null</c>.
    /// </exception>
    public void UpdateStore(ZhouyiTrigram trigram)
    {
        ArgumentNullException.ThrowIfNull(trigram);
        var paintingString = trigram.Painting.ToString();
        UpdateEntry(this.Groups.TrigramNameGroup, paintingString, trigram.Name);
        UpdateEntry(this.Groups.TrigramNatureGroup, paintingString, trigram.Nature);
    }

    /// <summary>
    /// 更新仓库。
    /// Update the store.
    /// 此操作不是线程安全的。
    /// The operation is not thread safe.
    /// </summary>
    /// <param name="hexagram">
    /// 要更新的内容。
    /// Things to update.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="hexagram"/> 是 <c>null</c> 。
    /// <paramref name="hexagram"/> is <c>null</c>.
    /// </exception>
    public void UpdateStore(ZhouyiHexagram hexagram)
    {
        ArgumentNullException.ThrowIfNull(hexagram);

        var paintingString = hexagram.Painting.ToString();
        UpdateEntry(this.Groups.HexagramIndexGroup, paintingString, hexagram.Index);
        UpdateEntry(this.Groups.HexagramNameGroup, paintingString, hexagram.Name);
        UpdateEntry(this.Groups.HexagramTextGroup, paintingString, hexagram.Text);

        UpdateEntry(this.Groups.XiangHexagramGroup, paintingString, hexagram.Xiang);
        UpdateEntry(this.Groups.TuanGroup, paintingString, hexagram.Tuan);
        UpdateEntry(this.Groups.WenyanGroup, paintingString, hexagram.Wenyan);

        UpdateSixContents(
            this.Groups.YaoTextGroup, hexagram.Painting,
            hexagram.FirstYao.YaoText,
            hexagram.SecondYao.YaoText,
            hexagram.ThirdYao.YaoText,
            hexagram.FourthYao.YaoText,
            hexagram.FifthYao.YaoText,
            hexagram.SixthYao.YaoText);

        UpdateSixContents(
            this.Groups.XiangYaoGroup, hexagram.Painting,
            hexagram.FirstYao.Xiang,
            hexagram.SecondYao.Xiang,
            hexagram.ThirdYao.Xiang,
            hexagram.FourthYao.Xiang,
            hexagram.FifthYao.Xiang,
            hexagram.SixthYao.Xiang);

        UpdateEntry(this.Groups.HexagramYongTextGroup, paintingString, hexagram.Yong.YaoText);
        UpdateEntry(this.Groups.XiangYongGroup, paintingString, hexagram.Yong.Xiang);
    }

    /// <summary>
    /// 更新仓库。
    /// Update the store.
    /// 此操作不是线程安全的。
    /// The operation is not thread safe.
    /// </summary>
    /// <param name="shuogua">
    /// 要更新的内容。
    /// Things to update.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="shuogua"/> 是 <c>null</c> 。
    /// <paramref name="shuogua"/> is <c>null</c>.
    /// </exception>
    public void UpdateStore(Shuogua shuogua)
    {
        ArgumentNullException.ThrowIfNull(shuogua);
        UpdateEntry(this.Groups.TheRestOfYizhuanGroup, ZhouyiGroups.TARGET_SHUOGUA, shuogua.Content);
    }

    /// <summary>
    /// 更新仓库。
    /// Update the store.
    /// 此操作不是线程安全的。
    /// The operation is not thread safe.
    /// </summary>
    /// <param name="xici">
    /// 要更新的内容。
    /// Things to update.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="xici"/> 是 <c>null</c> 。
    /// <paramref name="xici"/> is <c>null</c>.
    /// </exception>
    public void UpdateStore(Xici xici)
    {
        ArgumentNullException.ThrowIfNull(xici);
        UpdateEntry(this.Groups.TheRestOfYizhuanGroup, ZhouyiGroups.TARGET_XICI_A, xici.PartA);
        UpdateEntry(this.Groups.TheRestOfYizhuanGroup, ZhouyiGroups.TARGET_XICI_B, xici.PartB);
    }

    /// <summary>
    /// 更新仓库。
    /// Update the store.
    /// 此操作不是线程安全的。
    /// The operation is not thread safe.
    /// </summary>
    /// <param name="xugua">
    /// 要更新的内容。
    /// Things to update.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="xugua"/> 是 <c>null</c> 。
    /// <paramref name="xugua"/> is <c>null</c>.
    /// </exception>
    public void UpdateStore(Xugua xugua)
    {
        ArgumentNullException.ThrowIfNull(xugua);
        UpdateEntry(this.Groups.TheRestOfYizhuanGroup, ZhouyiGroups.TARGET_XUGUA, xugua.Content);
    }

    /// <summary>
    /// 更新仓库。
    /// Update the store.
    /// 此操作不是线程安全的。
    /// The operation is not thread safe.
    /// </summary>
    /// <param name="zagua">
    /// 要更新的内容。
    /// Things to update.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="zagua"/> 是 <c>null</c> 。
    /// <paramref name="zagua"/> is <c>null</c>.
    /// </exception>
    public void UpdateStore(Zagua zagua)
    {
        ArgumentNullException.ThrowIfNull(zagua);
        UpdateEntry(this.Groups.TheRestOfYizhuanGroup, ZhouyiGroups.TARGET_ZAGUA, zagua.Content);
    }
}