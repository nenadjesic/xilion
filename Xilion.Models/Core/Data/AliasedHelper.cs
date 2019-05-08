using System;
using System.Text.RegularExpressions;

namespace Xilion.Models.Core.Data
{
    public class AliasedHelper
    {
        public string GenerateAlias(string input)
        {
            string startingValue = GenerateAliasString(input);
            if (String.IsNullOrEmpty(startingValue))
                return String.Empty;

            string alias = startingValue;
            int count = 1;

            while (IsAliasTaken(alias))
            {
                alias = String.Concat(startingValue, "-", count);
                count++;
            }

            return alias;
        }

        public virtual bool IsAliasTaken(string alias)
        {
            return String.IsNullOrWhiteSpace(alias);
            //||Query().Any(x => x.Alias == alias);
        }

        private static string GenerateAliasString(string input)
        {
            if (String.IsNullOrEmpty(input))
                return String.Empty;

            string result = input.ToLowerInvariant()
                .Replace('č', 'c')
                .Replace('ć', 'c')
                .Replace('š', 's')
                .Replace('ž', 'z')
                .Replace('đ', 'd');
            result = Regex.Replace(result, @"[^a-z0-9\-]+", "-");
            result = Regex.Replace(result, @"-{2,}", "-");
            result = Regex.Replace(result, @"-$", "");

            return result;
        }
    }
}