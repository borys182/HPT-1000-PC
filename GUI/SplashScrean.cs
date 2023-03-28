using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using HPT1000.Source.Driver;
namespace HPT1000.GUI
{
    /*
     * Klasa splasha scream ktorego zadaniem jest uruchomienie watku w tle grafiki i wykonanie operacjie wymaganych przez apliakcje zanim zostanie uruchomiona aplikacja glowan
     * W naszym przypdku oczekujemy na nawiazanie komunikacji z baza danych oraz uruchomienie servera postgresa
     */ 
    public partial class SplashScrean : Form
    {
        private Thread      thread          = null;                 ///<Obiekt watku sluzacego do odczytu danych z PLC
        int                 maxTimeSplash   = 30;
        int                 loop            = 0;
        bool                running         = false;
        public bool Running
        {
            get { return running; }
        }
       //-------------------------------------------------------------------------------------------------------------
        public SplashScrean()
        {
            InitializeComponent();
        }
        //-------------------------------------------------------------------------------------------------------------
        //Metoda ma za zadanie uruchomienie watku splasha
        public void Start(ThreadStart funThread)
        {
            if (funThread != null)
            {
                thread = new Thread(funThread);
                thread.Start();
                running = true;
                timer.Enabled = true;
                ShowDialog();
            }
        }
        //Funkcja sprawdz czy watek zostasl zakonczony i zamyka okno splsaha
        //-------------------------------------------------------------------------------------------------------------
        private void timer_Tick(object sender, EventArgs e)
        {
            if (Visible)
            {
                if (thread != null && thread.ThreadState == ThreadState.Stopped)
                {
                    running = false;
                    Close();
                }

                if (timer.Interval != 0 && maxTimeSplash != 0 && (progressBar.Maximum / maxTimeSplash) != 0)
                {
                    if (loop > ((1000 / (progressBar.Maximum / maxTimeSplash)) / timer.Interval) && progressBar.Value < progressBar.Maximum)
                    {
                        progressBar.Value += 1;
                        loop = 0;
                    }
                }
                loop++;
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
}
