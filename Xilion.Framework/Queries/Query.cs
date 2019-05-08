using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Xilion.Framework.Data;
using Xilion.Framework.Configuration;

namespace Xilion.Framework.Queries
{
    /// <summary>
    /// Represents a query for performing full-text search.
    /// </summary>
    public class Query
    {
        private readonly List<QueryProperty> _properties = new List<QueryProperty>();

        public Query()
        {
            LogicOperator = LogicOperator.And;
            Paging = new PagerInfo();
        }

        /// <summary>
        ///   Gets or sets univerzal q property value. Q property includes in search any <see cref="QueryProperty" /> marked as IsQ=true.
        /// </summary>
        public string Q { get; set; }

        /// <summary>
        /// Gets or sets the culture information. Leave it as <see langword="null"/> for current culture.
        /// </summary>
        public CultureInfo Culture { get; set; }

        /// <summary>
        /// Gets or sets the paging information. Leave it as <see langword="null"/> to ignore paging.
        /// </summary>
        public PagerInfo Paging { get; set; }

        /// <summary>
        /// Gets or sets the search query. Leave it as <see langword="null"/>and the query will be recreated from
        /// properties.
        /// </summary>
        public string QueryString { get; set; }

        /// <summary>
        /// Gets or sets the sorting information. Leave it as <see langword="null"/> to ignore sorting.
        /// </summary>
        public SortingInfo Sorting { get; set; }

        /// <summary>
        /// Gets or sets the query type, or how the individual properties will be joined to form the search query.
        /// Default type is AND/>.
        /// </summary>
        public LogicOperator LogicOperator { get; set; }

        /// <summary>
        /// Gets the search query from Users modifiable property Query, or build the query from query properties.
        /// </summary>
        /// <returns>Users defined query string, or the query built from query properties.</returns>
        public string GetOrCreateQuery()
        {
            string query = QueryString;
            if (string.IsNullOrEmpty(query)) query = BuildQuery();
            return query.ToLowerInvariant().Replace(" to ", " TO ").Replace(" and ", " AND ").Replace(" or ", " OR ");
        }

        /// <summary>
        /// Checks to see if there is any negate property in query.
        /// </summary>
        /// <returns>True if there are some negate properties, false otherwise.</returns>
        public bool HasNegateProperty()
        {
            return _properties.Any(x => x.Name.StartsWith("-") && x.Value != null);
        }

        /// <summary>
        /// Checks to see whether the query is empty.
        /// </summary>
        /// <returns>True if no query string or sorting and paging properties are set, or false if any of them is set.
        /// </returns>
        public bool IsEmpty()
        {
            return String.IsNullOrWhiteSpace(GetOrCreateQuery()) && Paging == null && Sorting == null;
        }

        /// <summary>
        ///   Adds or removes property from list of so-called "Q" properties. Q properties are included in search by simple q param.
        /// </summary>
        /// <param name="propertyName"> Name of the property. </param>
        /// <param name="isq"> Valude indicates if this property should be searched by Q parameter. </param>
        public QueryProperty SetQ(string propertyName, bool isq = true)
        {
            AssertPropertyExists(propertyName);
            var property = GetProperty(propertyName).SetLogicOperator(LogicOperator.Or).SetOperator(QueryOperator.StartsWith).SetList();
            property.IsQ = isq;
            return property;
        }

        /// <summary>
        /// Adds a new query property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Returns <see cref="QueryProperty"/></returns>
        protected QueryProperty AddProperty(string propertyName)
        {
            var property = new QueryProperty(propertyName);
            _properties.Add(property);
            return property;
        }

        /// <summary>
        /// Gets query property by its name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        protected QueryProperty GetProperty(string propertyName)
        {
            return _properties.SingleOrDefault(x => x.Name == propertyName);
        }

        /// <summary>
        /// Gets the "from" value of the given range property.
        /// </summary>
        /// <typeparam name="T">"From" property type.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>A "from" property value.</returns>
        public T GetRangeFromValue<T>(string propertyName)
        {
            var property = GetValue<RangeDefinition>(propertyName);
            return (T) (property.FromValue ?? GetDefaultValue<T>(propertyName));
        }

        /// <summary>
        /// Gets the "to" value of the given range property.
        /// </summary>
        /// <typeparam name="T">"To" property type.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>A "to" property value.</returns>
        public T GetRangeToValue<T>(string propertyName)
        {
            var property = GetValue<RangeDefinition>(propertyName);
            return (T) (property.ToValue ?? GetDefaultValue<T>(propertyName));
        }

        /// <summary>
        /// Gets the value of the given property.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>A property value.</returns>
        public T GetValue<T>(string propertyName)
        {
            AssertPropertyExists(propertyName);

            QueryProperty property = _properties.Single(x => x.Name == propertyName);
            return (T) property.Value;
        }

