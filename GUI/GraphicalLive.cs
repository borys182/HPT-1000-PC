using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using HPT1000.Source.Chamber;
using HPT1000.Source.Driver;

namespace HPT1000.GUI
{
    /*Panel jest odpowiedzialny za rysowanie aktualnych danych na wykresie liniowym
    Wykres powinien zawierac:
        - pokaz danych z danego okna
        - automatyczne przesuwanie sie wykresu z nowymi danymi
        - mozliwosc zobaczenia danych spoza danego okna
        - zoom
    */
    public partial class GraphicalLive : UserControl
    {
        Dictionary<Series, Color> colorSeries = new Dictionary<Series, Color>();
        //Serie danych
        Series seriePressure = new Series("Pressure: [mBar]");
        Series seriePower    = new Series("Power: [W]");
        Series serieVoltage  = new Series("Voltage: [V]");
        Series serieCurent   = new Series("Curent: [A]");
        Series serieFlow1    = new Series("Gas flow 1: [sccm]");
        Series serieFlow2    = new Series("Gas flow 2: [sccm]");
        Series serieFlow3    = new Series("Gas flow 3: [sccm]");
        Series serieMotor1   = new Series("Motor 1");
        Series serieMotor2   = new Series("Motor 2");
        Series serieDosingValve = new Series("Dosing valve");
        //Seria danych kopiowanych - umozliwiaja prezentacje danych na kilku osiach Y
        Series serieCopyPressure    = new Series("Pressure Copy: [mBar]");
        Series serieCopyPower       = new Series("Power Copy: [%]");
        Series serieCopyFlow1       = new Series("Gas flow 1 Copy: [sccm]");
        Series serieCopyFlow2       = new Series("Gas flow 2 Copy: [sccm]");
        Series serieCopyFlow3       = new Series("Gas flow 3 Copy: [sccm]");
        Series serieCopyDosingValve = new Series("Dosing valve Copy");

        //wskazniki obiektow z ktorych czytane sa dane
        PressureControl presure         = null;
        ForePump        forePump        = null;
        MFC             mfc             = null;
        PowerSupplay    powerSupplay    = null;
        Vaporizer       vaporizer       = null;
        MotorDriver     motor1          = null;
        MotorDriver     motor2          = null;

        //wskaznik na forme jest potrzebny do wyliczania poprawnej pozycji kursora na formatce
        Form mainForm = null;

        //Zmienne pomocnioecze przy przesuwanieu wykresu. Okreslaja poczatkowe klikniecie myszka w wykres podczas przesuwania
        Point scrollStartPoint;
        bool overRangeAxisX = false;    //falga okresla ze wykres wyszedl poza max wartosc osi X
        //Mamy mozliwosc prezentacji danych zbiorczych na jednym wykresie lub podzilonych na trzy wykresy
        ChartArea chartAreaPressure = null;
        ChartArea chartAreaPower    = null;
        ChartArea chartAreaFlow     = null;
        ChartArea chartAreaMotor    = null;
        ChartArea chartAreaDosingValve = null;

        ChartArea chartLabelAreaPower    = null;        //Zmienna jest wykrozystywana do pokazywania wartoscu osi Y dla wykresu mocy
        ChartArea chartLabelAreaFlow     = null;        //Zmienna jest wykrozystywana do pokazywania wartoscu osi Y dla wykresu mocy
        ChartArea chartLabelAreaDosingValve = null;        //Zmienna jest wykrozystywana do pokazywania wartoscu osi Y dla wykresu mocy
    
        //Flagi automatycznego scrolowania danego wykresu
        bool autoScroll    = true;
   
        DateTime startTimeMesure    = DateTime.Now;

