// src/GreaterGrades/Services/IStorageService.cs
using System.Collections.Generic;

namespace GreaterGrades.Services
{
    /// <summary>
    /// Defines the contract for storage services that handle data persistence.
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Loads data from the storage medium and deserializes it into a list of type T.
        /// </summary>
        /// <typeparam name="T">The type of objects to deserialize.</typeparam>
        /// <returns>A list of deserialized objects of type T.</returns>
        List<T> LoadData<T>();

        /// <summary>
        /// Serializes a list of objects of type T and saves it to the storage medium.
        /// </summary>
        /// <typeparam name="T">The type of objects to serialize.</typeparam>
        /// <param name="data">The list of objects to serialize and save.</param>
        void SaveData<T>(List<T> data);
    }
}
