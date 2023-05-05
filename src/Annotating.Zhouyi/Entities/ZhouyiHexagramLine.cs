
using YiJingFramework.PrimitiveTypes;

namespace YiJingFramework.Annotating.Zhouyi.Entities;

/// <summary>
/// <seealso cref="ZhouyiHexagram"/> 中的一根爻。
/// A line of a <seealso cref="ZhouyiHexagram"/>.
/// 此类型的实例不可比较。
/// Instance of this type cannot be compared.
/// </summary>
public sealed class ZhouyiHexagramLine
{
    /// <summary>
    /// 所属的卦。
    /// The hexagram that the line belongs to.
    /// </summary>
    public ZhouyiHexagram Hexagram { get; }

    /// <summary>
    /// 从 1 开始的序号。
    /// 1-based index of the line.
    /// 为 <c>0</c> 表示用九用六。
    /// <c>0</c> represents Yong.
    /// </summary>
    public int LineIndex { get; }

    /// <summary>
    /// 爻阴阳。
    /// YinYang of the line.
    /// 若为 <c>null</c> 则表示是用九用六，即使乾坤也为 <c>null</c> 。
    /// <c>null</c> represents Yong, including Qian and Kun.
    /// </summary>
    public Yinyang? YinYang
    {
        get
        {
            if (this.LineIndex == 0)
                return null;
            return this.Hexagram.Painting[this.LineIndex - 1];
        }
    }

    internal ZhouyiHexagramLine(ZhouyiHexagram hexagram, int lineIndex)
    {
        this.Hexagram = hexagram;
        this.LineIndex = lineIndex;
    }

    /// <summary>
    /// 爻辞。
    /// Text of the line.
    /// </summary>
    public string? LineText { get; set; }

    /// <summary>
    /// 象曰。
    /// Text of the line in Xiang.
    /// </summary>
    public string? Xiang { get; set; }
}
