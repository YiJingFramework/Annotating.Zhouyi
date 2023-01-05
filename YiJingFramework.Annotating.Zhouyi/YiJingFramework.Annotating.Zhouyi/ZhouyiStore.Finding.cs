using System.Diagnostics;
using System.Text.Json;
using YiJingFramework.Annotating.Entities;
using YiJingFramework.Annotating.Zhouyi.Extensions;
using YiJingFramework.Core;

namespace YiJingFramework.Annotating.Zhouyi
{
    public sealed partial class ZhouyiStore
    {
        private static string? FindContent(AnnotationGroup<Painting> group, Painting target)
        {
            foreach (var e in group.Entries)
            {
                if (e.Target == target)
                    return e.Content;
            }
            return null;
        }

        private static string?[] FindSixContents(
            AnnotationGroup<PaintingLines> group, Painting target)
        {
            var result = new string?[6];
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

                    result[lineIndex] = entry.Content;
                    foundRecord[lineIndex] = true;
                    if (foundCount is 5)
                        break;
                    foundCount++;
                }
            }
            return result;
        }

        public ZhouyiTrigram GetTrigram(Painting painting)
        {
            if (painting.Count is not 3)
                throw new ArgumentException(
                    $"The painting {painting} does not represent a trigram.",
                    nameof(painting));

            return new ZhouyiTrigram(painting) {
                Name = FindContent(TrigramNameGroup, painting),
                Nature = FindContent(TrigramNatureGroup, painting)
            };
        }

        public ZhouyiHexagram GetHexagram(Painting painting)
        {
            if (painting.Count is not 6)
                throw new ArgumentException(
                    $"The painting {painting} does not represent a hexagram.",
                    nameof(painting));
            var result = new ZhouyiHexagram(painting) {
                Index = FindContent(HexagramIndexGroup, painting),
                Name = FindContent(HexagramNameGroup, painting),
                Text = FindContent(HexagramTextGroup, painting),
                ApplyNinthOrSixth = FindContent(ApplyNinthAndSixthGroup, painting)
            };

            var lines = FindSixContents(LineTextGroup, painting);
            result.FirstLine.LineText = lines[0];
            result.SecondLine.LineText = lines[1];
            result.ThirdLine.LineText = lines[2];
            result.FourthLine.LineText = lines[3];
            result.FifthLine.LineText = lines[4];
            result.SixthLine.LineText = lines[5];

            return result;
        }
    }
}