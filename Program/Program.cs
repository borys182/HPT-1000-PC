using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HPT1000.GUI;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices; //required for APIsusin
using Microsoft.VisualBasic;

namespace WindowsFormsApplication
{
    /* TO DO
     * 
     */
    static class Program
    {
        //Import the FindWindow API to find our window
        [DllImportAttribute("User32.dll")]
        private static extern int FindWindow(String ClassName, String WindowName);
        //Import the SetForeground API to activate it
        [DllImportAttribute("User32.dll")]
        private static extern IntPtr SetForegroundWindow(IntPtr hWnd);
        [DllImportAttribute("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Utwórz Mutex aby miec mozliwosc uruchomienia tylko jednej instancji aplikacji
            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, "HE-005", out createdNew))
            {
                //Udalo sie utworzyc wiec uruchom aplikacje
                if (createdNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());

                }
                else //Inna aplikajc jest juz uruchomiona
                {
                    MessageBox.Show("Program is already running. I can't run new instance.","Error");
                    //Sprobuj znalesc uruchomiona aplikacje i otworz jej okno
                    var prc = Process.GetProcessesByName("HE-005");
                    if (prc.Length > 0)
                    {
                        //Pokaz okno aplikacji klawiatury
                        ShowWindow(prc[0].MainWindowHandle.ToInt32(), (int)HPT1000.Source.SW.SW_SHOWMAXIMIZED);
                        SetForegroundWindow(prc[0].MainWindowHandle);
                    }                
                }
            }
        }
    }
}
