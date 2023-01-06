using System.Diagnostics;
using YiJingFramework.Annotating.Entities;
using YiJingFramework.Annotating.Zhouyi.Entities;
using YiJingFramework.Annotating.Zhouyi.Extensions;
using YiJingFramework.Core;

namespace YiJingFramework.Annotating.Zhouyi
{
    public sealed partial class ZhouyiStore
    {
        private static void UpdateEntry(
            AnnotationGroup<Painting> group, Painting target, string? content)
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
            AnnotationGroup<PaintingLines> group, Painting target, params string?[] contents)
        {
            Debug.Assert(contents.Length is 6);

            var foundRecord = new bool[6];
            int foundCount = 0;
            foreach (var entry in group.Entries)
            {
                if (entry.Target?.Painting == target)
                {
                    if (!entry.Target.IsSingleLine(out var lineIndex))
                        continue;
                    if (foundRecord[lineIndex] is true)
                        continue;

                    entry.Content = contents[lineIndex];
                    foundRecord[lineIndex] = true;
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
                        _ = group.AddEntry(new PaintingLines(target, i), content);
                }
            }
        }

        private static void UpdateFirstContent(AnnotationGroup<NoTarget> group, string? content)
        {
            var entries = group.Entries;
            if (entries.Count is 0 && content is not null)
                _ = group.AddEntry(default, content);
            else
                entries[0].Content = content;
        }

        public void UpdateStore(ZhouyiTrigram trigram)
        {
            var painting = trigram.Painting;
            UpdateEntry(Groups.TrigramNameGroup, painting, trigram.Name);
            UpdateEntry(Groups.TrigramNatureGroup, painting, trigram.Nature);
        }

        public void UpdateStore(ZhouyiHexagram hexagram)
        {
            var painting = hexagram.Painting;
            UpdateEntry(Groups.HexagramIndexGroup, painting, hexagram.Index);
            UpdateEntry(Groups.HexagramNameGroup, painting, hexagram.Name);
            UpdateEntry(Groups.HexagramTextGroup, painting, hexagram.Text);

            UpdateEntry(Groups.XiangHexagramGroup, painting, hexagram.Xiang);
            UpdateEntry(Groups.TuanGroup, painting, hexagram.Tuan);
            UpdateEntry(Groups.WenyanGroup, painting, hexagram.Wenyan);

            UpdateSixContents(
                Groups.LineTextGroup, painting,
                hexagram.FirstLine.LineText,
                hexagram.SecondLine.LineText,
                hexagram.ThirdLine.LineText,
                hexagram.FourthLine.LineText,
                hexagram.FifthLine.LineText,
                hexagram.SixthLine.LineText);

            UpdateSixContents(
                Groups.XiangLineGroup, painting,
                hexagram.FirstLine.Xiang,
                hexagram.SecondLine.Xiang,
                hexagram.ThirdLine.Xiang,
                hexagram.FourthLine.Xiang,
                hexagram.FifthLine.Xiang,
                hexagram.SixthLine.Xiang);

            UpdateEntry(Groups.HexagramYongTextGroup, painting, hexagram.Yong.LineText);
            UpdateEntry(Groups.XiangYongGroup, painting, hexagram.Yong.Xiang);
        }

        public void UpdateStore(Shuogua shuogua)
        {
            UpdateFirstContent(Groups.ShuoguaGroup, shuogua.Content);
        }

        public void UpdateStore(Xici xici)
        {
            var group = Groups.XiciGroup;
            var entries = group.Entries;
            var partA = xici.PartA;
            var partB = xici.PartB;
            switch (entries.Count)
            {
                case 0:
                    if (partA is not null || partB is not null)
                    {
                        _ = group.AddEntry(default, partA);
                        _ = group.AddEntry(default, partB);
                    }
                    break;
                case 1:
                    entries[0].Content = partA;
                    if(partB is not null)
                        _ = group.AddEntry(default, xici.PartB);
                    break;
                default:
                    entries[0].Content = xici.PartA;
                    entries[1].Content = xici.PartB;
                    break;
            }
        }

        public void UpdateStore(Xugua xugua)
        {
            UpdateFirstContent(Groups.XuguaGroup, xugua.Content);
        }

        public void UpdateStore(Zagua zagua)
        {
            UpdateFirstContent(Groups.ZaguaGroup, zagua.Content);
        }
    }
}