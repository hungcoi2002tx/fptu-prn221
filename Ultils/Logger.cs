﻿namespace Assignment.Ultils
{
    public class Logger
    {
        public void Log(string message)
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - {message}");
        }

        public void LogError(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - {errorMessage}");
            Console.ResetColor();
        }
    }
}
