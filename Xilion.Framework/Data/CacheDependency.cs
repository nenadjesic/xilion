using System;
using System.Collections.Generic;

namespace Xilion.Framework.Data
{
    public class CacheDependency
    {
        private static CacheDependency _current;
        private Dictionary<int, CacheDependencyNode> _subscribers = new Dictionary<int, CacheDependencyNode>();

        public static CacheDependency Current
        {
            get { return _current ?? (_current = new CacheDependency()); }
            set
            {
                if (_current != null)
                    value._subscribers = _current._subscribers;
                _current = value;
            }
        }

        protected int GetTypeCode(Type type)
        {
            if (type == null || type.FullName == null)
                throw new ArgumentNullException("type");

            return type.FullName.GetHashCode();
        }

        public void Notify(Type type)
        {
            Notify(type, EventArgs.Empty);
        }

        public void Notify(Type type, EventArgs e)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (e == null)
                throw new ArgumentNullException("e");

            Notify(type, null, e);
        }

        public virtual void Notify(Type type, object sender, EventArgs e)
        {
            NotifyInner(GetTypeCode(type), sender, e);
        }

        protected void NotifyInner(int typeHash, object sender, EventArgs e)
        {
            CacheDependencyNode node;
            if (_subscribers.TryGetValue(typeHash, out node))
                node.OnChange(sender, e);
        }

        public void Subscribe(Type type, EventHandler handler)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (handler == null)
                throw new ArgumentNullException("handler");

            int typeCode = GetTypeCode(type);
            CacheDependencyNode node;

            if (!_subscribers.TryGetValue(typeCode, out node))
            {
                node = new CacheDependencyNode();
                _subscribers.Add(typeCode, node);
            }
            node.Change += handler;
        }

        #region Nested type: CacheDependencyNode

        private class CacheDependencyNode
        {
            public event EventHandler Change;

            public void OnChange(object sender, EventArgs e)
            {
                if (Change != null)
                    Change(sender, e);
            }
        }

        #endregion
    }
}