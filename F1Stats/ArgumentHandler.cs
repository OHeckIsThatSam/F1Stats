using Database;
using Menus;
using System.Text;

public class ArgumentHandler
{
    private List<string> arguments;
    private string version = "0.1.2";

    public ArgumentHandler(string[] args)
    {
        arguments = new List<string>(args);
        if (arguments.Count == 0) return;

        Console.WriteLine(GetTitleArt());
        Console.WriteLine("Loading...");
        QueryManager.Init();

        HandleArguments();
    }

    public void HandleArguments()
    {
        Console.Clear();

        if (arguments[0] == "-v")
        {
            Console.Write($"Version: {version}");
            Console.ReadKey(true);
        }
        else if (arguments[0] == "-h")
        {
            Console.WriteLine(HelpInformation());
            Console.ReadKey(true);
        }
        else if (arguments[0] == "r")
        {
            if (arguments.Count == 1) new Wizard("View all Race");
            else if (arguments[1] == "v") new Wizard("Find Race");
            else if (arguments[1] == "r") new Wizard("View Race Result");
            else
            {
                Console.Write("The query '");
                foreach (string item in arguments)
                    Higlight($"{item} ", ConsoleColor.Yellow);
                Console.Write("' was not recognised. See command line arguments usage below... ");
                Console.WriteLine(HelpInformation());
            }
        }
        else if (arguments[0] == "d")
        {
            if (arguments.Count == 1) new Wizard("View all Driver");
            else if (arguments[1] == "v") new Wizard("Find Driver");
            else
            {
                Console.Write("The query '");
                foreach (string item in arguments)
                    Higlight($"{item} ", ConsoleColor.Yellow);
                Console.Write("' was not recognised. See command line arguments usage below... ");
                Console.WriteLine(HelpInformation());
            }
        }
        else if (arguments[0] == "s") new Wizard("Standings");
        else
        {
            Console.Write("The Command '");
            Higlight(arguments[0], ConsoleColor.Yellow);
            Console.Write("' was not recognised. See command line arguments usage below... ");
            Console.WriteLine(HelpInformation());
        }

        Console.Clear();
        Environment.Exit(0);
    }

    private string HelpInformation()
    {
        StringBuilder stringBuilder = new();

        stringBuilder.AppendLine("Command line arguments;");
        stringBuilder.AppendLine("-v\tReturns the current version of the program");
        stringBuilder.AppendLine("-h\tReturns a list of commands and their functions");
        stringBuilder.AppendLine("Quick Query arguments;");
        stringBuilder.AppendLine("r [v/r]\tReturns the view[v] or the results[r] for a race");
        stringBuilder.AppendLine("\tLeave [] blank to return a veiw for all races");
        stringBuilder.AppendLine("d [v]\tReturns the view for a driver");
        stringBuilder.AppendLine("\tLeave [] blank to return a veiw for all drivers");
        stringBuilder.AppendLine("s\tReturns the current standings of the championship");

        return stringBuilder.ToString();
    }

    private void Higlight(string text, ConsoleColor colour)
    {
        ConsoleColor defualt = Console.ForegroundColor;
        Console.ForegroundColor = colour;
        Console.Write(text);
        Console.ForegroundColor = defualt;
    }

    private string GetTitleArt()
    {
        string titleArt;

        try
        {
            titleArt = File.ReadAllText("C:\\Users\\sam\\source\\repos\\F1Stats\\F1Stats\\TitleScreenArt.txt");
        }
        catch
        {
            titleArt = "Title art not found";
        }
        
        return titleArt;
    }
}