        //-------------------------------------------------------------------------------
        public Form MainForm
        {
            set { mainForm = value; }
        }
        //-------------------------------------------------------------------------------
        /**
         * Konstruktor klasy
         */ 
        public GraphicalLive()
        {
            InitializeComponent();
            PrepareColorToSeries(); //Metoda tworzy slownk gdzie kazdej seri jest przyparzadkowany jeden kolor
            ConfigChart();          //Metoda konfigurje wykres
            ClearChart();           //Wycztsc wykres ze zbednych danych
            LoadBitmap();           //Zaladuj obrazki do buttonow wykresu
            PrepareColorChartArea();
        }
        //-------------------------------------------------------------------------------
        /**
         * Zadaniem metody jet przypozadkowanie kazdej z seri koloru. Wazne aby to utworzyc w liscie ktora umozliwi w dowolnej chwili odwolanie sie do danego kooru danej seri
         */
        private void PrepareColorToSeries()
        {
            colorSeries.Add(seriePressure, Color.Blue);
            colorSeries.Add(seriePower,    Color.Red);
            colorSeries.Add(serieVoltage,  Color.Green);
            colorSeries.Add(serieCurent,   Color.DeepPink);
            colorSeries.Add(serieFlow1,    Color.DarkOrange);
            colorSeries.Add(serieFlow2,    Color.DarkViolet);
            colorSeries.Add(serieFlow3,    Color.DeepSkyBlue);
            colorSeries.Add(serieMotor1,      Color.Blue);
            colorSeries.Add(serieMotor2,      Color.Red);
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
        /**
         * Zaladowanie bitmapy do przyciku kasujacego dane wartosci z wykresu
         */
        private void LoadBitmap()
        {
            Bitmap clearIcone = new Bitmap(Properties.Resources.ClearChart);
            clearIcone.MakeTransparent(Color.White);

            toolStripBtnClear.Image = clearIcone;
        }
        //-------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie przygotowanie konfiguracji seri i wykresu
         */ 
        private void ConfigChart()
        {
            //Usun serie utworzone na potrzeby wizualizacja charta w formie
             chart.Series.Clear();

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
            chart.Series.Add(serieCopyPressure);
            chart.Series.Add(serieCopyPower);
            chart.Series.Add(serieCopyFlow1);
            chart.Series.Add(serieCopyFlow2);
            chart.Series.Add(serieCopyFlow3);
            chart.Series.Add(serieCopyDosingValve);

            //Ustaw grubosvc  lini dla wszystkich serii na jeden rozmiar
            foreach (Series serie in chart.Series)
            {
                serie.BorderWidth = 1;
                serie.ChartType = SeriesChartType.Line;
                serie.XValueType = ChartValueType.DateTime;         //Ustaw typ serii jako czas
                //Ustaw kolory serii poprzez wyciagniece koloru z listy jaki zostal przyparzadkowany dla danej serii
                Color color;
                if (colorSeries.TryGetValue(serie, out color))
                    serie.Color = color;
            }
            serieCopyPressure.Color = seriePressure.Color;
            serieCopyPower.Color    = seriePower.Color;
            serieCopyFlow1.Color    = serieFlow1.Color;
            serieCopyFlow2.Color    = serieFlow2.Color;
            serieCopyFlow3.Color    = serieFlow3.Color;

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

            //Inicjalizacja ChartArea danych wykresow
            chartAreaPressure = chart.ChartAreas.FindByName("ChartAreaPressure");
            chartAreaMotor    = chart.ChartAreas.FindByName("ChartAreaMotor");

            //ustaw widocznosc serii tak jak sa poustawiane checkboxy
            cBox_CheckedChanged(null,EventArgs.Empty);

            //Utworz kilka osi Y
            ConfigMultipleYAxis();

            //Ustaw parametry zoom dla wykresow - wylaczam mozliwosc zomma
            foreach (ChartArea chartArea in chart.ChartAreas)
            {
                chartArea.AxisX.LabelStyle.Format = "HH:mm:ss";                     //wyswietlaj wartosci osi X jako godzina w postaci 22:10:04
                chartArea.AxisX.IntervalType      = DateTimeIntervalType.Seconds;   //ustaw interwal na osi X jako sekunda
                //Wlacz mozliwosc zoom dla obu osi X i Y
                chartArea.CursorX.IsUserSelectionEnabled = false;
                chartArea.CursorY.IsUserSelectionEnabled = false;
                chartArea.CursorX.IsUserEnabled = false;
                chartArea.CursorY.IsUserEnabled = false;
                chartArea.AxisX.ScaleView.Zoomable = false;
                chartArea.AxisY.ScaleView.Zoomable = false;
                //Ustaw min wartos do jakiej mozna zomowac os X
                chartArea.CursorX.Interval = 1;
                chartArea.CursorX.IntervalType = DateTimeIntervalType.Seconds;
                chartArea.AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;         
            }
            //Ustaw format danych dla kolejnych wykresow
            chartAreaPressure.AxisY.LabelStyle.Format           = "0.###E+0";

            chartAreaPower.AxisY.LabelStyle.Format              = "#";
            chartLabelAreaPower.AxisY.LabelStyle.Format         = "#";

            chartAreaFlow.AxisY.LabelStyle.Format               = "#";
            chartLabelAreaFlow.AxisY.LabelStyle.Format          = "#";

            chartAreaMotor.AxisY.LabelStyle.Format              = "#";

            chartAreaDosingValve.AxisY.LabelStyle.Format        = "#";
            chartLabelAreaDosingValve.AxisY.LabelStyle.Format   = "#";

            //Ustaw wybrane wykresy logarytmicznie
            chartAreaPressure.AxisY.IsLogarithmic   = true;
            chartAreaFlow.AxisY.IsLogarithmic       = true;
            chartLabelAreaFlow.AxisY.IsLogarithmic  = true;

            //Zaremuj markera - teraz to podaje w inny sposob - jako znainczik na punkcie 0
            //   ConfigSeriesMarker();

            //Ustawiam wartosci dla wykresu mocy wyrazonej w procentach
            chartLabelAreaPower.AxisY.Minimum = 0;
            chartLabelAreaPower.AxisY.Maximum = 1000;
       //     chartLabelAreaPower.AxisY.Interval = 10;
       //     chartLabelAreaPower.AxisY.MajorGrid.Interval = 10;

        }
        //-------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie skonfigurowanie wykresu aby prezentowac dane dla roznych osi Y przy jednej osi X 
         */
        private void ConfigMultipleYAxis()
        {         
            // Ustaw wykres w zadanej pozycji - glownym wykresem jest chartAreaPressure
            chartAreaPressure.Position          = new ElementPosition(27, 13, 69, 88);
            chartAreaPressure.InnerPlotPosition = new ElementPosition(10, 0, 90, 90);

            chartAreaMotor.Position             = new ElementPosition(27, 3, 69, 8);
            chartAreaMotor.InnerPlotPosition    = new ElementPosition(10, 0, 90, 90);

            seriePressure.ChartArea = chartAreaPressure.Name;
            serieMotor1.ChartArea   = chartAreaMotor.Name;
            // Utworz dodatkowe osi Y dla mocy oraz przeplywu
            chartAreaPower       = CreateDataYAxis(chartAreaPressure, seriePower);
            chartAreaFlow        = CreateDataYAxis(chartAreaPressure, serieFlow1);
            chartAreaDosingValve = CreateDataYAxis(chartAreaMotor, serieDosingValve);
            //Dodaj kolejne serie danych przeplywek do wykresu przeplywu
            serieFlow2.ChartArea = chartAreaFlow.Name;
            serieFlow3.ChartArea = chartAreaFlow.Name;
            serieMotor2.ChartArea = chartAreaMotor.Name;
            //Utworz przestrzen wykresu odpowiedzilna za prezentacje wartosci osi Y
            chartLabelAreaPower       = CreateLabelYAxis(chartAreaPressure, serieCopyPower, 15, 1, "Power [W]");
            chartLabelAreaFlow        = CreateLabelYAxis(chartAreaPressure, serieCopyFlow1, 27, 1, "Flow [sccm]");
            chartLabelAreaDosingValve = CreateLabelYAxis(chartAreaMotor, serieCopyDosingValve, 15, 1, "Valve");
            //Dodaj kolejne serie przpelywu do wykresy osi Y odpowiedzialnej za prezentacje wartosci na osi Y
            serieCopyFlow2.ChartArea = chartLabelAreaFlow.Name;
            serieCopyFlow3.ChartArea = chartLabelAreaFlow.Name;
            serieCopyPressure.ChartArea = chartAreaPressure.Name;
            //Ustaw charta dla valve tak jak dla motora
            chartAreaDosingValve.AxisY.Minimum  = 0;
            chartAreaDosingValve.AxisY.Maximum  = 1;
            chartAreaDosingValve.AxisY.Interval = 1;

            chartLabelAreaDosingValve.AxisY.Minimum     = 0;
            chartLabelAreaDosingValve.AxisY.Maximum     = 1;
            chartLabelAreaDosingValve.AxisY.Interval    = 1;
        }
        //-------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest dodanie do wykresu dodatkowej osi Y. Poniewaz nie da sie dodac kilka osi Y do jednego wykresu ale da sie dodac kilka odrebnych wykresow do jednego dlatego też
         * jest tworzonych kilka wykresow i nakladane jeden na drugi. Poprzez ustawienia przesuniecia oraz posuwania odpowidnich lini tworzy to efekt kilku osi Y
         */
        public ChartArea CreateDataYAxis(ChartArea mainArea, Series series)
        {
            // Utworz przestrzen wykresu dla podanej serii
            ChartArea areaSeries                    = chart.ChartAreas.Add("ChartArea_" + series.Name);
            areaSeries.BackColor                    = Color.Transparent;
            areaSeries.BorderColor                  = Color.Transparent;
            areaSeries.AxisX.MajorGrid.Enabled      = false;
            areaSeries.AxisX.MajorTickMark.Enabled  = false;
            areaSeries.AxisX.LabelStyle.Enabled     = false;
            areaSeries.AxisY.MajorGrid.Enabled      = false;
            areaSeries.AxisY.MajorTickMark.Enabled  = false;
            areaSeries.AxisY.LabelStyle.Enabled     = false;
            areaSeries.AxisX.IsMarginVisible        = mainArea.AxisX.IsMarginVisible;
            areaSeries.AxisY.IsStartedFromZero      = mainArea.AxisY.IsStartedFromZero;
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
            ChartArea areaAxis                   = chart.ChartAreas.Add("AxisY_" + series.Name);
            areaAxis.BackColor                   = Color.Transparent;
            areaAxis.BorderColor                 = Color.Transparent;
            areaAxis.AxisX.LineWidth             = 0;
            areaAxis.AxisX.MajorGrid.Enabled     = false;
            areaAxis.AxisX.MajorTickMark.Enabled = false;
            areaAxis.AxisX.LabelStyle.Enabled    = false;
            areaAxis.AxisY.MajorGrid.Enabled     = false;
            areaAxis.AxisY.IsStartedFromZero     = mainArea.AxisY.IsStartedFromZero;
            areaAxis.AxisY.LabelStyle.Font       = mainArea.AxisY.LabelStyle.Font;
            areaAxis.Position.FromRectangleF(mainArea.Position.ToRectangleF());
            areaAxis.InnerPlotPosition.FromRectangleF(mainArea.InnerPlotPosition.ToRectangleF());
            areaAxis.AxisY.Title = name;
            areaAxis.AxisY.TitleAlignment = StringAlignment.Near;
            // Ukryj serie
            series.IsVisibleInLegend        = false;
        //    series.Color                  = Color.Transparent;
        //    series.BorderColor            = Color.Transparent;
            series.ChartArea                = areaAxis.Name;

            // Ustaw os Y w odpowiednij pozycji
            areaAxis.Position.X          -= axisOffset;
            areaAxis.InnerPlotPosition.X += labelsSize;
            areaAxis.Position.Width      = 96 - areaAxis.Position.X;
       
            return areaAxis;
        }
        //-------------------------------------------------------------------------------
        //Kasuj dane z wykresu. Wszystkie punkty wykresu zostaja wykasowane
        public void ClearChart()
        {
            foreach (Series serie in chart.Series)
                serie.Points.Clear();
            //Ustawiam widok charta od 0 - przesuniecie wykresu do poczatku ukladu wspolrzednych
            if (chartAreaPressure != null)
            {
                chartAreaPressure.AxisX.Minimum = 0;
                chartAreaPressure.AxisX.Maximum = 0;
                chartAreaPressure.AxisX.ScaleView.Scroll(0.0);
            }
            if (chartAreaPower != null)
            {
                chartAreaPower.AxisX.Minimum = 0;
                chartAreaPower.AxisX.Maximum = 0;
                chartAreaPower.AxisY.Maximum = 1;
                chartAreaPower.AxisX.ScaleView.Scroll(0.0);
            }
            if (chartAreaFlow != null)
            {
                chartAreaFlow.AxisX.Minimum = 0;
                chartAreaFlow.AxisX.Maximum = 0;
                chartAreaFlow.AxisX.ScaleView.Scroll(0.0);
            }
            if (chartAreaMotor != null)
            {
                chartAreaMotor.AxisX.Minimum = 0;
                chartAreaMotor.AxisX.Maximum = 0;
                chartAreaMotor.AxisX.ScaleView.Scroll(0.0);
            }
            if (chartAreaDosingValve!= null)
            {
                chartAreaDosingValve.AxisX.Minimum = 0;
                chartAreaDosingValve.AxisX.Maximum = 0;
                chartAreaDosingValve.AxisX.ScaleView.Scroll(0.0);
            }
            startTimeMesure = DateTime.Now;
            //Dodaje do kazdej seri punktu wartosci z aktualna wartoscia aby wykresy sie nie chowaly
            InitSeriePoint();
        }
        //-------------------------------------------------------------------------------
        /*
		*Metoda ma za zadanie inicjalizacja seri punktami 0,0 wymuszajac przez to rysowanie sie wykresow
		*/
        private void InitSeriePoint()
        {        
            UpdateData();
            
              if (serieMotor1.Points.Count == 0)          serieMotor1.Points.AddXY(0, 0.1);
              if (serieCopyDosingValve.Points.Count == 0) serieCopyDosingValve.Points.AddXY(0, 0);
              if (serieCopyPower.Points.Count == 0)       serieCopyPower.Points.AddXY(0, 0);
              if (seriePressure.Points.Count == 0)        seriePressure.Points.AddXY(/*DateTime.MinValue.ToOADate()*/0, 0.1);
              if (serieCopyFlow1.Points.Count == 0)       serieCopyFlow1.Points.AddXY(0, 0.1);
       
        }    
        //-------------------------------------------------------------------------------
        //Aktualizacja danych na wykresie. Funkcja wywolywana z watku glownego aplikacji po odczytaniuy danych z PLC
        public void UpdateData()
        {
            //Odczytaj dane z danych obiektow i dodaj jako punkty do konkretnej serii
            DateTime timeMesure = new DateTime(DateTime.Now.Ticks - startTimeMesure.Ticks);
            //Z uwagi na fakt ze os moze byc logarytmiczna to nie dodawaj wartosci 0 lub mniejszych od zera
            if (presure != null)
            {
                double pressureV = presure.GetPressure();
                if (chartAreaPressure.AxisY.IsLogarithmic && pressureV <= 0)
                    pressureV = 0.00001;
                seriePressure.Points.AddXY(timeMesure, pressureV);

            }
            if (powerSupplay != null)
            {
                double power = powerSupplay.Power;

                if (chartAreaPower.AxisY.IsLogarithmic && power <= 0)
                    power = 0.00001;
                seriePower.Points.AddXY(timeMesure, power);
                //Z uwagi na brak wyswietlania wartosci dla produ i napiecia  nie aktualizuj takze ich punktow dla seri
                //serieVoltage.Points.AddXY(timeMesure, powerSupplay.Voltage);
                //serieCurent.Points.AddXY(timeMesure, powerSupplay.Curent);         
            }
            if (mfc != null)
            {
                double flow = mfc.GetActualFlow(1, Types.UnitFlow.sccm);
                if (chartAreaFlow.AxisY.IsLogarithmic && flow <= 0)
                    flow = 0.1;
                serieFlow1.Points.AddXY(timeMesure, flow);

                flow = mfc.GetActualFlow(2, Types.UnitFlow.sccm);
                if (chartAreaFlow.AxisY.IsLogarithmic && flow <= 0)
                    flow = 0.1;
                serieFlow2.Points.AddXY(timeMesure, flow);

                flow = mfc.GetActualFlow(3, Types.UnitFlow.sccm);
                if (chartAreaFlow.AxisY.IsLogarithmic && flow <= 0)
                    flow = 0.1;
                serieFlow3.Points.AddXY(timeMesure, flow);
            }
            //Poniewaz stan motora i zaworu musi generowac wykres prostokatny dlatego dodaje zawsze dwa punkty
            int value = 0;
            if (motor1 != null)
            {
                int stateMotor1 = 0;
                if (motor1.State == Types.StateFP.ON) stateMotor1 = 1;
                //Sprawdz czy stan sie nie zmienil jezeli tak to dodaj punkt aby usuzkac wykres prostakatny
                 if(IsPointChange(serieMotor1, stateMotor1, out value))serieMotor1.Points.AddXY(timeMesure, value);
                 serieMotor1.Points.AddXY(timeMesure.ToOADate(), stateMotor1);
            }
            if (motor2 !=null)
            {
                int stateMotor2 = 0;
                if (motor2.State == Types.StateFP.ON) stateMotor2 = 1;
                //Sprawdz czy stan sie nie zmienil jezeli tak to dodaj punkt aby usuzkac wykres prostakatny
                if (IsPointChange(serieMotor2, stateMotor2, out value)) serieMotor2.Points.AddXY(timeMesure, value);
                serieMotor2.Points.AddXY(timeMesure.ToOADate(), stateMotor2);
            }
            if (vaporizer != null)
            {
                int stateVapor = 0;
                if (vaporizer.State == Types.StateValve.Open) stateVapor = 1;
                //Sprawdz czy stan sie nie zmienil jezeli tak to dodaj punkt aby usuzkac wykres prostakatny
                if (IsPointChange(serieDosingValve, stateVapor,out value)) serieDosingValve.Points.AddXY(timeMesure, value);
                serieDosingValve.Points.AddXY(timeMesure.ToOADate(), stateVapor);
            }            

            //Wywolaj funkcje skalujace wykres, to znaczy przesuwajace okno zgodnie z nadchodzacymi nowymi wartosciami do wartosci window size
            ScaleChartToWindowSize(chartAreaPressure, seriePressure);
            ScaleChartToWindowSize(chartAreaPower, seriePower);
            ScaleChartToWindowSize(chartAreaFlow,  serieFlow1);
            ScaleChartToWindowSize(chartAreaMotor, serieMotor1);
            ScaleChartToWindowSize(chartAreaDosingValve, serieDosingValve);
            //Pokaz serie danych w zaleznosci od aktywnych przeplywek
            HideNoActiveSerieMFC();
            HideNoActiveSerieValve();
            //Aktualizuj dane dla seri bedacych kopia sluzaca tylko prezetnacji wartosci na osiach Y
            UpdateCopyData(timeMesure);
            //Ustaw marker do aktulnek wartosci
            //     ShowCurrentMarker();
        }
        //-------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie sprawdzenie czy ostatni punkt seri jest rozny od nowo dodanego
         */
        private bool IsPointChange(Series serie,int newValue, out int value)
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
        /**
         * Zadaniem metody jest wyswietlenie znacnzika aktulnej wartosci dla koljnych seri
         */
        private void ShowCurrentMarker()
        {
            //Ustaw marker na akutalnej wartosci
            if (chartAreaPressure != null && presure != null)
                chartAreaPressure.CursorY.Position = presure.GetPressure();

            if (chartLabelAreaPower != null && powerSupplay != null)
                chartLabelAreaPower.CursorY.Position = powerSupplay.Power;

            if (chartAreaFlow != null && chartLabelAreaFlow != null && mfc != null)
            {
                if (cBoxFlow3.Checked && cBoxFlow3.Visible)
                {
                    chartLabelAreaFlow.CursorY.Position  = mfc.GetActualFlow(3, Types.UnitFlow.sccm);
                    chartLabelAreaFlow.CursorY.LineColor = serieFlow3.Color;
                }

                if (cBoxFlow2.Checked && cBoxFlow2.Visible)
                {
                    chartLabelAreaFlow.CursorY.Position = mfc.GetActualFlow(2, Types.UnitFlow.sccm);
                    chartLabelAreaFlow.CursorY.LineColor = serieFlow2.Color;
                }
                if (cBoxFlow1.Checked && cBoxFlow1.Visible)
                {
                    chartLabelAreaFlow.CursorY.Position  = mfc.GetActualFlow(1, Types.UnitFlow.sccm);
                    chartLabelAreaFlow.CursorY.LineColor = serieFlow1.Color;
                }
            }

            //Schowaj marker gdy seria jest wylaczona
            if (cBoxPressure.Checked)
                chartAreaPressure.CursorY.LineDashStyle = ChartDashStyle.Dot;
            else
                chartAreaPressure.CursorY.LineDashStyle = ChartDashStyle.NotSet;

            if (cBoxPower.Checked)
                chartLabelAreaPower.CursorY.LineDashStyle = ChartDashStyle.Dot;
            else
                chartLabelAreaPower.CursorY.LineDashStyle = ChartDashStyle.NotSet;

            if ((cBoxFlow1.Checked && cBoxFlow1.Visible) || (cBoxFlow2.Checked && cBoxFlow2.Visible) || (cBoxFlow3.Checked && cBoxFlow3.Visible))
                chartLabelAreaFlow.CursorY.LineDashStyle = ChartDashStyle.Dot;
            else
                chartLabelAreaFlow.CursorY.LineDashStyle = ChartDashStyle.NotSet;
        }
        //-------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest konfigruacja markera dla kolejnych serii
         */
        private void ConfigSeriesMarker()
        {
            if (chartAreaPressure != null)
            {
                chartAreaPressure.CursorY.SelectionStart    = 0;
                chartAreaPressure.CursorY.SelectionEnd      = 2000;
                chartAreaPressure.CursorY.LineDashStyle     = ChartDashStyle.Dot;
                chartAreaPressure.CursorY.LineColor         = seriePressure.Color;
                chartAreaPressure.CursorY.SelectionColor    = Color.Transparent;
            }

            if (chartAreaPower != null)
            {
                chartAreaPower.CursorY.SelectionStart       = 0;
                chartAreaPower.CursorY.SelectionEnd         = 10000;
                chartAreaPower.CursorY.LineDashStyle        = ChartDashStyle.Dot;
                chartAreaPower.CursorY.LineColor            = seriePower.Color;
                chartAreaPower.CursorY.SelectionColor       = Color.Transparent;
            }
            if (chartLabelAreaPower != null)
            {
                chartLabelAreaPower.CursorY.SelectionStart = 0;
                chartLabelAreaPower.CursorY.SelectionEnd = 10000;
                chartLabelAreaPower.CursorY.LineDashStyle = ChartDashStyle.Dot;
                chartLabelAreaPower.CursorY.LineColor = seriePower.Color;
                chartLabelAreaPower.CursorY.SelectionColor = Color.Transparent;
            }

            if (chartAreaFlow != null)
            {
                chartAreaFlow.CursorY.SelectionStart        = 0;
                chartAreaFlow.CursorY.SelectionEnd          = 10000;
                chartAreaFlow.CursorY.LineDashStyle         = ChartDashStyle.Dot;
                chartAreaFlow.CursorY.LineColor             = serieFlow1.Color;
                chartAreaFlow.CursorY.SelectionColor        = Color.Transparent;
            }
           if (chartLabelAreaFlow != null)
            {
                chartLabelAreaFlow.CursorY.SelectionStart = 0;
                chartLabelAreaFlow.CursorY.SelectionEnd = 10000;
                chartLabelAreaFlow.CursorY.LineDashStyle = ChartDashStyle.Dot;
                chartLabelAreaFlow.CursorY.LineColor = serieFlow1.Color;
                chartLabelAreaFlow.CursorY.SelectionColor = Color.Transparent;
            }
        }
        //-------------------------------------------------------------------------------
        /**
        *   Funkcja ma za zadanie przedstawienie na ekranie tylko zadanej liczby probek dla danego wykresu (zgodnie z parametrem window size)
        */
        private void ScaleChartToWindowSize(ChartArea chartArea, Series serie)
        {
            if (chartArea != null && Source.Factory.Hpt1000 != null)
            {
                //Jezeli wartosc na X jest juz wieksza niz widow size to przesuwaj wykres
                if (serie.Points.Count > 0 && Source.Factory.Hpt1000 != null)
                {
                    //Jezeli jest wlaczone autoscrolowanie to przesuwaj wykres do najnowszych wartosci
                    if (autoScroll)
                    {
                        double sizeWindow = Source.Factory.Hpt1000.ChartWindowTime.ToOADate();  //Odczytasj wartosc okna danych jakie maja byc prezentowane na wykresie
                        DataPoint aMaxX = serie.Points.FindMaxByValue("X", 0);
                        if (aMaxX.XValue > Source.Factory.Hpt1000.ChartWindowTime.ToOADate())
                        {
                            chartArea.AxisX.Maximum = aMaxX.XValue;
                            if (chartArea.AxisX.Maximum > chartArea.AxisX.Minimum + sizeWindow)
                                chartArea.AxisX.Minimum = chartArea.AxisX.Maximum - sizeWindow;
                        }
                        else
                            chartArea.AxisX.Maximum = sizeWindow;
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------
        /**
         * Dodaj wartosci do seri odpowiedzialnych tylko za prezetnacje wartosic na osiach Y
         */
        private void UpdateCopyData(DateTime timeMesure)
        {
            //Dodaj punkt do seri odpowieidzlanej za prezentacje wartosci na osi Y. Os Y jest wykorzystywana do pokazywania zakresu wartosci jak tez znacznika ostatnio dodanej wartosci

            //Ustaw odpowiednie zalkresy osi X i Y wykresow etykie osi Y
            //Ustaw max moc dla osi mocy. Poniewaz moze sie zdarzyc ze moc wyjdzie poza zakres max wartosci dlatego w takim przypadku pamoietam najwieksza wartosc dla zakresu
            if (powerSupplay != null)
            {
                if(chartAreaPower.AxisY.Maximum < powerSupplay.MaxPower)
                    chartAreaPower.AxisY.Maximum = powerSupplay.MaxPower;

                int currentPower = (int)(powerSupplay.Power + 10);
                if (currentPower > chartAreaPower.AxisY.Maximum )
                    chartAreaPower.AxisY.Maximum = currentPower;
            }
            chartLabelAreaPower.AxisY.Maximum = chartAreaPower.AxisY.Maximum;
            chartLabelAreaPower.AxisX.Minimum = 0;
            chartLabelAreaPower.AxisX.Maximum = 1000;

            chartLabelAreaFlow.AxisY.Maximum    = Math.Pow(10, chartAreaFlow.AxisY.ScaleView.ViewMaximum);
            chartLabelAreaFlow.AxisY.Minimum    = Math.Pow(10, chartAreaFlow.AxisY.ScaleView.ViewMinimum);
            chartLabelAreaFlow.AxisX.Minimum    = 0;
            chartLabelAreaFlow.AxisX.Maximum    = 1000;

            //Ustaw punkt 0 na wartosc ostatnio odcyztna z urzadzenia oraz ustaw dla tego punktu znaicnzik
            if (presure != null)
            {
                double pressureV = presure.GetPressure();
                if (pressureV <= 0)                         pressureV = 0.0001;
                if (serieCopyPressure.Points.Count == 0)    serieCopyPressure.Points.AddXY(0, pressureV);
                serieCopyPressure.Points[0].SetValueXY(chartAreaPressure.AxisX.Minimum , pressureV);
            }
            if (powerSupplay != null)
            {
                double power = powerSupplay.Power;
                if (power <= 0) power = 0.0001;
                if (serieCopyPower.Points.Count == 0)   serieCopyPower.Points.AddXY(0, power);
                serieCopyPower.Points[0].SetValueY(power);
            }
            if (mfc != null)
            {
                //Dodaj punkt do seri odpowieidzlanej za prezentacje wartosci na osi Y. Poniewaz interesuje nas tylko max wartosc dlatego dodajemy tylko punkt gdy jesty wiekszy niz aktulany max
                double flow = mfc.GetActualFlow(1, Types.UnitFlow.sccm);
                if (flow <= 0)                          flow = 0.1;
                if (serieCopyFlow1.Points.Count == 0)   serieCopyFlow1.Points.AddXY(0, flow);
                serieCopyFlow1.Points[0].SetValueY(flow);

                flow = mfc.GetActualFlow(2, Types.UnitFlow.sccm);
                if (flow <= 0)                          flow = 0.1;
                if (serieCopyFlow2.Points.Count == 0)   serieCopyFlow2.Points.AddXY(0, flow);
                serieCopyFlow2.Points[0].SetValueY(flow);

                flow = mfc.GetActualFlow(3, Types.UnitFlow.sccm);
                if (flow <= 0)                          flow = 0.1;
                if (serieCopyFlow3.Points.Count == 0)   serieCopyFlow3.Points.AddXY(0, flow);
                serieCopyFlow3.Points[0].SetValueY(flow);
            }

            //Dodaj punkt do seri odpowieidzlanej za prezentacje wartosci na osi Y. Poniewaz interesuje nas tylko max wartosc dlatego dodajemy tylko punkt gdy jesty wiekszy niz aktulany max
            if (vaporizer != null)
            {
                int stateVapor = 0;
                if (vaporizer.State == Types.StateValve.Open) stateVapor = 1;
                if (serieCopyDosingValve.Points.Count == 0)
                   serieCopyDosingValve.Points.AddXY(timeMesure, stateVapor);
            }
            SetMarkerGraphic(serieCopyPressure);
            SetMarkerGraphic(serieCopyPower);
            SetMarkerGraphic(serieCopyFlow1);
            SetMarkerGraphic(serieCopyFlow2);
            SetMarkerGraphic(serieCopyFlow3);
        }
        //-------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie ustawienie wizualizacji markera dla punktu kotrey jest trkaowany jako wskaznik aktualnej waetosci na osi Y
         */
        private void SetMarkerGraphic(Series serie)
        {
            if (serie != null && serie.Points.Count > 0)
            {
                serie.Points[0].MarkerStyle         = MarkerStyle.Square;
                serie.Points[0].MarkerSize          = 5;
            }
        }
        //-------------------------------------------------------------------------------
        //Funkcja ustawia widocznosc wykresow zgodnie z wybranymi preferenacjimi. Widok danych albo na jednym albo na trzech wykreseach
        private void ShowDataOnChart(bool togheter)
        {
            /*
            if (chartAreaPressure != null && chartAreaPower != null && chartAreaMFC != null)
            {
                if (togheter)
                {
                    chartAreaPressure.AxisY.Title = "value";
                    chartAreaPower.Visible        = false;
                    chartAreaMFC.Visible          = false;

                    seriePressure.ChartArea = "ChartAreaPressure";
                    seriePower.ChartArea    = "ChartAreaPressure";
                    serieVoltage.ChartArea  = "ChartAreaPressure";
                    serieCurent.ChartArea   = "ChartAreaPressure";
                    serieFlow1.ChartArea    = "ChartAreaPressure";
                    serieFlow2.ChartArea    = "ChartAreaPressure";
                    serieFlow3.ChartArea    = "ChartAreaPressure";
                }
                else
                {
                    chartAreaPressure.AxisY.Title = "pressure [mBar]";
                    chartAreaPower.Visible        = true;
                    chartAreaMFC.Visible          = true;

                    seriePressure.ChartArea = "ChartAreaPressure";
                    seriePower.ChartArea    = "ChartAreaPower";
                    serieVoltage.ChartArea  = "ChartAreaPower";
                    serieCurent.ChartArea   = "ChartAreaPower";
                    serieFlow1.ChartArea    = "ChartAreaMFC";
                    serieFlow2.ChartArea    = "ChartAreaMFC";
                    serieFlow3.ChartArea    = "ChartAreaMFC";
                }
            }
            */
        }
        //-------------------------------------------------------------------------------
        //Grupa funkcji aktualizujaca wskazniki obiektow z ktorych sa czytane dane
        public void SetPresureObjPtr(PressureControl aPressure)
        {
            presure = aPressure;
        }
        //-------------------------------------------------------------------------------
        public void SetForePumpObjPtr(ForePump aPump)
        {
            forePump = aPump;
        }
        //-------------------------------------------------------------------------------
        public void SetMFCObjPtr(MFC aMFC)
        {
            mfc = aMFC;
        }
        //-------------------------------------------------------------------------------
        public void SetPowerSupplayObjPtr(PowerSupplay aPowerSupply)
        {
            powerSupplay = aPowerSupply;
        }
        //-------------------------------------------------------------------------------
        public void SetVaporizerObjPtr(Vaporizer aVaporizer)
        {
            vaporizer = aVaporizer;
        }
        //-------------------------------------------------------------------------------
        public void SetMotor1ObjPtr(MotorDriver aMotor)
        {
            motor1 = aMotor;
        }
        //-------------------------------------------------------------------------------
        public void SetMotor2ObjPtr(MotorDriver aMotor)
        {
            motor2 = aMotor;
        }   //-------------------------------------------------------------------------------
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearChart();
            autoScroll = true;
        }
        //-------------------------------------------------------------------------------
        //Pokaz serie danych w zaleznosci od aktywnych przeplywek
        private void HideNoActiveSerieMFC()
        {
            if(mfc != null)
            {
                serieFlow1.Enabled  = mfc.GetActive(1);
                cBoxFlow1.Visible   = mfc.GetActive(1);

                serieFlow2.Enabled  = mfc.GetActive(2);
                cBoxFlow2.Visible   = mfc.GetActive(2);

                serieFlow3.Enabled  = mfc.GetActive(3);
                cBoxFlow3.Visible   = mfc.GetActive(3);

                //jezlei zadme mfc nie jest aktywny to wylacz takze charta
                if (chartLabelAreaFlow != null)
                {
                    if (!mfc.GetActive(1) && !mfc.GetActive(2) && !mfc.GetActive(3))
                    {
                        chartAreaFlow.Visible = false;
                        chartLabelAreaFlow.Visible = false;
                    }
                    else
                    {
                        chartAreaFlow.Visible = true;
                        chartLabelAreaFlow.Visible = true;
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------
        //Pokaz serie danych w zaleznosci od aktywnych zaworo i motorow
        private void HideNoActiveSerieValve()
        {
            //Ustaw widocznosc seri zaworu vaporaziera. Pamietaj ze w sytuacji gdy nie ma aktywnych zadnaych matorow to seria jest wyswietlana na wykresie motora dlatego kopie nalezy takze schowac
            if (vaporizer != null)
            {
                serieCopyDosingValve.Enabled = vaporizer.GetActive() && (MotorDriver.Motor_1_Enable || MotorDriver.Motor_2_Enable);
                serieDosingValve.Enabled     = vaporizer.GetActive();
                cBoxDosingValve.Visible      = vaporizer.GetActive();
            }
            //Pokaz serie motorow zaleznosci od ich aktywnosci
            serieMotor1.Enabled = MotorDriver.Motor_1_Enable;
            cBoxMotor1.Visible  = MotorDriver.Motor_1_Enable;

            serieMotor2.Enabled = MotorDriver.Motor_2_Enable;
            cBoxMotor2.Visible  = MotorDriver.Motor_2_Enable;

            //Ustaw widocznosc wykresu w zaleznosci od akwynych zaworow i motorow. W sytaucji gdy motor i zawor nie sa dostepne wtedy wykreso przoni daj na calym ekranie
            if (vaporizer != null)
            {
                if (!MotorDriver.Motor_1_Enable && !MotorDriver.Motor_2_Enable && !vaporizer.GetActive())
                {
                    chartAreaMotor.Visible            = false;
                    chartAreaDosingValve.Visible      = false;
                    chartAreaPressure.Position.Height = 94;
                    chartAreaPressure.Position.Y      = 4;
                 }
                else
                {
                    chartAreaMotor.Visible            = true;
                    chartAreaPressure.Position.Height = 84;
                    chartAreaPressure.Position.Y      = 13;
                    //Przepnij serie zaworu na wykres motora gdy zawor jet dosteony a motory nie
                    Color color = Color.Black;
                    if (!MotorDriver.Motor_1_Enable && !MotorDriver.Motor_2_Enable && vaporizer.GetActive())
                    {
                        serieDosingValve.ChartArea = chartAreaMotor.Name;
                        chartAreaMotor.AxisY.Title = "Valve";
                        if (colorSeries.TryGetValue(serieDosingValve, out color))
                            chartAreaMotor.AxisY.TitleForeColor = color;
                    }
                    else
                    {
                        serieDosingValve.ChartArea = chartAreaDosingValve.Name;
                        chartAreaMotor.AxisY.Title = "Motor";
                        if (colorSeries.TryGetValue(serieMotor1, out color))
                            chartAreaMotor.AxisY.TitleForeColor =  color;
                    }
                }

                if (vaporizer.GetActive() && !MotorDriver.Motor_1_Enable && !MotorDriver.Motor_2_Enable)
                    chartAreaDosingValve.Visible = vaporizer.GetActive();
                //Ustaw wszystkie wykresy powiazane z wykresp prozni na jego wysokosc
                chartAreaFlow.Position.Height       = chartAreaPressure.Position.Height;
                chartAreaPower.Position.Height      = chartAreaPressure.Position.Height;
                chartLabelAreaFlow.Position.Height  = chartAreaPressure.Position.Height;
                chartLabelAreaPower.Position.Height = chartAreaPressure.Position.Height;
                chartAreaFlow.Position.Y            = chartAreaPressure.Position.Y;
                chartAreaPower.Position.Y           = chartAreaPressure.Position.Y;
                chartLabelAreaFlow.Position.Y       = chartAreaPressure.Position.Y;
                chartLabelAreaPower.Position.Y      = chartAreaPressure.Position.Y;
            }
        }
        //-------------------------------------------------------------------------------
        //Ustaw widocznosc danej serii
        private void cBox_CheckedChanged(object sender, EventArgs e)
        {
            ShowSerie(seriePressure, cBoxPressure.Checked);
            ShowSerie(seriePower,    cBoxPower.Checked);
            ShowSerie(serieVoltage,  cBoxVoltage.Checked);
            ShowSerie(serieCurent,   cBoxCurent.Checked);
            ShowSerie(serieFlow1,    cBoxFlow1.Checked);
            ShowSerie(serieFlow2,    cBoxFlow2.Checked);
            ShowSerie(serieFlow3,    cBoxFlow3.Checked);
            ShowSerie(serieMotor1,   cBoxMotor1.Checked);
            ShowSerie(serieMotor2,   cBoxMotor2.Checked);
            ShowSerie(serieDosingValve, cBoxDosingValve.Checked);

            serieCopyPressure.Color = seriePressure.Color;
            serieCopyPower.Color    = seriePower.Color;
            serieCopyFlow1.Color    = serieFlow1.Color;
            serieCopyFlow2.Color    = serieFlow2.Color;
            serieCopyFlow3.Color    = serieFlow3.Color;

            /*
            seriePressure.Enabled   = cBoxPressure.Checked;
            seriePower.Enabled      = cBoxPower.Checked;
            serieVoltage.Enabled    = cBoxVoltage.Checked;
            serieCurent.Enabled     = cBoxCurent.Checked;
            serieFlow1.Enabled      = cBoxFlow1.Checked;
            serieFlow2.Enabled      = cBoxFlow2.Checked;
            serieFlow3.Enabled      = cBoxFlow3.Checked;
            */
        }
        //-------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest pokaznie/schowanie danej serii
         */
        private void ShowSerie(Series serie,bool show)
        {
            if(serie != null)
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
                    serie.Color             = Color.Transparent;
                    serie.BorderColor       = Color.Transparent;
                }
            }
        }
        //-------------------------------------------------------------------------------
        //Wybor preferencji wyswietlania danuych albo na jednym wykresie albo na trzech
        private void toolStripComboBoxChart_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //-------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie sprawdzenie czy moge zminic zakres osi X o dana wartosc
         */
        private bool CanChangeRangeAxisX(ChartArea chartArea, Series serie, double value)
        {
            bool aRes = false;

            if (chartArea != null && serie != null && Source.Factory.Hpt1000 != null)
            {
                DataPoint aMaxX = serie.Points.FindMaxByValue("X", 0);

                if (chartArea.AxisX.Minimum + value > 0 && chartArea.AxisX.Maximum + value < aMaxX.XValue)
                    aRes = true;

                //Oblicz ile wynosci caly zakres wartosci
                double chartWindow = Source.Factory.Hpt1000.ChartWindowTime.ToOADate();
                //Uwzglednij warunek ze gdy nie jest pokazany caly zakres okna chart to zawsze zostaje wlaczony autoscroll
                double a = chartArea.AxisX.Maximum - chartArea.AxisX.Minimum;
                if (chartArea.AxisX.Maximum + value > aMaxX.XValue || chartWindow > chartArea.AxisX.Maximum)
                    overRangeAxisX = true;
            }
            return aRes;
        }
        //-------------------------------------------------------------------------------
        //Funkcja ma za zadanie sprawdzenie nad ktorym ChartArea zostal nacisniety klawisz myszy
        private bool ChecMousekArea(ElementPosition chartAreaPosition)
        {
            bool aRes = false;

            double minX = chartAreaPosition.X / 100 * chart.Width;
            double maxX = minX + chartAreaPosition.Width  * chart.Width / 100;
            double minY = chartAreaPosition.Y / 100 * chart.Height;
            double maxY = minY + chartAreaPosition.Height * chart.Height / 100;

            double mouseX = MousePosition.X - mainForm.Location.X - Location.X;
            double mouseY = MousePosition.Y - mainForm.Location.Y - Location.Y - 55;

            if (minX < mouseX && maxX > mouseX && minY < mouseY && maxY > mouseY)
                aRes = true;

            return aRes;
        }   
        //-------------------------------------------------------------------------------
        //Funkcja ustawia poczatkowy punkt wzgledem ktroego bedzie sie przesuwal wykres oraz wylacza autoscrol danego wykresu
        private void chart_MouseDown(object sender, MouseEventArgs e)
        {
            if (chartAreaPressure != null && ChecMousekArea(chartAreaPressure.Position))
            {
                scrollStartPoint.X  = e.X;
                autoScroll          = false;
                overRangeAxisX      = false;
            }
        }
        //-------------------------------------------------------------------------------
        /**
         * Funkcja przesuwa wykres zgdonie z ruchem myszki pod warunkeimn ze prawy przycisk myszy jest zlapany
         */
        private void chart_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                //Odczytasj wartosc okna danych jakie maja byc prezentowane na wykresie

                if (chartAreaPressure != null && chartAreaPower != null && chartAreaFlow != null && ChecMousekArea(chartAreaPressure.Position) && Source.Factory.Hpt1000 != null)
                {
                    //Odcvzytaj jakie jest okno czasowe dla wykresu
                    int windowSizeSec = Source.Factory.Hpt1000.ChartWindowTime.Hour * 60 * 60 + Source.Factory.Hpt1000.ChartWindowTime.Minute * 60 + Source.Factory.Hpt1000.ChartWindowTime.Second;
                    //Oblicz ile sekund przypada na jeden pixel
                    double pixelUnitSec = windowSizeSec / (chartAreaPressure.Position.Width * chart.Width / 100);
                    //Oblicz roznice pixeli jaka jest pomiedzy kuroserm myszki a poczatkiem            
                    int deltaMouseMove = scrollStartPoint.X - e.X;
                    //Jezeli przesuniecie myszki generuje przesuniecie wyklresu to przesun wykres i zapamietaj nowy punkt startu do przesuwania
                    if (Math.Abs(deltaMouseMove * pixelUnitSec) > 1)
                    {
                        deltaMouseMove = (int)(deltaMouseMove * pixelUnitSec);//Oblicz o ile sekund nalezy przesunac wykres
                        //Przygotuj zmienna ktora przekonwertuje wartosc sekund na warotsc double dla osi X
                        DateTime timeScroll = new DateTime();
                        timeScroll = timeScroll.AddSeconds(Math.Abs(deltaMouseMove));
                        //Konwertuj czas na double
                        double value = timeScroll.ToOADate();
                        //Wyznacz kierunek przeuswnai wykresu
                        if (deltaMouseMove < 0)
                            value *= -1;

                        //Sprawdz czy mieszcze sie w zakresie calkowitego min /max
                        if (CanChangeRangeAxisX(chartAreaPressure, seriePressure, value))
                        {
                            chartAreaPressure.AxisX.Maximum += value;   //Ustaw nowe wartosci min/max dla wykresu
                            chartAreaPressure.AxisX.Minimum += value;
                        }
                        if (CanChangeRangeAxisX(chartAreaPower, seriePower, value))
                        {
                            chartAreaPower.AxisX.Maximum += value;
                            chartAreaPower.AxisX.Minimum += value;
                        }
                        if (CanChangeRangeAxisX(chartAreaFlow, serieFlow1, value))
                        {
                            chartAreaFlow.AxisX.Maximum += value;
                            chartAreaFlow.AxisX.Minimum += value;
                        }
                        if (CanChangeRangeAxisX(chartAreaMotor, serieMotor1, value))
                        {
                            chartAreaMotor.AxisX.Maximum += value;
                            chartAreaMotor.AxisX.Minimum += value;
                        }
                        if (CanChangeRangeAxisX(chartAreaDosingValve, serieDosingValve, value))
                        {
                            chartAreaDosingValve.AxisX.Maximum += value;
                            chartAreaDosingValve.AxisX.Minimum += value;
                        }
                        //Zapamietaj nowa warotsc wzgledem ktroej bedziemy liczyc kolejne przesunieca
                        scrollStartPoint.X = e.X;
                    }

                }
            }
        }
        //-------------------------------------------------------------------------------
        //Wlaczenie autoscrolu gdy wykres zostal przesuniety do nowych wartosci
        private void chart_MouseUp(object sender, MouseEventArgs e)
        {
          /*  if (chartAreaPressure != null && ChecMousekArea(chartAreaPressure.Position))
            {
                DataPoint aMaxX = seriePressure.Points.FindMaxByValue("X", 0);
                DateTime a = new DateTime(chartAreaPressure.AxisX.Maximum);

                if (chartAreaPressure.AxisX.Maximum == aMaxX.XValue)
                    autoScroll = true;
            }
        */
            if(overRangeAxisX)
                autoScroll = true;
        }
        //-------------------------------------------------------------------------------
        //Kasowanie Zoom danego wykresu
        private void toolStripBtnZoomReset_Click(object sender, EventArgs e)
        {
            autoScroll = true;
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
    }
}
