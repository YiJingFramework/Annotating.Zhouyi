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

        public Painting JoinAsUpper(ZhouyiTrigram lower)
        {
            var upperPainting = this.Painting;
            var lowerPainting = lower.Painting;
            return new Painting(
                lowerPainting[0], lowerPainting[1], lowerPainting[2],
                upperPainting[0], upperPainting[1], upperPainting[2]);
        }

        public Painting JoinAsLower(ZhouyiTrigram upper)
        {
            var upperPainting = upper.Painting;
            var lowerPainting = this.Painting;
            return new Painting(
                lowerPainting[0], lowerPainting[1], lowerPainting[2],
                upperPainting[0], upperPainting[1], upperPainting[2]);
        }
    }
}
