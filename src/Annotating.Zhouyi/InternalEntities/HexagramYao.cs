using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiJingFramework.PrimitiveTypes;
using YiJingFramework.PrimitiveTypes.GuaWithFixedCount;

namespace YiJingFramework.Annotating.Zhouyi.InternalEntities;
internal sealed record HexagramYao(GuaHexagram Gua, int YaoIndex)
{
    public override string ToString()
    {
        return $"{this.Gua}-{this.YaoIndex}";
    }

    public static bool CheckAndParse(string? s, [MaybeNullWhen(false)] out HexagramYao result)
    {
        if(s is null)
        {
            result = null;
            return false;
        }

        var sp = s.Split('-');

        if(sp.Length != 2)
        {
            result = null;
            return false;
        }

        if (!GuaHexagram.TryParse(sp[0], out var h) || !int.TryParse(sp[1], out var i))
        {
            result = null;
            return false;
        }

        if (i < 0 || i >= h.Count)
        {
            result = null;
            return false;
        }

        result = new(h, i);
        return true;
    }
}