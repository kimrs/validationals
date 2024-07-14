namespace Validational;

public static class E
{
    public static Validational<List<T>> Elevate<T>(
        this List<Validational<T>> foo)
        => foo.SelectMany(x => x.Failures).ToList() is {Count: > 0} failures
            ? failures
            : foo.Select(x => x.Value!).ToList();
}