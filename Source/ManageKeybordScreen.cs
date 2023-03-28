using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices; //required for APIsusin
using System.Drawing;

namespace HPT1000.Source
{
    /// <summary>
    /// Typ wyliczeniowy wykorzystywany do przekazywania paramterow do funkcji pokazujacej okno
    /// </summary>
    public enum SW
    {
        SW_HIDE = 0,
        SW_SHOWNORMAL = 1,
        SW_NORMAL = 1,
        SW_SHOWMINIMIZED = 2,
        SW_SHOWMAXIMIZED = 3,
        SW_MAXIMIZE = 3,
        SW_SHOWNOACTIVATE = 4,
        SW_SHOW = 5,
        SW_MINIMIZE = 6,
        SW_SHOWMINNOACTIVE = 7,
        SW_SHOWNA = 8,
        SW_RESTORE = 9,
        SW_SHOWDEFAULT = 10,
        SW_MAX = 10
    }
    /// <summary>
    /// Struktura pozwalajaca na przechopwanie pozycji okna na ekranie
    /// </summary>
    public struct RECT
    {
        public int Left;        // x position of upper-left corner
        public int Top;         // y position of upper-left corner
        public int Right;       // x position of lower-right corner
        public int Bottom;      // y position of lower-right corner
    }
    /// <summary>
    /// Deklaraca typu wyliczeniowego wykorzystywanego jak parametre do fucnkji api
    /// </summary>
    [Flags()]
    public enum SetWindowPosFlags : uint
    {
        SynchronousWindowPosition = 0x4000,
        DeferErase = 0x2000,
        DrawFrame = 0x0020,
        FrameChanged = 0x0020,
        HideWindow = 0x0080,
        DoNotActivate = 0x0010,
        DoNotCopyBits = 0x0100,
        IgnoreMove = 0x0002,
        DoNotChangeOwnerZOrder = 0x0200,
        DoNotRedraw = 0x0008,
        DoNotReposition = 0x0200,
        DoNotSendChangingEvent = 0x0400,
        IgnoreResize = 0x0001,
        IgnoreZOrder = 0x0004,
        ShowWindow = 0x0040,
    }
    /// <summary>
    /// Klasa zarzadzajaca klawiatura dotykowa na ekranie monitroa. Jej zadaniem jest pokazywanie klawaitury numerczyenj badz tekstowej w sytuacji gdy kontrolka eycyjna posiada focus
    /// Wazne aby klawiatura pokazywala sie obok kontrolki - nie przyslaniala jej
    /// </summary>
    class ManageKeybordScreen
    {
        // Grupa deklaracji funkcji Winapi pozwalajaca na zarzadzanie aplikacja klawiatury z poziomu naszej aplikacji 
        [DllImportAttribute("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);

        [DllImportAttribute("User32")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int Y, int cx, int cy, SetWindowPosFlags wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);
        
        // Deklaraca wlasciwosci klasy
        Control FActiveControl = null;                      ///< Wskaznik na kontrolke ktora posiada focusa
        Control FLastActiveControl = null;                  ///< Wskaznik na kontrolke ktora poprzednio posiadala focusa
        List<Control> controls = new List<Control>();       ///< Lista kontenerow wsrod ktorych szukam kontrolki posiadajacej focusa
        Process keybordProc    = new Process();             ///< Obiekt procesu apliakcji klawaitury
        string pathKeybord = "KeyboardScreen.exe" ;//"d://Projekty//HPT-1000//WindowsFormsApplication5//bin//Debug//KeyboardScreen.exe"; ///< Sciezka aplikacji klawiatury

        //------------------------------------------------------------------------------------
        /// <summary>
        /// Konstruktor klasy ktory inicjalizucje parametry procesu aplikacji klawiatury
        /// </summary>
        public ManageKeybordScreen()
        {
            keybordProc.StartInfo.FileName  = pathKeybord;
            keybordProc.EnableRaisingEvents = true;
            //Aby program mogl dobrze wspolpracowac z klawiatura to w danym momenci musi byc uruchomiona tlyko jedna aplikacja klawaitry dlatego wszystkie inne nalezy pozamykac
            //Wyszukaj i ubij wszystkie procesy KeyboardScreen
            foreach(Process pr in Process.GetProcessesByName("KeyboardScreen"))
                pr.Kill();
        }
        //------------------------------------------------------------------------------------
        /// <summary>
        /// Metoda ma za zadanie dodanie kontenerow kontrolek wsrod ktorych bedziemy szukac kontrolki poasiadajacej focusa
        /// </summary>
        /// <param name="ctr"></param>
        public void AddControl(Control ctr)
        {
            controls.Add(ctr);
        }
        //------------------------------------------------------------------------------------
        /**
         *  Zadaniem metody jest wyswietlenie klawiatury gdy jest to potrzebene oraz ukrycie jej gdy jest to niepotrzebne
         */ 
        public void ManageKeyboard()
        {
            bool aShowKeyboard = false;                                 //Flaga okresla czy mam pokazac formatke klawiatury
            //Pobierz aktywna kontrolke aplikacji
            Control activeControl = GetActiveControl();
            if (activeControl != null)
            {
                //Sprawdz czy kontrolka jest edycyjna tylko dla liczb
                if ((activeControl is TextBox && activeControl.Parent is HPT1000.GUI.Cotrols.DoubleEdit) || activeControl is DateTimePicker)
                {
                    //Ustaw flage wymuszajaca pokaznie klawioatury
                    aShowKeyboard = true;
                    Clipboard.SetData("TypeKeyboardAsNum", true); //Wymus na programie kalwaitury pokaznie klawiatury tlyko numerycznej
                }
                if(activeControl is TextBox && !(activeControl.Parent is HPT1000.GUI.Cotrols.DoubleEdit))
                {
                    //Ustaw flage wymuszajaca pokaznie klawioatury
                    aShowKeyboard = true;
                    Clipboard.SetData("TypeKeyboardAsNum", false); //Wymus na programie kalwaitury pokaznie pelnej klawiatury a nie tylko numerycznej
                }
            }
            //Pokaz klawiature jezeli aktywan kontrolka jest ecycyjna w przeciwnym razie schowaj ja
            if (aShowKeyboard)
            {              
                FActiveControl = activeControl;   //Zapamietaj wksaznik na akwywna kontrolke
                ShowKeyboard(); //Pokaz klawiature
                ActiveForm(); // Aktywuj formatke na ktorej lezy aktywna kontrolka w celu przekazania jej focusa
            }
            //Schowaj klawiature
            else
            {
                //Jezeli klawoatura zopstala schowana to wyzeruj wskaznik obiektu aktulnie aktywnej kontrolki
                if(HideKeyboard())
                    FActiveControl = null;
            }
            FLastActiveControl = FActiveControl;
        }
        //------------------------------------------------------------------------------------
        //Zadaniem metody jest aktywowanie formatki na ktorej lezy nasza kontrolka
        private void ActiveForm()
        {
            if (FActiveControl != null)
            {
                Control ctr = FActiveControl.Parent; //zczytaj paretna
                //Ustaw jako ctr formtake na ktorej lezy nasza kontrolka. Przejdz po wszystkich parentach do gory az natrafisz na form
                while (!(ctr is Form))
                {
                    ctr = ctr.Parent;
                }
                if(ctr is Form)
                    ((Form)ctr).Activate();
            }
        }
        //------------------------------------------------------------------------------------
        /**
         *  Zadaniem metody jest pokaznie progamu klawiatury obok kontrolki ktora wymaga edycji
         */
        private void ShowKeyboard()
        {
            try
            {
                bool aKeyboardRunned = false;
                //Sprawdz czy klawiature zostala juz uruchomina
                try
                {
                    int aID = keybordProc.Id; //Jezeli proces jest uruchoniony to zworci ID w przecuwnym razie wywali wyjatek
                    Process pr = Process.GetProcessById(aID);
                    aKeyboardRunned = true;
                }
                catch (Exception ex) { }
                //Uruchom proces klawiatury jezeli jeszcze nie jest uruchomiony ale pod warunkiem ze zostal takze ustawiony focus na innej kontrolce niz byl poprzednio
                if (!aKeyboardRunned)
                {
                    if(FActiveControl != FLastActiveControl)
                    {
                        FLastActiveControl = null; // wyzeruj ostatnio aktywna kontrolke poniewaz proces klawiatury rusza od poczatku
                        keybordProc.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                        keybordProc.Start();
                        //Czekaj az proces uruchomi okno
                        System.Threading.Thread.Sleep(500);
                    }
                    else //Zakonczyla sie edycja pola to usun focuse z aktywnej kontrolki
                    {
                        if(FActiveControl.Parent != null)
                            FActiveControl.Parent.Focus();
                    }
                }
                //Pokaz okno aplikacji klawiatury
               ShowWindow(keybordProc.MainWindowHandle.ToInt32(), (int)SW.SW_SHOWNOACTIVATE);
                //Ustaw okno aplikacji klawiatury obok kontrolki ktora bedzie edytowana               
                SetKeyboardPosNearControl();
            }
            catch (Exception ex)
            {         
            }
        }
        //------------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest ustawienie okna klawiatury obok kontrolki
         */ 
        void SetKeyboardPosNearControl()
        {
            System.Drawing.Point aPoint = new System.Drawing.Point();   // Zmienna przechowuje wspolrzedne punktu kontrolki edycyjnej formy 
            if (FActiveControl != null)
            {
                //Jezeli mam nowa kontrolke aktywna to ustaw niedleko niej klawiature ekranowa
                if (FActiveControl != FLastActiveControl)
                {
                    //Odczytaj pozycje kontrolki
                    aPoint = FActiveControl.PointToScreen(new System.Drawing.Point(0, 0));
                    //Oblicz wspolrzedne kotre pozwola ustawic okno klawiatury na srodku wzgledem kontrolki
                    aPoint = CalculateKeybordPosition(aPoint);
                    //Ustaw okna w oblicznoym punkcie
                    SetWindowPos(keybordProc.MainWindowHandle, IntPtr.Zero, aPoint.X, aPoint.Y, 0, 0, SetWindowPosFlags.IgnoreResize | SetWindowPosFlags.DoNotActivate);
                }            
            }
        }
        //------------------------------------------------------------------------------------
        /**
         * Oblicz wspolrzedne kotre pozwola ustawic okno klawiatury na srodku wzgledem kontrolki
        */
        Point CalculateKeybordPosition(Point aControlLocation)
        {
            Point aPointRes = new Point();
            RECT rect = new RECT();
            int aScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            int aScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            GetWindowRect(keybordProc.MainWindowHandle, out rect);
            int aKeybordWitdht = rect.Right - rect.Left;
            int aKeybordHeight = rect.Bottom - rect.Top;

            //Obliczam polozenie
            aPointRes.X = aControlLocation.X - aKeybordWitdht / 2;
            aPointRes.Y = aControlLocation.Y + 25;

            //Sprawdzam czy nie wychodze z klawiatura poza rogi ekranu 
            //Lewy rog
            if (aPointRes.X < 0)
                aPointRes.X = 0;
            //Prawy rog
            if (aPointRes.X + aKeybordWitdht > aScreenWidth)
                aPointRes.X -= aPointRes.X + aKeybordWitdht - aScreenWidth;
            //Dol
            if (aPointRes.Y + aKeybordHeight > aScreenHeight)
                aPointRes.Y = aPointRes.Y - aKeybordHeight - 25;
            return aPointRes;
        }
        //------------------------------------------------------------------------------------
        /**
         *  Zadaniem metody jest minimalizacja okna aplikacji klawiatury ale tylko w przypadku gdy aktywna kontrolka plaikcja nie jest edycjna oraz sama klawaitrua nie posiada focusa
         */
        private bool HideKeyboard()
        {
            bool aRes = false;
            try
            {
                //Zanim wywolam funcje chowajaca okno sprawdzam czy proces aplikacji klawiatury jest uruchomiony
                int aID = keybordProc.Id; //Jezeli proces jest uruchoniony to zworci ID w przecuwnym razie wywali wyjatek
                //Odczytaj Handla aktywnej formatki
                IntPtr activeHandle = GetForegroundWindow();
                //Odczytaj ID aktywnej formatki
                int aIdActiveForm = 0;
                GetWindowThreadProcessId(activeHandle, out aIdActiveForm);
                //Ukryj klawiatury ale tylko gdy nie posiada focusa. Jest to sprawdzane poprzez porownanie ID okna aplikacji klawiatury z ID aktywnego okna
                if (aIdActiveForm != aID)
                {
                    ShowWindow(keybordProc.MainWindowHandle.ToInt32(), (int)SW.SW_MINIMIZE);
                    aRes = true;
                }
            }
            catch (Exception ex) { }

            return aRes;
        }
        //------------------------------------------------------------------------------------
        /**
        *  Zadaniem metody jest pokaznie progamu klawiatury
        */
        public void CloseKeyboard()
        {
            //Ubij proces klawiatury jezeli zostal uruchomiony
            try
            {
                keybordProc.Kill();
            }
            catch (Exception ex) { }
        }
        //------------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest zwrocenie aktywnej kontrolki aplikacji. Gdy aplikacja nie posiada focusa to zwracam poprzednio aktywna
         */ 
        private Control GetActiveControl()
        {
            Control aCtrRes = null ;

            foreach (Control ctr in controls)
            {
                Control ctrTmp = GetFocusedControl(ctr);
                if (ctrTmp != null)
                    aCtrRes = ctrTmp;
            }
            return aCtrRes;
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Zadaniem metody jest zwrocenie kontorlki ktora posiada focusa w danym kontenerze
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        private Control GetFocusedControl(Control parent)
        {
            if (parent != null)
            {
                if (parent.Focused)
                {
                    return parent;
                }
                foreach (Control ctrl in parent.Controls)
                {
                    Control temp = GetFocusedControl(ctrl);
                    if (temp != null)
                    {
                        return temp;
                    }
                }
            }
            return null;
        }
    }
}
