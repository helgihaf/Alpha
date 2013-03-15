using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Core
{
    public static class ExceptionExtensions
    {
        public static bool IsCritical(this Exception ex)
        {
            return
                ex is OutOfMemoryException
                || ex is AppDomainUnloadedException
                || ex is BadImageFormatException
                || ex is CannotUnloadAppDomainException
                || ex is InvalidProgramException
                || ex is System.Threading.ThreadAbortException
                ;
        }


        public static ExceptionSummary Summary(this Exception ex, bool includeStackTrace = true)
        {
            return new ExceptionSummary(ex, includeStackTrace);
        }
    }
}

