using System;

namespace UniversalNameGenerator.Utils
{
    public static class ConsoleEx
    {
        public static void WriteColoured(string text, ConsoleColor foreColour)
        {
            ConsoleColor oldForeColour = Console.ForegroundColor;

            Console.ForegroundColor = foreColour;

            Console.Write(text);

            Console.ForegroundColor = oldForeColour;
        }

        public static void WriteColoured(string text, ConsoleColor foreColour, ConsoleColor backColour)
        {
            ConsoleColor oldForeColour = Console.ForegroundColor;
            ConsoleColor oldBackColour = Console.BackgroundColor;

            Console.ForegroundColor = foreColour;
            Console.BackgroundColor = backColour;

            Console.Write(text);

            Console.BackgroundColor = oldForeColour;
            Console.BackgroundColor = oldBackColour;
        }

        public static void WriteLineColoured(string text, ConsoleColor foreColour)
        {
            ConsoleColor oldForeColour = Console.ForegroundColor;

            Console.ForegroundColor = foreColour;

            Console.WriteLine(text);

            Console.ForegroundColor = oldForeColour;
        }

        public static void WriteLineColoured(string text, ConsoleColor foreColour, ConsoleColor backColour)
        {
            ConsoleColor oldForeColour = Console.ForegroundColor;
            ConsoleColor oldBackColour = Console.BackgroundColor;

            Console.ForegroundColor = foreColour;
            Console.BackgroundColor = backColour;

            Console.WriteLine(text);

            Console.BackgroundColor = oldForeColour;
            Console.BackgroundColor = oldBackColour;
        }
    }
}
