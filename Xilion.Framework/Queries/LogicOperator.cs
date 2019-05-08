namespace Xilion.Framework.Queries
{
    public class LogicOperator : Enumeration
    {
        public static readonly LogicOperator Or = new LogicOperator(0, " ", "Or");
        public static readonly LogicOperator And = new LogicOperator(1, " AND ", "And");

        public LogicOperator(int value, string name, string displayName)
            : base(value, name, displayName)
        {
        }

        public LogicOperator(int value, string name)
            : base(value, name)
        {
        }
    }
}