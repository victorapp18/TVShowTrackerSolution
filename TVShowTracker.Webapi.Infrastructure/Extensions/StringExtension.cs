using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TVShowTracker.Webapi.Infrastructure.Extensions
{
    public static class StringExtension
    {
        public static string RemoveDoubleSpace(this string input, bool isLower = false)
        {
            string output = string.Empty;

            if (string.IsNullOrEmpty(input))
                return null;

            if (isLower)
                return Regex.Replace(input, @"/\s\s+/g", " ").ToLower();

            return Regex.Replace(input, @"/\s\s+/g", " ");
        }

        public static string MapValue(this string input) 
        {
            if (string.IsNullOrEmpty(input))
                return null;

            string result = string.Empty;
            Dictionary<string, List<string>> values = new Dictionary<string, List<string>>() 
            {
                { "All Sectors", new List<string>(){ "all sector", "all sectors" } },
                { "Portugal", new List<string>(){ "portfugal" } }
            };

            foreach (KeyValuePair<string, List<string>> value in values) 
            {
                if (value.Value.Contains(input.ToLower()))
                {
                    result = value.Key;
                    break;
                }
            }

            if (string.IsNullOrEmpty(result))
                result = input;

            return result;
        }
    }
}
