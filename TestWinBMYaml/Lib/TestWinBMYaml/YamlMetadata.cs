using System;
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
        public List<string> IllegalList { get; set; }

        public IllegalParamCollection Illegals { get; set; }

        public static YamlMetadata Create2(string content)
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

            var spec = new YamlMetadata();
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
                    case "step":
                        spec.SetStep(node);
                        break;
                    case "priority":
                        spec.SetPriority(node);
                        break;
                    default:
                        spec.Illegals.AddIllegalKey(node);
                        break;
                }
            }
            return result;
        }


        /// <summary>
        /// インスタンス作成用メソッド
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static YamlMetadata Create(string content)
        {
            Dictionary<string, string> paramset = null;

            using (var sr = new StringReader(content))
            {
                string readLine = "";
                while ((readLine = sr.ReadLine()) != null)
                {
                    if (readLine == "metadata:")
                    {
                        paramset = YamlFunctions.GetParameters(sr)[0];
                        break;
                    }
                }
            }

            var spec = new YamlMetadata();
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
                        if (bool.TryParse(pair.Value, out bool skip))
                        {
                            spec.Skip = skip;
                        }
                        else
                        {
                            spec.IllegalList.Add(pair.Key + ": " + pair.Value);
                        }
                        break;
                    case "step":
                        if (bool.TryParse(pair.Value, out bool step))
                        {
                            spec.Skip = step;
                        }
                        else
                        {
                            spec.IllegalList.Add(pair.Key + ": " + pair.Value);
                        }
                        break;
                    case "priority":
                        if (int.TryParse(pair.Value, out int priority))
                        {
                            spec.Priority = priority;
                        }
                        else
                        {
                            spec.IllegalList.Add(pair.Key + ": " + pair.Value);
                        }
                        break;
                    default:
                        spec.IllegalList.Add("[Illegal] " + pair.Key + ": " + pair.Value);
                        break;
                }
            }

            return spec;
        }

        public string SearchIllegal()
        {
            if (IllegalList.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendLine();
                foreach (var illegal in IllegalList)
                {
                    sb.AppendLine($"      {illegal}");
                }
                return sb.ToString();
            }
            return null;
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
