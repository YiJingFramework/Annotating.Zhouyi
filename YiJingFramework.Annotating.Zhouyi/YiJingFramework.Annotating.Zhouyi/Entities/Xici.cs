namespace YiJingFramework.Annotating.Zhouyi.Entities
{
    /// <summary>
    /// 《系辞》。
    /// Xici.
    /// 此类型的实例不可比较。
    /// Instance of this type cannot be compared.
    /// </summary>
    public sealed record Xici
    {
        /// <summary>
        /// 上。
        /// Part 1.
        /// </summary>
        public string? PartA { get; set; }
        /// <summary>
        /// 下。
        /// Part 2.
        /// </summary>
        public string? PartB { get; set; }
    }
}
