using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WinBM.PowerShell.Lib.TestWinBMYaml
{
    internal class YamlKind
    {
        public string Kind { get; set; }

        public IllegalParamCollection Illegals { get; set; }

        public static YamlKind Create(string content)
        {
            var result = new YamlKind();

            YamlNodeCollection collection = null;
            using (var sr = new StringReader(content))
            {
                string readLine = "";
                int line = 0;
                while ((readLine = sr.ReadLine()) != null)
                {
                    line++;
                    if (readLine.StartsWith("kind:"))
                    {
                        collection.Add(
                            line,
                            LineType.Kind,
                            "kind",
                            readLine.Substring(readLine.IndexOf(":") + 1).Trim());
                        break;
                    }
                }

                result.SetKind(collection.First());
            }
            return result;
        }

        public void SetKind(YamlNode node)
        {
            this.Kind = node.Value;
        }
    }
}
