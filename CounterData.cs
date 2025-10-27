using System.Text.Json;

namespace Counter
{
    public static class CounterData
    {
        static string filePath = Path.Combine(FileSystem.AppDataDirectory, "counters.json");

        public static async Task SaveAsync(List<Counter> counters)
        {
            var json = JsonSerializer.Serialize(counters);
            await File.WriteAllTextAsync(filePath, json);
        }

        public static async Task<List<Counter>> LoadAsync()
        {
            if (!File.Exists(filePath))
                return new List<Counter>();

            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<Counter>>(json);
        }
    }
}
