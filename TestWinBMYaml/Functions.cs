using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TestWinBMYaml
{
    internal class Functions
    {
        /// <summary>
        /// 行のインデントの深さを取得
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int GetIndentDepth(string text)
        {
            Regex indent_space = new Regex(@"(?<=^\s+)[^\s].*");

            string spaces = indent_space.Replace(text, "");
            return spaces.Length;
        }

        /// <summary>
        /// 子要素パラメータをリスト化して返す
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> GetParameters(StringReader sr)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

            string key = "";
            string readLine = "";
            int? indent = null;

            Dictionary<string, string> parameter = null;

            while ((readLine = sr.ReadLine()) != null)
            {
                if (readLine.Trim() == "")
                {
                    continue;
                }

                if (parameter == null || readLine.StartsWith("- "))
                {
                    readLine = Regex.Replace(readLine, @"(?<=\s*)- ", "  ");
                    parameter = new Dictionary<string, string>();
                    list.Add(parameter);
                }

                indent ??= GetIndentDepth(readLine);
                int nowIndent = GetIndentDepth(readLine);
                if (nowIndent < indent)
                {
                    break;
                }
                else if (nowIndent == indent)
                {
                    if (readLine.Contains(":"))
                    {
                        key = readLine.Substring(0, readLine.IndexOf(":")).Trim();
                        parameter[key] = readLine.Substring(readLine.IndexOf(":") + 1).Trim();
                    }
                    else
                    {
                        parameter[key] += (Environment.NewLine + readLine.Trim());
                    }
                }
                else if (nowIndent > indent)
                {
                    parameter[key] += (Environment.NewLine + readLine.Trim());
                }
            }

            return list;
        }


    }
}
