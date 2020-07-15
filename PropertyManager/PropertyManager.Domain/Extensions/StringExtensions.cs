using System;

namespace PropertyManager.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string CapitalizeFirstLetter(this string data)
        {
            if(string.IsNullOrWhiteSpace(data))
            {
                return data;
            }
            // Split on string
            if(data.IndexOf(" ") != -1)
            {
                var result = string.Empty;
                var split = data.Split(" ");
                for (int i = 0; i < split.Length; i++)
                {
                    var name = char.ToUpper(split[i][0]) + split[i].Substring(1);
                    result += $"{name} ";
                }
                result = result.TrimEnd();
                return result;
            }
            return char.ToUpper(data[0]) + data.Substring(1);
        }

        public static string GetFirstLetter(this string data)
        {
            if(string.IsNullOrWhiteSpace(data))
            {
                return data;
            }

            if(data.IndexOf(" ") != -1)
            {
                var result = string.Empty;
                var split = data.Split(" ");
                for (int i = 0; i < split.Length; i++)
                {
                    result += $"{char.ToUpper(split[i][0])} ";
                }
                result = result.TrimEnd();
                return result;
            }
            return char.ToUpper(data[0]).ToString();
        }
    }
}
