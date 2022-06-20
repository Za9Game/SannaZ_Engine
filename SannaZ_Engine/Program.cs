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


#if DEBUG
            Game1 game = new Game1();
            Editor editor = new Editor(game);
            game = new Game1(editor);
            game.Run();
#else
            Game1onlyRender game = new Game1onlyRender();
            game.Run();
#endif


        }
    }
}
