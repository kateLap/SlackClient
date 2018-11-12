using System;
using System.Text;

namespace SlackClient.Models
{
        public static class SnakeCaseUtils
        {
            public static string ToSnakeCase(string name)
            {
                var sb = new StringBuilder(name);
                int state = 0;
                for (int i = 0; i < sb.Length; ++i)
                {
                    var c = sb[i];
                    switch (state)
                    {
                        case 0:
                            if (char.IsUpper(c))
                            {
                                sb[i] = char.ToLowerInvariant(c);
                            }
                            state = 1;
                            break;
                        case 1:
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
                            break;
                        case 2:
                            if (char.IsUpper(c))
                            {
                                sb.Insert(i, '_');
                                state = 0;
                            }
                            break;
                    }
                }
                return sb.ToString();
            }
        }
}
