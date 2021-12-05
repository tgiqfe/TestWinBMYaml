using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBM.PowerShell.Lib.TestWinBMYaml
{
    internal class AdvancedStringReader : StringReader
    {
        public int Line = 0;

        public AdvancedStringReader(string s) : base(s) { }

        public override string ReadLine()
        {
            this.Line++;
            return base.ReadLine();
        }
    }
}
