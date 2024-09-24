using Database;
using Entities;
using System.Text;

namespace F1Stats.Visual;

public class Graph
{
    private List<Dictionary<int, int>> dataPoints = new();
    private Dictionary<int, int> dataPoint = new();

    private int xAxis = 0;
    private int yAxis = 0;

    public Graph(int driverId)
    {
        this.dataPoint = QueryManager.FindDriversRacesAndPoints(driverId);
    }

    public Graph(int driverId1, int driverId2)
    {
        dataPoints.Add(QueryManager.FindDriversRacesAndPoints(driverId1));
        dataPoints.Add(QueryManager.FindDriversRacesAndPoints(driverId2));
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        List<Race> races = new();
        // Make this a query
        int totalPoints = 0;
        // Get the total points for the driver
        foreach (var kv in dataPoint)
        {
            totalPoints += kv.Value;
            races.Add(QueryManager.FindRaceById(kv.Key));
        }
        races.OrderByDescending(r => r.RaceDate);

        xAxis = dataPoint.Count;
        yAxis = totalPoints;

        char[,] coords = new char[xAxis, yAxis];

        int points = 0;
        for (int i = 0; i < races.Count; i++)
        {
            points += dataPoint[races[i].Id];
            coords[i, points - 1] = 'x';
        }

        // Draw the graph
        // Loop over all the x and y coords starting from the top left of the graph
        for (int y = yAxis - 1; y >= 0; y--)
        {
            for (int x = 0; x < xAxis; x++)
            {
                if (x == 0)
                {
                    sb.Append($"{totalPoints}-|");
                    totalPoints -= 1;
                    continue;
                }

                if (coords[x, y] == 0)
                    sb.Append(" ");
                else
                    sb.Append(coords[x, y]);
            }
            sb.AppendLine();
        }


        ////Scale the xAxis
        //int gridSize = totalPoints / yAxis;

        //int padSize = totalPoints.ToString().Length + 1;

        //// Draw the x axis
        //sb.AppendLine(Pad(padSize) + "^");
        //for (int i = 0; i < yAxis; i++)
        //{
        //    sb.Append($"{totalPoints}-|");

        //    int raceId = races[i].Id;
        //        sb.Append($"{Pad((xAxis - i) * 2)}x");

        //    totalPoints -= gridSize;
        //    sb.AppendLine();
        //}
        //sb.Append($"{Pad(padSize-2)}0-|");

        //// Draw the y axis
        //for (int i = 0; i < xAxis; i++)
        //    sb.Append("__");
        //sb.AppendLine(">");

        //sb.Append($"{Pad(padSize)}|");
        //for (int i = 0; i < xAxis; i++)
        //    sb.Append($"{i + 1}|");

        return sb.ToString();
    }

    private string Pad(int n)
    {
        StringBuilder sb = new();

        for (int i = 0; i < n; i++)
        {
            sb.Append(' ');
        }
        return sb.ToString();
    }
}
