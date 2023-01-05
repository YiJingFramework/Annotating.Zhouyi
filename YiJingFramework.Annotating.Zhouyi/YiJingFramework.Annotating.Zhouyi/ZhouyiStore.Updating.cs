using System.Diagnostics;
using YiJingFramework.Annotating.Entities;
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
            group.AddEntry(target, content);
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

        public void UpdateStore(ZhouyiHexagram hexagram)
        {
            var painting = hexagram.Painting;
            UpdateEntry(HexagramIndexGroup, painting, hexagram.Index);
            UpdateEntry(HexagramNameGroup, painting, hexagram.Name);
            UpdateEntry(HexagramTextGroup, painting, hexagram.Text);
            UpdateEntry(ApplyNinthAndSixthGroup, painting, hexagram.ApplyNinthOrSixth);

            UpdateSixContents(
                LineTextGroup, painting,
                hexagram.FirstLine.LineText,
                hexagram.SecondLine.LineText,
                hexagram.ThirdLine.LineText,
                hexagram.FourthLine.LineText,
                hexagram.FifthLine.LineText,
                hexagram.SixthLine.LineText);
        }
    }
}