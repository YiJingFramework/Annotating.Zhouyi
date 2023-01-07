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
            var painting = trigram.Painting;
            UpdateEntry(Groups.TrigramNameGroup, painting, trigram.Name);
            UpdateEntry(Groups.TrigramNatureGroup, painting, trigram.Nature);
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

        private static void UpdateEntry(
            AnnotationGroup<string> group, string target, string? content)
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
            UpdateEntry(Groups.TheRestOfYizhuanGroup, ZhouyiGroups.TARGET_SHUOGUA, shuogua.Content);
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
            UpdateEntry(Groups.TheRestOfYizhuanGroup, ZhouyiGroups.TARGET_XICI_A, xici.PartA);
            UpdateEntry(Groups.TheRestOfYizhuanGroup, ZhouyiGroups.TARGET_XICI_B, xici.PartB);
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
            UpdateEntry(Groups.TheRestOfYizhuanGroup, ZhouyiGroups.TARGET_XUGUA, xugua.Content);
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
            UpdateEntry(Groups.TheRestOfYizhuanGroup, ZhouyiGroups.TARGET_ZAGUA, zagua.Content);
        }
    }
}