namespace Xilion.Framework.Queries
{
    public class QueryOperator : Enumeration
    {
        public static readonly QueryOperator Equal = new QueryOperator(0, "{0}", "Equal");
        public static readonly QueryOperator NotEqual = new QueryOperator(1, "{0}", "Not equal");
        public static readonly QueryOperator Between = new QueryOperator(2, "{{{0}}}", "Between");
        public static readonly QueryOperator StartsWith = new QueryOperator(3, "{0}*", "Start with");

        //public static readonly QueryOperator GreaterThan = new QueryOperator(4, ">", "Greater than");
        //public static readonly QueryOperator GreaterThanOrEqual = new QueryOperator(5, ">=", "Greater than or equal");
        //public static readonly QueryOperator LessThan = new QueryOperator(6, "<", "Less than");
        //public static readonly QueryOperator LessThanOrEqual = new QueryOperator(7, "<=", "Less than or equal");

        public QueryOperator(int value, string name, string displayName)
            : base(value, name, displayName)
        {
        }

        public QueryOperator(int value, string name)
            : base(value, name)
        {
        }
    }
}