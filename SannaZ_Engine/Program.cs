using System;
using System.Windows.Forms;

namespace SannaZ_Engine
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
#if DEBUG
        [STAThread]
#endif
#if !DEBUG
        [MTAThread]  
#endif
        static void Main()
        {
#if DEBUG
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Editor());
#endif
#if !DEBUG
            using (Game1 game = new Game1())
                game.Run();
#endif

        }
    }
}
