using System;
using System.Collections.Generic;
using System.Text;

namespace AutoList
{
    public static class AutoListPatterns
    {
        public const string LinesLengthPattern = @"[L,l]ength\s+=?\s*(?<number>\d+\.?\d*)";

        public const string HatchAreaPattern = @"Area\s*(?<number>\d+\.?\d*)";

        public const string TextPattern = @"(text|Contents:)\s*(?<text>.*)";
    }

    public enum ExportType
    {
        CSV
    }
}
