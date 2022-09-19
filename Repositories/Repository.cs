using NovoProjeto.Interfaces;
using NovoProjeto.Models;
using System.Text.Json;
using System.Collections.Generic;
using System.Collections;
namespace NovoProjeto
{
    public class Repository : IRepository
    {
        private readonly string _databaseFile;

        public Repository()
        {
            _databaseFile = $"{Environment.CurrentDirectory}\\database.json";
        }
        public void Insert(List<Employee> entity)
        {
            var desserialized = JsonSerializer.Deserialize<List<Employee>>(File.ReadAllText(_databaseFile));
            if(desserialized.Count > 0)
            {
                desserialized.AddRange(entity);
            } else
            {
                desserialized = entity;
            }
            var serialized = JsonSerializer.Serialize(desserialized);
            File.WriteAllText(_databaseFile, serialized);
        }

        public List<Employee> Get(int page, int maxResults)
        {
            var desserialized = JsonSerializer.Deserialize<List<Employee>>(File.ReadAllText(_databaseFile));
            var response = desserialized.Skip((page - 1) * maxResults).Take(maxResults).ToList();
            return response;
        }

        public int GetLastId()
        {
            try
            {
                var desserialized = JsonSerializer.Deserialize<List<Employee>>(File.ReadAllText(_databaseFile));
                return desserialized.Last().Id;
            } catch(FileNotFoundException fe)
            {
                return 0;
            }
        }
    }
}
