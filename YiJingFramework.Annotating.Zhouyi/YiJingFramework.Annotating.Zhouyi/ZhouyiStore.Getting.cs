using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;
using YiJingFramework.Annotating.Entities;
using YiJingFramework.Annotating.Zhouyi.Entities;
using YiJingFramework.Annotating.Zhouyi.Extensions;
using YiJingFramework.Core;
using static System.Net.Mime.MediaTypeNames;

namespace YiJingFramework.Annotating.Zhouyi
{
    public sealed partial class ZhouyiStore
    {
        private static string? GetFirstContent(AnnotationGroup<NoTarget> group)
        {
            var entries = group.Entries;
            if (entries.Count is 0)
                return null;
            return entries[0].Content;
        }

        public Xici GetXici()
        {
            var entries = XiciGroup.Entries;
            return entries.Count switch {
                0 => new Xici(),
                1 => new Xici() {
                    PartA = entries[0].Content
                },
                _ => new Xici() {
                    PartA = entries[0].Content,
                    PartB = entries[1].Content
                },
            };
        }

        public Shuogua GetShuogua()
        {
            return new Shuogua() {
                Content = GetFirstContent(ShuoguaGroup)
            };
        }

        public Xugua GetXugua()
        {
            return new Xugua() {
                Content = GetFirstContent(XuguaGroup)
            };
        }
        public Zagua GetZagua()
        {
            return new Zagua() {
                Content = GetFirstContent(ZaguaGroup)
            };
        }
    }
}