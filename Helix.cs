using System.Text;
using System.Text.Json;

public static class Helix<T> where T : new()
{
    private static T _data = default!;

    public static T Get()
    {
        return _data;
    }

    public static void Register()
    {
        var data = File.Exists(GetFileName()) ? File.ReadAllText(GetFileName(), Encoding.UTF8) : string.Empty;

        _data = (data == string.Empty ? new T() : JsonSerializer.Deserialize<T>(data))!;
    }

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

        return name.ToLowerInvariant().Trim() + ".helix";
    }
}