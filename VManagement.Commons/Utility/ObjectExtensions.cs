using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VManagement.Commons.Utility
{
    public static class ObjectExtensions
    {
        public static long ToLong(this object? obj)
        {
            try
            {
                return Int64.Parse(obj?.ToString() ?? "0");
            }
            catch
            {
                return default;
            }
        }

        public static int ToInt(this object? obj)
        {
            try
            {
                return Int32.Parse(obj?.ToString() ?? "0");
            }
            catch
            {
                return default;
            }
        }

        public static float ToFloat(this object? obj)
        {
            try
            {
                return float.Parse(obj?.ToString() ?? "0");
            }
            catch
            {
                return default;
            }
        }

        public static double ToDouble(this object? obj)
        {
            try
            {
                return Double.Parse(obj?.ToString() ?? "0");
            }
            catch
            {
                return default;
            }
        }

        public static DateTime ToDateTime(this object? obj)
        {
            try
            {
                return DateTime.Parse(obj?.ToString() ?? "");
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static string SafeToString(this object? obj)
        {
            try
            {
                return obj?.ToString() ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static bool IsNull(this object? obj)
        {
            return obj == null;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool In(this object? obj, params object[] values)
        {
            foreach (var value in values)
            {
                if (obj == value)
                    return true;
            }

            return false;
        }

        public static bool IsValidId(this long id)
        {
            return id > 0;
        }

        public static string AsParameter(this string value) =>
            string.Format("@{0}", value);

        /// <summary>
        /// Encontra valores parametrizados em uma string.
        ///     Modelo de string:
        ///     key=value;key=value;
        /// </summary>
        public static string FindValue(this string source, string search)
        {
            var notFoundExpection = new ArgumentOutOfRangeException($"Não há valor parametriazdo para a chave {search}");

            string[] keyValues = source.Split(';');

            string targetKeyValue = keyValues.FirstOrDefault(kv => kv.Contains(search))
                                    ?? throw notFoundExpection;

            try
            {
                return targetKeyValue.Split('=')[1];
            }
            catch (IndexOutOfRangeException)
            {
                throw notFoundExpection;
            }
        }

        public static string JoinText(this ICollection<string> values)
        {
            if (values.Count == 1)
                return values.ElementAt(0);

            string join = string.Empty;

            for (int i = 0; i < values.Count; i++)
            {
                if (i == values.Count - 1)
                    join += " e ";
                else if (i != 0)
                    join += ", ";

                join += values.ElementAt(i);
            }

            return join;
        }

        public static string RemoveDiacritics(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC)
                .Replace(" ", "");
        }
    }
}
