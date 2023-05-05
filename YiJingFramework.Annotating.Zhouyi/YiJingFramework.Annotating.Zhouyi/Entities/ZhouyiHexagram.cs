using YiJingFramework.PrimitiveTypes;

namespace YiJingFramework.Annotating.Zhouyi.Entities;

/// <summary>
/// 六爻卦。
/// A hexagram.
/// 此类型的实例不可比较。如果需要，可以比较 <seealso cref="Painting"/> 。
/// Instance of this type cannot be compared. If required, compare their <seealso cref="Painting"/>s.
/// </summary>
public sealed class ZhouyiHexagram
{
    /// <summary>
    /// 卦画。
    /// Painting of the hexagram.
    /// </summary>
    public Gua Painting { get; }

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
    /// <paramref name="painting"/> 不表示六爻卦。
    /// <paramref name="painting"/> does not represent a hexagram.
    /// </exception>
    public ZhouyiHexagram(Gua painting)
    {
        ArgumentNullException.ThrowIfNull(painting);

        if (painting.Count is not 6)
            throw new ArgumentException(
                $"The painting {painting} does not represent a hexagram.",
                nameof(painting));

        this.Painting = painting;

        this.FirstLine = new(this, 1);
        this.SecondLine = new(this, 2);
        this.ThirdLine = new(this, 3);
        this.FourthLine = new(this, 4);
        this.FifthLine = new(this, 5);
        this.SixthLine = new(this, 6);

        this.Yong = new(this, 0);
    }

    /// <summary>
    /// 卦序。
    /// Index of the hexagram.
    /// </summary>
    public string? Index { get; set; }

    /// <summary>
    /// 卦名。
    /// Name of the hexagram.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 卦辞。
    /// Text of the whole hexagram.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// 象曰。
    /// Text of the whole hexagram in Xiang.
    /// </summary>
    public string? Xiang { get; set; }

    /// <summary>
    /// 彖曰。
    /// Text of the whole hexagram in Tuan.
    /// </summary>
    public string? Tuan { get; set; }

    /// <summary>
    /// 文言曰。
    /// Text of the whole hexagram in Wenyan.
    /// </summary>
    public string? Wenyan { get; set; }

    /// <summary>
    /// 初爻。
    /// The first line.
    /// </summary>
    public ZhouyiHexagramLine FirstLine { get; }

    /// <summary>
    /// 二爻。
    /// The second line.
    /// </summary>
    public ZhouyiHexagramLine SecondLine { get; }

    /// <summary>
    /// 三爻。
    /// The third line.
    /// </summary>
    public ZhouyiHexagramLine ThirdLine { get; }

    /// <summary>
    /// 四爻。
    /// The fourth line.
    /// </summary>
    public ZhouyiHexagramLine FourthLine { get; }

    /// <summary>
    /// 五爻。
    /// The fifth line.
    /// </summary>
    public ZhouyiHexagramLine FifthLine { get; }

    /// <summary>
    /// 六爻。
    /// The sixth line.
    /// </summary>
    public ZhouyiHexagramLine SixthLine { get; }

    /// <summary>
    /// 用九用六。
    /// Yong.
    /// </summary>
    public ZhouyiHexagramLine Yong { get; }

    /// <summary>
    /// 返回包含所有爻的枚举。
    /// Enumerate all the lines.
    /// </summary>
    /// <param name="includeYong">
    /// 指示是否要包含用九用六。
    /// Indicates whether to include Yong or not.
    /// </param>
    /// <returns>
    /// 所有爻。
    /// All the lines.
    /// </returns>
    public IEnumerable<ZhouyiHexagramLine> EnumerateLines(bool includeYong = true)
    {
        yield return this.FirstLine;
        yield return this.SecondLine;
        yield return this.ThirdLine;
        yield return this.FourthLine;
        yield return this.FifthLine;
        yield return this.SixthLine;
        if (includeYong)
            yield return this.Yong;
    }

    /// <summary>
    /// 分割为上下两个三爻卦。
    /// Split to two trigrams in the middle.
    /// </summary>
    /// <returns>
    /// 两个三爻卦。
    /// Two trigrams.
    /// </returns>
    public (Gua upper, Gua lower) SplitToTrigrams()
    {
        var painting = this.Painting;
        return (
            upper: new Gua(painting[3], painting[4], painting[5]),
            lower: new Gua(painting[0], painting[1], painting[2])
            );
    }
}
