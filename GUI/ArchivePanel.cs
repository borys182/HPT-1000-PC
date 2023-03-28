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
using System.Windows.Forms.DataVisualization.Charting;
using HPT1000.Source.Program;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HPT1000.GUI
{
    public partial class ArchivePanel : UserControl
    {
        private HPT1000.Source.Driver.HPT1000 hpt1000 = null;
        private Source.DB dataBase = null;

        Dictionary<Series, Color> colorSeries = new Dictionary<Series, Color>();

        //Serie danych
        Series seriePressure    = new Series("Pressure: [mBar]");
        Series seriePower       = new Series("Power: [W]");
        Series serieVoltage     = new Series("Voltage: [V]");
        Series serieCurent      = new Series("Curent: [A]");
        Series serieFlow1       = new Series("Gas flow 1: [sccm]");
        Series serieFlow2       = new Series("Gas flow 2: [sccm]");
        Series serieFlow3       = new Series("Gas flow 3: [sccm]");
        Series serieMotor1      = new Series("Motor 1");
        Series serieMotor2      = new Series("Motor 2");
        Series serieDosingValve = new Series("Dosing valve");
        //Seria danych kopiowanych - umozliwiaja prezentacje danych na kilku osiach Y
        Series serieCopyPower   = new Series("Power Copy: [W]");
        Series serieCopyFlow1   = new Series("Gas flow 1 Copy: [sccm]");
        Series serieCopyFlow2   = new Series("Gas flow 2 Copy: [sccm]");
        Series serieCopyFlow3   = new Series("Gas flow 3 Copy: [sccm]");
        Series serieCopyDosingValve = new Series("Dosing valve Copy");
        Series serieEmptyMotor    = new Series();
        Series serieEmptyPressure = new Series();

        //wskazniki obiektow z ktorych czytane sa dane
        MFC mfc = null;

        //Mamy mozliwosc prezentacji danych zbiorczych na jednym wykresie lub podzilonych na trzy wykresy
        ChartArea chartAreaPressure     = null;
        ChartArea chartAreaPower        = null;
        ChartArea chartAreaFlow         = null;
        ChartArea chartAreaMotor        = null;
        ChartArea chartAreaDosingValve  = null;

        ChartArea chartLabelAreaPower   = null;        //Zmienna jest wykrozystywana do pokazywania wartoscu osi Y dla wykresu mocy
        ChartArea chartLabelAreaFlow    = null;        //Zmienna jest wykrozystywana do pokazywania wartoscu osi Y dla wykresu mocy
        ChartArea chartLabelAreaDosingValve = null;        //Zmienna jest wykrozystywana do pokazywania wartoscu osi Y dla wykresu mocy

        //wskaznik na forme jest potrzebny do wyliczania poprawnej pozycji kursora na formatce
        Form mainForm = null;
        LoadDataForm dataForm = new LoadDataForm();
        RemoveDataForm removeForm = new RemoveDataForm();
        //Obiekt danych procesu zaladowanego z archiwum
        Sesion sesion;

        int tabText = 30;           ///< Wartość określa o jaka wartosc powienie zostac wciety tekst dla jednego tabulatora
        iTextSharp.text.Font fontChapter = iTextSharp.text.FontFactory.GetFont("Verdana", 15, iTextSharp.text.Font.BOLD);
        iTextSharp.text.Font fontHeading = iTextSharp.text.FontFactory.GetFont("Verdana", 12, iTextSharp.text.Font.BOLD);
        iTextSharp.text.Font fontText    = iTextSharp.text.FontFactory.GetFont("Verdana", 12, iTextSharp.text.Font.NORMAL);
    
        //------------------------------------------------------------------------------------------
        public ArchivePanel()
        {
            InitializeComponent();
            //Inicjalizuj charta
            InitChartData();

            labSummaryProces.Text = "";
        }
        //-------------------------------------------------------------------------------
        public Source.DB DataBase
        {
            set { dataBase = value; }
        }
        //-------------------------------------------------------------------------------
        public Form MainForm
        {
            set { mainForm = value; }
        }
        //------------------------------------------------------------------------------------------
        public Source.Driver.HPT1000 Hpt1000
        {
            set
            {
                hpt1000 = value;
                if (hpt1000 != null)
                    mfc = hpt1000.GetMFC();    
            }
        }
        //------------------------------------------------------------------------------------------
        private void timer_Tick(object sender, EventArgs e)
        {
            //Ukryj serie danych jezeli nie aktywne sa przeplywki MFC
            HideNoActiveSerieMFC();
        }
        //------------------------------------------------------------------------------------------
        private void InitChartData()
        {
            //Metoda tworzy slownk gdzie kazdej seri jest przyparzadkowany jeden kolor
            PrepareColorToSeries();
            ConfigChart();
            ClearChart();
            InitSerie(); //Dodaj do kazdej seri punkt 0,0 aby wykresy sie jakis namalowal
            LoadBitmap();
            PrepareColorChartArea();
        }
        //-------------------------------------------------------------------------------
        //Zaladowanie bitmapy do przyciku kasujacego dane wartosci z wykresu
        private void LoadBitmap()
        {
            Bitmap clearIcone = new Bitmap(Properties.Resources.Clear);
            clearIcone.MakeTransparent(Color.White);

            // toolStripBtnClear.Image = clearIcone;
        }
        //-------------------------------------------------------------------------------
        //Metoda ma za zadanie inicjalizacja seri punktami 0,0 wymuszajac przez to rysowanie sie wykresow
        private void InitSerie()
        {
            if (serieMotor1.Points.Count == 0)          serieMotor1.Points.AddXY(0,0);
            if (serieCopyDosingValve.Points.Count == 0) serieCopyDosingValve.Points.AddXY(0,0);
            if (serieCopyPower.Points.Count == 0)       serieCopyPower.Points.AddXY(0,0);
            if (seriePressure.Points.Count == 0)        seriePressure.Points.AddXY(0, 0.1);
            if (serieCopyFlow1.Points.Count == 0)       serieCopyFlow1.Points.AddXY(0, 0.1);
        }
        //-------------------------------------------------------------------------------
        /**
         * Zadaniem metody jet przypozadkowanie kazdej z seri koloru. Wazne aby to utworzyc w liscie ktora umozliwi w dowolnej chwili odwolanie sie do danego kooru danej seri
         */
        private void PrepareColorToSeries()
        {
            colorSeries.Add(seriePressure, Color.Blue);
            colorSeries.Add(seriePower, Color.Red);
            colorSeries.Add(serieVoltage, Color.Green);
            colorSeries.Add(serieCurent, Color.DeepPink);
            colorSeries.Add(serieFlow1, Color.DarkOrange);
            colorSeries.Add(serieFlow2, Color.DarkViolet);
            colorSeries.Add(serieFlow3, Color.DeepSkyBlue);
            colorSeries.Add(serieMotor1, Color.Blue);
            colorSeries.Add(serieMotor2, Color.Red);
            colorSeries.Add(serieDosingValve, Color.Green);
        }
        //-------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest ustawienie kolorow opisow danych osi
         */
        private void PrepareColorChartArea()
        {
            Color color;
            if (colorSeries.TryGetValue(seriePressure, out color))
                chartAreaPressure.AxisY.TitleForeColor = color;

            if (colorSeries.TryGetValue(seriePower, out color))
                chartLabelAreaPower.AxisY.TitleForeColor = color;

            if (colorSeries.TryGetValue(serieFlow1, out color))
                chartLabelAreaFlow.AxisY.TitleForeColor = color;

            if (colorSeries.TryGetValue(serieMotor1, out color))
                chartAreaMotor.AxisY.TitleForeColor = color;

            if (colorSeries.TryGetValue(serieDosingValve, out color))
                chartLabelAreaDosingValve.AxisY.TitleForeColor = color;
        }

        //-------------------------------------------------------------------------------
        //Funkcja ma za zadanie przygotowanie konfiguracji seri i wykresu
        private void ConfigChart()
        {
            //Dodaj serie do wykresu
            chart.Series.Add(seriePressure);
            chart.Series.Add(seriePower);
            chart.Series.Add(serieVoltage);
            chart.Series.Add(serieCurent);
            chart.Series.Add(serieFlow1);
            chart.Series.Add(serieFlow2);
            chart.Series.Add(serieFlow3);
            chart.Series.Add(serieMotor1);
            chart.Series.Add(serieMotor2);
            chart.Series.Add(serieDosingValve);
            chart.Series.Add(serieCopyPower);
            chart.Series.Add(serieCopyFlow1);
            chart.Series.Add(serieCopyFlow2);
            chart.Series.Add(serieCopyFlow3);
            chart.Series.Add(serieCopyDosingValve);
            chart.Series.Add(serieEmptyPressure);
            chart.Series.Add(serieEmptyMotor);
            //Ustaw grubosvc  lini dla wszystkich serii na jeden rozmiar
            foreach (Series serie in chart.Series)
            {
                serie.BorderWidth = 1;
                serie.ChartType   = SeriesChartType.Line;
                serie.XValueType  = ChartValueType.DateTime;
                //Ustaw kolory serii poprzez wyciagniece koloru z listy jaki zostal przyparzadkowany dla danej serii
                Color color;
                if (colorSeries.TryGetValue(serie, out color))
                    serie.Color = color;
            }
            serieCopyPower.Color = seriePower.Color;
            serieCopyFlow1.Color = serieFlow1.Color;
            serieCopyFlow2.Color = serieFlow2.Color;
            serieCopyFlow3.Color = serieFlow3.Color;

            //Ustaw BackColor checkBoxom odpowiedzialnym za wizualizacje danej serii
            cBoxPressure.ForeColor  = seriePressure.Color;
            cBoxPower.ForeColor     = seriePower.Color;
            cBoxVoltage.ForeColor   = serieVoltage.Color;
            cBoxCurent.ForeColor    = serieCurent.Color;
            cBoxFlow1.ForeColor     = serieFlow1.Color;
            cBoxFlow2.ForeColor     = serieFlow2.Color;
            cBoxFlow3.ForeColor     = serieFlow3.Color;
            cBoxMotor1.ForeColor    = serieMotor1.Color;
            cBoxMotor2.ForeColor    = serieMotor2.Color;
            cBoxDosingValve.ForeColor = serieDosingValve.Color;

            //ustaw widocznosc serii tak jak sa poustawiane checkboxy
            cBox_CheckedChanged(null, EventArgs.Empty);

            //Inicjalizacja ChartArea danych wykresow
            chartAreaPressure   = chart.ChartAreas.FindByName("ChartAreaPressure");
            chartAreaMotor      = chart.ChartAreas.FindByName("ChartAreaMotor");

            //Utworz kilka osi Y
            ConfigMultipleYAxis();                 
          
            //Ustaw parametry zoom dla wykresow
            foreach (ChartArea chartArea in chart.ChartAreas)
            {
                chartArea.AxisX.LabelStyle.Format = "HH:mm:ss";                     //wyswietlaj wartosci osi X jako godzina w postaci 22:10:04
                chartArea.AxisX.IntervalType = DateTimeIntervalType.Seconds;   //ustaw interwal na osi X jako sekunda
                //Wlacz mozliwosc zoom dla obu osi X i Y
  /*              chartArea.CursorX.IsUserSelectionEnabled = true;
                chartArea.CursorY.IsUserSelectionEnabled = true;
                chartArea.CursorX.IsUserEnabled = true;
                chartArea.CursorY.IsUserEnabled = true;
                chartArea.AxisX.ScaleView.Zoomable = true;
                chartArea.AxisY.ScaleView.Zoomable = true;
    */          //Ustaw min wartos do jakiej mozna zomowac os X
                chartArea.CursorX.Interval = 1;
                chartArea.CursorX.IntervalType = DateTimeIntervalType.Seconds;
                chartArea.AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
            }
            //Ustaw format danych dla kolejnych wykresow
            chartAreaPressure.AxisY.LabelStyle.Format   = "0.###E+0";

            chartAreaPower.AxisY.LabelStyle.Format      = "#";
            chartLabelAreaPower.AxisY.LabelStyle.Format = "#";

            chartAreaFlow.AxisY.LabelStyle.Format       = "#";
            chartLabelAreaFlow.AxisY.LabelStyle.Format  = "#";

            chartAreaMotor.AxisY.LabelStyle.Format      = "#";

            chartAreaDosingValve.AxisY.LabelStyle.Format = "#";
            chartLabelAreaDosingValve.AxisY.LabelStyle.Format = "#";
            
            //Ustaw wybrane wykresy logarytmicznie
            chartAreaPressure.AxisY.IsLogarithmic = true;
            chartAreaFlow.AxisY.IsLogarithmic = true;
            chartLabelAreaFlow.AxisY.IsLogarithmic = true;

            serieEmptyPressure.ChartArea = chartAreaPressure.Name;
            serieEmptyMotor.ChartArea    = chartAreaMotor.Name;
            ShowSerie(serieEmptyPressure,false);
            ShowSerie(serieEmptyMotor, false);

            //Ustawiam wartosci dla wykresu mocy wyrazonej w procentach
            chartLabelAreaPower.AxisY.Minimum = 0;
       //     chartLabelAreaPower.AxisY.Maximum = 1000;
       //     chartLabelAreaPower.AxisY.Interval = 10;
       //     chartLabelAreaPower.AxisY.MajorGrid.Interval = 10;

            chartAreaPower.AxisY.Minimum = 0;
        //    chartAreaPower.AxisY.Maximum = 1000;
        //    chartAreaPower.AxisY.Interval = 10;
        //    chartAreaPower.AxisY.MajorGrid.Interval = 10;            
        }
        //-------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie skonfigurowanie wykresu aby prezentowac dane dla roznych osi Y przy jednej osi X 
         */
        private void ConfigMultipleYAxis()
        {
            // Ustaw wykres w zadanej pozycji - glownym wykresem jest chartAreaPressure
            chartAreaPressure.Position          = new ElementPosition(18, 14, 80, 87);
            chartAreaPressure.InnerPlotPosition = new ElementPosition(10, 0, 90, 90);

            chartAreaMotor.Position             = new ElementPosition(18, 2, 80, 8);
            chartAreaMotor.InnerPlotPosition    = new ElementPosition(10, 0, 90, 90);

            seriePressure.ChartArea = chartAreaPressure.Name;
            serieMotor1.ChartArea = chartAreaMotor.Name;
            // Utworz dodatkowe osi Y dla mocy oraz przeplywu
            chartAreaPower = CreateDataYAxis(chartAreaPressure, seriePower);
            chartAreaFlow = CreateDataYAxis(chartAreaPressure, serieFlow1);
            chartAreaDosingValve = CreateDataYAxis(chartAreaMotor, serieDosingValve);
            //Dodaj kolejne serie danych przeplywek do wykresu przeplywu
            serieFlow2.ChartArea = chartAreaFlow.Name;
            serieFlow3.ChartArea = chartAreaFlow.Name;
            serieMotor2.ChartArea = chartAreaMotor.Name;
            serieDosingValve.ChartArea = chartAreaMotor.Name;
            //Utworz przestrzen wykresu odpowiedzilna za prezentacje wartosci osi Y
            chartLabelAreaPower = CreateLabelYAxis(chartAreaPressure, serieCopyPower, 10, 1, "Power [W]");
            chartLabelAreaFlow = CreateLabelYAxis(chartAreaPressure, serieCopyFlow1, 18, 1, "Flow [sccm]");
            chartLabelAreaDosingValve = CreateLabelYAxis(chartAreaMotor, serieCopyDosingValve, 10, 1, "Valve");
            //Dodaj kolejne serie przpelywu do wykresy osi Y odpowiedzialnej za prezentacje wartosci na osi Y
            serieCopyFlow2.ChartArea = chartLabelAreaFlow.Name;
            serieCopyFlow3.ChartArea = chartLabelAreaFlow.Name;
            //Ustaw charta dla valve tak jak dla motora
            chartAreaDosingValve.AxisY.Minimum = 0;
            chartAreaDosingValve.AxisY.Maximum = 1;
            chartAreaDosingValve.AxisY.Interval = 1;

            chartLabelAreaDosingValve.AxisY.Minimum = 0;
            chartLabelAreaDosingValve.AxisY.Maximum = 1;
            chartLabelAreaDosingValve.AxisY.Interval = 1;

            ShowSerie(serieCopyFlow2, false);
            ShowSerie(serieCopyFlow3, false);

        }
        //-------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest dodanie do wykresu dodatkowej osi Y. Poniewaz nie da sie dodac kilka osi Y do jednego wykresu ale da sie dodac kilka odrebnych wykresow do jednego dlatego też
         * jest tworzonych kilka wykresow i nakladane jeden na drugi. Poprzez ustawienia przesuniecia oraz posuwania odpowidnich lini tworzy to efekt kilku osi Y
         */
        public ChartArea CreateDataYAxis(ChartArea mainArea, Series series)
        {
            // Utworz przestrzen wykresu dla podanej serii
            ChartArea areaSeries = chart.ChartAreas.Add("ChartArea_" + series.Name);
            areaSeries.BackColor = Color.Transparent;
            areaSeries.BorderColor = Color.Transparent;
            areaSeries.AxisX.MajorGrid.Enabled = false;
            areaSeries.AxisX.MajorTickMark.Enabled = false;
            areaSeries.AxisX.LabelStyle.Enabled = false;
            areaSeries.AxisY.MajorGrid.Enabled = false;
            areaSeries.AxisY.MajorTickMark.Enabled = false;
            areaSeries.AxisY.LabelStyle.Enabled = false;
            areaSeries.AxisX.IsMarginVisible = mainArea.AxisX.IsMarginVisible;
            areaSeries.AxisY.IsStartedFromZero = mainArea.AxisY.IsStartedFromZero;
            areaSeries.Position.FromRectangleF(mainArea.Position.ToRectangleF());
            areaSeries.InnerPlotPosition.FromRectangleF(mainArea.InnerPlotPosition.ToRectangleF());

            //Dodaj serie do danej przestrzeni wykresu
            series.ChartArea = areaSeries.Name;

            return areaSeries;
        }
        //-------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest utworzenie osi Y tylko z wartosciami dla seri danych. Wyswietlic sie powinna tylko sama os Y wszystko inne zostanie usuniete
         */
        public ChartArea CreateLabelYAxis(ChartArea mainArea, Series series, float axisOffset, float labelsSize, string name)
        {
            // Utworz przestrzen wykresu przeznaczona na prezentacje wartosci osi Y i ukryj cala przestrzen wykresu przeznaczona tylko na pokazywanie etykiet Y
            ChartArea areaAxis = chart.ChartAreas.Add("AxisY_" + series.Name);
            areaAxis.BackColor = Color.Transparent;
            areaAxis.BorderColor = Color.Transparent;
            areaAxis.AxisX.LineWidth = 0;
            areaAxis.AxisX.MajorGrid.Enabled = false;
            areaAxis.AxisX.MajorTickMark.Enabled = false;
            areaAxis.AxisX.LabelStyle.Enabled = false;
            areaAxis.AxisY.MajorGrid.Enabled = false;
            areaAxis.AxisY.IsStartedFromZero = mainArea.AxisY.IsStartedFromZero;
            areaAxis.AxisY.LabelStyle.Font = mainArea.AxisY.LabelStyle.Font;
            areaAxis.Position.FromRectangleF(mainArea.Position.ToRectangleF());
            areaAxis.InnerPlotPosition.FromRectangleF(mainArea.InnerPlotPosition.ToRectangleF());
            areaAxis.AxisY.Title = name;
            areaAxis.AxisY.TitleAlignment = StringAlignment.Near;
            // Ukryj serie
            series.IsVisibleInLegend = false;
            series.Color                  = Color.Transparent;
            series.BorderColor            = Color.Transparent;
            series.ChartArea = areaAxis.Name;

            // Ustaw os Y w odpowiednij pozycji
            areaAxis.Position.X -= axisOffset;
            areaAxis.InnerPlotPosition.X += labelsSize;
            areaAxis.Position.Width = 96 - areaAxis.Position.X;

            return areaAxis;
        }
        //-------------------------------------------------------------------------------
        //Kasuj dane z wykresu. Wszystkie punkty wykresu zostaja wykasowane
        public void ClearChart()
        {
            foreach (Series serie in chart.Series)
                serie.Points.Clear();

            chartAreaMotor.AxisX.Maximum    = Double.NaN;
            chartAreaPressure.AxisX.Maximum = Double.NaN;
        }
        //-------------------------------------------------------------------------------
        //Aktualizacja danych na wykresie. Funkcja wywolywana z watku glownego aplikacji po odczytaniuy danych z PLC
        public void LoadData(Sesion sesion)//DateTime startDate,DateTime endDate,string processName)
        {
            try
            {              
                if (dataBase != null)
                {
                    Cursor = Cursors.WaitCursor;
                    ClearChart();
                 //   chartAreaPressure.AxisX.LabelStyle.Format = "yyyy-MM-dd HH:mm:ss";                     //wyswietlaj wartosci osi X jako godzina w postaci 22:10:04
                    List<DataBaseData> listData = null;
                    //Pobierz dane z bazy danych albo dla podanego procesu albo wszystkie dane zgodnie z przedzialem czasowym        
                    if (sesion.ID != 0)
                        listData = dataBase.GetHistoryData(sesion.ID);
                    else //Pobierz dane zgodni z data
                        listData = dataBase.GetHistoryData(sesion.StartDate, sesion.EndDate);
                                      
                    if (listData != null && listData.Count > 0)
                    {
                        //Aby prezentowac dane w formie czasu odczytuje pierwsz wartosc daty (poniewaz jest ona posortowana to bedzie to najmniejsza)
                        DateTime startDate = sesion.StartDate;// listData[0].Date;
                        //Aby zachowac ciaglosc danych na wykresie od wartosci dat min do max sztucznie dodaje dla kazdego parametru dla wartosci max data ostatnia wartosc
                        AddPointToMaxDate(listData, sesion.EndDate);
                        foreach (DataBaseData data in listData)
                        {
                            DateTime timeMesure = new DateTime(data.Date.Ticks - startDate.Ticks);
                            //Przydziel dane do odpowiedniej seri
                            double value = data.Value;
                            switch (GetNameParameter(data.ID_Para))
                            {
                                case "MFC1 Flow":
                                    if (value <= 0) value = 0.1; //Poniewa os jest logarytmicznia nie moze miec wartosc rtownych badz mniejszych od 0
                                    serieFlow1.Points.AddXY(timeMesure, value);
                                    break;
                                case "MFC2 Flow":
                                    if (value <= 0) value = 0.1; //Poniewa os jest logarytmicznia nie moze miec wartosc rtownych badz mniejszych od 0
                                    serieFlow2.Points.AddXY(timeMesure, value);
                                    break;
                                case "MFC3 Flow":
                                    if (value <= 0) value = 0.1; //Poniewa os jest logarytmicznia nie moze miec wartosc rtownych badz mniejszych od 0
                                    serieFlow3.Points.AddXY(timeMesure, value);
                                    break;
                                case "Curent":
                                    serieCurent.Points.AddXY(timeMesure, data.Value);
                                    break;
                                case "Voltage":
                                    serieVoltage.Points.AddXY(timeMesure, data.Value);
                                    break;
                                case "Power":// Percent":
                                    //Sprawdz czy stan sie nie zmienil jezeli tak to dodaj punkt aby usuzkac wykres prostakatny
                    //                if (IsPointChange(seriePower, value, out value)) seriePower.Points.AddXY(timeMesure, value);
                                    seriePower.Points.AddXY(timeMesure, data.Value);
                                    break;
                                case "Pressure":
                                    if (value <= 0) value = 0.00001; //Poniewa os jest logarytmicznia nie moze miec wartosc rtownych badz mniejszych od 0
                                    seriePressure.Points.AddXY(timeMesure, value);
                                    break;
                                case "Motor 1 state":
                                    //Sprawdz czy stan sie nie zmienil jezeli tak to dodaj punkt aby usuzkac wykres prostakatny
                                    if (IsPointChange(serieMotor1, value, out value)) serieMotor1.Points.AddXY(timeMesure, value);
                                    serieMotor1.Points.AddXY(timeMesure, data.Value);
                                    //aby wymusic rysowanie labelu na wykresi presure dodaje sztuczniek punkty
                                    break;
                                case "Motor 2 state":
                                    //Sprawdz czy stan sie nie zmienil jezeli tak to dodaj punkt aby usuzkac wykres prostakatny
                                    if (IsPointChange(serieMotor2, value, out value)) serieMotor2.Points.AddXY(timeMesure, value);
                                    serieMotor2.Points.AddXY(timeMesure, data.Value);
                                    break;
                                case "Vaporaizer state":
                                    //Sprawdz czy stan sie nie zmienil jezeli tak to dodaj punkt aby usuzkac wykres prostakatny
                                    if (IsPointChange(serieDosingValve, value, out value)) serieDosingValve.Points.AddXY(timeMesure, value);
                                    serieDosingValve.Points.AddXY(timeMesure, data.Value);
                                    break;
                            }
                        }
                        UpdateCopyData();
                    }
                }
                //Dodaj pkt 0,0 do osi ktore nie posiadaja zadnego punktu przez co wymusze rysowanie ich wykresu
                InitSerie();         
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
            chart.Update();
            //Ustaw dobre zakresy osi X wykresow Motor i pressure - musza zostac synchronizowanne\
            SynchronizeChartArea();
       }
        //-------------------------------------------------------------------------------
        /**
         * Dodaj wartosci do seri odpowiedzialnych tylko za prezetnacje wartosic na osiach Y
         */
        private void UpdateCopyData()
        {
            chart.Update();
            //Dodaj punkt do seri odpowieidzlanej za prezentacje wartosci na osi Y. Os Y jest wykorzystywana do pokazywania zakresu wartosci jak tez znacznika ostatnio dodanej wartosci
          
            //Ustaw odpowiednie zalkresy osi X i Y wykresow etykie osi Y        
            chartLabelAreaPower.AxisY.Minimum = chartAreaPower.AxisY.Minimum;
            chartLabelAreaPower.AxisY.Maximum = chartAreaPower.AxisY.Maximum;

            double AMaxPoint = chartAreaPower.AxisY.Maximum;

            if (AMaxPoint < 20)
                chartLabelAreaPower.AxisY.LabelStyle.Format = "#.#";
            if (AMaxPoint < 10)
                chartLabelAreaPower.AxisY.LabelStyle.Format = "#.##";
            if (AMaxPoint < 1)
                chartLabelAreaPower.AxisY.LabelStyle.Format = "#.###";
            
            chartLabelAreaFlow.AxisY.Maximum = Math.Pow(10, chartAreaFlow.AxisY.ScaleView.ViewMaximum);
            chartLabelAreaFlow.AxisY.Minimum = Math.Pow(10, chartAreaFlow.AxisY.ScaleView.ViewMinimum);

            try
            {
                DataPoint aPoint;
                if (seriePower.Points.Count > 0)
                {
                    aPoint = seriePower.Points.FindMaxByValue("Y", 0);
                    if(aPoint.YValues.Length > 0)
                        serieCopyPower.Points.AddXY(0,aPoint.YValues[0]);
                }
                if (serieDosingValve.Points.Count > 0)
                {
                    aPoint = serieDosingValve.Points.FindMaxByValue("Y", 0);
                    if (aPoint.YValues.Length > 0)
                        serieCopyDosingValve.Points.AddXY(1, aPoint.YValues[0]);
                }
                if (serieFlow1.Points.Count > 0)
                {
                    aPoint = serieFlow1.Points.FindMaxByValue("Y", 0);
                    if (aPoint.YValues.Length > 0)
                    {
                        double value = aPoint.YValues[0];
                        if (value <= 0) value = 0.1; //Poniewaz oas jest logarytmicnza nie moze przykomowac wartosci mnijeszych badz rownych 0
                        serieCopyFlow1.Points.AddXY(1, value);
                    }
                }
                if (serieFlow2.Points.Count > 0)
                {
                    aPoint = serieFlow2.Points.FindMaxByValue("Y", 0);
                    if (aPoint.YValues.Length > 0)
                    {
                        double value = aPoint.YValues[0];
                        if (value <= 0) value = 0.1; //Poniewaz oas jest logarytmicnza nie moze przykomowac wartosci mnijeszych badz rownych 0
                        serieCopyFlow2.Points.AddXY(1, value);
                    }
                }
                if (serieFlow3.Points.Count > 0)
                {
                    aPoint = serieFlow3.Points.FindMaxByValue("Y", 0);
                    if (aPoint.YValues.Length > 0)
                    {
                        double value = aPoint.YValues[0];
                        if (value <= 0) value = 0.1; //Poniewaz oas jest logarytmicnza nie moze przykomowac wartosci mnijeszych badz rownych 0
                        serieCopyFlow3.Points.AddXY(1, value);
                    }
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }         
        }
        //-------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie synchronizacji osi X dwoch wykresow. Glownym wykresem jest pressure ktory tlyko posiada etyliety ois X ale musi on byc synchronizoweny z Motorem
         */
        private void SynchronizeChartArea()
        {
             //Sprawdz czy obie osie posiadaj punkty jezeli tak to ustal kto ma wieksza wartosc Max osi X i ustaw ja dla drugeoj
            if (IsChartPosessAnyPoint(chartAreaPressure) && IsChartPosessAnyPoint(chartAreaMotor))
            {
                if (chartAreaPressure.AxisX.ScaleView.ViewMaximum > chartAreaMotor.AxisX.ScaleView.ViewMaximum)
                {
                    serieEmptyMotor.Points.AddXY(chartAreaPressure.AxisX.Maximum, 0);
                    chartAreaMotor.AxisX.Maximum = chartAreaPressure.AxisX.ScaleView.ViewMaximum;
                }
                if (chartAreaMotor.AxisX.ScaleView.ViewMaximum > chartAreaPressure.AxisX.ScaleView.ViewMaximum)
                {
                    serieEmptyPressure.Points.AddXY(chartAreaMotor.AxisX.Maximum, 0.1);
                    chartAreaPressure.AxisX.Maximum = chartAreaMotor.AxisX.ScaleView.ViewMaximum;
                }
            }
            //Sprawdz czy wykres Pressure posiada jakies punkty jezeli nie to ustaw jego max na max motor
            if(!IsChartPosessAnyPoint(chartAreaPressure) && IsChartPosessAnyPoint(chartAreaMotor))
            {
                serieEmptyPressure.Points.AddXY(chartAreaMotor.AxisX.Maximum, 0.1);
                chartAreaPressure.AxisX.Maximum = chartAreaMotor.AxisX.ScaleView.ViewMaximum;
            }
            //Sprawdz czy wykres Motor posiada jakies punkty jezeli nie to ustaw jego max na max pressure
            if (!IsChartPosessAnyPoint(chartAreaMotor) && IsChartPosessAnyPoint(chartAreaPressure))
            {
                serieEmptyMotor.Points.AddXY(chartAreaPressure.AxisX.Maximum, 0);
                chartAreaMotor.AxisX.Maximum = chartAreaPressure.AxisX.ScaleView.ViewMaximum;
            }           
            serieEmptyPressure.Points.AddXY(0, 0.1);
            serieEmptyMotor.Points.AddXY(0, 0);
        }
        //-------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest sprawdzenie czy wykres posiada jakiekolwiek punkty poaz tymi przyspianymi do 0
         */
        private bool IsChartPosessAnyPoint(ChartArea chartArea)
        {
            bool res = false;
            if(chartArea != null)
            {
                foreach(Series serie in chart.Series)
                {
                    if (serie.ChartArea == chartArea.Name )
                    {
                        foreach (DataPoint point in serie.Points)
                        {
                            if (point.XValue != 0)
                            {
                                res = true;
                                break;
                            }
                        }
                    }
                }
            }
            return res;
        }
        //-------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie sprawdzenie czy ostatni punkt seri jest rozny od nowo dodanego
         */
        private bool IsPointChange(Series serie, double newValue, out double value)
        {
            bool res = false;
            value = 0;
            if (serie.Points.Count > 0 && serie.Points[serie.Points.Count - 1].YValues[serie.Points[serie.Points.Count - 1].YValues.Length - 1] != newValue)
            {
                value = (int)serie.Points[serie.Points.Count - 1].YValues[serie.Points[serie.Points.Count - 1].YValues.Length - 1];
                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------
        //Aby zachowac ciaglosc danych na wykresie od wartosci dat min do max sztucznie dodaje dla kazdego parametru dla wartosci max data ostatnia wartosc
        private void AddPointToMaxDate(List<DataBaseData> listData,DateTime endDate)
        {
            List<int> uniqParaList = new List<int>();//lista zawiera id parametrow dla ktorych mamy dane
            foreach(DataBaseData data in listData)
            {
                if (!uniqParaList.Contains(data.ID_Para))
                    uniqParaList.Add(data.ID_Para);
            }
            //Dla kazdego parametru znajdz ostatnia wartosc i dodaj nowy punkt jako end date
            foreach (int id_para in uniqParaList)
            {
                for (int i = listData.Count - 1; i >= 0; i--)
                {
                    if (listData[i].ID_Para == id_para)
                    {
                        DataBaseData data = new DataBaseData();
                        data.ID_Para = listData[i].ID_Para;
                        data.Value   = listData[i].Value;
                        data.Date    = endDate;
                        listData.Add(data);
                        break;
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------
        //Funkcja ma za zadanie dobranie intervalu oraz formatu osi X w zaleznosci od zakresu punktow ktory prezentuje
        private void GetRangeData(List<DataBaseData> listData,out DateTime minDate,out DateTime maxDate)
        {
            minDate = DateTime.MaxValue;
            maxDate = DateTime.MinValue;
            //Znajdz min/max pkt
            foreach (DataBaseData data in listData)
            {
                if (data.Date < minDate)
                    minDate = data.Date;

                if (data.Date > maxDate)
                    maxDate = data.Date;
            }            
        }
        //-------------------------------------------------------------------------------
        private void AutoScale(DateTime minDate, DateTime maxDate)
        {
            /*
            DateTimeIntervalType typeInterval = DateTimeIntervalType.Years;
            string format = "yyyy";
            if (maxDate.AddTicks(-minDate.Ticks).Ticks < DateTime.MinValue.AddYears(1).Ticks)
            {
                typeInterval = DateTimeIntervalType.Months;
                format = "yyyy-MM";
            }
            if (maxDate.AddTicks(-minDate.Ticks).Ticks < DateTime.MinValue.AddMonths(1).Ticks)
            {
                typeInterval = DateTimeIntervalType.Days;
                format = "MM-dd";
            }
            if (maxDate.AddTicks(-minDate.Ticks).Ticks < DateTime.MinValue.AddDays(1).Ticks)
            {
                typeInterval = DateTimeIntervalType.Hours;
                format = "HH:mm";
            }
            if (maxDate.AddTicks(-minDate.Ticks).Ticks < DateTime.MinValue.AddHours(1).Ticks)
            {
                typeInterval = DateTimeIntervalType.Minutes;
                format = "HH:mm:ss";
            }
            foreach (ChartArea chartArea in chart.ChartAreas)
            {
                chartArea.AxisX.LabelStyle.Format = format;
                chartArea.AxisX.Interval = 1;
                chartArea.AxisX.IntervalType = typeInterval;
                chartArea.AxisX.IntervalOffset = 1;

                chartArea.AxisX.Minimum = minDate.ToOADate();
                chartArea.AxisX.Maximum = maxDate.ToOADate();

            }
            */
        }
        //-------------------------------------------------------------------------------
        private void ClearTime(DateTime dateIn, out DateTime dateOut)
        {
            dateIn = dateIn.AddHours(-dateIn.Hour);
            dateIn = dateIn.AddMinutes(-dateIn.Minute);
            dateOut = dateIn.AddSeconds(-dateIn.Second);
        }
        //-------------------------------------------------------------------------------
        private string GetNameParameter(int idPara)
        {
            string aNamePara = null;

            if (hpt1000 != null)
            {
                foreach (Device device in hpt1000.Chamber.GetObjects())
                {
                    foreach (Parameter para in device.GetParameters())
                    {
                        if (para.ID == idPara)
                            aNamePara = para.Name;
                    }
                }
            }
            return aNamePara;
        }
        //-------------------------------------------------------------------------------
        //Funkcja ma za zadanie zwrocenie jednej daty jako polaczenie daty i czasu dwoch zmiennych
        private DateTime GetDateTime(DateTime date, DateTime time)
        {
            DateTime dateTime = new DateTime();
            if (date != null && time != null)
            {
                //Poniewaz nie mozna ustawina c wartosci czasu to jednym sposobnem jest ich dodawanie ale najpierw trzeba czas wyzerowac to znaczy ustawic na  00:00:00
                dateTime = date;
                //Ustaw czas na 00:00:00
                dateTime = dateTime.AddHours(-dateTime.Hour);
                dateTime = dateTime.AddMinutes(-dateTime.Minute);
                dateTime = dateTime.AddSeconds(-dateTime.Second);
                //Dodaj czas ktroy nalezy ustawic
                dateTime = dateTime.AddHours(time.Hour);
                dateTime = dateTime.AddMinutes(time.Minute);
                dateTime = dateTime.AddSeconds(time.Second);
            }
            return dateTime;
        }
        //-------------------------------------------------------------------------------
        //Pokaz serie danych w zaleznosci od aktywnych przeplywek
        private void HideNoActiveSerieMFC()
        {
 /*           if (mfc != null)
            {
                bool enabled = mfc.GetActive(1) && cBoxFlow1.Checked;
                if (serieFlow1.Enabled != enabled)
                    serieFlow1.Enabled = enabled;
                if(cBoxFlow1.Visible != mfc.GetActive(1))
                    cBoxFlow1.Visible   = mfc.GetActive(1);

                enabled = mfc.GetActive(2) && cBoxFlow2.Checked;
                if (serieFlow2.Enabled != enabled)
                    serieFlow2.Enabled = enabled;
                if (cBoxFlow2.Visible != mfc.GetActive(2))
                    cBoxFlow2.Visible = mfc.GetActive(2);

                enabled = mfc.GetActive(3) && cBoxFlow3.Checked;
                if (serieFlow3.Enabled != enabled)
                    serieFlow3.Enabled = enabled;
                if (cBoxFlow3.Visible != mfc.GetActive(3))
                    cBoxFlow3.Visible = mfc.GetActive(3);

                //jezlei zadme mfc nie jest aktywny to wylacz takze charta
                enabled = false;
                if (!mfc.GetActive(1) && !mfc.GetActive(2) && !mfc.GetActive(3))
                    enabled = false;
                if(chartAreaFlow.Visible != enabled)
                    chartAreaFlow.Visible = enabled;
            }
   */ 
        }
        //-------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest pokaznie/schowanie danej serii
         */
        private void ShowSerie(Series serie, bool show)
        {
            if (serie != null)
            {
                if (show)
                {
                    //Ustaw kolory serii
                    Color color;
                    if (colorSeries.TryGetValue(serie, out color))
                        serie.Color = color;

                }
                else
                {
                    serie.IsVisibleInLegend = false;
                    serie.Color = Color.Transparent;
                    serie.BorderColor = Color.Transparent;
                }
            }
        }
        //-------------------------------------------------------------------------------
        //Ustaw widocznosc danej serii
        private void cBox_CheckedChanged(object sender, EventArgs e)
        {
            ShowSerie(seriePressure, cBoxPressure.Checked);
            ShowSerie(seriePower, cBoxPower.Checked);
            ShowSerie(serieVoltage, cBoxVoltage.Checked);
            ShowSerie(serieCurent, cBoxCurent.Checked);
            ShowSerie(serieFlow1, cBoxFlow1.Checked);
            ShowSerie(serieFlow2, cBoxFlow2.Checked);
            ShowSerie(serieFlow3, cBoxFlow3.Checked);
            ShowSerie(serieMotor1, cBoxMotor1.Checked);
            ShowSerie(serieMotor2, cBoxMotor2.Checked);
            ShowSerie(serieDosingValve, cBoxDosingValve.Checked);
        }
        //-------------------------------------------------------------------------------
        //Wybor preferencji wyswietlania danuych albo na jednym wykresie albo na trzech
        private void toolStripComboBoxChart_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        //-------------------------------------------------------------------------------
        //Funkcja ma za zadanie sprawdzenie nad ktorym ChartArea zostal nacisniety klawisz myszy
        private bool ChecMousekArea(ElementPosition chartAreaPosition)
        {
            bool aRes = false;
            if (mainForm != null)
            {
                double minX = chartAreaPosition.X / 100 * chart.Width;
                double maxX = minX + chartAreaPosition.Width * chart.Width / 100;
                double minY = chartAreaPosition.Y / 100 * chart.Height;
                double maxY = minY + chartAreaPosition.Height * chart.Height / 100;

                double mouseX = MousePosition.X - mainForm.Location.X - Location.X;
                double mouseY = MousePosition.Y - mainForm.Location.Y - Location.Y - 55;

                if (minX < mouseX && maxX > mouseX && minY < mouseY && maxY > mouseY)
                    aRes = true;
            }
            return aRes;
        }
        //-------------------------------------------------------------------------------
        //Kasowanie Zoom danego wykresu
        private void toolStripBtnZoomReset_Click(object sender, EventArgs e)
        {
            /*
            if (chartAreaPressure != null)
            {
                chartAreaPressure.AxisX.ScaleView.ZoomReset(0);
                chartAreaPressure.AxisY.ScaleView.ZoomReset(0);
            }

            if (chartAreaPower != null)
            {
                chartAreaPower.AxisX.ScaleView.ZoomReset(0);
                chartAreaPower.AxisY.ScaleView.ZoomReset(0);
            }

            if (chartAreaFlow != null)
            {
                chartAreaFlow.AxisX.ScaleView.ZoomReset(0);
                chartAreaFlow.AxisY.ScaleView.ZoomReset(0);
            }
            */
        }
        //-------------------------------------------------------------------------------
        /**
        * Zadaniem metody jest obsluga zdarzenia OnClick przycisku Load Data
        */
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            DialogResult res = dataForm.ShowDialog();
            if (res == DialogResult.OK)
            {
                //Odcyztaj jakie dane mam zaladowac
                Sesion sesionTmp = dataForm.GetProcesData();
                //Pobierz dane dla danego procesu jezeli jakis zostal wybrany
                if (sesionTmp.ProcesName != null)
                {
                    sesion = sesionTmp;
                    LoadData(sesion);
                    //Pokaz infomracje na temat zaladowanego procesu
                    labSummaryProces.Text = "Proces: '" + sesion.ProcesName + "' " + sesion.StartDate.ToString() + " - " + sesion.EndDate.ToString();
                    ShowSummary(sesion);
                    dropDownBtnExportToFile.Enabled = true;
                }
                else
                    dropDownBtnExportToFile.Enabled = false;
            }
        }
        //-------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest wyeksportowanie raportu do pliku CSV
         */ 
        private void ExportToCSV(string path)
        {
            StringBuilder csv_txt = new StringBuilder();

            //Dodaj naglowek
            csv_txt.AppendLine("Report - summary of process '" + sesion.ProcesName + "'");
  
            //Dodaj kolejne linie raportu     
            foreach (string line in txtBoxSummary.Lines)
                csv_txt.AppendLine(line);

            //Dodaj dane z charta
            csv_txt.Append(GetChartDataExportToCSV());
            if (File.Exists(path))
                File.Delete(path);
            File.AppendAllText(path,csv_txt.ToString());
        }
        //-------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest wstawienie zgodnie z formatem csv danych pobranych z charta do zmiennej ktora zostanie zapisana w pliku csv
         */ 
        private StringBuilder GetChartDataExportToCSV()
        {
            StringBuilder csv_txt = new StringBuilder();            
            List<ItemChartCSV> seriesCSV = new List<ItemChartCSV>();//Utworz zbiorcza liste niepowtarzanych item wzgledem wartości X wykresu bazujac na wszystkich seriach
            //Przejdz po wszystkich seriach wykresu i utworz liste z unikalnych wartosci osi X
            foreach (Series serie in chart.Series)
            {
                //Przehdz po wszystkich punktach danej seri i odczytaj wartos osi X
                foreach (DataPoint point in serie.Points)
                {
                    ItemChartCSV serieCSV = new ItemChartCSV();
                    serieCSV.Time = point.XValue;
                    if (!seriesCSV.Contains(serieCSV) && point.XValue > 0 && point.XValue < 1) //sprawdzam takze poprawnesc zmiennej okreslajacaj czas - musi byc z zakresu 0-1
                        seriesCSV.Add(serieCSV);
                }
            }
            //Posortuj liste
            seriesCSV.Sort();
            //Przejdz po liscie i wyciagnij z kazdej serii wartosc dla danego X
            GetSeriesData(seriesCSV, seriePressure, Types.SeriesType.Pressure);
            GetSeriesData(seriesCSV, seriePower,    Types.SeriesType.Power);
            GetSeriesData(seriesCSV, serieFlow1,    Types.SeriesType.Flow1);
            GetSeriesData(seriesCSV, serieFlow2,    Types.SeriesType.Flow2);
            GetSeriesData(seriesCSV, serieFlow3,    Types.SeriesType.Flow3);
            GetSeriesData(seriesCSV, serieMotor1,   Types.SeriesType.Motor1);
            GetSeriesData(seriesCSV, serieMotor2,   Types.SeriesType.Motor2);
            GetSeriesData(seriesCSV, serieDosingValve, Types.SeriesType.DosingValve);

            //Zapisz dane do CSV
            csv_txt.AppendLine("Chart data");
            csv_txt.AppendLine("");
            csv_txt.AppendLine("Time,Pressure[mBar],Power[W],Flow1[sccm],Flow2[sccm],Flow3[sccm],Motor 1,Motor 2,Dosing valve");
            DateTime time;
            //Przejdz po liscie wartosci i utworz wpisy dla kolejnych rekordow danych
            foreach (ItemChartCSV itemCSV in seriesCSV)
            {
                time = DateTime.FromOADate(itemCSV.Time); // konwertuj czas z double
                string line = time.ToLongTimeString() + "." + time.Millisecond.ToString() + "," + itemCSV.Pressure.ToString("F3", System.Globalization.CultureInfo.InvariantCulture) + "," + itemCSV.Power.ToString("F3", System.Globalization.CultureInfo.InvariantCulture) + "," + itemCSV.Flow1.ToString("F3", System.Globalization.CultureInfo.InvariantCulture) + "," + itemCSV.Flow2.ToString("F3", System.Globalization.CultureInfo.InvariantCulture) + "," + itemCSV.Flow3.ToString("F3", System.Globalization.CultureInfo.InvariantCulture) + "," + itemCSV.Motor1.ToString("F3", System.Globalization.CultureInfo.InvariantCulture) + "," + itemCSV.Motor2.ToString("F3", System.Globalization.CultureInfo.InvariantCulture) + "," + itemCSV.DosigValve.ToString("F3",System.Globalization.CultureInfo.InvariantCulture);
                csv_txt.AppendLine(line);
            }
            
            return csv_txt;
        }
        //-------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest uzupelnienie wartosci dla danej seri i dla danej pozycji X w liscie seriesCSV. Pamietac nalezy ze nie wszystkie wartosci beda mialy pokrycie w tym przpyapdku nalezy przyspisac odstatnia wartosc
         */
        private void GetSeriesData(List<ItemChartCSV> seriesCSV, Series serieChart, Types.SeriesType serieType)
        {
            bool    exist   = false;
            double value    = 0;
            //Przejdz po wszystkich wartosciach X jakie powstaly z sumowania wszsytkich serii
            for (int i = 0; i < seriesCSV.Count; i++)
            {
                ItemChartCSV serieCSV = seriesCSV[i];
                exist = false;
                //Dla danego X pobierz odpowiednie Y z seri. Jezeli go nie ma to wstaw poprzednia wartosc (oznacza to ze w danym momencie wartosc sie nie zmieniala)
                for (int j = 0; j < serieChart.Points.Count; j++)
                {
                    DataPoint point = serieChart.Points[j];
                    if (serieCSV.Time == point.XValue)
                    {
                        value = point.YValues[0];
                        if(j + 1 < serieChart.Points.Count && serieChart.Points[j].XValue == serieChart.Points[j + 1].XValue)
                            value = serieChart.Points[j + 1].YValues[0];
                        exist = true;
                        break;
                    }
                }
                //Wstaw odczytana wartosc do odpowiedniej zmiennej
                switch (serieType)
                {
                    case Types.SeriesType.Pressure:
                        if (!exist && i > 0)         //Jezeli nie znalazlem to wstawiam wartosci poprzednia
                            serieCSV.Pressure = seriesCSV[i - 1].Pressure;
                        else
                            serieCSV.Pressure = value;
                        break;
                    case Types.SeriesType.Power:
                        if (!exist && i > 0)         //Jezeli nie znalazlem to wstawiam wartosci poprzednia
                            serieCSV.Power = seriesCSV[i - 1].Power;
                        else
                            serieCSV.Power = value;
                        break;
                    case Types.SeriesType.Flow1:
                        if (!exist && i > 0)         //Jezeli nie znalazlem to wstawiam wartosci poprzednia
                            serieCSV.Flow1 = seriesCSV[i - 1].Flow1;
                        else
                            serieCSV.Flow1 = value;
                        break;
                    case Types.SeriesType.Flow2:
                        if (!exist && i > 0)         //Jezeli nie znalazlem to wstawiam wartosci poprzednia
                            serieCSV.Flow2 = seriesCSV[i - 1].Flow2;
                        else
                            serieCSV.Flow2 = value;
                        break;
                    case Types.SeriesType.Flow3:
                        if (!exist && i > 0)         //Jezeli nie znalazlem to wstawiam wartosci poprzednia
                            serieCSV.Flow3 = seriesCSV[i - 1].Flow3;
                        else
                            serieCSV.Flow3 = value;
                        break;
                    case Types.SeriesType.Motor1:
                        if (!exist && i > 0)         //Jezeli nie znalazlem to wstawiam wartosci poprzednia
                            serieCSV.Motor1 = seriesCSV[i - 1].Motor1;
                        else
                            serieCSV.Motor1 = value;
                        break;
                    case Types.SeriesType.Motor2:
                        if (!exist && i > 0)         //Jezeli nie znalazlem to wstawiam wartosci poprzednia
                            serieCSV.Motor2 = seriesCSV[i - 1].Motor2;
                        else
                            serieCSV.Motor2 = value;
                        break;
                    case Types.SeriesType.DosingValve:
                        if (!exist && i > 0)         //Jezeli nie znalazlem to wstawiam wartosci poprzednia
                            serieCSV.DosigValve = seriesCSV[i - 1].DosigValve;
                        else
                            serieCSV.DosigValve = value;
                        break;                   
                }
            }
        }
        //-------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest obsluga zdarzenia OnClick przycisku Print ktorego zadaniem jest export raportu wraz z wykresem do pliku pdf
         */
        private void btnExportToCSV_Click(object sender, EventArgs e)
        {
            //Wyciagnij od usera miejsce gdzie chce sobie zapisac plik
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.Filter     = "CSV Files (*.csv)|*.csv";
            saveFileDialog.FileName   = sesion.ProcesName + " - " + Types.ConvertDataToCorrectFileName(sesion.StartDate);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;
                if (path != "")
                {
                    //Utworz wymagane zmienne
                    try
                    {
                        ExportToCSV(path);
                        MessageBox.Show("Summary of process has been correctly export to SCV file: " + path, "Export to file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest obsluga zdarzenia OnClick przycisku Print ktorego zadaniem jest export raportu wraz z wykresem do pliku pdf
         */
        private void btnExportToPDF_Click(object sender, EventArgs e)
        {
            //Wyciagnij od usera miejsce gdzie chce sobie zapisac plik
            saveFileDialog.DefaultExt   = "pdf";
            saveFileDialog.Filter       = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.FileName     = sesion.ProcesName + " - " + Types.ConvertDataToCorrectFileName(sesion.StartDate);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                if (fileName != "")
                {
                    //Utworz wymagane zmienne
                    try
                    {
                        Document    doc  = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                        FileStream  file = new FileStream(fileName, FileMode.Create);
                        PdfWriter   wri  = PdfWriter.GetInstance(doc, file);
                        doc.Open(); //Otworz dokumnet do zapisu
                        //Dodaj tekst do pliku PDF
                        AddTextReportToPDF(doc);
                        //Dodaj wykres do pliku PDF
                        AddChartReportToPDF(doc, saveFileDialog.FileName);
                        //Zamknij plik
                        doc.Close();
                        MessageBox.Show("Summary of process has been correctly export to PDF file: " + fileName, "Export to file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------
        /**
        * Zadanie mmetody jest odczytasnie raportu procesu pobranego z archiwum i umieszczeonego w komponencie Memoe zapisanie do pliku PDF
        */
        private void AddTextReportToPDF(Document doc)
        {
            int indentation = tabText;
            //Dodaj naglowek
            Paragraph para = new Paragraph("Report - summary of process '" + sesion.ProcesName + "'" , fontChapter);
            para.Alignment = 1;
            doc.Add(para);
            
            foreach (string line in txtBoxSummary.Lines)
            {
                //Ustal wartosc wciecia ile powinno byc tabulatorow
                indentation = tabText * GetTabulatorCount(line);
                if (IsHeading(line))
                {
                    para = new Paragraph(line, fontHeading);
                    //Dodaj enter
                    doc.Add(new Paragraph(" "));
                }
                else
                {
                    para = new Paragraph(line, fontText);
                }
                para.IndentationLeft = indentation;
                doc.Add(para);
            }
        }
        //-------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie zwracanie informacji o tym ile tabulator znajduje sie na poczatku tekstu
         */ 
        private int GetTabulatorCount(string line)
        {
            int countTab = 0;

            char[] tabChar = line.ToCharArray();
            foreach(char oneChar in tabChar)
            {
                if (oneChar == '\t')
                    countTab++;
                else
                    break;
            }
            return countTab;
        }
        //-------------------------------------------------------------------------------
        /**
        * Zadanie mmetody jest zapisanie obrazka wykresu procesu pobrango z archiwum do pliku PDF
        */
        private void AddChartReportToPDF(Document doc,string path)
        {
            //Dodaj naglowek
            //Ustaw wykres na nowej stronie
            doc.NewPage();
            doc.SetMargins(10, 10, 20, 20);
            Paragraph para = new Paragraph("Data chart", fontHeading);
            path = path.Remove(path.Length - 4, 4) + ".jpg";
            chart.SaveImage(path, ChartImageFormat.Jpeg);
           
		 	using (var imageStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var image = iTextSharp.text.Image.GetInstance(imageStream);
                image.ScaleAbsolute(new iTextSharp.text.Rectangle(doc.PageSize.Height - (doc.BottomMargin + doc.TopMargin + 55), doc.PageSize.Width - (doc.LeftMargin + doc.RightMargin)));// (600, 400));
                image.Rotation = (float)(Math.PI / 2);
                doc.Add(para);
                doc.Add(image);                            
            }
            //usun plik charta
            File.Delete(path);
        }
        //-------------------------------------------------------------------------------
        /**
        * Zadaiem metody jest sprawdzenie czy przekazny string mozemy traktowac jako naglowek. Naglowekiem jest text zakonczony kropka. Pamietaj aby numeru zakonczonego kropka nie traktowac jako naglowek 
        */
        private bool IsHeading(string txt)
        {
            bool res = false;
            int  tmp = 0;
            if (txt.Length > 1 && txt.ToCharArray()[txt.Length - 1] == '.' && !int.TryParse(txt.ToCharArray()[txt.Length - 2].ToString(),out tmp))
                res = true;
            return res;
        } 
        //-------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest wyświetlenie podsumownaia na temat procesu ktreogo dane sa prezentowane na wykresie
         */
        private void ShowSummary(Sesion sesion)
        {
            txtBoxSummary.Text = "";

            txtBoxSummary.Text += Environment.NewLine;

            AddProcessInfo(sesion);   //Podaj zakres dat czas trwania, nazwę i opis

            txtBoxSummary.Text += Environment.NewLine;

            AddUserInfo(sesion);      //Podaj info kto wykonal ten proces

            txtBoxSummary.Text += Environment.NewLine;

            AddParameterInfo(sesion); //Wyswietl informacje na temat parametrow subprogramow
        }
        //-------------------------------------------------------------------------------
        /**
         * Metoda podaj informacje na temat daty i czasu trwania procesu oraz nazwe i opis
         */
        private void AddProcessInfo(Sesion sesion)
        {
            if (sesion.ProcesParameter != null)
            {
                txtBoxSummary.Text += "Global information." + Environment.NewLine;
                txtBoxSummary.Text += "\tProcess name: \t" + sesion.ProcesName + Environment.NewLine;
                txtBoxSummary.Text += "\tProcess description: \t" + AddTabulatorToEachLine(sesion.ProcesParameter.Description) ;
                txtBoxSummary.Text += "\tProcess information: \t" + AddTabulatorToEachLine(sesion.ProcesParameter.ProcessInformation);

                txtBoxSummary.Text += Environment.NewLine;
                txtBoxSummary.Text += "\tStart of process: \t" + sesion.StartDate.ToString() + Environment.NewLine;
                txtBoxSummary.Text += "\tEnd of process: \t" + sesion.EndDate.ToString() + Environment.NewLine;
                TimeSpan timeProces = sesion.EndDate - sesion.StartDate;
                txtBoxSummary.Text += "\tProcess time: \t\t" + timeProces.Hours.ToString("00") + ":" + timeProces.Minutes.ToString("00") + ":" + timeProces.Seconds.ToString("00") + Environment.NewLine;
            }
            else
            {
                int a = 10;
            }
        }
        //-------------------------------------------------------------------------------
        private string AddTabulatorToEachLine(string lines)
        {
            string txt = string.Empty;
            foreach(string line in lines.Split(new[]{'\r', '\n'}))
            {
                if(line != "")
                {
                    if (txt != string.Empty)
                        txt += "\t\t\t\t" + line;
                    else
                        txt = line;
                    txt += Environment.NewLine;
                }
            }
             return txt;
        }
        //-------------------------------------------------------------------------------
        /**
         * Metoda podaje infomracje na temat usera ktory wykonywal program
         */
        private void AddUserInfo(Sesion sesion)
        {
            if (sesion.ProcesParameter != null)
            {
                txtBoxSummary.Text += "Rank." + Environment.NewLine;
                txtBoxSummary.Text += "\t Operator " + sesion.ProcesParameter.User + Environment.NewLine;
            }
        }
        //-------------------------------------------------------------------------------
        /**
        * Metoda podaje parametry procesu
        */
        private void AddParameterInfo(Sesion sesion)
        {
            txtBoxSummary.Text += "Subprograms list."    + Environment.NewLine;
            int index = 1;
            if (sesion.ProcesParameter != null)
            {
                foreach (SubprogramParameter subPr in sesion.ProcesParameter.SubprogramsPara)
                {
                    txtBoxSummary.Text += "\t" + index++ + "." + Environment.NewLine;
                    txtBoxSummary.Text += "\t Subprogram name: \t\t" + subPr.Name + Environment.NewLine;
                    txtBoxSummary.Text += "\t Subprogram description: \t" + subPr.Description + Environment.NewLine;
                    txtBoxSummary.Text += "\t Subprogram stages: \t" + Environment.NewLine;

                    if (subPr.Pump != null && subPr.Pump.Active)
                        ShowPumpProcesInfo(subPr.Pump);

                    if (subPr.Plasma != null && subPr.Plasma.Active)
                        ShowPlasmaProcesInfo(subPr.Plasma);

                    if (subPr.Gas != null && subPr.Gas.Active)
                        ShowGasProcesInfo(subPr.Gas);

                    if (subPr.Flush != null && subPr.Flush.Active)
                        ShowFlushProcesInfo(subPr.Flush);

                    if (subPr.Vent != null && subPr.Vent.Active)
                        ShowVentProcesInfo(subPr.Vent);

                    if (subPr.Motor != null && subPr.Motor.Active)
                        ShowMotorProcesInfo(subPr.Motor);

                    txtBoxSummary.Text += Environment.NewLine;
                }
            }
        }
        //-------------------------------------------------------------------------------
        private void ShowPumpProcesInfo(PumpProces pump)
        {
            if(pump != null)
            {
                txtBoxSummary.Text += "\t\t Pump stage:" + Environment.NewLine;
                txtBoxSummary.Text += "\t\t\t Pumping down pressure: \t" + pump.GetSetpoint().ToString("F3", System.Globalization.CultureInfo.InvariantCulture) + " [mBar]" +Environment.NewLine;
                txtBoxSummary.Text += "\t\t\t Max pumping down time: \t" + pump.GetTimeWaitForPumpDown().ToString("HH:mm:ss") + Environment.NewLine;      
            }
        }
        //-------------------------------------------------------------------------------
        private void ShowPlasmaProcesInfo(PlasmaProces plasma)
        {
            if (plasma != null)
            {
                txtBoxSummary.Text += "\t\t Plasma stage:" + Environment.NewLine;
                txtBoxSummary.Text += "\t\t\t Time operate: " + plasma.GetTimeOperate().ToString("HH:mm:ss") + Environment.NewLine;
                txtBoxSummary.Text += "\t\t\t Setpoint: \t" + plasma.GetSetpointValue() + " [W] " + Environment.NewLine;
            }
        }
        //-------------------------------------------------------------------------------
        private void ShowGasProcesInfo(GasProces gas)
        {
            if (gas != null)
            {
                txtBoxSummary.Text += "\t\t Gas stage:" + Environment.NewLine;
                txtBoxSummary.Text += "\t\t\t Time operate: " + gas.GetTimeProcesDuration().ToString("HH:mm:ss") + Environment.NewLine;
                if (gas.GetModeProces() == Types.GasProcesMode.FlowSP)
                {
                    txtBoxSummary.Text += "\t\t\t Gas mode: \tFlow Control" + Environment.NewLine;

                    if (gas.GetActiveFlow(1))
                    {
                        txtBoxSummary.Text += "\t\t\t MFC 1:" + Environment.NewLine;
                        txtBoxSummary.Text += "\t\t\t\t Gas: \t\t" + hpt1000.GetGasTypes().GetGasName(gas.GetGasType(1)) + Environment.NewLine;
                        txtBoxSummary.Text += "\t\t\t\t Flow: \t\t" + gas.GetGasFlow(1)   + " [sccm] "  + Environment.NewLine;
                        txtBoxSummary.Text += "\t\t\t\t Flow min: \t" + gas.GetMinGasFlow(1) + " [sccm] " + Environment.NewLine;
                        txtBoxSummary.Text += "\t\t\t\t Flow max: \t" + gas.GetMaxGasFlow(1) + " [sccm] " + Environment.NewLine;
                    }
                    if (gas.GetActiveFlow(2))
                    {
                        txtBoxSummary.Text += "\t\t\t MFC 2:" + Environment.NewLine;
                        txtBoxSummary.Text += "\t\t\t\t Gas: \t\t" + hpt1000.GetGasTypes().GetGasName(gas.GetGasType(2)) + Environment.NewLine;
                        txtBoxSummary.Text += "\t\t\t\t Flow: \t\t" + gas.GetGasFlow(2) + " [sccm] " + Environment.NewLine;
                        txtBoxSummary.Text += "\t\t\t\t Flow min: \t" + gas.GetMinGasFlow(2) + " [sccm] " + Environment.NewLine;
                        txtBoxSummary.Text += "\t\t\t\t Flow max: \t" + gas.GetMaxGasFlow(2) + " [sccm] " + Environment.NewLine;
                    }
                    if (gas.GetActiveFlow(3))
                    {
                        txtBoxSummary.Text += "\t\t\t MFC 3:" + Environment.NewLine;
                        txtBoxSummary.Text += "\t\t\t\t Gas: \t\t" + hpt1000.GetGasTypes().GetGasName(gas.GetGasType(3)) + Environment.NewLine;
                        txtBoxSummary.Text += "\t\t\t\t Flow: \t\t" + gas.GetGasFlow(3) + " [sccm] " + Environment.NewLine;
                        txtBoxSummary.Text += "\t\t\t\t Flow min: \t" + gas.GetMinGasFlow(3) + " [sccm] " + Environment.NewLine;
                        txtBoxSummary.Text += "\t\t\t\t Flow max: \t" + gas.GetMaxGasFlow(3) + " [sccm] " + Environment.NewLine;
                    }
                    if (gas.GetVaporiserActive())
                    {
                        txtBoxSummary.Text += "\t\t\t Vaporiser cycle time: " + gas.GetCycleTime() + " [ms] " + Environment.NewLine;
                        txtBoxSummary.Text += "\t\t\t Vaporiser On time : \t" + gas.GetOnTime() + " [%] " + Environment.NewLine;
                        txtBoxSummary.Text += "\t\t\t Vaporiser dosing: \t" + gas.GetDosing() + " [uL] " + Environment.NewLine;
                    }
                }
                else
                {
                    if (gas.GetModeProces() == Types.GasProcesMode.Presure_MFC)
                        txtBoxSummary.Text += "\t\t\t Gas mode: \tPressure control via MFC" + Environment.NewLine;
                    if (gas.GetModeProces() == Types.GasProcesMode.Pressure_Vap)
                        txtBoxSummary.Text += "\t\t\t Gas mode: \tPressure control via Vaporiser" + Environment.NewLine;

                    txtBoxSummary.Text += "\t\t\t Setpoint: \t\t\t  " + gas.GetSetpointPressure().ToString("F3", System.Globalization.CultureInfo.InvariantCulture) + " [mBar] " + Environment.NewLine;
                    txtBoxSummary.Text += "\t\t\t Minimum pressure devation:  - " + gas.GetMinDeviationPresure().ToString("F3", System.Globalization.CultureInfo.InvariantCulture) + " [mBar] " + Environment.NewLine;
                    txtBoxSummary.Text += "\t\t\t Maximum pressure devation: + " + gas.GetMinDeviationPresure().ToString("F3", System.Globalization.CultureInfo.InvariantCulture) + " [mBar] " + Environment.NewLine;

                    if (gas.GetModeProces() == Types.GasProcesMode.Presure_MFC)
                    {
                        if (gas.GetActiveFlow(1))
                        {
                            txtBoxSummary.Text += "\t\t\t MFC 1:" + Environment.NewLine;
                            txtBoxSummary.Text += "\t\t\t\t Gas: \t\t" + hpt1000.GetGasTypes().GetGasName(gas.GetGasType(1)) + Environment.NewLine;
                            txtBoxSummary.Text += "\t\t\t\t Shared: \t" + gas.GetShareGas(1) + " [%] " + Environment.NewLine;
                            txtBoxSummary.Text += "\t\t\t\t Max devation : " + gas.GetShareDevaition(1) + " [%] " + Environment.NewLine;
                        }
                        if (gas.GetActiveFlow(2))
                        {
                            txtBoxSummary.Text += "\t\t\t MFC 2:" + Environment.NewLine;
                            txtBoxSummary.Text += "\t\t\t\t Gas: \t\t" + hpt1000.GetGasTypes().GetGasName(gas.GetGasType(2)) + Environment.NewLine;
                            txtBoxSummary.Text += "\t\t\t\t Shared: \t" + gas.GetShareGas(2) + " [%] " + Environment.NewLine;
                            txtBoxSummary.Text += "\t\t\t\t Max devation : " + gas.GetShareDevaition(2) + " [%] " + Environment.NewLine;
                        }
                        if (gas.GetActiveFlow(3))
                        {
                            txtBoxSummary.Text += "\t\t\t MFC 3:" + Environment.NewLine;
                            txtBoxSummary.Text += "\t\t\t\t Gas: \t\t" + hpt1000.GetGasTypes().GetGasName(gas.GetGasType(3)) + Environment.NewLine;
                            txtBoxSummary.Text += "\t\t\t\t Shared: \t" + gas.GetShareGas(3) + " [%] " + Environment.NewLine;
                            txtBoxSummary.Text += "\t\t\t\t Max devation : " + gas.GetShareDevaition(3) + " [%] " + Environment.NewLine;
                        }
                    }
                    if (gas.GetModeProces() == Types.GasProcesMode.Pressure_Vap)
                    {
                        txtBoxSummary.Text += "\t\t\t Gas mode: \tPressure control via Vaporiser" + Environment.NewLine;
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------
        private void ShowFlushProcesInfo(FlushProces flush)
        {
            if (flush != null)
            {
                txtBoxSummary.Text += "\t\t Flush stage:" + Environment.NewLine;
                txtBoxSummary.Text += "\t\t\t Time flush: \t" + flush.GetTimePurge().ToString("HH:mm:ss") + Environment.NewLine;
            }
        }
        //-------------------------------------------------------------------------------
        private void ShowVentProcesInfo(VentProces vent)
        {
            if (vent != null)
            {
                txtBoxSummary.Text += "\t\t Vent stage:" + Environment.NewLine;
                txtBoxSummary.Text += "\t\t\t Time vent: \t" + vent.GetTimeVent().ToString("HH:mm:ss") + Environment.NewLine;
            }
        }
        //-------------------------------------------------------------------------------
        private void ShowMotorProcesInfo(MotorProces motor)
        {
            if (motor != null)
            {
                txtBoxSummary.Text += "\t\t Motor stage:" + Environment.NewLine;
                txtBoxSummary.Text += "\t\t\t Motor 1 time operate: \t" + motor.GetTimeMotor(1).ToString("HH:mm:ss") + Environment.NewLine;
                txtBoxSummary.Text += "\t\t\t Motor 2 time operate: \t" + motor.GetTimeMotor(2).ToString("HH:mm:ss") + Environment.NewLine;
            }
        }
        //-------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest sprawdzenie czy rozszerzenie podane jako nazwa pliku jest poprawne
         */ 
        void CheckIfFileHasCorrectExtension(object sender, CancelEventArgs e)
        {
            SaveFileDialog sv = (sender as SaveFileDialog);
            if (sv.DefaultExt == "pdf")
            {
                if (Path.GetExtension(sv.FileName).ToLower() != ".pdf")
                {
                    e.Cancel = true;
                    MessageBox.Show("Please omit the extension or use 'pdf'");
                    return;
                }
            }
            if (sv.DefaultExt == "csv")
            {
                if (Path.GetExtension(sv.FileName).ToLower() != ".csv")
                {
                    e.Cancel = true;
                    MessageBox.Show("Please omit the extension or use 'csv'");
                    return;
                }
            }
        }

        private void brnRemoveData_Click(object sender, EventArgs e)
        {
            DialogResult res = removeForm.ShowDialog();
            if (res == DialogResult.OK)
            {
                
            }
        }
        //-------------------------------------------------------------------------------
    }
}

