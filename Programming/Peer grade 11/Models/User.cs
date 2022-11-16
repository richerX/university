using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


public class User
{
    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    public User()
    {

    }

    /// <summary>
    /// Констурктор класса.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="email"></param>
    public User (int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    /// <summary>
    /// Свойства класса.
    /// </summary>
    public int Id { get; set; }

    [Required]
    [MinLength(2)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// Override метода ToString.
    /// </summary>
    /// <returns></returns>
    public override string ToString() => JsonSerializer.Serialize<User>(this);
}
