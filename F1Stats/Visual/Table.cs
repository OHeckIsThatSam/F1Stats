using Entities;
using System.Text;

namespace F1Stats.Visual;

public class Table
{
    private int[] maxColLength;

    private string[] header;

    private List<OptionEntity> options;

    private List<string> rows;

    public Table(string headerString)
    {
        header = headerString.Split('|');
        maxColLength = new int[header.Length];

        SetColumnLength();

        options = new List<OptionEntity>();
    }

    public Table(string headerString, List<string> rows)
    {
        header = headerString.Split('|');
        maxColLength = new int[header.Length];

        SetColumnLength();

        this.rows = rows;
        foreach (string row in rows)
            CheckMaxLength(row);
    }

    private void SetColumnLength()
    {
        for (int i = 0; i < maxColLength.Length; i++)
        {
            maxColLength[i] = header[i].Length;
        }
    }

    public void AddEntity(OptionEntity entity)
    {
        options.Add(entity);
        CheckMaxLength(entity.ToOption());
    }

    private void CheckMaxLength(string str)
    {
        string[] columns = str.Split('|');
        for (int i = 0; i < maxColLength.Length; i++)
        {
            if (columns[i].Length > maxColLength[i]) maxColLength[i] = columns[i].Length;
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        sb.Append(AddPadding(header));

        if (options != null)
        {
            foreach (OptionEntity option in options)
            {
                string[] columns = option.ToOption().Split('|');
                sb.Append(AddPadding(columns));
            }
        }
        else if (rows != null)
        {
            foreach (string row in rows)
            {
                string[] columns = row.Split('|');
                sb.Append(AddPadding(columns));
            }
                
        }

        return sb.ToString();
    }

    private string AddPadding(string[] strings)
    {
        StringBuilder sb = new();

        for (int i = 0; i < strings.Length; i++)
        {
            int difference = maxColLength[i] - strings[i].Length;

            int leftPad = difference / 2;
            int rightPad = difference - leftPad;

            sb.Append(Pad(leftPad));

            sb.Append(strings[i]);

            sb.Append(Pad(rightPad));

            if (i + 1 != strings.Length) sb.Append('|');
        }
        sb.AppendLine();
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