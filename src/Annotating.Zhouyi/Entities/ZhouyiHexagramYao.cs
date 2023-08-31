
using YiJingFramework.PrimitiveTypes;

namespace YiJingFramework.Annotating.Zhouyi.Entities;

/// <summary>
/// <seealso cref="ZhouyiHexagram"/> 中的一根爻。
/// 此类型的实例不可比较。
/// A Yao of a <seealso cref="ZhouyiHexagram"/>.
/// Instance of this type cannot be compared.
/// </summary>
public sealed class ZhouyiHexagramYao
{
    /// <summary>
    /// 所属的卦。
    /// The hexagram that the Yao belongs to.
    /// </summary>
    public ZhouyiHexagram Hexagram { get; }

    /// <summary>
    /// 从 1 开始的序号。
    /// 1-based index of the Yao.
    /// 为 <c>0</c> 表示用九用六。
    /// <c>0</c> represents Yong.
    /// </summary>
    public int YaoIndex { get; }

    /// <summary>
    /// 爻阴阳。
    /// 若为 <c>null</c> 则表示是用九用六，即使乾坤也为 <c>null</c> 。
    /// YinYang of the Yao.
    /// <c>null</c> represents Yong, including Qian and Kun.
    /// </summary>
    public Yinyang? YinYang
    {
        get
        {
            if (this.YaoIndex == 0)
                return null;
            return this.Hexagram.Painting[this.YaoIndex - 1];
        }
    }

    internal ZhouyiHexagramYao(ZhouyiHexagram hexagram, int yaoIndex)
    {
        this.Hexagram = hexagram;
        this.YaoIndex = yaoIndex;
    }

    /// <summary>
    /// 爻辞。
    /// Text of the Yao.
    /// </summary>
    public string? YaoText { get; set; }

    /// <summary>
    /// 象曰。
    /// Text of the Yao in Xiang.
    /// </summary>
    public string? Xiang { get; set; }
}
