using System;
using System.Text;

namespace SlackClient.Models
{
    /// <summary>
    /// Converts string to a camel case representation
    /// </summary>
    public static class SnakeCaseUtils
        {
        public static string ToSnakeCase(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }

            var stringBuilder = new StringBuilder(name);

            var state = 0;

            for (var i = 0; i < stringBuilder.Length; ++i)
            {
                var c = stringBuilder[i];
                switch (state)
                {
                    case 0:
                        if (char.IsUpper(c) || char.IsSeparator(c))
                        {
                            stringBuilder[i] = char.ToLowerInvariant(c);
                        }
                        state = 1;
                        break;
                    case 1:
                        if (char.IsUpper(c) || char.IsSeparator(c))
                        {
                            stringBuilder.Insert(i, '_');
                            state = 0;
                        }
                        else if (char.IsDigit(c))
                        {
                            stringBuilder.Insert(i, '_');
                            state = 2;
                        }
                        break;
                    case 2:
                        if (char.IsUpper(c) || char.IsSeparator(c))
                        {
                            stringBuilder.Insert(i, '_');
                            state = 0;
                        }
                        break;
                }
            }

            stringBuilder.Replace(" ", string.Empty);

            return stringBuilder.ToString();
        }
    }
}
