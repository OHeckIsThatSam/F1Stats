using Entities;
using System.Text;

namespace Menus;

public abstract class Menu
{
    protected Dictionary<int, string> options = new();
    protected string title = "";
    protected string prompt = "";

    private bool backFlag = false;

    public void Render()
    {
        while (!backFlag)
        {
            Console.Clear();
            Console.WriteLine(title);
            Console.WriteLine(Options());
            Console.Write(prompt);

            backFlag = ParseInput();
        }
    }

    private bool ParseInput()
    {
        while(true)
        {
            string userInput = Console.ReadLine();

            if (options.Count == 0)
            {
                OptionMenu mainMenu = new();
                return false;
            }
            
            if (userInput == "b") return true;

            if (Int32.TryParse(userInput, out int result))
            {
                if (options.ContainsKey(result))
                {
                    OptionMenu optionMenu = new(options[result]);
                    return false;
                }
            }
            Console.Write("That option is not regonised try agian: ");
        }
    }

    protected void AddOption(OptionEntity entity)
    {
        options.Add(options.Count + 1, entity.ToOption());
    }

    protected void AddOption(string option)
    {
        options.Add(options.Count + 1, option);
    }

    protected string Options()
    {
        if (options.Count == 0) return "";

        StringBuilder stringBuilder = new();

        foreach (var kv in options)
        {
            stringBuilder.Append(Convert.ToString(kv.Key));
            stringBuilder.Append(" : ");
            stringBuilder.AppendLine(kv.Value);
        }

        return stringBuilder.ToString();
    }
}