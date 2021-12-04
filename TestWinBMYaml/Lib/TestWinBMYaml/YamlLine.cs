using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBM.PowerShell.Lib.TestWinBMYaml
{
    internal class YamlLine
    {
        public int Line { get; set; }
        public LineType Type { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public YamlLine(int line, LineType type)
        {
            this.Line = line;
            this.Type = type;
        }
    }
}
