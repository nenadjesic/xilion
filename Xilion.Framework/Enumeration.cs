using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Xilion.Framework
{
    /// <summary>
    /// Class to use instead of enums.
    /// </summary>
    /// <remarks>
    /// The code is from the following URL and CodeCampServer:
    /// http://www.lostechies.com/blogs/jimmy_bogard/archive/2008/08/12/enumeration-classes.aspx
    /// Changed to differe Name and DispllayName (localizable)
    /// </remarks>
    [Serializable]
    [DataContract]
    public abstract class Enumeration : IComparable
    {
        protected Enumeration(int value, string name, string displayName)
        {
            Value = value;
            Name = name;
            DisplayName = displayName;
        }

        protected Enumeration(int value, string name)
        {
            Value = value;
            Name = name;
            DisplayName = name;
        }

        [DataMember]
        public int Value { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string LowerName
        {
            get { return Name.ToLowerInvariant(); }
        }

        [DataMember]
        public string DisplayName { get; private set; }

        #region IComparable Members

        public virtual int CompareTo(object other)
        {
            Guard.IsNotNull(other, "other");
            Guard.IsOfType<Enumeration>(other, "other");

            return Value.CompareTo(((Enumeration) other).Value);
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            return typeof (T)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(info => info.GetValue(null))
                .Cast<T>();
        }

        public static IEnumerable<Enumeration> GetAll(Type type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(info => info.GetValue(null))
                .Cast<Enumeration>();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
                return false;

            bool typeMatches = GetType() == obj.GetType();
            bool valueMatches = Value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static T FromValue<T>(int value) where T : Enumeration
        {
            return (T) FromValue(typeof (T), value);
        }

        public static Enumeration FromValue(Type type, int value)
        {
            return Parse(type, value, "value", item => item.Value == value);
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            return (T) FromDisplayName(typeof (T), displayName);
        }

        public static Enumeration FromDisplayName(Type type, string name)
        {
            return Parse(type, name, "display name", item => item.Name == name);
        }

        private static Enumeration Parse<TValue>(
            Type type, TValue value, string description, Func<Enumeration, bool> predicate)
        {
            Enumeration matchingItem = GetAll(type).FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                string message = String.Format("'{0}' is not a valid {1} in {2}", value, description, type);
                throw new InvalidOperationException(message);
            }

            return matchingItem;
        }
    }
}