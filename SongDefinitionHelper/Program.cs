namespace SongDefinitionHelper
{
    internal static class Program
    {
        internal static string songDefinitionsPath = null;
        internal static string dlcPath = null;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length >= 2)
            {
                songDefinitionsPath = args[0];
                dlcPath = args[1];
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}