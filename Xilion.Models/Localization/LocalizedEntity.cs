using Xilion.Framework.Domain;

namespace Xilion.Models.Localization
{
    public class LocalizedEntity : Entity
    {
        public virtual LocalizedEntityID ID { get; set; }
        public virtual string Value { get; set; }
    }

    public class LocalizedEntityID
    {
        public virtual int Culture { get; set; }
        public virtual string Type { get; set; }
        public virtual string Property { get; set; }
        public virtual string EntityID { get; set; }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                var other = obj as LocalizedEntityID;
                if (other != null)
                {
                    return Type == other.Type &&
                           Property == other.Property &&
                           EntityID == other.EntityID &&
                           Culture == other.Culture;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}