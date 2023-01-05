using YiJingFramework.Core;

namespace YiJingFramework.Annotating.Zhouyi
{
    public sealed class ZhouyiHexagram
    {
        public Painting Painting { get; }
        internal ZhouyiHexagram(Painting painting)
        {
            this.Painting = painting;
            this.FirstLine = new(this, 1);
            this.SecondLine = new(this, 2);
            this.ThirdLine = new(this, 3);
            this.FourthLine = new(this, 4);
            this.FifthLine = new(this, 5);
            this.SixthLine = new(this, 6);
        }

        public string? Index { get; set; }
        public string? Name { get; set; }
        public string? Text { get; set; }
        public string? ApplyNinthOrSixth { get; set; }

        public ZhouyiHexagramLine FirstLine { get; }
        public ZhouyiHexagramLine SecondLine { get; }
        public ZhouyiHexagramLine ThirdLine { get; }
        public ZhouyiHexagramLine FourthLine { get; }
        public ZhouyiHexagramLine FifthLine { get; }
        public ZhouyiHexagramLine SixthLine { get; }
    }
}
