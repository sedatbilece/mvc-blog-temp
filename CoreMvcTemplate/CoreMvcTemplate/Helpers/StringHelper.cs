using System.Text.RegularExpressions;

namespace CoreMvcTemplate.Helpers
{
    public static class StringHelper
    {
        public static string OnlyString(string input)
        {
            if(input==null)
                return string.Empty;

            // Replace HTML entities
            string result = input.Replace("&nbsp;", " ");

            // Remove HTML tags using regex
            result = Regex.Replace(result, "<.*?>", string.Empty);

            return result;
        }
    }
}
