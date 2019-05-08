using System;
using NHibernate.Search.Attributes;

namespace Xilion.Framework.Domain
{
    public interface IEntity
    {
        bool IsTransient();

        object GetId();

        Type GetIdType();

        Type GetTypeUnproxied();
    }

    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; }
    }
}