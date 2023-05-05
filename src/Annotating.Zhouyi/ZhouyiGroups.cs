using YiJingFramework.Annotating.Entities;
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

    private AnnotationGroup<Gua> DoGroupPropertyWork(
        ref AnnotationGroup<Gua>? field, string title)
    {
        if (field is not null)
            return field;
        var annotationStore = this.store.Store;
        foreach (var g in annotationStore.GuaGroups)
        {
            if (g.Title == title)
            {
                field = g;
                return field;
            }
        }
        field = annotationStore.AddGuaGroup(title);
        return field;
    }

    private AnnotationGroup<GuaLines> DoGroupPropertyWork(
        ref AnnotationGroup<GuaLines>? field, string title)
    {
        if (field is not null)
            return field;
        var annotationStore = this.store.Store;
        foreach (var g in annotationStore.GuaLinesGroups)
        {
            if (g.Title == title)
            {
                field = g;
                return field;
            }
        }
        field = annotationStore.AddGuaLinesGroup(title);
        return field;
    }

    private AnnotationGroup<string> DoGroupPropertyWork(
        ref AnnotationGroup<string>? field, string title)
    {
        if (field is not null)
            return field;
        var annotationStore = this.store.Store;
        foreach (var g in annotationStore.StringGroups)
        {
            if (g.Title == title)
            {
                field = g;
                return field;
            }
        }
        field = annotationStore.AddStringGroup(title);
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

    private AnnotationGroup<Gua>? trigramNameGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup<Gua> TrigramNameGroup
        => this.DoGroupPropertyWork(ref this.trigramNameGroup, TITLE_TRIGRAM_NAME);

    private AnnotationGroup<Gua>? trigramNatureGroup;
    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup<Gua> TrigramNatureGroup
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

    private AnnotationGroup<Gua>? hexagramNameGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup<Gua> HexagramNameGroup
        => this.DoGroupPropertyWork(ref this.hexagramNameGroup, TITLE_HEXAGRAM_NAME);

    private AnnotationGroup<Gua>? hexagramIndexGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup<Gua> HexagramIndexGroup
        => this.DoGroupPropertyWork(ref this.hexagramIndexGroup, TITLE_HEXAGRAM_INDEX);

    private AnnotationGroup<Gua>? hexagramTextGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup<Gua> HexagramTextGroup
        => this.DoGroupPropertyWork(ref this.hexagramTextGroup, TITLE_HEXAGRAM_TEXT);

    private AnnotationGroup<Gua>? hexagramYongTextGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup<Gua> HexagramYongTextGroup
        => this.DoGroupPropertyWork(ref this.hexagramYongTextGroup, TITLE_HEXAGRAM_YONG_TEXT);

    private AnnotationGroup<GuaLines>? lineTextGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup<GuaLines> LineTextGroup
        => this.DoGroupPropertyWork(ref this.lineTextGroup, TITLE_LINE_TEXT);

    private AnnotationGroup<Gua>? xiangHexagramGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup<Gua> XiangHexagramGroup
        => this.DoGroupPropertyWork(ref this.xiangHexagramGroup, TITLE_XIANG_HEXAGRAM);

    private AnnotationGroup<Gua>? tuanGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup<Gua> TuanGroup
        => this.DoGroupPropertyWork(ref this.tuanGroup, TITLE_TUAN);

    private AnnotationGroup<Gua>? wenyanGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup<Gua> WenyanGroup
        => this.DoGroupPropertyWork(ref this.wenyanGroup, TITLE_WENYAN);

    private AnnotationGroup<GuaLines>? xiangLineGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup<GuaLines> XiangLineGroup
        => this.DoGroupPropertyWork(ref this.xiangLineGroup, TITLE_XIANG_LINE);

    private AnnotationGroup<Gua>? xiangYongGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup<Gua> XiangYongGroup
        => this.DoGroupPropertyWork(ref this.xiangYongGroup, TITLE_XIANG_YONG);

    #endregion hexagram

    #region the rest of Yizhuan
    /// <summary>
    /// 
    /// </summary>
    public const string TITLE_THE_REST_OF_YIZHUAN = "The rest (of Yizhuan)";

    private AnnotationGroup<string>? theRestOfYizhuanGroup;

    /// <summary>
    /// 
    /// </summary>
    public AnnotationGroup<string> TheRestOfYizhuanGroup
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