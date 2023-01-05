using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using YiJingFramework.Annotating.Entities;

namespace YiJingFramework.Annotating.Zhouyi.Extensions
{
    internal static class PaintingLinesExtensions
    {
        public static bool IsSingleLine(
            this PaintingLines paintingLinesOfHexagramsOrTrigrams, 
            out int lineIndex)
        {
            bool TryLog2(int x, out int result)
            {
                Debug.Assert(x > 0);

                result = 0;

                if ((x & (x - 1)) is not 0)
                    return false;

                for (; ; )
                {
                    x >>= 1;
                    if (x > 0)
                    {
                        result++;
                        continue;
                    }
                    return true;
                }
            }

            Debug.Assert(paintingLinesOfHexagramsOrTrigrams.Lines.Count <= 7);

            var linesByte = paintingLinesOfHexagramsOrTrigrams.Lines.ToBytes()[0] - 
                (1 << paintingLinesOfHexagramsOrTrigrams.Lines.Count);

            return TryLog2(linesByte, out lineIndex);
        }
    }
}
