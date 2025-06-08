using System.IO;
using System.Reflection;

namespace UniversalNameGenerator.DataAccess
{
    public static class ApplicationPaths
    {
        static string rootDirectory;

        /// <summary>
        /// The application directory.
        /// </summary>
        public static string ApplicationDirectory
        {
            get
            {
                rootDirectory ??= Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                return rootDirectory;
            }
        }

        public static string WordlistsDirectory => Path.Combine(ApplicationDirectory, "Wordlists");
    }
}
