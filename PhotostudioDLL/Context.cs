namespace PhotostudioDLL;

public static class Context
{
    public static ApplicationContext db { get; private set; }

    internal static void AddDB(ApplicationContext dbb)
    {
        db = dbb;
    }
}