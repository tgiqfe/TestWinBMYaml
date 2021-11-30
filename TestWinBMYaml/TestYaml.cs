using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBM.Recipe;
using System.Text.RegularExpressions;

namespace TestWinBMYaml
{
    internal class TestYaml
    {
        bool Quiet = false;

        public void CheckFiles(List<string> fileList)
        {
            //  全Yamlのデシリアライズチェック
            var list = TestAllYaml(fileList);
            if (Quiet)
            {
                //WriteObject(list != null);
                Console.WriteLine("Result: {0}", list != null);
                return;
            }
            else if (list != null)
            {
                //WriteObject(list);
                Console.WriteLine("Result: True");
                return;
            }

            //  WinBMYamlインスタンスを取得
            List<WinBMYaml> winBMYamlList = SplitPage(fileList);
        }

        /// <summary>
        /// Recipe全体を一括デシリアライズ可否チェック
        /// </summary>
        /// <param name="fileList"></param>
        /// <returns></returns>
        private List<WinBM.Recipe.Page> TestAllYaml(List<string> fileList)
        {
            List<WinBM.Recipe.Page> list = new List<WinBM.Recipe.Page>();
            try
            {
                fileList.ForEach(x =>
                {
                    using (var sr = new StreamReader(x, Encoding.UTF8))
                    {
                        list.AddRange(WinBM.Recipe.Page.Deserialize(sr));
                    }
                });
                return list;
            }
            catch { }

            return null;
        }

        /// <summary>
        /// ファイルから「---」単位でのPageTextを取得
        /// </summary>
        /// <param name="fileList"></param>
        /// <returns></returns>
        public List<WinBMYaml> SplitPage(List<string> fileList)
        {
            Regex ymlDelimiter = new Regex(@"---\r?\n");
            Func<string, string[]> splitPageText = (string filePath) =>
            {
                using (var sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    return ymlDelimiter.Split(sr.ReadToEnd());
                }
            };

            var list = new List<WinBMYaml>();

            foreach (string filePath in fileList)
            {
                int index = 0;
                splitPageText(filePath).ToList().ForEach(x =>
                {
                    list.Add(new WinBMYaml(filePath, ++index, x));
                });
            }
            return list;
        }
    }
}
