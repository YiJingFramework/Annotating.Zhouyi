using YiJingFramework.Core;

namespace YiJingFramework.Annotating.Zhouyi.Entities
{
    public sealed class ZhouyiHexagramLine
    {
        public ZhouyiHexagram Hexagram { get; }
        public int LineIndex { get; }
        public YinYang? YinYang
        {
            get
            {
                if (LineIndex == 0)
                    return null;
                return Hexagram.Painting[LineIndex - 1];
            }
        }
        internal ZhouyiHexagramLine(ZhouyiHexagram hexagram, int lineIndex)
        {
            Hexagram = hexagram;
            LineIndex = lineIndex;
        }

        public string? LineText { get; set; }
        public string? Xiang { get; set; }
    }
}
