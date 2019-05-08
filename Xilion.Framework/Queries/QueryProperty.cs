using System;

namespace Xilion.Framework.Queries
{
    public class QueryProperty
    {
        private readonly LogicOperator _listOperator = LogicOperator.Or;

        public QueryProperty()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        public QueryProperty(string propertyName)
        {
            Name = propertyName;
            LogicOperator = LogicOperator.And;
            DefaultValue = String.Empty;
            IsLocalized = false;
            QueryOperator = QueryOperator.Equal;
        }
        
        public string Name { get; private set; }
        public object Value { get; set; }
        public bool IsQ { get; set; }
        public object DefaultValue { get; private set; }
        public GroupProperty Group { get; private set; }

        /// <summary>
        /// Sets property group
        /// </summary>
        /// <param name="group"><see cref="GroupProperty"/> group</param>
        /// <returns></returns>
        public QueryProperty SetToGroup(GroupProperty group)
        {
            Group = group;
            return this;
        }

        /// <summary>
        /// Sets property default value.
        /// </summary>
        /// <param name="defaultValue"><see cref="String.Empty"/></param>
        /// <returns><see cref="QueryProperty"/></returns>
        public QueryProperty SetDefaultValue(object defaultValue)
        {
            DefaultValue = defaultValue;
            return this;
        }

        public bool IsLocalized { get; private set; }

        /// <summary>
        /// Sets is property localized.
        /// Default is false.
        /// </summary>
        /// <returns><see cref="QueryProperty"/></returns>
        public QueryProperty SetIsLocalized(bool isLocalized)
        {
            IsLocalized = isLocalized;
            return this;
        }

        public QueryOperator QueryOperator { get; private set; }
        public LogicOperator ListOperator { get; private set; }
        public bool IsList { get; private set; }

        /// <summary>
        /// Sets property is list.
        /// </summary>
        /// <param name="listOperator">Define list internal logic operator. Default is Or</param>
        /// <returns><see cref="QueryProperty"/></returns>
        public QueryProperty SetList(LogicOperator listOperator = null)
        {
            IsList = true;
            ListOperator = listOperator ?? _listOperator;
            return this;
        }

        /// <summary>
        /// Sets property query operator.
        /// </summary>
        /// <param name="queryOperator"><see cref="QueryOperator"/>. Default is equal. </param>
        /// <returns><see cref="QueryProperty"/></returns>
        public QueryProperty SetOperator(QueryOperator queryOperator)
        {
            if (queryOperator == QueryOperator.Between)
                Value = new RangeDefinition(DateTime.MinValue, DateTime.MaxValue);

            QueryOperator = queryOperator;
            return this;
        }

        public LogicOperator LogicOperator { get; private set; }

        /// <summary>
        /// Sets property logic operator.
        /// Default is And
        /// </summary>
        /// <param name="logicOperator"></param>
        /// <returns></returns>
        public QueryProperty SetLogicOperator(LogicOperator logicOperator)
        {
            LogicOperator = logicOperator;
            return this;
        }

        public virtual bool HasValue()
        {
            if (Value == null)
                return false;
            if (Value is string[] && ((string[]) Value).Length == 0)
                return false;
            if (Value is RangeDefinition)
            {
                var range = Value as RangeDefinition;
                if (range.IsNull())
                    return false;
                if (range.FromValue is DateTime)
                    return !Value.Equals(new RangeDefinition(DateTime.MinValue, DateTime.MaxValue));
            }
            return !Value.Equals(DefaultValue);
        }

    }
}