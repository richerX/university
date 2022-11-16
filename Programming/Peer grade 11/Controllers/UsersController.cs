using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


public class UsersController : Controller
{
    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="filesService"></param>
    public UsersController(JsonFilesService filesService)
    {
        FilesService = filesService;
    }

    /// <summary>
    /// Поле класса.
    /// </summary>
    public JsonFilesService FilesService { get; }

    /// <summary>
    /// Страница с пользователями.
    /// </summary>
    /// <param name="userEmail"></param>
    /// <returns></returns>
    [HttpGet, Route("users")]
    public IActionResult Index(string userEmail)
    {
        var users = FilesService.GetUsers();
        if (!String.IsNullOrEmpty(userEmail))
            users = users.Where(x => x.Email.Contains(userEmail));
        if (users.Count() == 0)
            return View("404", "Пользователей не найдено");
        return View("Index", users);
    }

    /// <summary>
    /// Страница с пользователем.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet, Route("users/{id}")]
    public IActionResult Product(string id)
    {
        try
        {
            return View("ViewItem", FilesService.GetUsers().First(x => x.Id.ToString() == id));
        }
        catch
        {
            return View("404", "Пользователь не найден");
        }
    }

    /// <summary>
    /// GET запрос пользователей.
    /// </summary>
    /// <param name="searchString"></param>
    /// <returns></returns>
    [HttpGet("get-users")]
    public IActionResult GetIndex(string searchString)
    {
        var users = FilesService.GetUsers();
        if (!String.IsNullOrEmpty(searchString))
            users = users.Where(x => x.Email.Contains(searchString));
        if (users.Count() == 0)
            return NotFound(new { Message = $"Пользователей не найдено" });
        return Ok(users);
    }

    /// <summary>
    /// GET запрос пользователя.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-users/{id}")]
    public IActionResult GetProduct(string id)
    {
        try
        {
            return Ok(FilesService.GetUsers().First(x => x.Id.ToString() == id));
        }
        catch
        {
            return NotFound(new { Message = $"Пользователь не найден" });
        }
    }
}
