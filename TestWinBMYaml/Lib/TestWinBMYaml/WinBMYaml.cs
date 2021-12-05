using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using WinBM.Recipe;

namespace WinBM.PowerShell.Lib.TestWinBMYaml
{
    internal class WinBMYaml
    {
        public string FilePath { get; set; }
        public int PageIndex { get; set; }
        public string Content { get; set; }

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
            this.Content = content;
        }

        public bool TestDeserialize()
        {
            try
            {
                using (var sr = new StringReader(this.Content))
                {
                    _ = WinBM.Recipe.Page.Deserialize(sr);
                }
                return true;
            }
            catch { }
            return false;
        }

        public string TestParameter()
        {
            LoadFromContent();


            return "";
        }

        private void LoadFromContent()
        {
            if (Content.Trim() != "")
            {
                /*
                Regex comment_hash = new Regex(@"(?<=(('[^']*){2})*)\s*#.*$");
                var sb = new StringBuilder();
                using (var sr = new StringReader(Content))
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
                string content = sb.ToString();
                */
                string content = Content;

                this.Kind = YamlKind.Create(content);
                this.Metadata = YamlMetadata.Create(content);
                this.Config = YamlConfig.Create(content);
                this.Output = YamlOutput.Create(content);
                this.Require = YamlRequire.Create(content);
                this.Work = YamlWork.Create(content);
            }
        }

        private void LoadToYamlLine()
        {
            using (var sr = new StringReader(this.Content))
            {
                Regex comment_hash = new Regex(@"(?<=(('[^']*){2})*)\s*#.*$");

                string readLine = "";
                int line = 0;
                while ((readLine = sr.ReadLine()) != null)
                {
                    line++;

                }

            }
        }
    }
}

