using System;
using System.Collections.Generic;
using System.Linq;

using UniversalNameGenerator.Utils;

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

        /// <summary>
        /// Gets or sets the title colour.
        /// </summary>
        /// <value>The title colour.</value>
        public ConsoleColor TitleColour { get; set; } = ConsoleColor.Green;

        /// <summary>
        /// Gets or sets the title decoration colour.
        /// </summary>
        /// <value>The title decoration colour.</value>
        public ConsoleColor TitleDecorationColour { get; set; } = ConsoleColor.Yellow;

        /// <summary>
        /// Gets or sets the prompt colour.
        /// </summary>
        /// <value>The prompt colour.</value>
        public ConsoleColor PromptColour { get; set; } = ConsoleColor.White;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the title decoration on the left.
        /// </summary>
        /// <value>The title decoration on the left.</value>
        public string TitleDecorationLeft { get; set; } = "-==< ";

        /// <summary>
        /// Gets or sets the title decoration on the right.
        /// </summary>
        /// <value>The title decoration on the right.</value>
        public string TitleDecorationRight { get; set; } = " >==-";

        /// <summary>
        /// Gets or sets the prompt.
        /// </summary>
        /// <value>The prompt.</value>
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

        /// <summary>
        /// Prints the title.
        /// </summary>
        void PrintTitle()
        {
            ConsoleEx.WriteColoured(TitleDecorationLeft, TitleDecorationColour);
            ConsoleEx.WriteColoured(Title, TitleColour);
            ConsoleEx.WriteColoured(TitleDecorationRight, TitleDecorationColour);

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
            ConsoleEx.WriteColoured(Prompt, PromptColour);

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
