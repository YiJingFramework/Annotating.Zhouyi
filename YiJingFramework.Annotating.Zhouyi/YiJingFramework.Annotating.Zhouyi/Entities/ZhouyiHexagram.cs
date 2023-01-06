using YiJingFramework.Core;

namespace YiJingFramework.Annotating.Zhouyi.Entities
{
    public sealed class ZhouyiHexagram
    {
        public Painting Painting { get; }
        internal ZhouyiHexagram(Painting painting)
        {
            Painting = painting;
            FirstLine = new(this, 1);
            SecondLine = new(this, 2);
            ThirdLine = new(this, 3);
            FourthLine = new(this, 4);
            FifthLine = new(this, 5);
            SixthLine = new(this, 6);
        }

        public IEnumerable<ZhouyiHexagramLine> EnumerateLines()
        {
            yield return FirstLine;
            yield return SecondLine;
            yield return ThirdLine;
            yield return FourthLine;
            yield return FifthLine;
            yield return SixthLine;
        }

        public string? Index { get; set; }
        public string? Name { get; set; }
        public string? Text { get; set; }
        public string? YongText { get; set; }

        public string? Xiang { get; set; }
        public string? Tuan { get; set; }
        public string? Wenyan { get; set; }

        public ZhouyiHexagramLine FirstLine { get; }
        public ZhouyiHexagramLine SecondLine { get; }
        public ZhouyiHexagramLine ThirdLine { get; }
        public ZhouyiHexagramLine FourthLine { get; }
        public ZhouyiHexagramLine FifthLine { get; }
        public ZhouyiHexagramLine SixthLine { get; }
    }
}
