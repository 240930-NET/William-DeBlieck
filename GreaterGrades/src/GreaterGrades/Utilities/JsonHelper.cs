// src/GreaterGrades/Utilities/JsonHelper.cs
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GreaterGrades.Utilities
{
    public static class JsonHelper
    {
        public static List<T> ReadFromJson<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public static void WriteToJson<T>(string filePath, List<T> data)
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
