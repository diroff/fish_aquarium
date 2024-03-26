using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public class JsonToFileStorageService : IStorageService
{
    public void Save(string key, object data, Action<bool> callback)
    {
        string path = BuildPath(key);
        string json = JsonConvert.SerializeObject(data);

        using (var fileStream = new StreamWriter(path))
        {
            fileStream.Write(json);
        }

        callback?.Invoke(true);
    }

    public void Load<T>(string key, Action<T> callback)
    {
        string path = BuildPath(key);

        if (!File.Exists(path))
        {
            callback.Invoke(default);
            return;
        }

        using (var fileStream = new StreamReader(path))
        {
            var json = fileStream.ReadToEnd();
            var data = JsonConvert.DeserializeObject<T>(json);

            callback.Invoke(data);
        }
    }

    public string BuildPath(string key)
    {
        return Path.Combine(Application.persistentDataPath, key);
    }
}