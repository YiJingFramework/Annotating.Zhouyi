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

    private AnnotationGroup DoGroupPropertyWork(
        ref AnnotationGroup? field, string title)
    {
        if (field is not null)
            return field;
        var annotationStore = this.store.Store;
        foreach (var g in annotationStore.Groups)
        {
            if (g.Title == title)
            {
                field = g;
                return field;
            }
        }
        field = annotationStore.AddGroup(title);
        return field;
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

    private AnnotationGroup? trigramNameGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup TrigramNameGroup
        => this.DoGroupPropertyWork(ref this.trigramNameGroup, TITLE_TRIGRAM_NAME);

    private AnnotationGroup? trigramNatureGroup;
    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup TrigramNatureGroup
        => this.DoGroupPropertyWork(ref this.trigramNatureGroup, TITLE_TRIGRAM_NATURE);
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
    public const string TITLE_LINE_TEXT = "Line Text";

    /// <summary>
    /// 
    /// </summary>
    public const string TITLE_XIANG_HEXAGRAM = "Xiang (Hexagram)";

    /// <summary>
    /// 
    /// </summary>
    public const string TITLE_XIANG_LINE = "Xiang (Line)";

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

    private AnnotationGroup? hexagramNameGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup HexagramNameGroup
        => this.DoGroupPropertyWork(ref this.hexagramNameGroup, TITLE_HEXAGRAM_NAME);

    private AnnotationGroup? hexagramIndexGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup HexagramIndexGroup
        => this.DoGroupPropertyWork(ref this.hexagramIndexGroup, TITLE_HEXAGRAM_INDEX);

    private AnnotationGroup? hexagramTextGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup HexagramTextGroup
        => this.DoGroupPropertyWork(ref this.hexagramTextGroup, TITLE_HEXAGRAM_TEXT);

    private AnnotationGroup? hexagramYongTextGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup HexagramYongTextGroup
        => this.DoGroupPropertyWork(ref this.hexagramYongTextGroup, TITLE_HEXAGRAM_YONG_TEXT);

    private AnnotationGroup? lineTextGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup LineTextGroup
        => this.DoGroupPropertyWork(ref this.lineTextGroup, TITLE_LINE_TEXT);

    private AnnotationGroup? xiangHexagramGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup XiangHexagramGroup
        => this.DoGroupPropertyWork(ref this.xiangHexagramGroup, TITLE_XIANG_HEXAGRAM);

    private AnnotationGroup? tuanGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup TuanGroup
        => this.DoGroupPropertyWork(ref this.tuanGroup, TITLE_TUAN);

    private AnnotationGroup? wenyanGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup WenyanGroup
        => this.DoGroupPropertyWork(ref this.wenyanGroup, TITLE_WENYAN);

    private AnnotationGroup? xiangLineGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup XiangLineGroup
        => this.DoGroupPropertyWork(ref this.xiangLineGroup, TITLE_XIANG_LINE);

    private AnnotationGroup? xiangYongGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup XiangYongGroup
        => this.DoGroupPropertyWork(ref this.xiangYongGroup, TITLE_XIANG_YONG);

    #endregion hexagram

    #region the rest of Yizhuan
    /// <summary>
    /// 
    /// </summary>
    public const string TITLE_THE_REST_OF_YIZHUAN = "The rest (of Yizhuan)";

    private AnnotationGroup? theRestOfYizhuanGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup TheRestOfYizhuanGroup
        => this.DoGroupPropertyWork(ref this.theRestOfYizhuanGroup, TITLE_THE_REST_OF_YIZHUAN);

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