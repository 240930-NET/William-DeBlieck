﻿// src/GreaterGrades/Repositories/ClassRepository.cs
using System;
using System.Collections.Generic;
using System.Linq;
using GreaterGrades.Models;
using GreaterGrades.Services;

namespace GreaterGrades.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly IStorageService _storageService;
        private readonly string _filePath = "data/classes.json";
        private List<Class> _classes;

        public ClassRepository(IStorageService storageService)
        {
            _storageService = storageService;
            _classes = _storageService.LoadData<Class>();
        }

        public IEnumerable<Class> GetAll()
        {
            return _classes;
        }

        public Class GetById(Guid id)
        {
            return _classes.FirstOrDefault(s => s.Id == id);
        }

        public void Add(Class newClass)
        {
            _classes.Add(newClass);
            _storageService.SaveData(_classes);
        }

        public void Update(Class existingClass)
        {
            var existing = GetById(existingClass.Id);
            if (existing != null)
            {
                existing.Subject = existingClass.Subject;
                existing.Assignments = existingClass.Assignments;
                existing.Students = existingClass.Students;
                // Update other properties as needed
                _storageService.SaveData(_classes);
            }
        }

        public void Delete(Guid id)
        {
            var existingClass = GetById(id);
            if (existingClass != null)
            {
                _classes.Remove(existingClass);
                _storageService.SaveData(_classes);
            }
        }
    }
}