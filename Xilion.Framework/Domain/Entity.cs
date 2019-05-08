using System;
using System.Xml.Serialization;
using NHibernate.Intercept;
using NHibernate.Proxy;
using NHibernate.Proxy.DynamicProxy;
using NHibernate.Search.Attributes;
using Xilion.Framework.Attributes;

namespace Xilion.Framework.Domain
{
    /// <summary>
    /// Represents an entity with <c>long</c> ID.
    /// </summary>
    [Serializable]
    [Ignore]
    public abstract class Entity : Entity<long>
    {
        public override bool IsTransient()
        {
            return Id <= 0;
        }
    }

    [Serializable]
    [Ignore]
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        [XmlIgnore]
        public virtual TKey Id { get; protected set; }

        public virtual bool IsTransient()
        {
            return Id == null || Id.Equals(default(TKey));
        }

        public virtual bool IsPersistent
        {
            get { return !IsTransient(); }
        }

        public virtual object GetId()
        {
            return Id;
        }

        public virtual Type GetIdType()
        {
            return typeof(TKey);
        }

        public virtual Type GetTypeUnproxied()
        {
            return GetProxyRealType(this);
        }

        private static Type GetProxyRealType(object proxy)
        {
            var nhProxy = proxy as IProxy;
            if (nhProxy == null)
            {
                return proxy.GetType();
            }

            var lazyInitializer = nhProxy.Interceptor as ILazyInitializer;
            if (lazyInitializer != null)
            {
                return lazyInitializer.PersistentClass;
            }

            var fieldInterceptorAccessor = nhProxy.Interceptor as IFieldInterceptorAccessor;
            if (fieldInterceptorAccessor != null)
            {
                return fieldInterceptorAccessor.FieldInterceptor == null
                    ? proxy.GetType().BaseType
                    : fieldInterceptorAccessor.FieldInterceptor.MappedClass;
            }

            return proxy.GetType();
        }
    }
}