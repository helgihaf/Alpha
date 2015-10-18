using System;

namespace Marson.Compare.Core
{
    public static class FileComparerFactory
    {
        internal static IFileComparer Create(CompareOptions options)
        {
            switch (options.CompareType)
            {
                case CompareType.DateTime:
                    return new DateTimeFileComparer();
                case CompareType.Binary:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}