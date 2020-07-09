using System.Text;
using PropertyManager.Domain.Extensions;

namespace PropertyManager.Application.Common.Helpers
{
    public class NameHelper
    {
        public static string FormatFullName(
            string firstName,
            string lastName,
            string title = null,
            string middleNames = null)
        {
            var result = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(title))
            {
                result.Append($"{title.CapitalizeFirstLetter()}. ");
            }
            result.Append($"{firstName.CapitalizeFirstLetter()} ");
            if (!string.IsNullOrWhiteSpace(middleNames))
            {
                result.Append($"{middleNames.GetFirstLetter()} ");
            }
            result.Append($"{lastName.CapitalizeFirstLetter()}");

            return result.ToString();
        }
    }
}
