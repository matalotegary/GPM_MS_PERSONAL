namespace application.Common.Helpers
{
    public class PersonalInfoHelper
    {
        public static string CapitalizeFirstLetter(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }

            // Capitalize the first letter and make the rest lowercase
            return char.ToUpper(name[0]) + name.Substring(1).ToLower();
        }
        public static string CapitalizeEachWord(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }

            // Split the name into words, capitalize each word, and join them back together
            var words = name.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(words[i]))
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                }
            }

            return string.Join(' ', words);
        }
    }
}
