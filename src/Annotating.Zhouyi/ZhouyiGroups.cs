using YiJingFramework.PrimitiveTypes;

namespace YiJingFramework.Annotating.Zhouyi;

/// <summary>
/// <seealso cref="ZhouyiStore"/> 的注解组。
/// Annotation groups of <seealso cref="ZhouyiStore"/>s.
/// </summary>
public sealed class ZhouyiGroups
{
    internal ZhouyiGroups(ZhouyiStore store)
    {
        this.store = store;
    }

    private readonly ZhouyiStore store;

    private AnnotationGroup GetGroup(string title)
    {
        var annotationStore = this.store.Store;
        var result = annotationStore.GetGroup(title);
        if (result is not null)
            return result;
        return annotationStore.AddGroup(title);
    }
    #region trigram
    /// <summary>
    /// 
    /// </summary>
    public const string TITLE_TRIGRAM_NAME = "Trigram Name";
    /// <summary>
    /// 
    /// </summary>
    public const string TITLE_TRIGRAM_NATURE = "Trigram Nature";

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup TrigramNameGroup => this.GetGroup(TITLE_TRIGRAM_NAME);
    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup TrigramNatureGroup => this.GetGroup(TITLE_TRIGRAM_NATURE);
    #endregion trigram

    #region hexagram
    /// <summary>
    /// 
    /// </summary>
    public const string TITLE_HEXAGRAM_NAME = "Hexagram Name";
    /// <summary>
    /// 
    /// </summary>
    public const string TITLE_HEXAGRAM_INDEX = "Hexagram Index";
    /// <summary>
    /// 
    /// </summary>
    public const string TITLE_HEXAGRAM_TEXT = "Hexagram Text";
    /// <summary>
    /// 
    /// </summary>
    public const string TITLE_HEXAGRAM_YONG_TEXT = "Hexagram Yong Text";
    /// <summary>
    /// 
    /// </summary>
    public const string TITLE_YAO_TEXT = "Line Text";

    /// <summary>
    /// 
    /// </summary>
    public const string TITLE_XIANG_HEXAGRAM = "Xiang (Hexagram)";

    /// <summary>
    /// 
    /// </summary>
    public const string TITLE_XIANG_YAO = "Xiang (Line)";

    /// <summary>
    /// 
    /// </summary>
    public const string TITLE_XIANG_YONG = "Xiang (Yong)";

    /// <summary>
    /// 
    /// </summary>
    public const string TITLE_TUAN = "Tuan";

    /// <summary>
    /// 
    /// </summary>
    public const string TITLE_WENYAN = "Wenyan";

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup HexagramNameGroup => this.GetGroup(TITLE_HEXAGRAM_NAME);
    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup HexagramIndexGroup => this.GetGroup(TITLE_HEXAGRAM_INDEX);
    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup HexagramTextGroup => this.GetGroup(TITLE_HEXAGRAM_TEXT);
    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup HexagramYongTextGroup => this.GetGroup(TITLE_HEXAGRAM_YONG_TEXT);
    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup YaoTextGroup => this.GetGroup(TITLE_YAO_TEXT);
    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup XiangHexagramGroup => this.GetGroup(TITLE_XIANG_HEXAGRAM);

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup TuanGroup => this.GetGroup(TITLE_TUAN);
    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup WenyanGroup => this.GetGroup(TITLE_WENYAN);

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup XiangYaoGroup => this.GetGroup(TITLE_XIANG_YAO);

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup XiangYongGroup => this.GetGroup(TITLE_XIANG_YONG);

    #endregion hexagram

    #region the rest of Yizhuan
    /// <summary>
    /// 
    /// </summary>
    public const string TITLE_THE_REST_OF_YIZHUAN = "The rest (of Yizhuan)";

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup TheRestOfYizhuanGroup => this.GetGroup(TITLE_THE_REST_OF_YIZHUAN);

    /// <summary>
    /// 
    /// </summary>
    public const string TARGET_XICI_A = "Xici A";
    /// <summary>
    /// 
    /// </summary>
    public const string TARGET_XICI_B = "Xici B";
    /// <summary>
    /// 
    /// </summary>
    public const string TARGET_SHUOGUA = "Shuogua";
    /// <summary>
    /// 
    /// </summary>
    public const string TARGET_XUGUA = "Xugua";
    /// <summary>
    /// 
    /// </summary>
    public const string TARGET_ZAGUA = "Zagua";
    #endregion
}