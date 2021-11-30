using System.Text;
using System.Text.Json;

public static class Helix<T>
{
    private static List<T> _data = new();

    /// <summary>
    /// Gets the current instance of List T
    /// </summary>
    /// <returns></returns>
    public static List<T> Get()
    {
        return _data;
    }

    /// <summary>
    /// Registers a new instance of List T and load existing data into memory
    /// </summary>
    public static void Register()
    {
        var data = File.Exists(GetFileName()) ? File.ReadAllText(GetFileName(), Encoding.UTF8) : string.Empty;

        _data = ((data.Any() ? JsonSerializer.Deserialize<List<T>>(data) : new List<T>())!);
    }

    /// <summary>
    /// Persists the current instance of List T to disk, use environment variable HELIX to configure a custom path
    /// </summary>
    public static void Persist()
    {
        File.WriteAllText(GetFileName(), JsonSerializer.Serialize(_data), Encoding.UTF8);
    }

    private static string GetFileName() => Path.Combine(Environment.GetEnvironmentVariable("HELIX") ?? string.Empty, typeof(T).Name.ToLowerInvariant().Trim() + ".helix");

}
