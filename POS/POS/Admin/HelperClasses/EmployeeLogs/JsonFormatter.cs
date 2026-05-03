using System.Text;

namespace POS.Admin
{
    public static class JsonFormatter
    {
        public static string Format(string json)
        {
            if (string.IsNullOrEmpty(json)) return "(none)";

            try
            {
                var sb = new StringBuilder();
                int indent = 0;
                bool inString = false;

                foreach (char c in json)
                {
                    if (c == '"') inString = !inString;

                    if (!inString)
                    {
                        if (c == '{' || c == '[')
                        {
                            sb.Append(c).AppendLine().Append(new string(' ', ++indent * 2));
                            continue;
                        }
                        if (c == '}' || c == ']')
                        {
                            sb.AppendLine().Append(new string(' ', --indent * 2)).Append(c);
                            continue;
                        }
                        if (c == ',')
                        {
                            sb.Append(c).AppendLine().Append(new string(' ', indent * 2));
                            continue;
                        }
                        if (c == ':') { sb.Append(": "); continue; }
                    }
                    sb.Append(c);
                }
                return sb.ToString();
            }
            catch { return json; }
        }
    }
}