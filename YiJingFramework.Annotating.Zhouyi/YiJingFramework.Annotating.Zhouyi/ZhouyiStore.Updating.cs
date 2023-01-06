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
                        break;
                    foundCount++;
                }
            }
        }

        private static void UpdateFirstContent(AnnotationGroup<NoTarget> group, string? content)
        {
            var entries = group.Entries;
            if (entries.Count is 0)
                _ = group.AddEntry(default, content);
            else
                entries[0].Content = content;
        }

        public void UpdateStore(ZhouyiTrigram trigram)
        {
            var painting = trigram.Painting;
            UpdateEntry(TrigramNameGroup, painting, trigram.Name);
            UpdateEntry(TrigramNatureGroup, painting, trigram.Nature);
        }

        public void UpdateStore(ZhouyiHexagram hexagram)
        {
            var painting = hexagram.Painting;
            UpdateEntry(HexagramIndexGroup, painting, hexagram.Index);
            UpdateEntry(HexagramNameGroup, painting, hexagram.Name);
            UpdateEntry(HexagramTextGroup, painting, hexagram.Text);
            UpdateEntry(HexagramYongTextGroup, painting, hexagram.YongText);

            UpdateSixContents(
                LineTextGroup, painting,
                hexagram.FirstLine.LineText,
                hexagram.SecondLine.LineText,
                hexagram.ThirdLine.LineText,
                hexagram.FourthLine.LineText,
                hexagram.FifthLine.LineText,
                hexagram.SixthLine.LineText);
        }

        public void UpdateStore(Shuogua shuogua)
        {
            UpdateFirstContent(ShuoguaGroup, shuogua.Content);
        }

        public void UpdateStore(Xici xici)
        {
            var group = XiciGroup;
            var entries = group.Entries;
            switch (entries.Count)
            {
                case 0:
                    _ = group.AddEntry(default, xici.PartA);
                    _ = group.AddEntry(default, xici.PartB);
                    break;
                case 1:
                    entries[0].Content = xici.PartA;
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
            UpdateFirstContent(XuguaGroup, xugua.Content);
        }

        public void UpdateStore(Zagua zagua)
        {
            UpdateFirstContent(ZaguaGroup, zagua.Content);
        }
    }
}