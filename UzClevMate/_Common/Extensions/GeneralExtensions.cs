using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace UzClevMate._Common.Extensions
{
    public static class GeneralExtensions
    {
        public static string LimitStringLength(this string input, int maxLength = 50)
        {
            if (input.Length <= maxLength)
            {
                return input;
            }
            else
            {
                return input.Substring(0, maxLength);
            }
        }

        public static bool IsEqual(this double first, double second)
        {
            return Math.Abs(first - second) < 0.0000001;
        }

        public static string GetGuid(int length = 0)
        {
            var guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            var processedGuid = guid.Where(c => char.IsLetterOrDigit(c)).ToArray();

            if (length > 0)
            {
                processedGuid = processedGuid.Take(length).ToArray();
                return new string(processedGuid).ToUpper();
            }
            else
            {
                return new string(processedGuid);
            }
        }

        public static int GetRatio(this int part, int whole)
        {
            if (whole == 0)
            {
                return 0;
            }

            return part * 100 / whole;
        }

        public static double GetRatioDouble(this int part, int whole)
        {
            if (whole == 0)
            {
                return 0;
            }

            return part * 100 / whole;
        }

        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T GetRandomEnumValue<T>()
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("T must be an enum type");
            }

            Array values = Enum.GetValues(enumType);
            return (T)values.GetValue(new Random().Next(values.Length));
        }

        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
        {
            return source.Skip(Math.Max(0, source.Count() - N));
        }

        public static T GetRandomFromList<T>(this List<T> list)
        {
            var random = new Random();
            int index = random.Next(list.Count);

            return list[index];
        }

        public static T Clone<T>(this T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public static bool HasElements<T>(this IEnumerable<T> src)
        {
            return src != null &&
                   src.All(el => el != null) &&
                   src.Count() > 0;
        }

        /// <summary>
        /// checks if string has value
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool HasValue(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// tries to convert string to int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt32(this string str)
        {
            if (!str.HasValue())
            {
                return 0;
            }

            if (!Int32.TryParse(str, out int result))
            {
                return 0;
            }

            return result;
        }

        /// <summary>
        /// tries to convert string to int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long ToLong(this string str)
        {
            if (!str.HasValue())
            {
                return 0;
            }

            if (!long.TryParse(str, out long result))
            {
                return 0;
            }

            return result;
        }

        /// <summary>
        /// tries to convert string to bool
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ToBool(this string str)
        {
            if (!str.HasValue())
            {
                return false;
            }

            try
            {
                return Convert.ToBoolean(str);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// tries to convert string to double
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double ToDouble(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return 0;
            }

            str = str.Replace(",", ".");
            try
            {
                return Convert.ToDouble(str, new NumberFormatInfo() { NumberDecimalSeparator = "." });
            }
            catch { return 0; }
        }

        /// <summary>
        /// tries to convert string of specified format to datetime
        /// </summary>
        /// <param name="str"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str, string format = null)
        {
            if (!str.HasValue())
            {
                return default(DateTime);
            }

            try
            {
                if (format.HasValue())
                {
                    return DateTime.ParseExact(str, format, CultureInfo.InvariantCulture);
                }

                return Convert.ToDateTime(str);
            }
            catch
            {
                return default(DateTime);
            }
        }
    }
}