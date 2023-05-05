using YiJingFramework.PrimitiveTypes;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount;

namespace YiJingFramework.Annotating.Zhouyi.Entities;

/// <summary>
/// 三爻卦。
/// A trigram.
/// 此类型的实例不可比较。如果需要，可以比较 <seealso cref="Painting"/> 。
/// Instance of this type cannot be compared. If required, compare their <seealso cref="Painting"/>s.
/// </summary>
public sealed class ZhouyiTrigram
{
    /// <summary>
    /// 卦画。
    /// Painting of the trigram.
    /// </summary>
    public GuaTrigram Painting { get; }

    /// <summary>
    /// 创建一个没有任何信息的 <see cref="ZhouyiHexagram"/> 实例。
    /// Create an instance of <see cref="ZhouyiHexagram"/> without any information.
    /// 如果是需要查询信息，应通过 <seealso cref="ZhouyiStore"/> 来进行获取。
    /// Get the instance with <seealso cref="ZhouyiStore"/> if you wants to get the information.
    /// </summary>
    /// <param name="painting">
    /// 卦画。
    /// The painting.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="painting"/> 是 <c>null</c> 。
    /// <paramref name="painting"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="painting"/> 不表示三爻卦。
    /// <paramref name="painting"/> does not represent a trigram.
    /// </exception>
    public ZhouyiTrigram(GuaTrigram painting)
    {
        ArgumentNullException.ThrowIfNull(painting);

        if (painting.Count is not 3)
            throw new ArgumentException(
                $"The painting {painting} does not represent a trigram.",
                nameof(painting));
        this.Painting = painting;
    }

    /// <summary>
    /// 卦名。
    /// Name of the trigram.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 卦对应的自然事物。
    /// The corresponding nature of the trigram.
    /// </summary>
    public string? Nature { get; set; }

    /// <summary>
    /// 作为上卦，和另外的三爻卦组成一个六爻卦。
    /// As the upper one, be combined with another trigram to form a hexagram.
    /// </summary>
    /// <param name="lower">
    /// 下卦。
    /// The lower one.
    /// </param>
    /// <returns>
    /// 六爻卦。
    /// A hexagram.
    /// </returns>
    public Gua JoinAsUpper(ZhouyiTrigram lower)
    {
        var upperPainting = this.Painting;
        var lowerPainting = lower.Painting;
        return new Gua(
            lowerPainting[0], lowerPainting[1], lowerPainting[2],
            upperPainting[0], upperPainting[1], upperPainting[2]);
    }

    /// <summary>
    /// 作为下卦，和另外的三爻卦组成一个六爻卦。
    /// As the lower one, be combined with another trigram to form a hexagram.
    /// </summary>
    /// <param name="upper">
    /// 上卦。
    /// The upper one.
    /// </param>
    /// <returns>
    /// 六爻卦。
    /// A hexagram.
    /// </returns>
    public Gua JoinAsLower(ZhouyiTrigram upper)
    {
        var upperPainting = upper.Painting;
        var lowerPainting = this.Painting;
        return new Gua(
            lowerPainting[0], lowerPainting[1], lowerPainting[2],
            upperPainting[0], upperPainting[1], upperPainting[2]);
    }
}
