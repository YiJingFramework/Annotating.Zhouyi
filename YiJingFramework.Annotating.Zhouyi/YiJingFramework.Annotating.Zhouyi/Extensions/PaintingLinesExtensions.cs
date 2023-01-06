using YiJingFramework.Annotating.Entities;

namespace YiJingFramework.Annotating.Zhouyi.Extensions
{
    /// <summary>
    /// <seealso cref="PaintingLines"/> 的扩展。
    /// Extensions of <seealso cref="PaintingLines"/>.
    /// </summary>
    public static class PaintingLinesExtensions
    {
        /// <summary>
        /// 判断某个 <seealso cref="PaintingLines"/> 是否只表示一根爻。
        /// Judge whether a <seealso cref="PaintingLines"/> represents one line only.
        /// </summary>
        /// <param name="paintingLines">
        /// 要判断的 <seealso cref="PaintingLines"/> 。
        /// The <seealso cref="PaintingLines"/>.
        /// </param>
        /// <param name="lineIndex">
        /// 若确实只表示一根爻，则此参数为此爻的位置（从 0 开始）。
        /// If it does represent one line only, this parameter is the index of the line (0-based).
        /// </param>
        /// <returns>
        /// 判断结果。
        /// The result.
        /// </returns>
        public static bool IsSingleLine(
            this PaintingLines paintingLines,
            out int lineIndex)
        {
            var lines = paintingLines.Lines;

            lineIndex = -1;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].IsYang)
                {
                    if (lineIndex is not -1)
                        return false;

                    lineIndex = i;
                }
            }
            return lineIndex is not -1;
        }
    }
}
