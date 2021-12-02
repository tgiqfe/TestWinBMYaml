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
            WinBMYamlCollection collection = new WinBMYamlCollection(fileList);
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
    }
}
