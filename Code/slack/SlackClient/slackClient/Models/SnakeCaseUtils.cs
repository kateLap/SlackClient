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
                var sb = new StringBuilder(name);
                var state = 0;
                for (var i = 0; i < sb.Length; ++i)
                {
                    var c = sb[i];
                    if (state == 0)
                    {
                        if (char.IsUpper(c))
                        {
                            sb[i] = char.ToLowerInvariant(c);
                        }

                        state = 1;
                    }
                    else if (state == 1)
                    {
                        if (char.IsUpper(c))
                        {
                            sb.Insert(i, '_');
                            state = 0;
                        }
                        else if (char.IsDigit(c))
                        {
                            sb.Insert(i, '_');
                            state = 2;
                        }
                    }
                    else if (state == 2)
                    {
                        if (!char.IsUpper(c)) continue;
                        sb.Insert(i, '_');
                        state = 0;
                    }
                }
                return sb.ToString();
            }
        }
}
