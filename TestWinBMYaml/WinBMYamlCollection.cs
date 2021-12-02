﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace TestWinBMYaml
{
    internal class WinBMYamlCollection : List<WinBMYaml>
    {
        public WinBMYamlCollection(IEnumerable<string> fileList)
        {
            Regex ymlDelimiter = new Regex(@"---\r?\n");

            foreach (string filePath in fileList)
            {
                int index = 0;
                using (var sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    string[] pageText = ymlDelimiter.Split(sr.ReadToEnd());
                    pageText.ToList().ForEach(x =>
                        this.Add(new WinBMYaml(filePath, ++index, x)));
                }
            }
        }
    }
}
