// src/GreaterGrades/Services/JsonStorageService.cs
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using GreaterGrades.Models;

namespace GreaterGrades.Services
{
    public class JsonStorageService : IStorageService
    {
        private readonly string _filePath;
        
        public JsonStorageService(string filePath)
        {

            _filePath = filePath;
            Console.WriteLine(_filePath);
        }

        public List<T> LoadData<T>()
        {
            if (!File.Exists(_filePath))
            {
                //Console.WriteLine("FILE NOT FOUND");
                //Console.WriteLine(_filePath);
                return new List<T>();
    
            }

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public void SaveData<T>(List<T> data)
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}
