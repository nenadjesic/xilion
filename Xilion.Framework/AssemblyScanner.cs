using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Xilion.Framework
{
    public static class AssemblyScanner
    {
        public static IEnumerable<Assembly> GetAllReferencingFrameCore()
        {
            Assembly thisAssembly = typeof (AssemblyScanner).Assembly;

            return new[] {thisAssembly}
                .Union(AppDomain.CurrentDomain.GetAssemblies()
                           .Where(x => !x.IsDynamic)
                           .Where(x => x.GetReferencedAssemblies()
                                           .Any(a => a.FullName == thisAssembly.FullName)));
        }
    }
}