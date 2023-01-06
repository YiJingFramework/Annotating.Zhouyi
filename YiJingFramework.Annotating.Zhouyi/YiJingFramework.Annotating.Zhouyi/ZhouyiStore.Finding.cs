using YiJingFramework.Annotating.Entities;
using YiJingFramework.Annotating.Zhouyi.Entities;
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
        private static AnnotationEntry<Painting>? FindEntry(
            AnnotationGroup<Painting> group, 
            string? content,
            StringComparison comparisonType)
        {
            foreach (var e in group.Entries)
            {
                if (string.Equals(e.Content, content, comparisonType))
                    return e;
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
        /// <exception cref="ArgumentException">
        /// <paramref name="painting"/> 不表示一个三爻卦。
        /// <paramref name="painting"/> does not represent a trigram.
        /// </exception>
        public ZhouyiTrigram GetTrigram(Painting painting)
        {
            ArgumentNullException.ThrowIfNull(painting);

            if (painting.Count is not 3)
                throw new ArgumentException(
                    $"The painting {painting} does not represent a trigram.",
                    nameof(painting));

            return new ZhouyiTrigram(painting) {
                Name = FindContent(Groups.TrigramNameGroup, painting),
                Nature = FindContent(Groups.TrigramNatureGroup, painting)
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
            var entry = FindEntry(Groups.TrigramNameGroup, name, comparisonType);
            if (entry?.Target is null)
                return null;
            var painting = entry.Target;
            return new ZhouyiTrigram(painting) {
                Name = entry.Content,
                Nature = FindContent(Groups.TrigramNatureGroup, painting)
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
            var entry = FindEntry(Groups.TrigramNatureGroup, nature, comparisonType);
            if (entry?.Target is null)
                return null;
            var painting = entry.Target;
            return new ZhouyiTrigram(painting) {
                Name = FindContent(Groups.TrigramNameGroup, painting),
                Nature = entry.Content
            };
        }

        private void FillNoFindingProperties(ZhouyiHexagram hexagram)
        {
            var painting = hexagram.Painting;
            hexagram.Text = FindContent(Groups.HexagramTextGroup, painting);

            hexagram.Xiang = FindContent(Groups.XiangHexagramGroup, painting);
            hexagram.Tuan = FindContent(Groups.TuanGroup, painting);
            hexagram.Wenyan = FindContent(Groups.WenyanGroup, painting);

            var linesText = FindSixContents(Groups.LineTextGroup, painting);
            var linesXiang = FindSixContents(Groups.XiangLineGroup, painting);

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
            line.LineText = FindContent(Groups.HexagramYongTextGroup, painting);
            line.Xiang = FindContent(Groups.XiangYongGroup, painting);
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
        /// <exception cref="ArgumentException">
        /// <paramref name="painting"/> 不表示一个六爻卦。
        /// <paramref name="painting"/> does not represent a hexagram.
        /// </exception>
        public ZhouyiHexagram GetHexagram(Painting painting)
        {
            ArgumentNullException.ThrowIfNull(painting);

            if (painting.Count is not 6)
                throw new ArgumentException(
                    $"The painting {painting} does not represent a hexagram.",
                    nameof(painting));
            var result = new ZhouyiHexagram(painting) {
                Name = FindContent(Groups.HexagramNameGroup, painting),
                Index = FindContent(Groups.HexagramIndexGroup, painting)
            };
            FillNoFindingProperties(result);
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
            var entry = FindEntry(Groups.HexagramNameGroup, name, comparisonType);
            if (entry?.Target is null)
                return null;
            var painting = entry.Target;
            var result = new ZhouyiHexagram(painting) {
                Name = entry.Content,
                Index = FindContent(Groups.HexagramIndexGroup, painting)
            };
            FillNoFindingProperties(result);
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
            var entry = FindEntry(Groups.HexagramIndexGroup, index, comparisonType);
            if (entry?.Target is null)
                return null;
            var painting = entry.Target;
            var result = new ZhouyiHexagram(painting) {
                Name = FindContent(Groups.HexagramNameGroup, painting),
                Index = entry.Content
            };
            FillNoFindingProperties(result);
            return result;
        }
    }
}