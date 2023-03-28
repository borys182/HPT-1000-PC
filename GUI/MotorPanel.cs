using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HPT1000.Source.Chamber;
using HPT1000.Source.Driver;
using HPT1000.Source;


namespace HPT1000.GUI
{
    public partial class MotorPanel : UserControl
    {
        private MotorDriver motor = null;               ///<Referencja obiektu pompy

        Bitmap pictureBtnStart  = new Bitmap(Properties.Resources.ButtonStart1); ///< Obrazek ktory zostanie naniesiony na przycisk start
        Bitmap pictureBtnStop   = new Bitmap(Properties.Resources.ButtonStop);   ///<Obrazek ktory zostanie naniesiony na przycisk stop

        Bitmap pictureMotorON   = new Bitmap(Properties.Resources.MotorON);   ///<Obrazek ktory zostanie naniesiony na przycisk stop
        Bitmap pictureMotorOFF  = new Bitmap(Properties.Resources.MotorOFF);   ///<Obrazek ktory zostanie naniesiony na przycisk stop
        Bitmap pictureMotorERR  = new Bitmap(Properties.Resources.MotorERR);   ///<Obrazek ktory zostanie naniesiony na przycisk stop

        //-----------------------------------------------------------------------------------------
        /**
         * Konstriktor
         */
        public MotorPanel()
        {
            InitializeComponent();
            //Ustaw kolor przezroczystosci
            pictureBtnStart.MakeTransparent(Color.White);
            pictureBtnStop.MakeTransparent(Color.White);

            pictureMotorON.MakeTransparent(Color.White);
            pictureMotorOFF.MakeTransparent(Color.White);

            //Ustaw skalowanie obrazka

            RefreshImage();
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Odsiwez obrazek na przyciskach
         */ 
        void RefreshImage()
        {
       //     btnStart.Image = pictureBtnStart;
       //     btnStop.Image  = pictureBtnStop;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * ustaw referncje pompy
         */ 
        public void SetMotorPtr(MotorDriver motorPtr)
        {
            motor = motorPtr;
            if (motor != null)
                labName.Text = "Motor " + motor.ID.ToString();
        }
        //-----------------------------------------------------------------------------------------
        public void RefreshData()
        {
            if (motor != null)
            {
                if (motor.State == Types.StateFP.ON)
                {
                    cBoxMotor.Image = pictureMotorON;
                    cBoxMotor.Checked = true;
                }
                if (motor.State == Types.StateFP.OFF)
                {
                    cBoxMotor.Image = pictureMotorOFF;
                    cBoxMotor.Checked = false;
                }
                if (motor.State == Types.StateFP.Error)
                {
                    cBoxMotor.Image = pictureMotorERR;
                    cBoxMotor.Checked = false;
                }
            }
            AdjustPanelToPriviligesOfUser();
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie dostosowanie panelu do uprawnien usera
         */ 
        private void AdjustPanelToPriviligesOfUser()
        {
            if(ApplicationData.UserChanged)
            {
                if (ApplicationData.LoggedUser.Privilige == Types.UserPrivilige.Operator)
                    cBoxMotor.Enabled = false;
                else
                    cBoxMotor.Enabled = true;
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Obsluga akcji startu pompowania
         */
        private void btnStart_Click(object sender, EventArgs e)
        {
            ItemLogger aErr = new ItemLogger();
            if (motor != null)
            {
                aErr = motor.ControlMotor(Types.StateFP.ON);
            }
            Logger.AddError(aErr);
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Obsluga akcji zatrzymania pompowania
        */
        private void btnStop_Click(object sender, EventArgs e)
        {
            ItemLogger aErr = new ItemLogger();
            if (motor != null)
            {
                aErr = motor.ControlMotor(Types.StateFP.OFF);
            }
            Logger.AddError(aErr);
        }
        //-----------------------------------------------------------------------------------------
        //Odswiez obrazek poniewaz zmianie ulegl rozmiar butona
        private void pictureBoxStop_SizeChanged(object sender, EventArgs e)
        {
            RefreshImage();
        }
        //-----------------------------------------------------------------------------------------
        //Odswiez obrazek poniewaz zmianie ulegl rozmiar butona
        private void pictureBoxStart_SizeChanged(object sender, EventArgs e)
        {
            RefreshImage();
        }
        //-----------------------------------------------------------------------------------------
        private void cBoxMotor_Click(object sender, EventArgs e)
        {
            ItemLogger aErr = new ItemLogger();
            if (motor != null)
            {
                if(cBoxMotor.Checked)
                    aErr = motor.ControlMotor(Types.StateFP.ON);
                else
                    aErr = motor.ControlMotor(Types.StateFP.OFF);
            }
            Logger.AddError(aErr);
        }
        //-----------------------------------------------------------------------------------------

    }
}
