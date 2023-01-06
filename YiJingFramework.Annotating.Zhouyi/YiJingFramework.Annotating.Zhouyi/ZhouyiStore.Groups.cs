using System.Diagnostics;
using System.Text.Json;
using YiJingFramework.Annotating.Entities;
using YiJingFramework.Core;

namespace YiJingFramework.Annotating.Zhouyi
{
    public sealed partial class ZhouyiStore
    {
        private AnnotationGroup<Painting> DoGroupPropertyWork(
            ref AnnotationGroup<Painting>? field, string title)
        {
            if (field is not null)
                return field;
            foreach (var g in Store.PaintingGroups)
            {
                if (g.Title == title)
                {
                    field = g;
                    return field;
                }
            }
            field = Store.AddPaintingGroup(title);
            return field;
        }

        private AnnotationGroup<PaintingLines> DoGroupPropertyWork(
            ref AnnotationGroup<PaintingLines>? field, string title)
        {
            if (field is not null)
                return field;
            foreach (var g in Store.PaintingLinesGroups)
            {
                if (g.Title == title)
                {
                    field = g;
                    return field;
                }
            }
            field = Store.AddPaintingLinesGroup(title);
            return field;
        }

        private AnnotationGroup<NoTarget> DoGroupPropertyWork(
            ref AnnotationGroup<NoTarget>? field, string title)
        {
            if (field is not null)
                return field;
            foreach (var g in Store.NoTargetGroups)
            {
                if (g.Title == title)
                {
                    field = g;
                    return field;
                }
            }
            field = Store.AddNoTargetGroup(title);
            return field;
        }

        #region trigram
        private const string TITLE_TRIGRAM_NAME = "Trigram Name";
        private const string TITLE_TRIGRAM_NATURE = "Trigram Nature";

        private AnnotationGroup<Painting>? trigramNameGroup;
        public AnnotationGroup<Painting> TrigramNameGroup
            => DoGroupPropertyWork(ref trigramNameGroup, TITLE_TRIGRAM_NAME);

        private AnnotationGroup<Painting>? trigramNatureGroup;
        public AnnotationGroup<Painting> TrigramNatureGroup
            => DoGroupPropertyWork(ref trigramNatureGroup, TITLE_TRIGRAM_NATURE);
        #endregion trigram

        #region hexagram
        private const string TITLE_HEXAGRAM_NAME = "Hexagram Name";
        private const string TITLE_HEXAGRAM_INDEX = "Hexagram Index";
        private const string TITLE_HEXAGRAM_TEXT = "Hexagram Text";
        private const string TITLE_HEXAGRAM_YONG_TEXT = "Hexagram Yong Text";
        private const string TITLE_LINE_TEXT = "Line Text";

        private const string TITLE_XIANG_HEXAGRAM = "Xiang (Hexagram)";
        private const string TITLE_XIANG_LINE = "Xiang (Line)";
        private const string TITLE_TUAN = "Tuan";
        private const string TITLE_WENYAN = "Wenyan";

        private AnnotationGroup<Painting>? hexagramNameGroup;
        public AnnotationGroup<Painting> HexagramNameGroup
            => DoGroupPropertyWork(ref hexagramNameGroup, TITLE_HEXAGRAM_NAME);

        private AnnotationGroup<Painting>? hexagramIndexGroup;
        public AnnotationGroup<Painting> HexagramIndexGroup
            => DoGroupPropertyWork(ref hexagramIndexGroup, TITLE_HEXAGRAM_INDEX);

        private AnnotationGroup<Painting>? hexagramTextGroup;
        public AnnotationGroup<Painting> HexagramTextGroup
            => DoGroupPropertyWork(ref hexagramTextGroup, TITLE_HEXAGRAM_TEXT);

        private AnnotationGroup<Painting>? hexagramYongTextGroup;
        public AnnotationGroup<Painting> HexagramYongTextGroup
            => DoGroupPropertyWork(ref hexagramYongTextGroup, TITLE_HEXAGRAM_YONG_TEXT);

        private AnnotationGroup<PaintingLines>? lineTextGroup;
        public AnnotationGroup<PaintingLines> LineTextGroup
            => DoGroupPropertyWork(ref lineTextGroup, TITLE_LINE_TEXT);

        private AnnotationGroup<Painting>? hexagramXiangGroup;
        public AnnotationGroup<Painting> HexagramXiangGroup
            => DoGroupPropertyWork(ref hexagramXiangGroup, TITLE_XIANG_HEXAGRAM);

        private AnnotationGroup<Painting>? hexagramTuanGroup;
        public AnnotationGroup<Painting> HexagramTuanGroup
            => DoGroupPropertyWork(ref hexagramTuanGroup, TITLE_TUAN);

        private AnnotationGroup<Painting>? hexagramWenyanGroup;
        public AnnotationGroup<Painting> HexagramWenyanGroup
            => DoGroupPropertyWork(ref hexagramWenyanGroup, TITLE_WENYAN);

        private AnnotationGroup<PaintingLines>? lineXiangGroup;
        public AnnotationGroup<PaintingLines> LineXiangGroup
            => DoGroupPropertyWork(ref lineXiangGroup, TITLE_XIANG_LINE);
        #endregion hexagram

        #region other
        private const string TITLE_XICI = "Xici";
        private const string TITLE_SHUOGUA = "Shuogua";
        private const string TITLE_XUGUA = "Xugua";
        private const string TITLE_ZAGUA = "Zagua";

        private AnnotationGroup<NoTarget>? xiciGroup;
        public AnnotationGroup<NoTarget> XiciGroup
            => DoGroupPropertyWork(ref xiciGroup, TITLE_XICI);

        private AnnotationGroup<NoTarget>? shuoguaGroup;
        public AnnotationGroup<NoTarget> ShuoguaGroup
            => DoGroupPropertyWork(ref shuoguaGroup, TITLE_SHUOGUA);

        private AnnotationGroup<NoTarget>? xuguaGroup;
        public AnnotationGroup<NoTarget> XuguaGroup
            => DoGroupPropertyWork(ref xuguaGroup, TITLE_XUGUA);

        private AnnotationGroup<NoTarget>? zaguaGroup;
        public AnnotationGroup<NoTarget> ZaguaGroup
            => DoGroupPropertyWork(ref zaguaGroup, TITLE_ZAGUA);
        #endregion
    }
}