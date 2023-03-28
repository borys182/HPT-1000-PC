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
    public partial class PumpComponent : UserControl
    {

        ForePump pump = null;
        //-----------------------------------------------------------------------------------------
        public PumpComponent()
        {
            InitializeComponent();
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            LoadBitmap();
        }

        //-----------------------------------------------------------------------------------------
        public void SetPumpPtr(ForePump pumpPtr)
        {
            pump = pumpPtr;
        }
        //-----------------------------------------------------------------------------------------
        public void RefreshData()
        {
            LoadBitmap();
        }
        //-----------------------------------------------------------------------------------------
        private void LoadBitmap()
        {
            Bitmap valvePicture = new Bitmap(Properties.Resources.ForePumpErr);

            if (pump != null)
            {
                if (pump.GetState() == Types.StateFP.OFF)
                    valvePicture = new Bitmap(Properties.Resources.ForePumpOff);

                if (pump.GetState() == Types.StateFP.ON)
                    valvePicture = new Bitmap(Properties.Resources.ForePumpOn);
            }
            valvePicture.MakeTransparent(Color.White);
            picture.Image = valvePicture;
        }
        //-----------------------------------------------------------------------------------------
        private void picture_Click(object sender, EventArgs e)
        {
            ItemLogger aErr = new ItemLogger();

            if (pump != null)
            {
                if (pump.GetState() == Types.StateFP.OFF)
                    aErr = pump.ControlPump(Types.StateFP.ON);

                if (pump.GetState() != Types.StateFP.OFF)
                    aErr = pump.ControlPump(Types.StateFP.OFF);
            }
            Logger.AddError(aErr);
        }
        //-----------------------------------------------------------------------------------------
        private void picture_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        //-----------------------------------------------------------------------------------------
        private void picture_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
        //-----------------------------------------------------------------------------------------
    }
}
