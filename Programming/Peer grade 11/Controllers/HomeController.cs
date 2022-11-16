using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Peergrade.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

public class HomeController : Controller
{
    /// <summary>
    /// Поле класса.
    /// </summary>
    private readonly ILogger<HomeController> _logger;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="logger"></param>
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Основная страница сайта.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Страница со swagger.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Swagger()
    {
        return Redirect("/swagger");
    }

    /// <summary>
    /// Страница при ошибке.
    /// </summary>
    /// <returns></returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
