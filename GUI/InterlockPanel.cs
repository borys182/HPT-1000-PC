using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HPT1000.Source.Driver;
using HPT1000.Source.Chamber;
namespace HPT1000.GUI
{
    public partial class InterlockPanel : UserControl
    {
        Interlock           interlock       = null;
        Types.TypeInterlock typeInterlock   = Types.TypeInterlock.None;

        private Bitmap pictureStateError            = new Bitmap(Properties.Resources.Interlock_Err);
        private Bitmap pictureStateOn               = new Bitmap(Properties.Resources.Interlock_ON);
        private Bitmap pictureStateOff              = new Bitmap(Properties.Resources.interlock_OFF);

        
        //---------------------------------------------------------------------------------------------------------------
        public InterlockPanel()
        {
            InitializeComponent();

            picture.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureStateOn.MakeTransparent(Color.White);
            pictureStateOff.MakeTransparent(Color.White);
            pictureStateError.MakeTransparent(Color.White);
        }
        //-----------------------------------------------------------------------------------------
        public void SetInterlockPtr(Interlock aInterlock, Types.TypeInterlock aTypeInterlock)
        {
            interlock       = aInterlock;
            typeInterlock   = aTypeInterlock;
        }
        //---------------------------------------------------------------------------------------------------------------
        //Aktualizauj dane na temat stanu danego interloka. Sa one zawarte na 2 bitach {00 - BrakInterlocka, 01 - OFF ; 10 - ON ; 11- Error}
        public void RefreshPanel()
        {           
            ShowBitmap();
        }
        //---------------------------------------------------------------------------------------------------------------
        public void ShowBitmap()
        {
            picture.Visible = true;
            Types.StateValve state = Types.StateValve.Unknown;

            if (interlock != null)
                state = interlock.GetState(typeInterlock);

            picture.Image = pictureStateOff;

            if (state == Types.StateValve.Error)
                picture.Image = pictureStateError;

            if (state == Types.StateValve.Close)
                picture.Image = pictureStateOff;

            if (state == Types.StateValve.Open)
                picture.Image = pictureStateOn;

       //     if (state == Types.StateValve.Unknown)
       //         picture.Visible = false;
        }
        //---------------------------------------------------------------------------------------------------------------

    }
}
