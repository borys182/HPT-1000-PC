using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HPT1000.GUI.Cotrols
{
    public partial class ButtonUpDown : UserControl
    {
        private double value_   = 0;
        private double min      = 0;
        private double max      = 100.0;
        private double step     = 1;

        bool btnUp_Press        = false;
        bool btnDown_Press      = false;
        bool tickChangeValue    = false;

        int timer               = 0;
        int timerChange         = 0;
        double intervalChange   = 10;

        public event EventHandler ValueChanged;

        //----------------------------------------------------------------------------------------------------------------------------------------
        public double Value
        {
            set { value_ = value; }
            get { return value_; }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        public double Minimum
        {
            set { min = value; }
            get { return min; }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        public double Maximum
        {
            set { max = value; }
            get { return max; }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        public double Step
        {
            set { step = value; }
            get { return step; }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        public int WidthComponent
        {
            set
            {   
           /*     btnUp.Width = value;
                btnDown.Width = value;
                Width = value;
           */ }
            get { return Width; }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        public int HeightComponent
        {
            set
            {
           /*     btnUp.Height = value;
                btnDown.Height = value;
                btnDown.Top = Height;
                Height = value;
          */   }
            get { return Height; }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        public ButtonUpDown()
        {
            InitializeComponent();
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        protected virtual void OnValueChanged(EventArgs e)
        {
            EventHandler handler = this.ValueChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        private void ValueInc()
        {
            if (value_ < max)
                value_ += step;

            if (value_ > max)
                value_ = max;
            EventArgs e = new EventArgs();
            OnValueChanged(e);
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        private void ValueDec()
        {
            if (value_ > min )
                value_ -= step;

            if (value_ < min)
                value_ = min;

            EventArgs e = new EventArgs();
            OnValueChanged(e);

        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        private void timer_Tick(object sender, EventArgs e)
        {
            ControlTick();

            if (btnUp_Press && tickChangeValue)
                ValueInc();

            if (btnDown_Press && tickChangeValue)
                ValueDec();

            tickChangeValue = false;

            //Ustaw rozmar przyciskow tworzacych komponent
            SetButtonSize();
         
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        private void ControlTick()
        {
            if(btnUp_Press || btnDown_Press)
            {
                if (timerChange > intervalChange)
                {
                    tickChangeValue = true;
                    timerChange = 0;
                }
            }

            if (timer > 50)
            {
                intervalChange /= 2;
                timer = 0;
            }
            timer++;
            timerChange++;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        private void btnUp_MouseDown(object sender, MouseEventArgs e)
        {
            intervalChange  = 10;
            timerChange     = 100;
            timer           = 0;
            btnUp_Press     = true;
            btnDown_Press   = false;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        private void btnUp_MouseLeave(object sender, EventArgs e)
        {
            btnUp_Press = false;
        }
        private void btnUp_MouseUp(object sender, MouseEventArgs e)
        {
            btnUp_Press = false;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        private void btnDown_MouseDown(object sender, MouseEventArgs e)
        {
            intervalChange  = 10;
            timerChange     = 100;
            timer           = 0;
            btnUp_Press     = false;
            btnDown_Press   = true;
         }
        //----------------------------------------------------------------------------------------------------------------------------------------
        private void btnDown_MouseLeave(object sender, EventArgs e)
        {
            btnDown_Press = false;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        private void btnDown_MouseUp(object sender, MouseEventArgs e)
        {
            btnDown_Press = false;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie dostosowanie rozmiarow przycisku UP/DOWN do rozmoaru komponentu
         */
        void SetButtonSize()
        {
     
        }
        //----------------------------------------------------------------------------------------------------------------------------------------
    }
}