        /// <summary>
        /// Gets the value of the given property.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>A property value.</returns>
        public T GetDefaultValue<T>(string propertyName)
        {
            AssertPropertyExists(propertyName);

            QueryProperty property = _properties.Single(x => x.Name == propertyName);
            return (T) property.DefaultValue;
        }

        /// <summary>
        /// Sets the "from" value of the given range property.
        /// </summary>
        /// <typeparam name="T">"From" property type.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">"From" property value to set.</param>
        public void SetRangeFromValue<T>(string propertyName, T value)
        {
            var property = GetValue<RangeDefinition>(propertyName);
            if (value == null)
                property.FromValue = GetDefaultValue<RangeDefinition>(propertyName).FromValue;
            else
                property.FromValue = value;
        }

        /// <summary>
        /// Sets the "to" value of the given range property.
        /// </summary>
        /// <typeparam name="T">"To" property type.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">"To" property value to set.</param>
        public void SetRangeToValue<T>(string propertyName, T value)
        {
            var property = GetValue<RangeDefinition>(propertyName);
            if (value == null)
                property.ToValue = GetDefaultValue<RangeDefinition>(propertyName).ToValue;
            else
                property.ToValue = value;
        }

        /// <summary>
        /// Sets the value of the given property.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">Property value to set.</param>
        public void SetValue<T>(string propertyName, T value)
        {
            AssertPropertyExists(propertyName);

            //TODO: Hack for array with empty string. Better way would be to have it on ModelBinder.
            if(value is string[])
            {
                var arr = value as string[];
                if (arr.Length == 1 && (arr[0] == null || arr[0].Length == 0))
                    return;
            }

            QueryProperty property = _properties.Single(x => x.Name == propertyName);
            property.Value = value;
        }

        // ReSharper disable UnusedParameter.Local
        private void AssertPropertyExists(string propertyName)
        // ReSharper restore UnusedParameter.Local
        {
            if (_properties.All(x => x.Name != propertyName))
                throw new KeyNotFoundException(
                    "Property with the given name not found. " +
                    "You must declare all properties up front by calling AddProperty method.");
        }

        private string BuildQuery()
        {
            var result = new StringBuilder();
            var isGroupFirst = true;

            var queryProperties = _properties.Where(x => x.HasValue()).ToList();
            foreach (var group in queryProperties.GroupBy(x => x.Group))
            {
                var isFirst = isGroupFirst;
                if (group.Key != null)
                {
                    result.Append(String.Format("{0}(", isGroupFirst ? String.Empty : group.Key.GroupOperator.Name));
                    isFirst = true;
                }

                foreach (var property in group)
                {
                    if (!isFirst)
                        result.AppendFormat(property.LogicOperator.Name);

                    var builded = BuildProperty(property);

                    result.Append(builded);
                    isFirst = false;
                }

                isGroupFirst = false;
                if (group.Key != null)
                    result.Append(")");
            }

            var query = result.ToString();
            Trace.Write(query);
            return query;
        }

        private string BuildProperty(QueryProperty property)
        {
            var propertyName = GetPropertyName(property);
            var value = property.Value;
            var queryOperator = property.QueryOperator.Name;

            if (property.IsList)
            {
                var values = property.Value as string[];
                if (values == null)
                {
                    var s = (property.Value as string);
                    if (s != null)
                        values = s.Split(' ');
                }
                if (values == null || values.Length == 0)
                    return String.Empty;

                value = String.Join(property.ListOperator.Name, 
                    values.Select(x => String.Format(property.QueryOperator.Name, x)));
                queryOperator = "({0})";
            }

            if (property.QueryOperator == QueryOperator.Between)
                value = GetRangeValue((RangeDefinition)property.Value);

            return String.Format("{0}:{1}", propertyName, String.Format(queryOperator, value));
        }

        private string GetPropertyName(QueryProperty property)
        {
            string propertyName = property.Name.ToLowerInvariant();
            if (property.IsLocalized)
            {
                var cultureName = ConfigHelper.LocalizationSection.Cultures.DefaultCulture.ToString();
                if (cultureName == "sr")
                    cultureName = "sr-Cyrl-CS";
                if (propertyName.StartsWith("metadata"))
                    propertyName = propertyName.Replace("metadata", "metadata." + cultureName);
                else
                    propertyName += "." + cultureName;
            }
            return propertyName;
        }

        private static string GetRangeValue(RangeDefinition range)
        {
            if (range.FromValue is DateTime)
            {
                var from = (DateTime) range.FromValue;
                var to = (DateTime) range.ToValue;

                return String.Format("{0} TO {1}",
                                     (from == DateTime.MinValue ? from : from.AddDays(-1)).ToString("yyyyMMddHHmmssfff"),
                                     (to == DateTime.MaxValue ? to : to.AddDays(1)).ToString("yyyyMMddHHmmssfff"));
            }

            return String.Format("{0} TO {1}", range.FromValue, range.ToValue);
        }
    }
}