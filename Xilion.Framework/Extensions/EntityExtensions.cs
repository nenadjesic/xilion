using System;
using System.Collections.Generic;
using System.Linq;
using Xilion.Framework.Domain;

namespace Xilion.Framework.Extensions
{
    public static class EntityExtensions
    {
        public static string ToCommaSeparatedIdString<T>(this IEnumerable<T> entities) where T : Entity
        {
            return String.Join(",", entities.Select(x => x.Id.ToString()));
        }
    }
}