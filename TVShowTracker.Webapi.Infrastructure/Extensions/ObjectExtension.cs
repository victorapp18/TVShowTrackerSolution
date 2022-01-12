using System;
using System.Text.RegularExpressions;

namespace TVShowTracker.Webapi.Infrastructure.Extensions
{
    public static class ObjectExtension
    {
        public static double? AsDouble(this object input)
        {
            double result = 0;
            string _input = input.ToString();

            if (string.IsNullOrEmpty(_input))
                return null;

            if (!double.TryParse(_input, out result))
                return null;

            return result;
        }

        public static double? TryGetDoubleValue(this object input)
        {
            double result = 0;
            double? _result = null;

            try
            {
                string _input = input.ToString();

                if (double.TryParse(_input, out result))
                    _result = result.AsDouble();
            }
            catch
            { }

            return _result;
        }

        public static string AsString(this object input)
        {
            string output = string.Empty;
            string _input = input?.ToString();

            if (string.IsNullOrEmpty(_input))
                return null;

            if (_input == "Portfugal")
                _input = "Portugal";

            return Regex.Replace(_input.Trim(), @"/\s\s+/g", " ");
        }

        public static DateTime AsDateTime(this object input, int row = 0) 
        {
            DateTime output = DateTime.MinValue;
            string _input = input?.ToString();

            if (string.IsNullOrEmpty(_input) || !DateTime.TryParse(_input, out output))
                throw new Exception("Could not conver to DateTime.");

            return output;
        }
    }
}
