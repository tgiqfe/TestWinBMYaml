using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace TestWinBMYaml
{
    internal class WinBMYaml
    {
        public string FilePath { get; set; }
        public int PageIndex { get; set; }
        public string Content { get; set; }
        public bool Enabled { get; set; }

        public YamlKind Kind { get; set; }
        public YamlMetadata Metadata { get; set; }
        public List<YamlConfig> Config { get; set; }
        public List<YamlOutput> Output { get; set; }
        public List<YamlRequire> Require { get; set; }
        public List<YamlWork> Work { get; set; }

        public WinBMYaml() { }
        public WinBMYaml(string filePath, int pageIndex, string content)
        {
            this.FilePath = filePath;
            this.PageIndex = pageIndex;
            if (content.Trim() != "")
            {
                Regex comment_hash = new Regex(@"(?<=(('[^']*){2})*)\s*#.*$");
                var sb = new StringBuilder();
                using (var sr = new StringReader(content))
                {
                    string readLine = "";
                    while ((readLine = sr.ReadLine()) != null)
                    {
                        if (readLine.Contains("#"))
                        {
                            readLine = comment_hash.Replace(readLine, "");
                        }
                        if (readLine.Trim() == "")
                        {
                            continue;
                        }
                        sb.AppendLine(readLine);
                    }
                }
                this.Content = sb.ToString();

                this.Kind = YamlKind.Create(this.Content);
                this.Metadata = YamlMetadata.Create(this.Content);
                this.Config = YamlConfig.Create(this.Content);
                this.Output = YamlOutput.Create(this.Content);
                this.Require = YamlRequire.Create(this.Content);
                this.Work = YamlWork.Create(this.Content);

                this.Enabled = true;
            }
        }
    }
}

