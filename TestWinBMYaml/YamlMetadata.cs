using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWinBMYaml
{
    internal class WinBmYamlMetadata
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Skip { get; set; }
        public bool? Step { get; set; }
        public int? Priority { get; set; }
        public List<string> IllegalList { get; set; }

        public WinBmYamlMetadata() { }

        public static WinBmYamlMetadata Create(string content)
        {
            Dictionary<string, string> paramset = null;

            using (var sr = new StringReader(content))
            {
                string readLine = "";
                while ((readLine = sr.ReadLine()) != null)
                {
                    if (readLine == "metadata:")
                    {
                        paramset = Functions.GetParameters(sr)[0];
                        break;
                    }
                }
            }

            var spec = new WinBmYamlMetadata();
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
                    case "step":
                        spec.Step = bool.TryParse(pair.Value, out bool step) ? step : null;
                        break;
                    case "priority":
                        spec.Priority = int.TryParse(pair.Value, out int priority) ? priority : null;
                        break;
                    default:
                        spec.IllegalList ??= new List<string>();
                        spec.IllegalList.Add(pair.Key + ": " + pair.Value);
                        break;
                }
            }

            return spec;
        }
    }
}
