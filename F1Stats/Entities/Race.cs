using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Race : OptionEntity
{
    public new int Id { get; set; }
    public string Name { get; set; }
    public string Weather { get; set; }

    [DataType(DataType.Date)]
    public DateTime RaceDate { get; set; }

    public List<DriverRaces>? DriverRaces { get; set; }

    public override string ToOption()
    {
        return $"{Name} Grand Prix|{Weather}|{RaceDate.ToShortDateString()}";
    }
}
