using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Driver : OptionEntity
{
    public new int Id { get; set; }
    public int DriverNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TeamName { get; set; }

    [Display(Name = "Name")]
    public string FullName { get => $"{FirstName} {LastName}"; }

    public List<DriverRaces>? DriverRaces { get; set; }

    public override string ToString()
    {
        return $"Id=[{Id}], {FullName}";
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return DriverNumber;
    }

    public override string ToOption()
    {     
        return DriverNumber + "|" + FullName + "|" + TeamName;
    }
}
