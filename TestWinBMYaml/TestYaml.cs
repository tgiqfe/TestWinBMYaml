using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBM.Recipe;
using System.Text.RegularExpressions;

namespace WinBM.PowerShell.Lib.TestWinBMYaml
{
    internal class TestYaml
    {
        bool Quiet = false;

        public void CheckFiles(List<string> fileList)
        {
            //  全Yamlのデシリアライズチェック
            List<WinBM.Recipe.Page> pageList = new List<WinBM.Recipe.Page>();
            bool isSuccess = false;
            try
            {
                fileList.ForEach(x =>
                {
                    using (var sr = new StreamReader(x, Encoding.UTF8))
                    {
                        pageList.AddRange(WinBM.Recipe.Page.Deserialize(sr));
                    }
                });
                isSuccess = true;
            }
            catch { }

            if (Quiet)
            {
                //WriteObject(isSuccess);
                Console.WriteLine("Result: {0}", isSuccess);
                return;
            }
            else if (isSuccess)
            {
                //WriteObject(pageList);
                Console.WriteLine("Result: True");
                return;
            }

            //  WinBMYamlインスタンスを取得
            WinBMYamlCollection collection = new WinBMYamlCollection(fileList);

            string viewFilePath = "";
            foreach (var winBMYaml in collection)
            {
                if (viewFilePath != winBMYaml.FilePath)
                {
                    viewFilePath = winBMYaml.FilePath;
                    Console.WriteLine(viewFilePath);
                }
                bool ret = winBMYaml.TestDeserialize();

                if (ret)
                {
                    Console.Write($"  Page {winBMYaml.PageIndex} : [");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("Success");
                    Console.ResetColor();
                    Console.WriteLine("]");
                }
                else
                {
                    Console.Write($"  Page {winBMYaml.PageIndex} : [");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("Failed");
                    Console.ResetColor();
                    Console.WriteLine("]");

                    winBMYaml.TestParameter();
                }
            }
        }
    }
}
