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
    public static class PaintingLinesExtensions
    {
        public static bool IsSingleLine(
            this PaintingLines paintingLines,
            out int lineIndex)
        {
            var lines = paintingLines.Lines;

            lineIndex = -1;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].IsYang)
                {
                    if (lineIndex is not -1)
                        return false;

                    lineIndex = i;
                }
            }
            return lineIndex is not -1;
        }
    }
}
