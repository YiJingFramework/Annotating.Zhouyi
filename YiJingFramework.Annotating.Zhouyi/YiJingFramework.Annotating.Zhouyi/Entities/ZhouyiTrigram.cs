using YiJingFramework.Core;

namespace YiJingFramework.Annotating.Zhouyi.Entities
{
    public sealed class ZhouyiTrigram
    {
        public Painting Painting { get; }
        internal ZhouyiTrigram(Painting painting)
        {
            Painting = painting;
        }
        public string? Name { get; set; }
        public string? Nature { get; set; }
    }
}
