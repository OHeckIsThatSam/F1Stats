namespace Menus;

public class OptionMenu : Menu
{
    public OptionMenu()
    {
        AddOption("Drivers");
        AddOption("Races");

        title = "Main Menu";
        prompt = "<-- Input b to return\nInput the index of the choices above: ";

        Render();
    }

    public OptionMenu(string option)
    {
        if (option == "Drivers")
        {
            AddOption("View all Drivers");
            AddOption("Find Driver");
            AddOption("Add Driver");
            AddOption("Remove Driver");
            AddOption("Edit Driver");
            AddOption("Standings");

            title = "Driver Options";
            prompt = "<-- Input b to return\nInput the index of the choices above: ";

            Render();
        } else if (option == "Races")
        {
            AddOption("View all Races");
            AddOption("Find Race");
            AddOption("View Race Result");
            AddOption("Add Race");

            title = "Race Options";
            prompt = "<-- Input b to return\nInput the index of the choices above: ";

            Render();
        }
        else
        {
            new Wizard(option);
        }
    }
}
