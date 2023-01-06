using YiJingFramework.Annotating.Entities;
using YiJingFramework.Core;

namespace YiJingFramework.Annotating.Zhouyi
{
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

        private AnnotationGroup<Painting> DoGroupPropertyWork(
            ref AnnotationGroup<Painting>? field, string title)
        {
            if (field is not null)
                return field;
            var annotationStore = this.store.Store;
            foreach (var g in annotationStore.PaintingGroups)
            {
                if (g.Title == title)
                {
                    field = g;
                    return field;
                }
            }
            field = annotationStore.AddPaintingGroup(title);
            return field;
        }

        private AnnotationGroup<PaintingLines> DoGroupPropertyWork(
            ref AnnotationGroup<PaintingLines>? field, string title)
        {
            if (field is not null)
                return field;
            var annotationStore = this.store.Store;
            foreach (var g in annotationStore.PaintingLinesGroups)
            {
                if (g.Title == title)
                {
                    field = g;
                    return field;
                }
            }
            field = annotationStore.AddPaintingLinesGroup(title);
            return field;
        }

        private AnnotationGroup<NoTarget> DoGroupPropertyWork(
            ref AnnotationGroup<NoTarget>? field, string title)
        {
            if (field is not null)
                return field;
            var annotationStore = this.store.Store;
            foreach (var g in annotationStore.NoTargetGroups)
            {
                if (g.Title == title)
                {
                    field = g;
                    return field;
                }
            }
            field = annotationStore.AddNoTargetGroup(title);
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

        private AnnotationGroup<Painting>? trigramNameGroup;

        /// <summary>
        /// 
        /// </summary>
        public AnnotationGroup<Painting> TrigramNameGroup
            => DoGroupPropertyWork(ref trigramNameGroup, TITLE_TRIGRAM_NAME);

        private AnnotationGroup<Painting>? trigramNatureGroup;
        /// <summary>
        /// 
        /// </summary>
        public AnnotationGroup<Painting> TrigramNatureGroup
            => DoGroupPropertyWork(ref trigramNatureGroup, TITLE_TRIGRAM_NATURE);
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

        private AnnotationGroup<Painting>? hexagramNameGroup;

        /// <summary>
        /// 
        /// </summary>
        public AnnotationGroup<Painting> HexagramNameGroup
            => DoGroupPropertyWork(ref hexagramNameGroup, TITLE_HEXAGRAM_NAME);

        private AnnotationGroup<Painting>? hexagramIndexGroup;

        /// <summary>
        /// 
        /// </summary>
        public AnnotationGroup<Painting> HexagramIndexGroup
            => DoGroupPropertyWork(ref hexagramIndexGroup, TITLE_HEXAGRAM_INDEX);

        private AnnotationGroup<Painting>? hexagramTextGroup;

        /// <summary>
        /// 
        /// </summary>
        public AnnotationGroup<Painting> HexagramTextGroup
            => DoGroupPropertyWork(ref hexagramTextGroup, TITLE_HEXAGRAM_TEXT);

        private AnnotationGroup<Painting>? hexagramYongTextGroup;

        /// <summary>
        /// 
        /// </summary>
        public AnnotationGroup<Painting> HexagramYongTextGroup
            => DoGroupPropertyWork(ref hexagramYongTextGroup, TITLE_HEXAGRAM_YONG_TEXT);

        private AnnotationGroup<PaintingLines>? lineTextGroup;

        /// <summary>
        /// 
        /// </summary>
        public AnnotationGroup<PaintingLines> LineTextGroup
            => DoGroupPropertyWork(ref lineTextGroup, TITLE_LINE_TEXT);

        private AnnotationGroup<Painting>? xiangHexagramGroup;

        /// <summary>
        /// 
        /// </summary>
        public AnnotationGroup<Painting> XiangHexagramGroup
            => DoGroupPropertyWork(ref xiangHexagramGroup, TITLE_XIANG_HEXAGRAM);

        private AnnotationGroup<Painting>? tuanGroup;

        /// <summary>
        /// 
        /// </summary>
        public AnnotationGroup<Painting> TuanGroup
            => DoGroupPropertyWork(ref tuanGroup, TITLE_TUAN);

        private AnnotationGroup<Painting>? wenyanGroup;

        /// <summary>
        /// 
        /// </summary>
        public AnnotationGroup<Painting> WenyanGroup
            => DoGroupPropertyWork(ref wenyanGroup, TITLE_WENYAN);

        private AnnotationGroup<PaintingLines>? xiangLineGroup;

        /// <summary>
        /// 
        /// </summary>
        public AnnotationGroup<PaintingLines> XiangLineGroup
            => DoGroupPropertyWork(ref xiangLineGroup, TITLE_XIANG_LINE);

        private AnnotationGroup<Painting>? xiangYongGroup;

        /// <summary>
        /// 
        /// </summary>
        public AnnotationGroup<Painting> XiangYongGroup
            => DoGroupPropertyWork(ref xiangYongGroup, TITLE_XIANG_YONG);

        #endregion hexagram

        #region others
        /// <summary>
        /// 
        /// </summary>
        public const string TITLE_XICI = "Xici";

        /// <summary>
        /// 
        /// </summary>
        public const string TITLE_SHUOGUA = "Shuogua";

        /// <summary>
        /// 
        /// </summary>
        public const string TITLE_XUGUA = "Xugua";

        /// <summary>
        /// 
        /// </summary>
        public const string TITLE_ZAGUA = "Zagua";

        private AnnotationGroup<NoTarget>? xiciGroup;

        /// <summary>
        /// 
        /// </summary>
        public AnnotationGroup<NoTarget> XiciGroup
            => DoGroupPropertyWork(ref xiciGroup, TITLE_XICI);

        private AnnotationGroup<NoTarget>? shuoguaGroup;

        /// <summary>
        /// 
        /// </summary>
        public AnnotationGroup<NoTarget> ShuoguaGroup
            => DoGroupPropertyWork(ref shuoguaGroup, TITLE_SHUOGUA);

        private AnnotationGroup<NoTarget>? xuguaGroup;

        /// <summary>
        /// 
        /// </summary>
        public AnnotationGroup<NoTarget> XuguaGroup
            => DoGroupPropertyWork(ref xuguaGroup, TITLE_XUGUA);

        private AnnotationGroup<NoTarget>? zaguaGroup;

        /// <summary>
        /// 
        /// </summary>
        public AnnotationGroup<NoTarget> ZaguaGroup
            => DoGroupPropertyWork(ref zaguaGroup, TITLE_ZAGUA);
        #endregion
    }
}