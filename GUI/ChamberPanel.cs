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

namespace HPT1000.GUI
{
    public partial class ChamberPanel : UserControl
    {
        PowerSupplay powerSupply = null;
        PressureControl pressure = null;
        Maintenance maintenace = null;

        Bitmap chamber_Image = new Bitmap(Properties.Resources.Chamber1);

        //-----------------------------------------------------------------------------------------------------------------------
        public ChamberPanel()
        {
            InitializeComponent();
                             
            Bitmap pictureChamber = new Bitmap(Properties.Resources.Chamber1);
            pictureChamber.MakeTransparent(Color.White);

            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Image    = pictureChamber;  
        }
        //-----------------------------------------------------------------------------------------------------------------------
        public void SetPressure(PressureControl presPtr)
        {
            pressure = presPtr;
        }
        //-----------------------------------------------------------------------------------------------------------------------
        public void SetGenerator(PowerSupplay powerPtr)
        {
            powerSupply = powerPtr;
        }
        //-----------------------------------------------------------------------------------------------------------------------
        public void RefreshPanel()
        {
            if (pressure != null)
            {
                labPressure.Text = pressure.ToString();
                if(labPressure.Text.Length > 11)
                {
                    labPressure.Location = new Point(15,labPressure.Location.Y);
                }
                else
                {
                    labPressure.Location = new Point(17, labPressure.Location.Y);
                }
            }
            if(powerSupply != null)
            {
                labCurent.Text  = powerSupply.Curent.ToString("F3")  + " A";
                labVoltage.Text = powerSupply.Voltage.ToString("F3") + " V";
            }

            //pokaz status na temat leak testu
            if(maintenace != null)
            {
                labLeakTestStatus.Visible = false;
                if (maintenace.StateLeakTest == Source.Driver.Types.StateLeaktest.Run_MesureLeak || maintenace.StateLeakTest == Source.Driver.Types.StateLeaktest.Run_PumpDown)
                {
                    labLeakTestStatus.Text = "Leak test running";
                    labLeakTestStatus.Visible = true;
                }
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------
        public void SetMainanteceObject(Maintenance aMaintenace)
        {
            maintenace = aMaintenace;
        }
    }
}
