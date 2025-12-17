public class SaveService : IInitializableService
{
    private static ISaveProvider provider;

    public void Initialize()
    {
        provider = new JsonSaveProvider();
    }

    public static void Save<T>(string key, T data)
    {
        provider.Save(key, data);
    }

    public static T Load<T>(string key)
    {
        return provider.Load<T>(key);
    }

    public static bool Exists(string key)
    {
        return provider.Exists(key);
    }

    public static void Delete(string key)
    {
        provider.Delete(key);
    }
}
