using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJingFramework.Annotating.Zhouyi.Entities
{
    /// <summary>
    /// 《杂卦》。
    /// Zagua.
    /// 此类型的实例不可比较。
    /// Instance of this type cannot be compared.
    /// </summary>
    public sealed record Zagua
    {
        /// <summary>
        /// 内容。
        /// The content.
        /// </summary>
        public string? Content { get; set; }
    }
}
