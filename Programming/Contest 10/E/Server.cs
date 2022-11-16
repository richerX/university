using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

public class Server
{
    public static Dictionary<string, DateTime> lastLogin = new Dictionary<string, DateTime>();
    public static Dictionary<string, DateTime> wrongPass = new Dictionary<string, DateTime>();
    public static Dictionary<string, DateTime> block = new Dictionary<string, DateTime>();

    public static void ProcessAuthorization(string requestsPath, string requestsResultsPath)
    {
        using (StreamWriter writer = new StreamWriter(File.Open(requestsResultsPath, FileMode.OpenOrCreate, FileAccess.Write), Encoding.ASCII))
        {
            using (StreamReader reader = new StreamReader(File.Open(requestsPath, FileMode.Open, FileAccess.Read), Encoding.ASCII))
            {
                string[] request;
                while (reader.Peek() != -1)
                {
                    request = reader.ReadLine().Split();

                    // SI <username> <password> <timestamp> Ц вход на сайт
                    if (request[0] == "SI")
                        processLogin(request, writer);

                    // SO <username> <timestamp> Ц выход с сайта
                    else if (request[0] == "SO")
                    {
                        if (block.ContainsKey(request[1]))
                            writer.Write($"{request[1]}> account blocked due suspicious login attempt" + Environment.NewLine);
                        else
                            writer.Write($"{request[1]}> sign out successful" + Environment.NewLine);
                        /*
                        if (block.ContainsKey(request[1]))
                            writer.Write($"{request[1]}> account blocked due suspicious login attempt" + Environment.NewLine);
                        else if (lastLogin.ContainsKey(request[1]))
                            writer.Write($"{request[1]}> sign out successful" + Environment.NewLine);
                        else if (!UserDb.Users.ContainsKey(request[1]))
                            writer.Write($"{request[1]}> NO SUCH USER IN DATABASE" + Environment.NewLine);
                        else
                            writer.Write($"{request[1]}> USER WASNT LOGGED IN" + Environment.NewLine);
                        */
                    }
                }
            }
        }
    }

    // SI <username> <password> <timestamp> Ц вход на сайт
    public static void processLogin(string[] request, StreamWriter writer)
    {
        string login = request[1];
        string password = request[2];
        DateTime time = DateTime.ParseExact(request[3] + " " + request[4], "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);

        // <login>> account blocked due suspicious login attempt Ц при блокировке аккаунта
        if (block.ContainsKey(login))
        {
            writer.Write($"{login}> account blocked due suspicious login attempt" + Environment.NewLine);
        }

        // <login>> account blocked due suspicious login attempt Ц при блокировке аккаунта
        else if (lastLogin.ContainsKey(login) && lastLogin[login].AddHours(24) > time)
        {
            writer.Write($"{login}> account blocked due suspicious login attempt" + Environment.NewLine);
            block[login] = time;
        }

        // <login>> no user with such login Ц при неудачной попытке входа из-за отсутстви€ логина в базе данных
        else if (!UserDb.Users.ContainsKey(login))
        {
            writer.Write($"{login}> no user with such login" + Environment.NewLine);
        }

        // <login>> incorrect password Ц при неудачной попытке входа из-за неверного парол€
        else if (UserDb.Users[login] != password)
        {
            if (wrongPass.ContainsKey(login) && wrongPass[login].AddHours(1) > time)
            {
                writer.Write($"{login}> account blocked due suspicious login attempt" + Environment.NewLine);
                block[login] = time;
            }
            else
            {
                writer.Write($"{login}> incorrect password" + Environment.NewLine);
                wrongPass[login] = time;
            }
        }

        // <login>> sign in successful Ц при успешной попытке входа
        else
        {
            writer.Write($"{login}> sign in successful" + Environment.NewLine);
            lastLogin[login] = time;
        }
    }
}