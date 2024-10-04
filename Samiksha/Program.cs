using MyProjectTy125;
using System;
using System.Windows.Forms;

namespace Samiksha
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); // Ensure this is the correct form class
        }
    }
}
