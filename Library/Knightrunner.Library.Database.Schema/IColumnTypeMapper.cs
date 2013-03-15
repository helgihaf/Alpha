using System;
using System.Collections.Specialized;

namespace Knightrunner.Library.Database.Schema
{
    public interface IColumnTypeMapper
    {
        string GetColumnTypeString(TargetSystem targetSystem, Column column);
    }
}
