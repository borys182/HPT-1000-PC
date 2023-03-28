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
    public partial class ValvePanel : UserControl
    {
        Types.TypeValve typeValve    = Types.TypeValve.None;
        Valve           valve        = null;
        bool            vertical     = true;

        //-----------------------------------------------------------------------------------------
       public bool Vertical
        {
            set
            {
                vertical = value;
                if (vertical)
                    picture.Image = Properties.Resources.ValveError;
                else
                    picture.Image = Properties.Resources.ValveErrorH;
            }
            get { return vertical; }
        }
        //-----------------------------------------------------------------------------------------
        public ValvePanel()
        {
            InitializeComponent();
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            LoadBitmap();
        }
        //-----------------------------------------------------------------------------------------
        public void SetValvePtr(Valve aValve, Source.Driver.Types.TypeValve aTypeValve)
        {
            valve       = aValve;
            typeValve   = aTypeValve;
        }
        //-----------------------------------------------------------------------------------------
        public void RefreshData()
        {
            LoadBitmap();
        }
        //-----------------------------------------------------------------------------------------
        private void LoadBitmap()
        {
            Bitmap valvePicture = null;

            if(vertical)
                valvePicture = new Bitmap(Properties.Resources.ValveError);
            else
                valvePicture = new Bitmap(Properties.Resources.ValveErrorH);

            if (valve != null)
            {
                if (valve.GetState(typeValve) == Types.StateValve.Close)
                {
                    if (vertical)
                        valvePicture = new Bitmap(Properties.Resources.ValveClose);
                    else
                        valvePicture = new Bitmap(Properties.Resources.ValveClose_H);
                }
                if (valve.GetState(typeValve) == Types.StateValve.Open)
                {
                    if (vertical)
                        valvePicture = new Bitmap(Properties.Resources.ValveOpen);
                    else
                        valvePicture = new Bitmap(Properties.Resources.ValveOpen_H);
                }
            }
            valvePicture.MakeTransparent(Color.White);
            picture.Image = valvePicture;
        }
        //-----------------------------------------------------------------------------------------
        private void picture_Click(object sender, EventArgs e)
        {
            ItemLogger aErr = new ItemLogger();

            if (valve != null)
            {
                if(valve.GetState(typeValve) == Types.StateValve.Close)
                    aErr = valve.SetState(Source.Driver.Types.StateValve.Open, typeValve);

                if (valve.GetState(typeValve) == Types.StateValve.Open)
                    aErr = valve.SetState(Source.Driver.Types.StateValve.Close, typeValve);
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
