using System;
using System.Collections.Generic;
using System.Linq;
using Xilion.Models.Core.Domain;

namespace Xilion.Models.Core.Extensions
{
    public static class HierarchyExtensions
    {
        /// <summary>
        ///   Checks if hierarchy item is equal to reference item or its child (disregarding of level).
        /// </summary>
        /// <param name="source"> Source item. </param>
        /// <param name="reference"> Reference item. </param>
        /// <returns> Value indicates if source is equal to reference or reference is child of source. </returns>
        public static bool IsEqualOrChildOf(this IHierarchy source, IHierarchy reference)
        {
            return source.Equals(reference) || reference.Children.Any(source.IsEqualOrChildOf);
        }


        /// <summary>
        ///   Sets hierarchy item parent
        /// </summary>
        /// <param name="source"> Hierarchy item to manipulate with. </param>
        /// <param name="parent"> Parent hierarchy item. </param>
        public static void SetParent(this IHierarchy source, IHierarchy parent)
        {
            MoveTo(source, parent, HierarchyMovePosition.Over);
        }

        /// <summary>
        ///   Moves source under specified destination item. 
        ///   If destination is inside of source tree it will throw exception.
        /// </summary>
        /// <param name="source"> Source item. </param>
        /// <param name="destination"> Destination item. </param>
        /// <param name="position"> <see cref="HierarchyMovePosition" /> value </param>
        public static void MoveTo(this IHierarchy source, IHierarchy destination, HierarchyMovePosition position)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (destination == null)
                throw new ArgumentNullException("destination");

            if (source.IsEqualOrChildOf(destination))
                throw new HierarchyException("Source entity cannot be a parent or sibling to itself or its child.");

            switch (position)
            {
                case HierarchyMovePosition.Over:
                    source.Parent = destination;
                    source.Ordinal = destination.Children.Max(x=>x.Ordinal) + 1;
                    Reindex(destination.Parent.Children);
                    return;
                case HierarchyMovePosition.After:
                    source.Parent = destination.Parent;
                    // todo: Ordinal property has to be set here
                    if (destination.Parent != null)
                        Reindex(destination.Parent.Children);
                    break;
                case HierarchyMovePosition.Before:
                    source.Parent = destination.Parent;
                    if (destination.Parent != null)
                        Reindex(destination.Parent.Children);
                    // todo: Ordinal property has to be set here
                    break;
            }
        }

        /// <summary>
        ///   Moves source to the top level (Parent is <c>null</c>)
        /// </summary>
        /// <param name="source"> Source item. </param>
        public static void MoveOnTop(this IHierarchy source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            source.Parent = null;
        }

        /// <summary>
        ///   Gets item hierarchy level.
        /// </summary>
        /// <param name="item"> Hierarchy item </param>
        /// <returns> </returns>
        public static int Level(this IHierarchy item)
        {
            var level = -1;
            if (level == -1)
            {
                level = 0;
                var parent = item.Parent;
                while (parent != null)
                {
                    parent = parent.Parent;
                    level++;
                }
            }
            return level;
        }

        /// <summary>
        ///   Gets a value indicates if label has children.
        /// </summary>
        /// <param name="item"> Hierarchy item </param>
        /// <returns> </returns>
        public static bool HasChildren(this IHierarchy item)
        {
            return item.Children != null && item.Children.Any();
        }

        /// <summary>
        ///   Adjusts Ordinal property for hierarchy.
        /// </summary>
        /// <param name="list"> </param>
        private static void Reindex(this IEnumerable<IHierarchy> list)
        {
            var index = 1;
            foreach (var label in list)
            {
                label.Ordinal = index;
                index++;
            }
        }
    }
}