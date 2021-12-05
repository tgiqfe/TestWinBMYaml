﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WinBM.PowerShell.Lib.TestWinBMYaml
{
    internal class YamlMetadata
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Skip { get; set; }
        public bool? Step { get; set; }
        public int? Priority { get; set; }

        public IllegalParamCollection Illegals { get; set; }

        public static YamlMetadata Create(string content)
        {
            var result = new YamlMetadata();

            YamlNodeCollection collection = null;
            using (var sr = new StringReader(content))
            {
                string readLine = "";
                int line = 0;
                while ((readLine = sr.ReadLine()) != null)
                {
                    line++;
                    if (readLine == "metadata:")
                    {
                        collection = YamlFunctions.GetParameters(sr, line, LineType.Metadata)[0];
                        break;
                    }
                }
            }

            foreach (YamlNode node in collection)
            {
                switch (node.Key)
                {
                    case "name":
                        result.SetName(node);
                        break;
                    case "description":
                        result.SetDescription(node);
                        break;
                    case "skip":
                        result.SetSkip(node);
                        break;
                    case "step":
                        result.SetStep(node);
                        break;
                    case "priority":
                        result.SetPriority(node);
                        break;
                    default:
                        spec.Illegals ??= new IllegalParamCollection();
                        result.Illegals.AddIllegalKey(node);
                        break;
                }
            }
            return result;
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

        public void SetStep(YamlNode node)
        {
            if (bool.TryParse(node.Value, out bool step))
            {
                this.Skip = step;
            }
            else
            {
                this.Illegals ??= new IllegalParamCollection();
                Illegals.AddIllegalValue(node);
            }
        }

        public void SetPriority(YamlNode node)
        {
            if (int.TryParse(node.Value, out int priority))
            {
                this.Priority = priority;
            }
            else
            {
                this.Illegals ??= new IllegalParamCollection();
                Illegals.AddIllegalValue(node);
            }
        }
    }
}
