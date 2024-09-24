namespace Menus;

public class TitleMenu : Menu
{
    string path = "C:\\Users\\sam\\source\\repos\\F1Stats\\F1Stats\\TitleScreenArt.txt";

    public TitleMenu()
    {
        string titleArt;

        try
        {
            titleArt = File.ReadAllText(path);
        }
        catch
        {
            titleArt = "Title art not found";
        }

        title = titleArt;
        prompt = "Press enter to continue... ";

        Render();
    }
}
