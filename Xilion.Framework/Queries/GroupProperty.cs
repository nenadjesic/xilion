namespace Xilion.Framework.Queries
{
    public class GroupProperty
    {
        private readonly LogicOperator _groupOperator = LogicOperator.And;

        /// <summary>
        ///     Creates new properties group.
        /// </summary>
        /// <param name="groupOperator">
        ///     Group <see cref="LogicOperator" /> operator. Default is And.
        /// </param>
        public GroupProperty(LogicOperator groupOperator = null)
        {
            GroupOperator = groupOperator ?? _groupOperator;
        }

        public LogicOperator GroupOperator { get; private set; }
    }
}