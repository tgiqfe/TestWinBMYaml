﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace WinBM.PowerShell.Lib.TestWinBMYaml
{
    internal class YamlWork
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Skip { get; set; }
        public string Task { get; set; }
        public Dictionary<string, string> Param { get; set; }
        public string Failed { get; set; }
        public bool? Progress { get; set; }

        public IllegalParamCollection Illegals { get; set; }

        public static List<YamlWork> Create(string content)
        {
            var resultList = new List<YamlWork>();

            Func<string, string, LineType, List<YamlNodeCollection>> searchContent = (category, spec, type) =>
            {
                using (var asr = new AdvancedStringReader(content))
                {
                    string readLine = "";
                    bool inChild = false;
                    while ((readLine = asr.ReadLine()) != null)
                    {
                        if (readLine.Contains("#"))
                        {
                            readLine = YamlFunctions.RemoveComment(readLine);
                        }
                        if (readLine == category)
                        {
                            if (string.IsNullOrEmpty(spec))
                            {
                                return YamlFunctions.GetParameters(asr, type);
                            }
                            else
                            {
                                inChild = true;
                                continue;
                            }
                        }
                        if (inChild && readLine.Trim() == spec)
                        {
                            return YamlFunctions.GetParameters(asr, type);
                        }
                    }
                }
                return null;
            };

            foreach (var collection in searchContent("job:", "work:", LineType.JobWork))
            {
                var spec = new YamlWork();
                foreach (YamlNode node in collection)
                {
                    switch (node.Key)
                    {
                        case "name":
                            spec.SetName(node);
                            break;
                        case "description":
                            spec.SetDescription(node);
                            break;
                        case "skip":
                            spec.SetSkip(node);
                            break;
                        case "task":
                            spec.SetTask(node);
                            break;
                        case "param":
                            spec.SetParam(node);
                            break;
                        case "failed":
                            spec.SetFailed(node);
                            break;
                        case "progress":
                            spec.SetProgress(node);
                            break;
                        default:
                            spec.Illegals ??= new IllegalParamCollection();
                            spec.Illegals.AddIllegalKey(node);
                            break;
                    }
                }
                resultList.Add(spec);
            }
            return resultList;
        }

        public void SetName(YamlNode node)
        {
            this.Name = node.Value;
        }

        public void SetDescription(YamlNode node)
        {
            this.Description = node.Value;
        }

        public void SetSkip(YamlNode node)
        {
            if (bool.TryParse(node.Value, out bool skip))
            {
                this.Skip = skip;
            }
            else
            {
                this.Illegals ??= new IllegalParamCollection();
                Illegals.AddIllegalValue(node);
            }
        }

        public void SetTask(YamlNode node)
        {
            this.Task = node.Value;
        }

        public void SetParam(YamlNode node)
        {
            using (var asr = new AdvancedStringReader(node.Value))
            {
                this.Param = YamlFunctions.GetParameters(asr, LineType.JobWorkParam)[0].ToDictionary();
            }
        }

        public void SetFailed(YamlNode node)
        {
            this.Failed = node.Value;
        }

        public void SetProgress(YamlNode node)
        {
            if (bool.TryParse(node.Value, out bool progress))
            {
                this.Progress = progress;
            }
            else
            {
                this.Illegals ??= new IllegalParamCollection();
                Illegals.AddIllegalValue(node);
            }
        }
    }
}
