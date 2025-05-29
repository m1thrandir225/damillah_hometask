using System.Text.Json;
using damilah_hometask.domain;

namespace damilah_hometask.data.providers.impl;

public class JsonProvider : ISubjectProvider
{
    private readonly string _filePath;

    public JsonProvider(string filePath = "subjects.json")
    {
        _filePath = filePath;
    }

    public async Task<List<Subject>> GetSubjectsAsync()
    {
        if (!File.Exists(_filePath))
        {
            Console.WriteLine($"JSON file not found at path {_filePath}");
            return new List<Subject>();
        }

        try
        {
            var jsonString = await File.ReadAllTextAsync(_filePath);
            var subjects = JsonSerializer.Deserialize<List<Subject>>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return subjects ?? new List<Subject>();
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error reading JSON file: {ex.Message}. Returning empty list.");
            return new List<Subject>();
        }
    }
}