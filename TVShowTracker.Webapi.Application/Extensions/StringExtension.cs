using System.Text.RegularExpressions;

namespace TVShowTracker.Webapi.Application.Extensions
{
    public static class StringExtension
    {
        public static string RemoveDoubleSpace(this string input, bool isLower = false) 
        {
            string output = string.Empty;

            if (string.IsNullOrEmpty(input))
                return null;

            if (isLower)
                return Regex.Replace(input, @"\s+", " ").ToLower();

            return Regex.Replace(input, @"\s+", " ");
        }
    }
}
