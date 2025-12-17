using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class JsonSaveProvider : ISaveProvider
{
    private readonly string savePath;

    public JsonSaveProvider(string folderName = "SaveData")
    {
        savePath = Path.Combine(Application.persistentDataPath, folderName);
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);
    }

    public void Save<T>(string key, T data)
    {
        string json = JsonConvert.SerializeObject(
        data,
        Formatting.Indented);

        File.WriteAllText(Path.Combine(savePath, key + ".json"), json);
    }

    public T Load<T>(string key)
    {
        string filePath = Path.Combine(savePath, key + ".json");

        if (!File.Exists(filePath))
            return default;

        string json = File.ReadAllText(filePath);

        return JsonConvert.DeserializeObject<T>(json);
    }

    public bool Exists(string key)
    {
        return File.Exists(Path.Combine(savePath, key + ".json"));
    }

    public void Delete(string key)
    {
        string filePath = Path.Combine(savePath, key + ".json");
        if (File.Exists(filePath))
            File.Delete(filePath);
    }
}