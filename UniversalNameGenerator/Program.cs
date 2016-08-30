using UniversalNameGenerator.Views;

namespace UniversalNameGenerator
{
    /// <summary>
    /// Main class.
    /// </summary>
    class MainClass
    {
        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Run();
        }
    }
}
