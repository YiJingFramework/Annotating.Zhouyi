using YiJingFramework.Core;

namespace YiJingFramework.Annotating.Zhouyi.Entities
{
    public sealed class ZhouyiTrigram
    {
        public Painting Painting { get; }
        public ZhouyiTrigram(Painting painting)
        {
            if (painting.Count is not 3)
                throw new ArgumentException(
                    $"The painting {painting} does not represent a trigram.",
                    nameof(painting));
            Painting = painting;
        }
        public string? Name { get; set; }
        public string? Nature { get; set; }
    }
}
