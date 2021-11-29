using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWinBMYaml
{
    internal class WinBMYaml
    {
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
        public Dictionary<string ,string> ConfigParam { get; set; }

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
    }
}
