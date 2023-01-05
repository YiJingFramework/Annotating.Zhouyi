using YiJingFramework.Core;

namespace YiJingFramework.Annotating.Zhouyi
{
    public sealed class ZhouyiTrigram
    {
        public Painting Painting { get; }
        internal ZhouyiTrigram(Painting painting)
        {
            this.Painting = painting;
        }
        public string? Name { get; set; }
        public string? Nature { get; set; }
    }
}
