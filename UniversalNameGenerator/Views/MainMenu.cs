using UniversalNameGenerator.Models;
using UniversalNameGenerator.Controllers;

namespace UniversalNameGenerator.Views
{
    /// <summary>
    /// Main menu.
    /// </summary>
    public class MainMenu : Menu
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UniversalNameGenerator.Views.LanguageMenu"/> class.
        /// </summary>
        public MainMenu()
        {
            Title = "Universal Name Generator";

            LanguageController languageController = new LanguageController();

            foreach(Language language in languageController.GetAll())
                AddCommand(language.Id, language.Name + " language", new LanguageMenu(language).Run);
        }
    }
}