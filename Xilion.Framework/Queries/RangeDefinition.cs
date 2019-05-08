namespace Xilion.Framework.Queries
{
    public class RangeDefinition
    {
        public RangeDefinition()
        {
        }

        public RangeDefinition(object fromValue, object toValue)
        {
            FromValue = fromValue;
            ToValue = toValue;
        }

        public object FromValue { get; set; }
        public object ToValue { get; set; }

        public bool IsNull()
        {
            return FromValue == null || ToValue == null;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (RangeDefinition)) return false;
            return Equals((RangeDefinition) obj);
        }

        public bool Equals(RangeDefinition other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.FromValue, FromValue) && Equals(other.ToValue, ToValue);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((FromValue != null ? FromValue.GetHashCode() : 0) * 397) ^
                       (ToValue != null ? ToValue.GetHashCode() : 0);
            }
        }
    }
}