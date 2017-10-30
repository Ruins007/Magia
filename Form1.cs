using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Process[] Proc_list = Process.GetProcesses();
            List<ProcessThreadCollection> PrArr = new List<ProcessThreadCollection>();
            ushort prok = 0, prer = 0;
            for (ushort n = 0; n < Proc_list.Length; n++)
            {
                try
                {
                    if (Proc_list[n].ProcessName != Program.ProcName)
                    {
                        PrArr.Add(Proc_list[n].Threads);
                        Proc_list[n].ProcessorAffinity = Program.Other;
                        prok += 1;
                    }
                }
                catch { prer += 1; }
            }

            ushort succes = 0, error = 0;

            for (ushort n = 0; n < Proc_list.Length; n++)
            {
                for(ushort n1 = 0; n1 < PrArr[n].Count; n1++)
                {
                    try
                    {
                        PrArr[n][n1].ProcessorAffinity = Program.Other;

                        succes += 1;
                    }
                    catch { error += 1; }
                }
            }
            Process ms;

            try
            {
                ms = Process.GetProcessesByName(Program.ProcName)[0];
                ms.ProcessorAffinity = Program.Me;
                ms.PriorityClass = ProcessPriorityClass.High;
                foreach( ProcessThread temps in ms.Threads )
                {
                    temps.ProcessorAffinity = Program.Me;
                }
            }
            catch { MessageBox.Show("Ошибка процесс с таким именем не найден"); }
            MessageBox.Show("Processes:\nSucces: " + prok.ToString() + "\nErrors: " + prer.ToString() + "\nThreads:\nSucces: " + succes.ToString() + "\nErrors: " + error.ToString());
            Environment.Exit(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
