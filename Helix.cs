using System.Text;
using System.Text.Json;

public static class Helix<T> where T : new()
{
    private static T _data = default!;

    /// <summary>
    /// Gets the current instance of T
    /// </summary>
    /// <returns></returns>
    public static T Get()
    {
        return _data;
    }

    /// <summary>
    /// Registers a new instance of Helix T and load existing data into memory
    /// </summary>
    public static void Register()
    {
        var data = File.Exists(GetFileName()) ? File.ReadAllText(GetFileName(), Encoding.UTF8) : string.Empty;

        _data = (data == string.Empty ? new T() : JsonSerializer.Deserialize<T>(data))!;
    }

    /// <summary>
    /// Persists the current instance of T to disk, use environment variable HELIX to configure a custom path
    /// </summary>
    public static void Persist()
    {
        if (_data == null) return;

        File.WriteAllText(GetFileName(), JsonSerializer.Serialize(_data), Encoding.UTF8);
    }

    private static string GetFileName()
    {
        var name = typeof(T).Name;

        if (typeof(T).GenericTypeArguments.Any())
        {
            name = typeof(T).GenericTypeArguments.First().Name;
        }

        return Path.Combine(Environment.GetEnvironmentVariable("HELIX") ?? string.Empty, name.ToLowerInvariant().Trim() + ".helix");
    }
}
