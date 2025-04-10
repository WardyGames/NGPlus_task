using System;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonDataService : IDataService
{
    /// <summary>
    /// .SaveData("/Player-stats.json", data)
    /// </summary>
    /// <param name="relativePath"></param>
    /// <param name="data"></param>
    /// <param name="encrypted"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public bool SaveData<T>(string relativePath, T data, bool encrypted = false)
    {
        string path = Application.persistentDataPath + relativePath;

            try
            {
                if (File.Exists(path))
                {
                    Debug.Log("Data exists. Deleting old one and replace with new data");
                    File.Delete(path);
                }
                else
                {
                    Debug.Log("Creating new data file");
                }

                
                using FileStream stream = File.Create(path);
                stream.Close();
                File.WriteAllText(path, JsonConvert.SerializeObject(data));
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Unable to save data: error msg: {e}");
                return false;
            }
    }

    public T LoadData<T>(string relativePath, bool encrypted)
    {
        string path = Application.persistentDataPath + relativePath;

            if (!File.Exists(path))
            {
                Debug.Log($"Cannot load file at {path}, fiule does not exist");
                throw new FileNotFoundException($"{path} does not exist");
            }
            
        try
        {
            //var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            //settings.Converters.Add(new InventoryItemConverter());

            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load data: {e.Message} {e.StackTrace}");
            throw e;
        }
    }
}

// public class InventoryItemConverter : JsonConverter
// {
//     public override bool CanConvert(Type objectType)
//     {
//         return objectType == typeof(InventoryItem);
//     }
//
//     public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//     {
//         return serializer.Deserialize(reader, typeof(InventoryItem));
//     }
//
//     public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//     {
//         serializer.Serialize(writer, value);
//     }
// }
