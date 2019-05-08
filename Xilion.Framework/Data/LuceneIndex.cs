using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Search;
using NHibernate.Search.Attributes;
using Xilion.Framework.Domain;

namespace Xilion.Framework.Data
{
    public static class LuceneIndex
    {
        public const string IndexLocation = "~/_content/Index";

        public static void Reindex()
        {
            var entityTypes = new List<Type>();
            foreach (Assembly assembly in AssemblyScanner.GetAllReferencingFrameCore())
                entityTypes.AddRange(assembly.GetTypes().Where(x => typeof (Entity).IsAssignableFrom(x)));

            foreach (Type t in entityTypes)
                if (TypeDescriptor.GetAttributes(t)[typeof (IndexedAttribute)] != null)
                    ReindexEntity(t);
        }

        private static void ReindexEntity(Type t)
        {
            bool stop = false;
            int index = 0;
            const int pageSize = 500;

            do
            {
                var session = (IFullTextSession) new SessionBuilder().GetSession();

                IList list = session.CreateCriteria(t)
                    .SetFirstResult(index)
                    .SetMaxResults(pageSize).List();

                using (ITransaction transaction = session.BeginTransaction())
                {
                    foreach (object itm in list)
                        session.Index(itm);
                    transaction.Commit();
                }

                index += pageSize;
                if (list.Count < pageSize) stop = true;
            } while (!stop);
        }
    }
}