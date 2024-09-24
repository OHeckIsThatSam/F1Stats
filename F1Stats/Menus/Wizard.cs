using Database;
using Entities;
using F1Stats.Visual;

namespace Menus;

public class Wizard : Menu
{
    private string header = "";
    private Table table;
    private Graph graph;

    public Wizard(string option)
    {
        title = option;
        
        if (option.Contains("View"))
        {
            prompt = "Press any key to return...";

            List<OptionEntity> entities = new();

            if (option.Contains("all Driver"))
            {
                entities = QueryManager.GetAllDrivers().ConvertAll(e => (OptionEntity)e);
                table = new Table("Number|Name|Team Name");
            }
            else if (option.Contains("all Race"))
            {
                entities = QueryManager.GetAllRaces().ConvertAll(e => (OptionEntity)e);
                table = new Table("Name|Weather|Date");
            }
            else if (option.Contains("Race Result"))
            {
                prompt = "Enter the name of the race: ";
                string raceName = RenderStep();

                Race race = QueryManager.FindRaceByName(raceName);
                if (race.Id != 0)
                {
                    entities = QueryManager.FrindDriverRaces(race.Id).ConvertAll(
                    e => (OptionEntity)e);
                    table = new Table("Position|Name|Points");

                    prompt = "Press any key to return...";
                }
                else prompt = "Race not found. Press any key to return...";
            }
            
            if (entities.Any())
            {
                foreach (OptionEntity entity in entities)
                    table.AddEntity(entity);
            }
        }
        else if (option.Contains("Find"))
        {
            OptionEntity entity;

            if (option.Contains("Driver"))
            {
                entity = QueryManager.FindDriverByDriverNumber(GetDriverNumFromUser());
                table = new Table("Number|Name|Team Name");
            }
            else if (option.Contains("Race"))
            {
                entity = QueryManager.FindRaceByName(GetRaceNameFromUser());
                table = new Table("Name|Weather|Date");
            }
            else return;

            if (entity == null) prompt = "No result found. Press any key to return...";
            else
            {
                table.AddEntity(entity);
                prompt = "Press any key to return...";
            }
        }
        else if (option.Contains("Remove"))
        {
            OptionEntity entity;

            if (option.Contains("Driver"))
            {
                entity = QueryManager.FindDriverByDriverNumber(GetDriverNumFromUser());
            } 
            else if (option.Contains("Race"))
            {
                entity = QueryManager.FindRaceByName(GetRaceNameFromUser());
            } 
            else return;

            if (entity != null) AddOption(entity);
            else return;

            prompt = "Are you sure you want delete this [y/n]: ";

            string userInput = RenderStep().ToLower();
            if (userInput == "y")
            {
                if (QueryManager.Remove(entity)) 
                    prompt = "The entity was succesfully removed. Press any key to return...";
                else prompt = "The entity was not removed. Press any key to return...";
            }
            else prompt = "The entity was not removed. Press any key to return...";
        }
        else if (option.Contains("Edit"))
        {
            OptionEntity entity;

            if (option.Contains("Driver"))
            {
                Driver driver = QueryManager.FindDriverByDriverNumber(GetDriverNumFromUser());

                header = "Current Driver's number: " + driver.DriverNumber;
                prompt = "Enter new Value or leave blank: ";


                if (int.TryParse(RenderStep(), out int number))
                {
                    driver.DriverNumber = number;
                }

                header = "Current Driver's first name: " + driver.FirstName;
                string userInput = RenderStep();
                if (userInput != "")
                {
                    driver.FirstName = userInput;
                }

                header = "Current Driver's last name: " + driver.LastName;
                userInput = RenderStep();
                if (userInput != "")
                {
                    driver.LastName = userInput;
                }

                header = "Current Driver's team name: " + driver.TeamName;
                userInput = RenderStep();
                if (userInput != "")
                {
                    driver.TeamName = userInput;
                }

                entity = (OptionEntity)driver;
            }
            else if (option.Contains("Race"))
            {
                Race race = QueryManager.FindRaceByName(GetRaceNameFromUser());

                header = "Current Race's name: " + race.Name;
                prompt = "Enter new Value or leave blank: ";

                string userInput = RenderStep();
                if (userInput != "")
                {
                    race.Name = userInput;
                }

                header = "Current Race's conditions: " + race.Weather;
                userInput = RenderStep();
                if (userInput != "")
                {
                    race.Weather = userInput;
                }

                header = "Current Race's Date: " + race.RaceDate.Date;
                userInput = RenderStep();
                if (userInput != "")
                {
                    if (DateTime.TryParse(userInput, out DateTime result))
                        race.RaceDate = result;
                }

                entity = (OptionEntity)race;
            }
            else return;

            header = "";
            prompt = "Are you sure you want edit this? [y/n]: ";

            if (RenderStep().ToLower() == "y")
            {
                if (QueryManager.Edit(entity)) 
                    prompt = "The Entity was succesfully edited. Press any key to return...";
                else prompt = "The Entity was not edited. Press any key to return...";
            }
            else base.prompt = "The Entity was not edited. Press any key to return...";
        }
        else if (option.Contains("Add"))
        {
            if (option.Contains("Driver"))
            {
                Driver driver = new();

                driver.DriverNumber = GetDriverNumFromUser();

                prompt = "Enter the driver's frist name: ";

                driver.FirstName = RenderStep();

                prompt = "Enter the driver's last name: ";

                driver.LastName = RenderStep();

                prompt = "Enter the driver's team name: ";

                driver.TeamName = RenderStep();

                if (QueryManager.AddDriver(driver)) prompt = "Driver added. Press any key to return...";
                else prompt = "Driver not added. Press any key to return.";
            }
            else if (option.Contains("Race"))
            {
                Race race = new();

                race.Name = GetRaceNameFromUser();

                prompt = "Enter the race conditions [Dry/Wet/Very Wet]: ";

                race.Weather = RenderStep();

                prompt = "Enter the date of the Race [dd/mm/yyy]: ";

                DateTime raceDate = DateTime.Parse(RenderStep());

                race.RaceDate = raceDate;

                if (QueryManager.AddRace(race))
                {
                    prompt = "Race added. Press any key to continue...";
                    RenderStep();

                    // Read in the results from file
                    prompt = "Enter the absolute path of the results file: ";
                    string filePath = RenderStep();

                    if (File.Exists(filePath))
                    {
                        string[] lines = File.ReadAllLines(filePath);

                        // Loop over evry row except the first
                        for (int i = 1; i < lines.Length; i++)
                        {
                            string[] results = lines[i].Split(',');

                            // If the postition is not a number set it to 0
                            if (!int.TryParse(results[0], out int position)) position = 0;

                            int driverNumber = Convert.ToInt32(results[1]);
                            int laps = Convert.ToInt32(results[2]);

                            bool DNF = false;
                            if (results[3] == "DNF") DNF = true;

                            int points = Convert.ToInt32(results[4]);

                            QueryManager.AddDriverRace(driverNumber, race.Name, position, points, laps, DNF);
                        }
                        prompt = "Race results added. Press any key to continue... ";
                    }
                    else
                    {
                        QueryManager.Remove(race);
                        prompt = "File not found. Press any key to return.";
                    }
                }
                else base.prompt = "Race not added. Press any key to return.";
            }
        }
        else if (option.Contains("Compare"))
        {
            if (option.Contains("Drivers"))
            {
                // Get driver 1
                Driver driver1 = new();
                Driver driver2 = new();

                while (driver1.Id == 0 && driver2.Id == 0) 
                {
                    driver1 = QueryManager.FindDriverByDriverNumber(
                    GetDriverNumFromUser());

                    driver2 = QueryManager.FindDriverByDriverNumber(
                    GetDriverNumFromUser());
                }

                graph = new(driver1.Id);



                // figure out the maximum of each axis and split into grid

                // Map each 
                prompt = "Press any key to return...";
            }
        }
        else if (option.Contains("Standings"))
        {
            prompt = "Press any key to return...";

            List<KeyValuePair<Driver, int>> drivers = new();

            foreach (Driver driver in QueryManager.GetAllDrivers())
                drivers.Add(new (driver, QueryManager.FindDriversPoints(driver.Id)));

            List<string> rows = new();

            drivers = drivers.OrderByDescending(kv => kv.Value).ToList();
            for (int i = 0; i < drivers.Count; i++)
                rows.Add($"{i + 1}|{drivers[i].Key.FullName}|{drivers[i].Value}");

            table = new Table("Position|Name|Points", rows);
        }

        Render();
    }

    private int GetDriverNumFromUser()
    {
        base.prompt = "Enter the Driver Number [1-99]: ";

        int driverNum;
        do int.TryParse(RenderStep(), out driverNum);
        while (!(1 <= driverNum && driverNum <= 99));

        return driverNum;
    }

    private string GetRaceNameFromUser()
    {
        base.prompt = "Enter the Race name: ";

        string raceName = RenderStep();
        if (raceName.Contains("Grand Prix"))
        {
            raceName = raceName.Replace("Grand Prix", "").TrimEnd();
        }

        return raceName;
    }

    new private void Render()
    {
        Console.Clear();
        Console.WriteLine(title);
        if (table != null) Console.WriteLine(table.ToString());
        else if (graph != null) Console.WriteLine(graph.ToString());
        else
        {
            if (header != "") Console.WriteLine(header);
            if (options.Count > 0) Console.WriteLine(Options());
        }
        Console.Write(prompt);

        Console.ReadKey(true);
    }

    private string RenderStep()
    {
        Console.Clear();
        Console.WriteLine(title);
        if (header != "") Console.WriteLine(header);
        if (options.Count > 0) Console.WriteLine(Options());
        Console.Write(prompt);

        return Console.ReadLine();
    }
}