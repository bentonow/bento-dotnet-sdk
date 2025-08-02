namespace Bento.Extensions;

public static class ObjectExtensions 
{
    public static Dictionary<string, string?> ToDictionary(this object obj)
    {
        return obj.GetType()
            .GetProperties()
            .Where(p => p.GetValue(obj, null) != null)
            .ToDictionary(
                p => p.Name.ToLowerInvariant(),
                p => p.GetValue(obj, null)?.ToString()
            );
    }
}