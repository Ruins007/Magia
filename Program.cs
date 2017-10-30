using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                if (args[0] != "") { ProcName = args[0]; }
            }
            catch { ProcName = "xrEngine"; }
            try
            {
                 if (args[1] != "") { Me = ToBinary(args[1]); }
            }
            catch { Me = ToBinary("0011"); }
            try
            {
                if (args[2] != "") { Other = ToBinary(args[2]); }
            }
            catch
            {
                try
                {
                    Other = ToBinary(args[1].Replace('0', '3').Replace('1', '4').Replace('4', '0').Replace('3', '1'));
                }
                catch
                {
                    Other = ToBinary("1100");
                }
            }
            Application.Run(new Form1());
        }
        public static string ProcName = "xrEngine";
        public static IntPtr Me, Other;

        public static IntPtr ToBinary(string inp)
        {
            return (IntPtr)Convert.ToInt32(inp, 2);
        }
    }
}