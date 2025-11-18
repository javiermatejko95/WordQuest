using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveService : MonoBehaviour
{
    private static ISaveProvider provider;

    public static void Initialize(ISaveProvider saveProvider)
    {
        provider = saveProvider;
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
