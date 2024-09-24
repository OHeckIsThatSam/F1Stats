using Database;

namespace Entities;

public class DriverRaces : OptionEntity
{
    public int DriverId { get; set; }
    public Driver Driver { get; set; }

    public int RaceId { get; set; }
    public Race Race { get; set; }

    public int Postion { get; set; }
    public int Points { get; set; }
    public int Laps { get; set; }
    public bool DNF { get; set; }

    public override string ToOption()
    {
        return $"{Postion}|{QueryManager.FindDriverById(DriverId).FullName}|{Points}";
    }
}
