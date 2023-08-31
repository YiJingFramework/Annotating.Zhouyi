using YiJingFramework.PrimitiveTypes;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount;

namespace YiJingFramework.Annotating.Zhouyi.Entities;

/// <summary>
/// 六爻卦。
/// 此类型的实例不可比较。如果需要，可以比较 <seealso cref="Painting"/> 。
/// A hexagram.
/// Instance of this type cannot be compared. If required, compare their <seealso cref="Painting"/>s.
/// </summary>
public sealed class ZhouyiHexagram
{
    /// <summary>
    /// 卦画。
    /// Painting of the hexagram.
    /// </summary>
    public GuaHexagram Painting { get; }

    /// <summary>
    /// 创建一个没有任何信息的 <see cref="ZhouyiHexagram"/> 实例。
    /// 如果是需要查询信息，应通过 <seealso cref="ZhouyiStore"/> 来进行获取。
    /// Create an instance of <see cref="ZhouyiHexagram"/> without any information.
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
    public ZhouyiHexagram(GuaHexagram painting)
    {
        ArgumentNullException.ThrowIfNull(painting);

        this.Painting = painting;

        this.FirstYao = new(this, 1);
        this.SecondYao = new(this, 2);
        this.ThirdYao = new(this, 3);
        this.FourthYao = new(this, 4);
        this.FifthYao = new(this, 5);
        this.SixthYao = new(this, 6);

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
    /// The first Yao.
    /// </summary>
    public ZhouyiHexagramYao FirstYao { get; }

    /// <summary>
    /// 二爻。
    /// The second Yao.
    /// </summary>
    public ZhouyiHexagramYao SecondYao { get; }

    /// <summary>
    /// 三爻。
    /// The third Yao.
    /// </summary>
    public ZhouyiHexagramYao ThirdYao { get; }

    /// <summary>
    /// 四爻。
    /// The fourth Yao.
    /// </summary>
    public ZhouyiHexagramYao FourthYao { get; }

    /// <summary>
    /// 五爻。
    /// The fifth Yao.
    /// </summary>
    public ZhouyiHexagramYao FifthYao { get; }

    /// <summary>
    /// 六爻。
    /// The sixth Yao.
    /// </summary>
    public ZhouyiHexagramYao SixthYao { get; }

    /// <summary>
    /// 用九用六。
    /// Yong.
    /// </summary>
    public ZhouyiHexagramYao Yong { get; }

    /// <summary>
    /// 返回包含所有爻的枚举。
    /// Enumerate all the Yao-s.
    /// </summary>
    /// <param name="includeYong">
    /// 指示是否要包含用九用六。
    /// Indicates whether to include Yong or not.
    /// </param>
    /// <returns>
    /// 所有爻。
    /// All the Yao-s.
    /// </returns>
    public IEnumerable<ZhouyiHexagramYao> EnumerateYaos(bool includeYong = true)
    {
        yield return this.FirstYao;
        yield return this.SecondYao;
        yield return this.ThirdYao;
        yield return this.FourthYao;
        yield return this.FifthYao;
        yield return this.SixthYao;
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
    public (GuaTrigram upper, GuaTrigram lower) SplitToTrigrams()
    {
        var painting = this.Painting;
        return (
            upper: new GuaTrigram(painting[3], painting[4], painting[5]),
            lower: new GuaTrigram(painting[0], painting[1], painting[2])
            );
    }

    /// <summary>
    /// 分割为上下两个三爻卦。
    /// Split to two trigrams in the middle.
    /// </summary>
    /// <param name="store">
    /// 提供信息的注解仓库。
    /// The annotation store to provide information.
    /// </param>
    /// <returns>
    /// 两个三爻卦。
    /// Two trigrams.
    /// </returns>
    public (ZhouyiTrigram upper, ZhouyiTrigram lower) SplitToTrigrams(ZhouyiStore store)
    {
        var (upper, lower) = this.SplitToTrigrams();
        return (store.GetTrigram(upper), store.GetTrigram(lower));
    }
}
