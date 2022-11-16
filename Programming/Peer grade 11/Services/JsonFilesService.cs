using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Globalization;


public class JsonFilesService
{
    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="webHostEnvironment"></param>
    public JsonFilesService(IWebHostEnvironment webHostEnvironment)
    {
        WebHostEnvironment = webHostEnvironment;
    }

    /// <summary>
    /// Поля класса.
    /// </summary>
    public IWebHostEnvironment WebHostEnvironment { get; }

    private string JsonFileNameMessages => Path.Combine(WebHostEnvironment.WebRootPath, "data", "messages.json");
    private string JsonFileNameUsers => Path.Combine(WebHostEnvironment.WebRootPath, "data", "users.json");

    /// <summary>
    /// Получение сообщение из файла.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Message> GetMessages()
    {
        using (var jsonFileReader = File.OpenText(JsonFileNameMessages))
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<Message[]>(jsonFileReader.ReadToEnd(), options);
        }
    }

    /// <summary>
    /// Получение пользователей из файла.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<User> GetUsers()
    {
        using (var jsonFileReader = File.OpenText(JsonFileNameUsers))
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<User[]>(jsonFileReader.ReadToEnd(), options);
        }
    }

    /// <summary>
    /// Создание рандомных сообщений и пользователей.
    /// </summary>
    public void Generate()
    {
        var users = Generator.GenerateUsers();
        var messages = Generator.GenerateMessages(users);

        File.WriteAllText(JsonFileNameUsers, JsonSerializer.Serialize(users));
        File.WriteAllText(JsonFileNameMessages, JsonSerializer.Serialize(messages));
    }
}
