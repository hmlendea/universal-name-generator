using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversalNameGenerator.Views
{
    /// <summary>
    /// Command-line Menu.
    /// </summary>
    public class Menu
    {
        string cmd;
        bool isRunning;

        readonly Dictionary<string, string> commandTexts;
        readonly Dictionary<string, Action> commandActions;

        public ConsoleColor TitleColor { get; set; } = ConsoleColor.Green;
        public ConsoleColor TitleDecorationColor { get; set; } = ConsoleColor.Yellow;
        public ConsoleColor PromptColor { get; set; } = ConsoleColor.White;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        public string TitleDecorationLeft { get; set; } = "-==< ";
        public string TitleDecorationRight { get; set; } = " >==-";

        public string Prompt { get; set; } = "> ";

        /// <summary>
        /// Initializes a new instance of the <see cref="UniversalNameGenerator.Views.Menu"/> class.
        /// </summary>
        public Menu()
        {
            commandTexts = new Dictionary<string, string>();
            commandActions = new Dictionary<string, Action>();

            AddCommand("exit", "Exit this menu", Exit);
            AddCommand("print", "Print the command list", PrintCommandList);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniversalNameGenerator.Views.Menu"/> class.
        /// </summary>
        /// <param name="title">Title.</param>
        public Menu(string title)
            : this()
        {
            Title = title;
        }

        /// <summary>
        /// Input the specified prompt.
        /// </summary>
        /// <param name="prompt">Prompt.</param>
        public virtual string Input(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        /// <summary>
        /// Inputs the permission.
        /// </summary>
        /// <returns><c>true</c>, if permission was input, <c>false</c> otherwise.</returns>
        /// <param name="prompt">Prompt.</param>
        public bool InputPermission(string prompt)
        {
            Console.Write(prompt);
            Console.Write(" (y/N) ");

            while (true)
            {
                ConsoleKeyInfo c = Console.ReadKey();

                switch (c.Key)
                {
                    case ConsoleKey.Y:
                        Console.WriteLine();
                        return true;

                    case ConsoleKey.N:
                    case ConsoleKey.Enter:
                        Console.WriteLine();
                        return false;

                    default:
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        break;
                }
            }
        }

        /// <summary>
        /// Adds the command.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <param name="text">Text.</param>
        /// <param name="action">Action.</param>
        public void AddCommand(string command, string text, Action action)
        {
            commandTexts.Add(command, text);
            commandActions.Add(command, action);
        }

        /// <summary>
        /// Runs this menu.
        /// </summary>
        public void Run()
        {
            isRunning = true;

            PrintTitle();
            PrintCommandList();

            while (isRunning)
            {
                Console.WriteLine();

                GetCommand();
                HandleCommand();
            }
        }

        void PrintTitle()
        {
            Console.ForegroundColor = TitleDecorationColor;
            Console.Write(TitleDecorationLeft);

            Console.ForegroundColor = TitleColor;
            Console.Write(Title);

            Console.ForegroundColor = TitleDecorationColor;
            Console.Write(TitleDecorationRight);

            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Prints the command list.
        /// </summary>
        void PrintCommandList()
        {
            int commandColumnWidth = commandTexts.Keys.Aggregate
                ("", (max, cur) => max.Length > cur.Length ? max : cur).Length + 4;

            foreach (KeyValuePair<string, string> entry in commandTexts)
                Console.WriteLine("{0} {1}", entry.Key.PadRight(commandColumnWidth), entry.Value);
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <returns>The command.</returns>
        string GetCommand()
        {
            Console.ForegroundColor = PromptColor;
            Console.Write(Prompt);
            Console.ResetColor();

            cmd = Console.ReadLine();
            return cmd;
        }

        /// <summary>
        /// Handles the command.
        /// </summary>
        void HandleCommand()
        {
            foreach (string command in commandActions.Keys)
                if (cmd == command)
                {
                    commandActions[cmd]();

                    return;
                }

            Console.WriteLine("Invalid command");
        }

        /// <summary>
        /// Exit this menu.
        /// </summary>
        void Exit()
        {
            isRunning = false;
        }
    }
}
