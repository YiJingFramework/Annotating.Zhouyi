namespace YiJingFramework.Annotating.Zhouyi.Entities;

/// <summary>
/// 《说卦》。
/// Shuogua.
/// 此类型的实例不可比较。
/// Instance of this type cannot be compared.
/// </summary>
public sealed record Shuogua
{
    /// <summary>
    /// 内容。
    /// The content.
    /// </summary>
    public string? Content { get; set; }
}
