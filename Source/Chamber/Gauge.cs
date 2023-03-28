using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;

namespace HPT1000.Source.Chamber
{
    /// <summary>
    /// Klasa reprezentuje glowice zainstalowana w systemie
    /// </summary>
    public class Gauge : ChamberObject
    {
        Types.GaugeType gaugeType = Types.GaugeType.None;

        //-----------------------------------------------------------------------------------------

        public Types.GaugeType Type { get { return this.gaugeType; } }
    
        //-----------------------------------------------------------------------------------------
        /**
         * Aktualizuj typ glowicy odczytany z plc
         */
        override public void UpdateExtraSettingsData(int[] aData)
        {
            if (aData.Length > Types.OFFSET_GAUGE_TYPE)
            {
                if (Enum.IsDefined(typeof(Types.GaugeType), aData[Types.OFFSET_GAUGE_TYPE]))
                    this.gaugeType = (Types.GaugeType)Enum.Parse(typeof(Types.GaugeType), (aData[Types.OFFSET_GAUGE_TYPE]).ToString()); // konwertuj int na Enum
                else
                    this.gaugeType = Types.GaugeType.None;
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja umozliwia ustawianie setpointa prozni dla regulatora PID
         */
        public ItemLogger SetGaugeType(Types.GaugeType typeGauge)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[1];

            aData[0] = (int)typeGauge;
            if (plc != null)
            {
                int aCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.ExtraSettings, Types.OFFSET_GAUGE_TYPE), 1, aData);
                aErr.SetErrorMXComponents(Types.EventType.SET_MODE_PRESSURE, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                this.gaugeType = typeGauge;

            return aErr;
        }
        //-----------------------------------------------------------------------------------------

    }
}
