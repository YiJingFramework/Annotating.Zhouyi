using YiJingFramework.Core;

namespace YiJingFramework.Annotating.Zhouyi.Entities
{
    public sealed class ZhouyiHexagram
    {
        public Painting Painting { get; }
        public ZhouyiHexagram(Painting painting)
        {
            if (painting.Count is not 6)
                throw new ArgumentException(
                    $"The painting {painting} does not represent a hexagram.",
                    nameof(painting));

            Painting = painting;

            FirstLine = new(this, 1);
            SecondLine = new(this, 2);
            ThirdLine = new(this, 3);
            FourthLine = new(this, 4);
            FifthLine = new(this, 5);
            SixthLine = new(this, 6);

            Yong = new(this, 0);
        }

        public string? Index { get; set; }
        public string? Name { get; set; }
        public string? Text { get; set; }

        public string? Xiang { get; set; }
        public string? Tuan { get; set; }
        public string? Wenyan { get; set; }

        public ZhouyiHexagramLine FirstLine { get; }
        public ZhouyiHexagramLine SecondLine { get; }
        public ZhouyiHexagramLine ThirdLine { get; }
        public ZhouyiHexagramLine FourthLine { get; }
        public ZhouyiHexagramLine FifthLine { get; }
        public ZhouyiHexagramLine SixthLine { get; }
        public ZhouyiHexagramLine Yong { get; }

        public IEnumerable<ZhouyiHexagramLine> EnumerateLines(bool includeYong = true)
        {
            yield return FirstLine;
            yield return SecondLine;
            yield return ThirdLine;
            yield return FourthLine;
            yield return FifthLine;
            yield return SixthLine;
            if (includeYong)
                yield return Yong;
        }

        public (Painting upper, Painting lower) SplitToTrigrams()
        {
            var painting = this.Painting;
            return (
                upper: new Painting(painting[3], painting[4], painting[5]),
                lower: new Painting(painting[0], painting[1], painting[2])
                );
        }
    }
}
