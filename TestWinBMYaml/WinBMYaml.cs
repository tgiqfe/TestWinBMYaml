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

        public string MetadataName { get; set; }
        public string MetadataDescription { get; set; }
        public bool? MetadataSkip { get; set; }
        public bool? MetadataStep { get; set; }
        public int? MetadataPriority { get; set; }

        public string ConfigName { get; set; }
        public string ConfigDescription { get; set; }
        public bool? ConfigSkip { get; set; }
        public string ConfigTask { get; set; }
        public Dictionary<string, string> ConfigParam { get; set; }

        public string OutputName { get; set; }
        public string OutputDescription { get; set; }
        public bool? OutputSkip { get; set; }
        public string OutputTask { get; set; }
        public Dictionary<string, string> OutputParam { get; set; }

        public string RequireName { get; set; }
        public string RequireDescription { get; set; }
        public bool? RequireSkip { get; set; }
        public string RequireTask { get; set; }
        public Dictionary<string, string> RequireParam { get; set; }
        public string RequireFailed { get; set; }

        public string WorkName { get; set; }
        public string WorkDescription { get; set; }
        public bool? WorkSkip { get; set; }
        public string WorkTask { get; set; }
        public Dictionary<string, string> WorkParam { get; set; }
        public string WorkFailed { get; set; }
        public bool? WorkProgress { get; set; }

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

        private int GetIndentDepth(string text)
        {
            Regex indent_space = new Regex(@"(?<=^\s+)[^\s].*");

            string spaces = indent_space.Replace(text, "");
            return spaces.Length;
        }


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
                        this.Kind = readLine.Substring(readLine.IndexOf("#") + 1).Trim();
                    }
                }
            }
        }

        /// <summary>
        /// Metadata部を読み込み
        /// </summary>
        private void ReadMetadata()
        {
            using (var sr = new StringReader(Content))
            {
                string readLine = "";
                while ((readLine = sr.ReadLine()) != null)
                {
                    if (readLine.StartsWith("metadata:"))
                    {
                        int indent = 0;
                        while ((readLine = sr.ReadLine()) != null)
                        {
                            int nowIndent = GetIndentDepth(readLine);
                            if (indent > nowIndent)
                            {
                                break;
                            }
                            indent = nowIndent;

                            string key = readLine.Substring(0, readLine.IndexOf(":")).Trim();
                            string val = readLine.Substring(readLine.IndexOf(":") + 1).Trim();
                            switch (key)
                            {
                                case "name":
                                    this.MetadataName = val;
                                    break;
                                case "description":
                                    this.MetadataDescription = val;
                                    break;
                                case "skip":
                                    this.MetadataSkip = bool.TryParse(val, out bool tempSkip) ? tempSkip : null;
                                    break;
                                case "step":
                                    this.MetadataStep = bool.TryParse(val, out bool tempStep) ? tempStep : null;
                                    break;
                                case "priority":
                                    this.MetadataPriority = int.TryParse(val, out int tempPriority) ? tempPriority : null;
                                    break;
                                default:
                                    //  ここで不正パラメータであることを表示する処理
                                    break;
                            }
                        }
                        break;
                    }
                }
            }
        }
    }
}
