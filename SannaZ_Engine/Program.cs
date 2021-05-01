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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Game1 game = new Game1();

#if DEBUG
            Editor editor = new Editor(game);
            game = new Game1(editor);
#endif
            game.Run();

        }
    }
}
