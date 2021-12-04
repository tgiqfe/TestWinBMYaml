using WinBM.Recipe;
using WinBM.PowerShell.Lib.TestWinBMYaml;


string targetPath = "Test.yml";

var list = new List<string>();
list.Add(targetPath);

TestYaml test = new TestYaml();
test.CheckFiles(list);








Console.ReadLine();