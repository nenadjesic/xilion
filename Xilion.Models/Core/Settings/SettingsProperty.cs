using System;

namespace Xilion.Models.Core.Settings
{

    [Serializable]
    public class SettingsProperty
    {
        public virtual string Name { get; set; }

        public virtual object Value { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is SettingsProperty))
                throw new InvalidCastException();

            return (Name == ((SettingsProperty) obj).Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}