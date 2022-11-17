using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

public partial class Program
{
    public static bool ParseCommandsCount(string input, out int count)
    {
        if (!int.TryParse(input, out count) || count < 1)
            return false;
        return true;
    }

    public class Logger
    {
        List<string> logs = new List<string>();
        private static Logger logger = new Logger();

        public static Logger GetLogger() => logger;

        public static void HandleCommand(string command)
        {
            var logger = GetLogger();
            var commandArray = command.Split();
            if (commandArray[0] == "AddLog")
            {
                string line = commandArray[1];
                logger.logs.Add(line.Substring(1, line.Length - 2));
            }
            if (commandArray[0] == "DeleteLastLog")
            {
                if (logger.logs.Count == 0)
                    File.AppendAllText("logs.log", "No active logs\n");
                else
                    logger.logs.RemoveAt(logger.logs.Count - 1);
            }
            if (commandArray[0] == "WriteAllLogs")
            {
                if (logger.logs.Count == 0)
                    File.AppendAllText("logs.log", "No active logs\n");
                else
                {
                    File.AppendAllLines("logs.log", logger.logs);
                    logger.logs.Clear();
                }
            }
        }
    }

}