using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


public class MessagesController : Controller
{
    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="filesService"></param>
    public MessagesController(JsonFilesService filesService)
    {
        FilesService = filesService;
    }

    /// <summary>
    /// Поле класса.
    /// </summary>
    public JsonFilesService FilesService { get; }

    /// <summary>
    /// Страница с сообщениями.
    /// </summary>
    /// <param name="senderEmail"></param>
    /// <param name="receiverEmail"></param>
    /// <returns></returns>
    [HttpGet, Route("messages")]
    public IActionResult Index(string senderEmail, string receiverEmail)
    {
        var messages = FilesService.GetMessages();
        if (!String.IsNullOrEmpty(senderEmail))
            messages = messages.Where(x => x.SenderEmail.Contains(senderEmail));
        if (!String.IsNullOrEmpty(receiverEmail))
            messages = messages.Where(x => x.ReceiverEmail.Contains(receiverEmail));
        if (messages.Count() == 0)
            return View("404", "Сообщений не найдено");
        return View("Index", messages);
    }

    /// <summary>
    /// Страница с сообщением.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet, Route("messages/{id}")]
    public IActionResult Product(string id)
    {
        try
        {
            return View("ViewItem", FilesService.GetMessages().First(x => x.Id.ToString() == id));
        }
        catch
        {
            return View("404", "Сообщение не найдено");
        }
    }

    /// <summary>
    /// GET запрос сообщений.
    /// </summary>
    /// <param name="senderEmail"></param>
    /// <param name="receiverEmail"></param>
    /// <returns></returns>
    [HttpGet("get-messages")]
    public IActionResult GetIndex(string senderEmail, string receiverEmail)
    {
        var messages = FilesService.GetMessages();
        if (!String.IsNullOrEmpty(senderEmail))
            messages = messages.Where(x => x.SenderEmail.Contains(senderEmail));
        if (!String.IsNullOrEmpty(receiverEmail))
            messages = messages.Where(x => x.ReceiverEmail.Contains(receiverEmail));
        if (messages.Count() == 0)
            return NotFound(new { Message = $"Сообщений не найдено" });
        return Ok(messages);
    }

    /// <summary>
    /// GET запрос сообщения.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get-messages/{id}")]
    public IActionResult GetProduct(string id)
    {
        try
        {
            return Ok(FilesService.GetMessages().First(x => x.Id.ToString() == id));
        }
        catch
        {
            return NotFound(new { Message = $"Сообщения не найдено" });
        }
    }
}
