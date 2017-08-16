using System;
using System.Text;

using UniversalNameGenerator.Views;

namespace UniversalNameGenerator
{
    /// <summary>
    /// Main class.
    /// </summary>
    public static class MainClass
    {
        static string applicationDirectory;

        /// <summary>
        /// Gets the application directory.
        /// </summary>
        /// <value>The application directory.</value>
        public static string ApplicationDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(applicationDirectory))
                    applicationDirectory = AppDomain.CurrentDomain.BaseDirectory;

                return applicationDirectory;
            }
        }

        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            MainMenu mainMenu = new MainMenu();
            mainMenu.Run();
        }
    }
}
