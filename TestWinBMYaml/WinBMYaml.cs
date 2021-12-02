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

        public string Kind { get; set; }
        public YamlMetadata Metadata { get; set; }
        public YamlConfig[] Config { get; set; }
        public YamlOutput[] Output { get; set; }
        public YamlRequire[] Require { get; set; }
        public YamlRequire[] Work { get; set; }

        public WinBMYaml() { }
        public WinBMYaml(string filePath, int pageIndex, string content)
        {
            this.FilePath = filePath;
            this.PageIndex = pageIndex;
            if (content.Trim() != "")
            {
                this.Enabled = true;
                this.Content = TrimComment(content);
            }
        }

        /// <summary>
        /// コメント行、行途中のコメント文字以下、空行を削除
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private string TrimComment(string content)
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

            return sb.ToString();
        }

        #region Read from Content

        /// <summary>
        /// Kindの値を読み込み
        /// </summary>
        private void ReadKind()
        {
            using (var sr = new StringReader(Content))
            {
                string readLine = "";
                while ((readLine = sr.ReadLine()) != null)
                {
                    if (readLine.StartsWith("kind:"))
                    {
                        this.Kind = readLine.Substring(readLine.IndexOf(":") + 1).Trim();
                        break;
                    }
                }
            }
        }

        #endregion
    }
}

