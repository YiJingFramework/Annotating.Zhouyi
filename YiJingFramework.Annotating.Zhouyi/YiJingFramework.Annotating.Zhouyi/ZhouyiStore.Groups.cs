using System.Diagnostics;
using System.Text.Json;
using YiJingFramework.Annotating.Entities;
using YiJingFramework.Core;

namespace YiJingFramework.Annotating.Zhouyi
{
    public sealed partial class ZhouyiStore
    {
        private const string TITLE_TRIGRAM_NAME = "TN";
        private const string TITLE_TRIGRAM_NATURE = "TNa";

        private const string TITLE_HEXAGRAM_NAME = "HN";
        private const string TITLE_HEXAGRAM_INDEX = "HI";
        private const string TITLE_HEXAGRAM_TEXT = "HT";
        private const string TITLE_HEXAGRAM_APPLY_NINTH_AND_SIXTH = "HA";
        private const string TITLE_LINE_TEXT = "LT";

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

        private AnnotationGroup<Painting>? trigramNameGroup;
        public AnnotationGroup<Painting> TrigramNameGroup
            => DoGroupPropertyWork(ref trigramNameGroup, TITLE_TRIGRAM_NAME);

        private AnnotationGroup<Painting>? trigramNatureGroup;
        public AnnotationGroup<Painting> TrigramNatureGroup
            => DoGroupPropertyWork(ref trigramNatureGroup, TITLE_TRIGRAM_NATURE);

        private AnnotationGroup<Painting>? hexagramNameGroup;
        public AnnotationGroup<Painting> HexagramNameGroup
            => DoGroupPropertyWork(ref hexagramNameGroup, TITLE_HEXAGRAM_NAME);

        private AnnotationGroup<Painting>? hexagramIndexGroup;
        public AnnotationGroup<Painting> HexagramIndexGroup
            => DoGroupPropertyWork(ref hexagramIndexGroup, TITLE_HEXAGRAM_INDEX);

        private AnnotationGroup<Painting>? hexagramTextGroup;
        public AnnotationGroup<Painting> HexagramTextGroup
            => DoGroupPropertyWork(ref hexagramTextGroup, TITLE_HEXAGRAM_TEXT);

        private AnnotationGroup<Painting>? applyNinthAndSixthGroup;
        public AnnotationGroup<Painting> ApplyNinthAndSixthGroup
            => DoGroupPropertyWork(
                ref applyNinthAndSixthGroup,
                TITLE_HEXAGRAM_APPLY_NINTH_AND_SIXTH);

        private AnnotationGroup<PaintingLines>? lineTextGroup;
        public AnnotationGroup<PaintingLines> LineTextGroup
            => DoGroupPropertyWork(ref lineTextGroup, TITLE_LINE_TEXT);
    }
}