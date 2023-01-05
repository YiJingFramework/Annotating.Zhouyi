using YiJingFramework.Core;

namespace YiJingFramework.Annotating.Zhouyi
{
    public sealed class ZhouyiHexagramLine
    {
        public ZhouyiHexagram Hexagram { get; }
        public int LineIndex { get; }
        internal ZhouyiHexagramLine(ZhouyiHexagram hexagram, int lineIndex)
        {
            this.Hexagram = hexagram;
            this.LineIndex = lineIndex;
        }

        public string? LineText { get; set; }
    }
}
