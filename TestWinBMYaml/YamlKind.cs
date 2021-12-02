using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWinBMYaml
{
    internal class YamlKind
    {
        public string Kind { get; set; }

        public static YamlKind Create(string content)
        {
            var spec = new YamlKind();

            using (var sr = new StringReader(content))
            {
                string readLine = "";
                while ((readLine = sr.ReadLine()) != null)
                {
                    if (readLine.StartsWith("kind:"))
                    {
                        spec.Kind = readLine.Substring(readLine.IndexOf(":") + 1).Trim();
                        break;
                    }
                }
            }

            return spec;
        }

    }
}
