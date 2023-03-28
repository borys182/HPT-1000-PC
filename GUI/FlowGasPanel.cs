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

namespace HPT1000
{
    /// <summary>
    /// Klasa prezentuje wartosc przeplywu na formatce
    /// </summary>
    public partial class FlowGasPanel : UserControl
    {
        private MFC mfc = null;
        private int id = 0;

        //-----------------------------------------------------------------------------------------
        public FlowGasPanel()
        {
            InitializeComponent();
        }
        //-----------------------------------------------------------------------------------------
        public void SetMFC(MFC mfcPtr, int aID)
        {
            mfc = mfcPtr;
            id = aID;
        }
        //-----------------------------------------------------------------------------------------
        public void RefreshPanel()
        {
            if(mfc != null)
                labValue.Text = mfc.GetActualFlow(id,Types.UnitFlow.sccm).ToString() + " sccm - " + mfc.GetActualFlow(id, Types.UnitFlow.percent).ToString("0.##") + " %";
        }
        //-----------------------------------------------------------------------------------------

    }
}
