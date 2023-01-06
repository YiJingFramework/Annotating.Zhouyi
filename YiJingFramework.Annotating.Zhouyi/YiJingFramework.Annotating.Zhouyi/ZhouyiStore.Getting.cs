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

        /// <summary>
        /// 获取《系辞》。
        /// Get Xici.
        /// 得到的结果和本实例没有绑定关系。
        /// 如要修改此仓库的内容，需再调用 <seealso cref="UpdateStore(Xici)"/> 。
        /// The result is not bound to this instance.
        /// Use <seealso cref="UpdateStore(Xici)"/> if you want to edit the store.
        /// </summary>
        /// <returns>
        /// 结果。
        /// The result.
        /// </returns>
        public Xici GetXici()
        {
            var entries = Groups.XiciGroup.Entries;
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

        /// <summary>
        /// 获取《说卦》。
        /// Get Shuogua.
        /// 得到的结果和本实例没有绑定关系。
        /// 如要修改此仓库的内容，需再调用 <seealso cref="UpdateStore(Shuogua)"/> 。
        /// The result is not bound to this instance.
        /// Use <seealso cref="UpdateStore(Shuogua)"/> if you want to edit the store.
        /// </summary>
        /// <returns>
        /// 结果。
        /// The result.
        /// </returns>
        public Shuogua GetShuogua()
        {
            return new Shuogua() {
                Content = GetFirstContent(Groups.ShuoguaGroup)
            };
        }

        /// <summary>
        /// 获取《序卦》。
        /// Get Xugua.
        /// 得到的结果和本实例没有绑定关系。
        /// 如要修改此仓库的内容，需再调用 <seealso cref="UpdateStore(Xugua)"/> 。
        /// The result is not bound to this instance.
        /// Use <seealso cref="UpdateStore(Xugua)"/> if you want to edit the store.
        /// </summary>
        /// <returns>
        /// 结果。
        /// The result.
        /// </returns>
        public Xugua GetXugua()
        {
            return new Xugua() {
                Content = GetFirstContent(Groups.XuguaGroup)
            };
        }

        /// <summary>
        /// 获取《杂卦》。
        /// Get Zagua.
        /// 得到的结果和本实例没有绑定关系。
        /// 如要修改此仓库的内容，需再调用 <seealso cref="UpdateStore(Zagua)"/> 。
        /// The result is not bound to this instance.
        /// Use <seealso cref="UpdateStore(Zagua)"/> if you want to edit the store.
        /// </summary>
        /// <returns>
        /// 结果。
        /// The result.
        /// </returns>
        public Zagua GetZagua()
        {
            return new Zagua() {
                Content = GetFirstContent(Groups.ZaguaGroup)
            };
        }
    }
}