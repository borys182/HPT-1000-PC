using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HPT1000.Source.Driver;
using HPT1000.Source;

namespace HPT1000.GUI
{
    /**
     * Zadaniem fomratki jest wylisotwanie wszystkich nazw procesow dla kotrych sa zapisane dane w bazie danych oraz umozliwienie userwoi wygodny sposob dotarcia do szukanego procesu
     */ 
    public partial class LoadDataForm : Form
    {
        private List<Sesion> sesions        = null;
        private string       nameAllProces  = "All procesess";
        private Sesion       selectedSesion;
        private bool         btnChangeDateClicked = false;      ///< Zmienna okresla czy zdarzenie clue changed pochodzi od przycisku. W ten sposob blukuje wykonywanie metody OnChanged w przypadku innych zdarzen
        private bool         cBoxProcesBlockClicked = false;    ///< Zmienna okresla czy zdarzenie clue changed pochodzi od przycisku. W ten sposob blukuje wykonywanie metody OnChanged w przypadku innych zdarzen
        private string       selectedProces = "";               ///< Zmienna okresl nazwe ostatnio wybranego procesu w filtrze szukania danych archiwalnych
        private int          lastCountData;                     ///< Ostatnia liczba danych w bazie zapisana w dniu dzisiejszym
            //-------------------------------------------------------------------------------------------------------------------
        /**
         * Konstruktor klasy
         */
        public LoadDataForm()
        {
            InitializeComponent();
            listViewData.Items.Clear();
            dateTimeStart.Value = GetTimeDate(DateTime.Today,1); //Ustaw date na sekunde po polnocy  
            dateTimeEnd.Value   = GetTimeDate(DateTime.Today, 2); //Ustaw date na sekunde przed polnoca        
        }
        /**
         * Dodaj kolejno procesy dla ktorych istnieja dane w bazie danych
         */
        //-------------------------------------------------------------------------------------------------------------------
        private void FillComboBoxProcessName(List<Sesion> sesions)
        {
            if (sesions != null)
            {
                cBoxProcesName.Items.Clear();
                cBoxProcesName.Items.Add(nameAllProces);
                foreach (Sesion sesion in sesions)
                {
                    if (sesion.ProcesName != null && !cBoxProcesName.Items.Contains(sesion))
                        cBoxProcesName.Items.Add(sesion);
                }
            }
            cBoxProcesBlockClicked = true;
            cBoxProcesName.SelectedIndex = 0;
            cBoxProcesBlockClicked = false;
        }
        //-------------------------------------------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest wylistowanie nazw procesow oraz dat ich startu zgodnie z wybranym filtrem
         */ 
         private void ShowProcesessName()
        {
            //Pokaz zakres dat dla ktorych listujemy procesy
            DateTime dateStart = GetTimeDate(dateTimeStart.Value,1);
            DateTime dateEnd   = GetTimeDate(dateTimeEnd.Value,2);

            grBoxListArchiveProces.Text = "List archived processes: " + dateStart.ToShortDateString() + " - " + dateEnd.ToShortDateString();
            if (sesions != null)
            {
                //Z uwagi na fakt ze tworzenie nowej listy od podstaw trwa dlugo to listy itemow sa dodawane tylko nowe (data sie poszerzyla) badz usuwane niepotrzebne (data sie skurczyla) 
                bool moreOlderNoExist = false; // flaga okres ze wiecej sesji ktorych data jest mniejsza od daty poczatku juz nie istneije
                bool moreNewerNoExist = false; // flaga okres ze wiecej sesji ktorych data jest wieksza od daty konca juz nie istneije
                //Przejdz po wszystkich prezentowanych sesjach
                for (int i = 0; i < listViewData.Items.Count; i++)
                {
                    ListViewItem itemOlder   = listViewData.Items[i];
                    ListViewItem itemNewer   = listViewData.Items[listViewData.Items.Count - i - 1];
                    Sesion       sesionOlder = (Sesion)itemOlder.Tag; //Najstarsza sesja
                    Sesion       sesionNewer = (Sesion)itemNewer.Tag; //Najnowsza sesja
                    //usun itemy ktorych data jest mniejsza od startu
                    if (sesionOlder.StartDate < dateStart)
                    {
                        listViewData.Scrollable = false;
                        listViewData.Items.Remove(itemOlder);
                        i--;

                        continue;
                    }
                    else
                        moreOlderNoExist = true;
                    //usun itemy ktorych data jest wieksza od konca
                    if (sesionNewer.StartDate > dateEnd)
                    {
                        listViewData.Scrollable = false;
                        listViewData.Items.Remove(itemOlder);
                        i--;
                    }
                    else
                        moreNewerNoExist = true;
                    //Nie ma wiecej itemow do usunieca d opusc petle
                    if (moreOlderNoExist && moreNewerNoExist)
                        break;
                }
                //Pobierz daty najmlodzego i nastarszego itema z prezentowanej listy
                DateTime procesDateOlder  = DateTime.MaxValue;
                DateTime procesDateNewer  = DateTime.MinValue;
                if(listViewData.Items.Count > 0)
                {
                    procesDateOlder = ((Sesion)listViewData.Items[0].Tag).StartDate;
                    procesDateNewer = ((Sesion)listViewData.Items[listViewData.Items.Count - 1].Tag).StartDate;
                }
                procesDateOlder = GetTimeDate( procesDateOlder,1);   //Ustaw czas aby porownywac cale dni to znaczy ustaw odpowiednio godziny aby czas byl 00.00.01
                procesDateNewer = GetTimeDate( procesDateNewer,2);   //Ustaw czas aby porownywac cale dni to znaczy ustaw odpowiednio godziny aby czas byl 23.59.59
                //Dodaj nowe itemy ale tylko te ktorych zakres jest mniejszy od najmniejszego w liscie i wiekszy do najwiekszego w liscie
                int indexDown = 0;
                foreach (Sesion sesion in sesions)
                {
                    //Jezeli lista jest pusta to dodaj pierwszy patrzac na daty wybrane przez usera item oraz aktualizuj daty
                    if (listViewData.Items.Count == 0)
                    {
                        if (AddProces(sesion, 0))
                        {
                            procesDateOlder = ((Sesion)listViewData.Items[0].Tag).StartDate;
                            procesDateNewer = ((Sesion)listViewData.Items[listViewData.Items.Count - 1].Tag).StartDate;
                        }

                    }
                    //Dodawaj itemy gdy juz jakis istnieje
                    else
                    {
                        if (sesion.StartDate < procesDateOlder)
                            if (AddProces(sesion, indexDown))
                                indexDown++;
                        if (sesion.EndDate > procesDateNewer)
                            AddProces(sesion, listViewData.Items.Count);
                               
                    }
                }
            }
            listViewData.Scrollable = true;

        }
        //-------------------------------------------------------------------------------------------------------------------
        /**
         * Zadaniem metody jesat utworzenie oraz dodanie nowego itemama procesu w odpowiednie miejsce
         */
        private bool AddProces(Sesion sesion,int index)
        {
            bool res = false;
            //Odczytaj ustawiony zakres przez usera i ustaw odpowiednio godziny aby porowynwac poprawnie cale dni
            DateTime dateStart = GetTimeDate(dateTimeStart.Value,1);
            DateTime dateEnd   = GetTimeDate(dateTimeEnd.Value, 2);

            if (( sesion.StartDate >= dateStart && sesion.StartDate <= dateEnd && cBoxProcesName.SelectedItem != null) && 
               ((cBoxProcesName.SelectedItem == nameAllProces) || 
                (cBoxProcesName.SelectedItem != nameAllProces && sesion.ProcesName == cBoxProcesName.SelectedItem.ToString())))
            {
                ListViewItem item = new ListViewItem();
                item.Text = sesion.StartDate.ToString() + " - "+ sesion.ProcesName;
                item.Tag  = sesion;
                item.ImageIndex = 0;
                listViewData.Items.Insert(index,item);
                res = true;
            }               
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie sprawdzenie czy listview zawiera juz podana sesje
         */
        private bool IsListViewContains(string sesionName)
        {
            bool res =  false;
            foreach (ListViewItem item in listViewData.Items)
            {
                if (item.Text == sesionName)
                {
                    res = true;
                    break;
                }
            }
           return res;
        }
        //-------------------------------------------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest ustawienie czasu na wartosc jednej sekundy po polnocy badz 1 sekundy przed polnoca. W ten sposob moge porownac cale dni
         */ 
        private DateTime GetTimeDate(DateTime date, int type)
        {
            DateTime res = date;
            res = res.AddHours(-date.Hour);
            res = res.AddMinutes(-date.Minute);
            res = res.AddSeconds(-date.Second);

            switch (type)
            {
                case 1: // ustaw 00.00.01
                    res = res.AddSeconds(1);
                    break;
                case 2: // ustaw 23.59.59
                    res = res.AddHours(23);
                    res = res.AddMinutes(59);
                    res = res.AddSeconds(59);
                    break;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie zworcenie zaznaczonej sesji
         */
        public Sesion GetProcesData()
        {
            return selectedSesion;
        }
        //-------------------------------------------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest odczytanie danych z archiwum i wyswietlenie ich userowi
         */ 
        private void ShowAvalibleArchiveData()
        {
            Cursor aLastCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            listViewData.Items.Clear();
            if (sesions != null)
                sesions.Clear();
            if (Factory.DataBase != null)
            {
                //    sesions = Factory.DataBase.GetSesions(DateTime.MinValue, DateTime.MaxValue);
                sesions = Factory.DataBase.GetSesions(dateTimeStart.Value, dateTimeEnd.Value);
                FillComboBoxProcessName(sesions);
                //Ustaw poprzednio wybrny proces
                if (selectedProces != "")
                {
                    for (int i = 0; i < cBoxProcesName.Items.Count; i++)
                    {
                        if (cBoxProcesName.Items[i].ToString() == selectedProces)
                        {
                            cBoxProcesBlockClicked = true;
                            cBoxProcesName.SelectedIndex = i;
                            cBoxProcesBlockClicked = false;
                        }
                    }
                }
            }
            //Pokaz dostpna liste sesji
            ShowProcesessName();
            //Ustaw ze nie wybrano jeszcze zadanej sesji
            selectedSesion.ProcesName = null;
            //Przywroc poprzedni cursor
            Cursor.Current = aLastCursor;
        }
        //-------------------------------------------------------------------------------------------------------------------
        /**
         * Wykonaj pewne operacje ktroe sa wymagane w momenci gdy formatka jest podnoszona do woizaulizacji
         */
        private void LoadDataForm_VisibleChanged(object sender, EventArgs e)
        {
            //Zczytaj sesje gdy formatka jest widoczna
            if (Visible && Factory.DataBase != null)
            {
                int aCountData = Factory.DataBase.GetCountData(GetTimeDate(DateTime.Today,1), GetTimeDate(DateTime.Today, 2));
                //Jezeli dane nie zostaly jeszcze pokazne lub doszly nowe to pobierz je z bazy i pokaz 
                if (aCountData != lastCountData)
                    ShowAvalibleArchiveData();
                lastCountData = aCountData;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------
        /**
         * Obsluga zdarzenia przycisku Cancel - zamknij fomrtke bez zadnej akcji
         */ 
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        //-------------------------------------------------------------------------------------------------------------------
        /**
         * Obsluga zdarzenia przycisku OK - zwroc wybrany proces i zamknij formtke
         */
        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        //-------------------------------------------------------------------------------------------------------------------
        /**
         * Obsluga zdarzenia zmiany datu startu badz konca wyszikawanej sesji- finalny efekt powinine byc wylisotwanie wszystkicg sesji z podanego zakresu
         */
        private void dateTimePicker_CloseUp(object sender, EventArgs e)
        {
            ShowProcesessName();
            //Pokdaz procesy dla wybranego zakresu dat
            ShowAvalibleArchiveData();
        }
        //-------------------------------------------------------------------------------------------------------------------
        /**
         * Obsluga zdarzenia zmiany nazwy sesji dla ktroej nalyz wylistowac wszystkie wystapienia- finalny efekt powinine byc wylisotwanie wszystkicg sesji z podanego zakresu oraz nazwy
         */
        private void cBoxProcesName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cBoxProcesName.SelectedItem != null && !cBoxProcesBlockClicked)
                selectedProces = cBoxProcesName.SelectedItem.ToString();
            listViewData.Items.Clear();
            ShowProcesessName();
        }
        //-------------------------------------------------------------------------------------------------------------------
        /**
         * Obsluga zdarzenia wyboru sesji
         */ 
        private void listViewData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewData.SelectedItems.Count > 0)
                selectedSesion = (Sesion)listViewData.SelectedItems[0].Tag;
        }
        //-------------------------------------------------------------------------------------------------------------------
        /**
         * Obsluga zdarzenia klikniecia na przycisk zmniejszjaca date startu procesu ktorych archiowum szukamy
         */ 
        private void btnTimeStartDown_Click(object sender, EventArgs e)
        {
            dateTimeStart.Focus();
            SendKeys.Send("{DOWN}");
            btnChangeDateClicked = true;
            //Pokdaz procesy dla wybranego zakresu dat
            ShowAvalibleArchiveData();
        }
        //-------------------------------------------------------------------------------------------------------------------
        /**
         * Obsluga zdarzenia klikniecia na przycisk zwiekszajacy date startu procesu ktorych archiowum szukamy
         */
        private void btnTimeStartUp_Click(object sender, EventArgs e)
        {
            dateTimeStart.Focus();
            SendKeys.Send("{UP}");
            btnChangeDateClicked = true;
            //Pokdaz procesy dla wybranego zakresu dat
            ShowAvalibleArchiveData();
        }
        //-------------------------------------------------------------------------------------------------------------------
        /**
         * Obsluga zdarzenia klikniecia na przycisk zmniejszjaca date konca procesu ktorych archiowum szukamy
         */
        private void btnTimeEndDown_Click(object sender, EventArgs e)
        {
            dateTimeEnd.Focus();
            SendKeys.Send("{DOWN}");
            btnChangeDateClicked = true;
            //Pokdaz procesy dla wybranego zakresu dat
            ShowAvalibleArchiveData();
        }
        //-------------------------------------------------------------------------------------------------------------------
        /**
         * Obsluga zdarzenia klikniecia na przycisk zwiekszajacy date konca procesu ktorych archiowum szukamy
         */
        private void btnTimeEndUp_Click(object sender, EventArgs e)
        {
            dateTimeEnd.Focus();
            SendKeys.Send("{UP}");
            btnChangeDateClicked = true;
            //Pokdaz procesy dla wybranego zakresu dat
            ShowAvalibleArchiveData();
        }
        //-------------------------------------------------------------------------------------------------------------------
        /**
         * Obsluga zdarzenia zmiany daty procesow. Wazne aby metoda zostala wykonana tylko na zinae daty pochodzac od przyciskow zwiekszajacych/zmniejszajacych zakres dat
         */
        private void dateProces_ValueChanged(object sender, EventArgs e)
        {         
            if (btnChangeDateClicked)
              ShowProcesessName();
            btnChangeDateClicked = false;
       }
        //-------------------------------------------------------------------------------------------------------------------
        /**
         * Obsluga zdarzenia podwojnego klikniecia procesu w wyniku czego zamykam formatke i zwracam zaznaczony proces
         */
        private void listViewData_DoubleClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        //-------------------------------------------------------------------------------------------------------------------

    }
}
