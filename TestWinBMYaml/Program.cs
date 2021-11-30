using WinBM.Recipe;

string targetPath = "Test.yml";

List<Page> list = Page.Deserialize(targetPath);


foreach (var page in list)
{
    Console.WriteLine(page.Serial);
}


Console.ReadLine();