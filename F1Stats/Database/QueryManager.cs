using Entities;

namespace Database;

public static class QueryManager
{
    private static DatabaseContext context;

    public static void Init()
    {
        context = new DatabaseContext();
        context.Database.EnsureCreated();
        context.Drivers.Where(d => d.DriverNumber == 0).ToList();
    }

    public static void Persist()
    {
        context.SaveChanges();
    }

    public static bool AddDriver(Driver driver)
    {
        var drivers = context.Drivers.AsParallel().Where(
            d => d.DriverNumber == driver.DriverNumber);
        
        if (drivers.Any()) return false;

        context.Drivers.Add(driver);
        Persist();

        return true;
    }

    public static bool Edit(OptionEntity entity)
    {
        if (entity == null) return false;

        context.Update(entity);

        Persist();
        return true;
    }

    public static bool Remove(OptionEntity entity)
    {
        if (entity == null) return false;

        context.Remove(entity);
        Persist();
        return true;
    }

    public static Driver FindDriverById(int id)
    {
        var drivers = context.Drivers.AsParallel().Where(d => d.Id == id);

        if (!drivers.Any()) return new Driver();

        return (Driver) drivers.First();
    }

    public static Driver FindDriverByDriverNumber(int driverNumber)
    {
        var drivers = context.Drivers.AsParallel().Where(d => d.DriverNumber == driverNumber);

        if (!drivers.Any()) return new Driver();

        return (Driver) drivers.First();
    }

    public static List<Driver> GetAllDrivers()
    {
        return context.Drivers.OrderBy(d => d.DriverNumber).ToList();
    }

    public static bool AddRace(Race race)
    {
        var races = context.Races.AsParallel().Where(
            r => r.Name == race.Name);

        if (races.Any()) return false;

        context.Races.Add(race);
        Persist();

        return true;
    }

    public static Race FindRaceById(int id)
    {
        var races = context.Races.AsParallel().Where(r => r.Id == id);

        if (!races.Any()) return new Race();

        return (Race)races.First();
    }

    public static Race FindRaceByName(string raceName)
    {
        var races = context.Races.AsParallel().Where(r => r.Name == raceName);

        if (!races.Any()) return new Race();

        return (Race)races.First();
    }

    public static List<Race> GetAllRaces()
    {
        return context.Races.ToList();
    }

    public static List<DriverRaces> FrindDriverRaces(int raceId)
    {
        return context.DriverRaces.AsParallel().Where(
            dr => dr.RaceId == raceId).OrderByDescending(dr => dr.Laps).
            ThenBy(dr => dr.Postion).ToList();
    }

    public static Dictionary<int, int> FindDriversRacesAndPoints(int driverId)
    {
        Dictionary<int, int> racesPoints = new();

        var results = context.DriverRaces.AsParallel().Where(
            dr => dr.DriverId == driverId).Select(dr => 
            new { dr.RaceId, dr.Points });

        foreach (var result in results)
            racesPoints.Add(result.RaceId, result.Points);

        return racesPoints;
    }

    public static int FindDriversPoints(int driverId)
    {
        var results = context.DriverRaces.AsParallel().Where(
            dr => dr.DriverId == driverId).Select(dr => dr.Points);

        if (!results.Any()) return 0;
        
        return results.Sum();
    }

    public static bool AddDriverRace(int driverNum, string raceName, int position, int points, int laps, bool DNF)
    {
        DriverRaces driverRaces = new DriverRaces();

        driverRaces.Driver = FindDriverByDriverNumber(driverNum);
        driverRaces.Race = FindRaceByName(raceName);
        driverRaces.Postion = position;
        driverRaces.Points = points;
        driverRaces.Laps = laps;
        driverRaces.DNF = DNF;

        context.DriverRaces.Add(driverRaces);
        Persist();
        return true;
    }
}
