using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


public class GenerateController : Controller
{
    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="filesService"></param>
    public GenerateController(JsonFilesService filesService)
    {
        FilesService = filesService;
    }

    /// <summary>
    /// Поле класса.
    /// </summary>
    public JsonFilesService FilesService { get; }

    /// <summary>
    /// Страница с рандомной генерацией.
    /// </summary>
    /// <returns></returns>
    [HttpGet, Route("generate")]
    public IActionResult Index()
    {
        FilesService.Generate();
        int messages = FilesService.GetMessages().Count();
        int users = FilesService.GetUsers().Count();
        var array = new List<int>() { messages, users };
        return View("Index", array);
    }

    /// <summary>
    /// POST запрос рандомной генерации.
    /// </summary>
    /// <returns></returns>
    [HttpPost("post-generate")]
    public IActionResult PostGeneration()
    {
        FilesService.Generate();
        var array = new List<object>() { FilesService.GetUsers(), FilesService.GetMessages() };
        return Ok(array);
    }
}
