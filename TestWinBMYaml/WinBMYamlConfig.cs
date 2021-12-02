﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWinBMYaml
{
    internal class WinBMYamlConfig
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Skip { get; set; }
        public string Task { get; set; }
        public Dictionary<string, string> Param { get; set; }
        public List<string> IllegalList { get; set; }

        public WinBMYamlConfig() { }

        public static List<WinBMYamlConfig> Create(string content)
        {
            List<Dictionary<string, string>> specList = new List<Dictionary<string, string>>();

            using (var sr = new StringReader(content))
            {
                string readLine = "";
                bool inChild = false;
                while ((readLine = sr.ReadLine()) != null)
                {
                    if (readLine == "config:")
                    {
                        inChild = true;
                        continue;
                    }
                    if (inChild && readLine.Trim() == "spec:")
                    {
                        specList = Functions.GetParameters(sr);
                        break;
                    }
                }
            }

            List<WinBMYamlConfig> list = new List<WinBMYamlConfig>();
            foreach (Dictionary<string, string> paramset in specList)
            {
                var spec = new WinBMYamlConfig();
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
                            using(var sr = new StringReader(pair.Value))
                            {
                                spec.Param = Functions.GetParameters(sr)[0];
                            }
                            break;
                        default:
                            spec.IllegalList ??= new List<string>();
                            spec.IllegalList.Add(pair.Key + ": " + pair.Value);
                            break;
                    }
                }
            }

            return list;
        }
    }
}
