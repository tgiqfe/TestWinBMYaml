﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWinBMYaml
{
    internal class YamlRequire 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Skip { get; set; }
        public string Task { get; set; }
        public Dictionary<string, string> Param { get; set; }
        public string Failed { get; set; }
        public bool? Progress { get; set; }
        public List<string> IllegalList { get; set; }

        /// <summary>
        /// インスタンス作成用メソッド
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static List<YamlRequire> Create(string content)
        {
            List<Dictionary<string, string>> paramsetList = new List<Dictionary<string, string>>();

            using (var sr = new StringReader(content))
            {
                string readLine = "";
                bool inChild = false;
                while ((readLine = sr.ReadLine()) != null)
                {
                    if (readLine == "job:")
                    {
                        inChild = true;
                        continue;
                    }
                    if (inChild && readLine.Trim() == "require:")
                    {
                        paramsetList = Functions.GetParameters(sr);
                        break;
                    }
                }
            }

            List<YamlRequire> list = new List<YamlRequire>();
            foreach (Dictionary<string, string> paramset in paramsetList)
            {
                var spec = new YamlRequire();
                spec.IllegalList = new List<string>();
                foreach (KeyValuePair<string, string> pair in paramset)
                {
                    switch (pair.Key)
                    {
                        case "name":
                            spec.Name = pair.Value;
                            break;
                        case "description":
                            spec.Description = pair.Value;
                            break;
                        case "skip":
                            spec.Skip = bool.TryParse(pair.Value, out bool skip) ? skip : null;
                            break;
                        case "task":
                            spec.Task = pair.Value;
                            break;
                        case "param":
                            using (var sr = new StringReader(pair.Value))
                            {
                                spec.Param = Functions.GetParameters(sr)[0];
                            }
                            break;
                        case "failed":
                            spec.Failed = pair.Value;
                            break;
                        case "progress":
                            spec.Progress = bool.TryParse(pair.Value, out bool progress) ? progress : null;
                            break;
                        default:
                            spec.IllegalList.Add("[Illegal] " + pair.Key + ": " + pair.Value);
                            break;
                    }
                }
            }

            return list;
        }

        public string SearchIllegal()
        {
            return null;
        }
    }
}