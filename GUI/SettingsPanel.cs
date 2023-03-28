using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HPT1000.Source.Driver;
using HPT1000.Source.Chamber;

namespace HPT1000.GUI
{
    public partial class SettingsPanel : UserControl
    {
        //Referencje na obiekty wymagna przez panel serwisowy
        private Source.Driver.HPT1000   hpt1000         = null;
        private Source.DB               dataBase        = null;
        private bool                    permission      = false;
        private bool                    oldPermission   = false;
        private Parameter               selectedPara    = null;
        private bool                    lastAcqEnabled  = false;
        //----------------------------------------------------------------------------------------------------------------------------
        //Ustaw wskaznik na obiekt bazy danych. Jest on wymagany do zapisu parametrow akwizycji
        public Source.DB DataBase
        {
            set { dataBase = value; }
        }
        //----------------------------------------------------------------------------------------------------------------------------
        public Source.Driver.HPT1000 HPT1000
        {
            set { hpt1000 = value; }
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Konstruktor klasy jest odpowiedzilny za przygotowania komonentow gradicnzych oraz inicjalizacja ich wartosciami
        public SettingsPanel()
        {
            InitializeComponent();
            //Wypelnij dostepne jezyki
            FillComboBoxLanguge();
            //Wyszarz na poczatek mozliwosc wprwadzania wartosci parametrow bo nie wiesz jaki jest wezel wybrany
            //grBoxParameter.Visible = false;
            //Ustaw poczatkowa wartosc na checked a pozniej reaguj na jego zmiane
            labPressure.Enabled      = cBoxActivePressure.Checked;
            labUnitPressure.Enabled  = cBoxActivePressure.Checked;
            dEditAcqPressure.Enabled = cBoxActivePressure.Checked;
            //Inicjalizuj ComboBoxa Mode wartosciami z Enuma
            FillComboBoxMode();
            //Pokaz dostepne typy komunikacji z PLC
            FillComboBoxCommunication();
            //Daj mozliwosc ustawienia backgourndcolor dla tab of pagecontrol
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Wypelnij liste dostepnych jezyki dla aplikacji
        private void FillComboBoxLanguge()
        {
            cBoxLanguge.Items.Clear();
            foreach (string aName in Enum.GetNames(typeof(Types.Language)))
            {
                cBoxLanguge.Items.Add(aName);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Odswiez parametry prezentowane na panelu
        public void RefreshSettingsPanel()
        {
            if (hpt1000 != null)
            {
                //Pokaz aktualna konfiguracje urzadzen i parametrow.  nie jest zmieniana dlatego moge ja zalafowac podczas tworzenia formatki
                //Pokaz liste urzadzen
                ShowListDevice();
                //Pokaz parametry akwizycji
                ShowAcqPara();
                //Wyswietl aktualnie wybrany jezyk
                for (int i = 0; i < cBoxLanguge.Items.Count; i++)
                {
                    if (cBoxLanguge.Items[i].ToString() == Source.Driver.HPT1000.LanguageApp.ToString())
                        cBoxLanguge.SelectedIndex = i;
                }
                if (hpt1000.GetPLC() != null)
                {
                    //Przedstaw aktualnie wybrany tryb dummy mode
                    cBoxDummyMode.Checked = hpt1000.GetPLC().GetDummyMode();
                    //Przedstasw aktualnie wybrany typ komunikacji z PLC
                    for (int i = 0; i < cBoxComm.Items.Count; i++)
                    {
                        if (cBoxComm.Items[i].ToString() == hpt1000.GetPLC().GetTypeComm().ToString())
                            cBoxComm.SelectedIndex = i;
                    }
                    ShowPanelIP(hpt1000.GetPLC());
                }
                dEditChartWindow_Hour.Value     = hpt1000.ChartWindowTime.Hour;
                dEditChartWindow_Minute.Value   = hpt1000.ChartWindowTime.Minute;
                dEdit_ChartWindow_Sec.Value     = hpt1000.ChartWindowTime.Second;
            }
            ShowComponetsPersmission(Source.ApplicationData.LoggedUser);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        //Pokaz dostpene komponety w zaleznocsi od zalogowanego usera
        public void ShowComponetsPersmission(Source.User user)
        {
            if(user != null)
            {
                if(user.Privilige != Types.UserPrivilige.Operator && user.Privilige != Types.UserPrivilige.Administrator)
                {
                    permission              = true;
                    btnSave.Visible         = true;
                    cBoxComm.Enabled        = true;
                    cBoxDummyMode.Enabled   = true;
                   // treeViewDevices.Size    = new Size(285, 450); //286,495
                    listViewDevices.Size    = new Size(285, 290);  //286, 330
                    SetPermissionModifyPara(tabControl1.TabPages[0], true);
                    SetPermissionModifyPara(grBoxAcq, true);
                }
                else
                {
                    permission              = false;
                    cBoxComm.Enabled        = false;
                    cBoxDummyMode.Enabled   = false;
                    btnSave.Visible         = false;
                   // treeViewDevices.Size    = new Size(285, 483);
                    listViewDevices.Size    = new Size(288, 320);
                    SetPermissionModifyPara(tabControl1.TabPages[0], false);
                    SetPermissionModifyPara(grBoxAcq, false);
                }
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        //Zablokuj/odblokuj mozeliwosc edytowania programow
        private void SetPermissionModifyPara(Control control , bool permission)
        {
            foreach (Control ctr in control.Controls)
            {
                if (ctr.Controls.Count > 0)
                    SetPermissionModifyPara(ctr,permission);
                else
                {
                    if(ctr.Name != "listViewDevices" && ctr.Name != "btnSave")
                        ctr.Enabled = permission;
                }
            }
            rBtnAcqAllTime.Enabled = false; // wylacz na zawsze mozliwosc ustawinai akwizycji danych caly czas
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        //Zablokuj/odblokuj mozeliwosc edytowania programow
        private void SetEnabledComponent(Control control, bool permission)
        {
            foreach (Control ctr in control.Controls)
            {
                if (ctr.Controls.Count > 0)
                    SetEnabledComponent(ctr, permission);
                else
                {
                    if (ctr.Name != "btnSave")
                        ctr.Enabled = permission;
                }
            }
            rBtnAcqAllTime.Enabled = false;// wylacz na zawsze mozliwosc ustawinai akwizycji danych caly czas
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja wypelnia ComboBoxa dostepnymi rodzajami komunikacji z PLC
        private void FillComboBoxCommunication()
        {
            cBoxComm.Items.Clear();
            foreach (string aName in Enum.GetNames(typeof(Types.TypeComm)))
            {
                cBoxComm.Items.Add(aName);
            }
        }
        //------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie dopisanie do listy combobox wszystkich mozliwych do wybrania trybow pracy
        private void FillComboBoxMode()
        {
            cBoxMode.Items.Clear();
            foreach (string nameMode in Enum.GetNames(typeof(Types.ModeAcq)))
            {
                cBoxMode.Items.Add(nameMode);
            }
        }
        //------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie utworzenie listy urzadzen wraz z jej parametrami
      /*  private void ShowListDevice()
        {
            TreeNode nodeDevices = null;
            //Wyczysc aktualna liste
            treeViewDevices.Nodes.Clear();
            //Utworz pierwszy wezel lsity jezeli jeszcze nie istnieje
            if (treeViewDevices.Nodes.Count == 0)
                treeViewDevices.Nodes.Add("Devices list", "Devices list", 0, 0);
            //Ustaw obiekt pierwszego wezla drzewa
            if (treeViewDevices.Nodes.Count > 0)
                nodeDevices = treeViewDevices.Nodes[0];
            //Utworz drzewo urzadzen wraz z ich parametrami jako liste ale tylko te urzadzenia oraz te parametry ktroe sa przewidziane do archiwizowania czyli posiadaja parametry
            if (hpt1000 != null && hpt1000.Chamber.GetObjects() != null && nodeDevices != null)
            {
                //Pobierz liste wszystkich urzadzen
                foreach (Device device in hpt1000.Chamber.GetObjects())
                {
                    //Utworz wezel urzadzenia tylko dla urzadzen posiadajacych parametry przeznaczone do archiwizowania
                    if (device.GetParameters().Count > 0)
                    {
                        TreeNode nodeDevice = new TreeNode(device.Name, 1, 1);  //Utworz wezle urzadzenia
                        nodeDevice.Tag = device;
                        //Pobierz liste parametrow urzadzenia
                        foreach (Parameter para in device.GetParameters())
                        {
                            TreeNode nodePara = new TreeNode(para.Name, 2, 2);//Utworz wezl parametru
                            nodePara.Tag = para;
                            nodeDevice.Nodes.Add(nodePara);     //Dodaj wezel parametru do wezla urzadzenia
                        }
                        //Doadj wezle urzadzenia do wezla lsity urzadzen
                        if (nodeDevices != null)
                            nodeDevices.Nodes.Add(nodeDevice);
                    }
                }
            }
        }
        */
        //------------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie utworzenie listy urzadzen wraz z jej parametrami
         */ 
        private void ShowListDevice()
        {
            //Wyczysc aktualna liste
            listViewDevices.Items.Clear();
            //Utworz listę urzadzen wraz z ich parametrami jako liste ale tylko te urzadzenia oraz te parametry ktroe sa przewidziane do archiwizowania czyli posiadaja parametry
            if (hpt1000 != null && hpt1000.Chamber.GetObjects() != null )
            {
                //Pobierz liste wszystkich urzadzen
                foreach (Device device in hpt1000.Chamber.GetObjects())
                {
                    //Pokaz liste parametrow kolejnych urzadzen ktore sa przeznaczone do archiwizowania
                    if (device.GetParameters().Count > 0)
                    {
                        //Pobierz liste parametrow urzadzenia
                        foreach (Parameter para in device.GetParameters())
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = para.Name;
                            item.Tag = para;
                            //Poprawa literowki
                            if(para.Name == "Vaporaizer state")
                                item.Text = "Vaporiser state";
                            listViewDevices.Items.Add(item); //Dodaj wezel parametru do wezla urzadzenia
                        }
                    }
                }
                //Ustaw zaznaczenie na poprzednio wybranym wezle
                for (int i = 0; i < listViewDevices.Items.Count; i++)
                {
                    if(listViewDevices.Items[i].Tag == selectedPara)
                        listViewDevices.Items[i].Selected = true;
                }
            }
        }
        //------------------------------------------------------------------------------------------
        //Funkcja wyswietla parametry parametru urzadzenia
        private void ShowParameter(Parameter para)
        {
            if (para != null)
            {
                dEditFrqAcq.Value = para.Frequency;
                dEditDifferencesValue.Value = para.Differance;
                cBoxParaActive.Checked = para.EnabledAcq;
                labParaUnit.Text = "[" + para.Unit + "]";

                for (int i = 0; i < cBoxMode.Items.Count; i++)
                {
                    if (cBoxMode.Items[i].ToString() == para.Mode.ToString())
                        cBoxMode.SelectedIndex = i;
                }
            }
        }
        //------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie pokaznie parametrow akwizycji danych zapisanych w obiekcie odpowiedzinalnym za akwizycje danych
        private void ShowAcqPara()
        {
            if (hpt1000 != null)
            {
                dEditAcqPressure.Value = hpt1000.PressureAcq;
                cBoxActivePressure.Checked = hpt1000.ActiveCheckPressureAcq;
                rBtnAcqDuringProcess.Checked = hpt1000.AcqDuringOnlyProcess;
                rBtnAcqAllTime.Checked = hpt1000.AcqAllTime;
                cBoxEnabledAcq.Checked = hpt1000.EnabledAcq;
                cBoxAskAcq.Checked      = hpt1000.AskAcq;
            }
        }
        //------------------------------------------------------------------------------------------
        //Podaj obiekt parametru z aktualnie wybranego wezla drzewa
       /* private Parameter GetSelectedPara()
        {
            Parameter para = null;

            if (treeViewDevices.SelectedNode != null && treeViewDevices.SelectedNode.Level == 2)
                para = (Parameter)treeViewDevices.SelectedNode.Tag;

            return para;
        }
        */
        //------------------------------------------------------------------------------------------
        //Podaj obiekt parametru z aktualnie wybranego itema listy
        private Parameter GetSelectedPara()
        {
            Parameter para = null;

            if (listViewDevices.SelectedItems.Count > 0)
            {
                para = (Parameter)listViewDevices.SelectedItems[0].Tag;
                selectedPara = para;
            }
            return para;
        }
        //------------------------------------------------------------------------------------------
        //Funkcja wywolywana akcja klikniecia na dowolny wezel listy urzadzen. Pokaz wartosci parametrow wybranego wezla parametru urzadzenia
        private void treeViewDevices_AfterSelect(object sender, TreeViewEventArgs e)
        {
      /*      //Pokaz parametry wybranego wezla
            ShowParameter(GetSelectedPara());
            //Jezeli wybrany wezel nie jest wezlem parametru to wyszarz wartosci parametru
            if (treeViewDevices.SelectedNode != null && treeViewDevices.SelectedNode.Level != 2)
            {
                grBoxParameter.Visible = false;
            }
            else
                grBoxParameter.Visible = true;
       */ }
        //------------------------------------------------------------------------------------------
        /**
         * Funkcja akcja wyboru dowlnego kanalu pomiarowego. Pokaz wartosci parametrow wybranego parametru urzadzenia
         */
        private void listViewDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pokaz parametry wybranego wezla
            ShowParameter(GetSelectedPara());
        }
        //------------------------------------------------------------------------------------------
        //Podswietlaj caly czas aktualnie zaznaczony program/subprogram
        private void LightSelectedNode(TreeNodeCollection nodes)
        {
            if (nodes != null)
            {
                foreach (TreeNode node in nodes)
                {
                    //jezeli mam dzieci to wywolaj funkcje rekurencyjnie jeszcze raz
                    if (node.Nodes.Count > 0)
                        LightSelectedNode(node.Nodes);
                    //Wezel jest zaznaczony to ustaw backcolor jako podswietlony zaznaczony
                    if (node.IsSelected)
                    {
                        node.BackColor = SystemColors.Highlight;
                        node.ForeColor = Color.White;
                    }
                    //Wezel nie jest juz wybrany to ustaw kolor jako nie wybrany
                    else
                    {
                        node.BackColor = Color.Transparent;
                        node.ForeColor = Color.Black;
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------
        private void rBtnAcqDuringProcess_CheckedChanged(object sender, EventArgs e)
        {
         //   if (hpt1000 != null)
         //       hpt1000.AcqDuringOnlyProcess = rBtnAcqDuringProcess.Checked;
        }
        //-------------------------------------------------------------------------------
        private void rBtnAcqAllTime_CheckedChanged(object sender, EventArgs e)
        {
        //    if (hpt1000 != null)
        //        hpt1000.AcqAllTime = rBtnAcqAllTime.Checked;
        }
        //------------------------------------------------------------------------------------------
        private bool dEditAcqPressure_EnterOn()
        {
            if (hpt1000 != null)
                hpt1000.PressureAcq = dEditAcqPressure.Value;

            return true;
        }
        //------------------------------------------------------------------------------------------
        private bool dEditFrqAcq_EnterOn()
        {
            Parameter para = GetSelectedPara();

            if (para != null)
                para.Frequency = dEditFrqAcq.Value;

            return true;
        }
        //------------------------------------------------------------------------------------------
        private bool dEditDifferencesValue_EnterOn()
        {
            Parameter para = GetSelectedPara();

            if (para != null)
                para.Differance = dEditDifferencesValue.Value;

            return true;
        }
        //------------------------------------------------------------------------------------------
        private void timer_Tick(object sender, EventArgs e)
        {
            //Podswietalj aktulnie zaznaczony wezel caly czas
            LightSelectedNode(treeViewDevices.Nodes);
            //Ustaw widocznosc pol do wprawadzania parametrow akwizycji w zaleznosci od wlaczonej opcji akwizycji danych jak i wybranego wezla
            if (hpt1000 != null && permission)
            {
                //Pola akwizyji urzadzenia sa dezaktywowane gdy jest wylaczona akwizycja
                if (lastAcqEnabled != hpt1000.EnabledAcq)
                    SetEnabledComponent(grBoxAcqPara, hpt1000.EnabledAcq);// grBoxAcqPara.Enabled = hpt1000.EnabledAcq;
                //Pola parametrow sa dezaktywowane gdy jest wylaczona akwizycja lub nie jest wybrany wezel parametru
                /*
                if (hpt1000.EnabledAcq && treeViewDevices.SelectedNode != null && treeViewDevices.SelectedNode.Level == 2)
                    grBoxParameter.Visible = true;
                else
                    grBoxParameter.Visible = true;
                */
                lastAcqEnabled = hpt1000.EnabledAcq;
            }
            //Wywolaj metode tylko raz gdy Enabled jest na True
            if (!permission && permission != oldPermission)
                SetPermissionModifyPara(grBoxAcqPara, false);
            oldPermission = permission;
        }
        //------------------------------------------------------------------------------------------
        //Wyszaz mozliwosc wprowadzania wartosci prozni gdy opcja jest wylaczona
        private void cBoxActivePressure_CheckedChanged(object sender, EventArgs e)
        {
            labPressure.Enabled      = cBoxActivePressure.Checked;
            labUnitPressure.Enabled  = cBoxActivePressure.Checked;
            dEditAcqPressure.Enabled = cBoxActivePressure.Checked;

            if (hpt1000 != null)
                hpt1000.ActiveCheckPressureAcq = cBoxActivePressure.Checked;
        }
        //------------------------------------------------------------------------------------------
        private void cBoxParaActive_CheckedChanged(object sender, EventArgs e)
        {
            Parameter para = GetSelectedPara();
            if (para != null)
            {
                para.EnabledAcq = cBoxParaActive.Checked;
            }
        }
        //------------------------------------------------------------------------------------------
        private void cBoxMode_SelectedValueChanged(object sender, EventArgs e)
        {
            Parameter para = GetSelectedPara();
            if (para != null)
            {
                para.Mode = (Types.ModeAcq)Enum.Parse(typeof(Types.ModeAcq), cBoxMode.SelectedItem.ToString());
            }
        }
        //------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie zapisanie parametrow wszystkich urzadzen w bazie danych
        private void btnSave_Click(object sender, EventArgs e)
        {
            //SPrawdz czy istnieja referencje na wykorzystywane obiekty
            if (dataBase != null && hpt1000 != null && hpt1000.Chamber.GetObjects() != null)
            {
                //Pobierz liste wszystkich urzadzen
                foreach (Device device in hpt1000.Chamber.GetObjects())
                {
                    //Wykonaj zapis parametrow ale tylko urzadzenia ktore zapisuje parametry w bazie danych
                    if (device.AcqData)
                    {
                        //Pobierz liste parametrow urzadzenia
                        foreach (Parameter para in device.GetParameters())
                        {
                            //wykonaj zapis parametrow danego parametru w bazie dnaych
                            dataBase.ModifyConfigPara(para);
                        }
                    }
                }
                //Zapisz konfigruacje dla urzadzenia
                int res = hpt1000.SaveData();
                if (res == 0)
                    MessageBox.Show("Acquisition settings has been saved successfully", "Acquisition settings",MessageBoxButtons.OK,MessageBoxIcon.Information);
                else
                    MessageBox.Show("Acquisition settings has been saved wrong", "Acquisition settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //------------------------------------------------------------------------------------------
        //Funckaj ustawia flage wlaczenia/wylacze akwizycji danych
        private void cBoxEnabledAcq_CheckedChanged(object sender, EventArgs e)
        {
            if (hpt1000 != null)
                hpt1000.EnabledAcq = cBoxEnabledAcq.Checked;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Aktywuj/deaktywuj tryb dumy mode
        private void cBoxDummyMode_CheckedChanged(object sender, EventArgs e)
        {
            if (hpt1000 != null && hpt1000.GetPLC() != null)
                hpt1000.GetPLC().SetDummyMode(cBoxDummyMode.Checked);
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Zdarzenie powoduje odswiezenie wartosci prezentowanych na panelu
        private void SettingsPanel_Load(object sender, EventArgs e)
        {
            RefreshSettingsPanel();
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie obsluzenie zdarzenia zmiany typu komunikacji z PLC
        private void cBoxComm_SelectedIndexChanged(object sender, EventArgs e)
        {
            lock (cBoxComm)
            {
                if (hpt1000 != null && hpt1000.GetPLC() != null)
                {
                    if (cBoxComm.SelectedItem.ToString() == "USB")
                    {
                        panelAddressIP.Visible = false;
                        hpt1000.GetPLC().SetTypeComm(Types.TypeComm.USB);
                    }
                    if (cBoxComm.SelectedItem.ToString() == "TCP")
                    {
                        panelAddressIP.Visible = true;
                        hpt1000.GetPLC().SetTypeComm(Types.TypeComm.TCP);
                    }
                    hpt1000.GetPLC().Connect();
                }
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------        
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //e.DrawBackground();
            using (LinearGradientBrush br = new LinearGradientBrush(e.Bounds, Color.LightGray, Color.Gray, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(br, e.Bounds); //Wrysuj odpowidni kolor zakladki

                //Wyrysuj tekst
                SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.White, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                //Ustaw zaznaczenie
                Rectangle rect = e.Bounds;
                rect.Offset(0, 1);
                rect.Inflate(0, -1);
                e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                e.DrawFocusRectangle();
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------  
        void ShowPanelIP(PLC aPLC)
        {
            if(aPLC != null)
            {
                if (aPLC.GetTypeComm() == Types.TypeComm.TCP)
                    panelAddressIP.Visible = true;
                else
                    panelAddressIP.Visible = false;
               string[] aAddrIP = aPLC.GetAddrIP().Split('.');
                try
                {
                    dEditIP1.Value = Convert.ToInt32(aAddrIP[0]);
                    dEditIP2.Value = Convert.ToInt32(aAddrIP[1]);
                    dEditIP3.Value = Convert.ToInt32(aAddrIP[2]);
                    dEditIP4.Value = Convert.ToInt32(aAddrIP[3]);
                }
                catch(Exception ex)
                {

                }

            }
        }
        //----------------------------------------------------------------------------------------------------------------------------  
        private void btnSetIP_Click(object sender, EventArgs e)
        {
            if (hpt1000 != null && hpt1000.GetPLC() != null)
            {
                string aAddrIP = dEditIP1.Value.ToString() + "." + dEditIP2.Value.ToString() + "." + dEditIP3.Value.ToString() + "." + dEditIP4.Value.ToString();

                hpt1000.GetPLC().SetAddrIP(aAddrIP);
                hpt1000.GetPLC().Connect();
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------  
        /**
         * Ustaw wartosc dla okna czasowego wykresu
         */ 
        private bool eEditChartWindow_Hour_EnterOn()
        {
            bool res = true;

            int windowChart = (int)(dEditChartWindow_Hour.Value * 60 * 60 + dEditChartWindow_Minute.Value * 60 + dEdit_ChartWindow_Sec.Value); //Konwertuj wartosc na sekundy
            DateTime chartWindow = new DateTime();
            chartWindow = chartWindow.AddSeconds(windowChart);
            if (hpt1000 != null)
                hpt1000.ChartWindowTime = chartWindow;
            return res;
        }
        //----------------------------------------------------------------------------------------------------------------------------  
        private void SettingsPanel_VisibleChanged(object sender, EventArgs e)
        {
            RefreshSettingsPanel();
            if(hpt1000 != null)
                SetEnabledComponent(grBoxAcqPara, hpt1000.EnabledAcq);
        }
        //----------------------------------------------------------------------------------------------------------------------------  
        //------GRUPA FUNKCJI DO ZARZADZANIA WYGLADEM LIST VIEW DLA GAZOW--------------------------
        private void listViewDevices_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (StringFormat sf = new StringFormat())
            {
                // Store the column text alignment, letting it default
                // to Left if it has not been set to Center or Right.
                switch (e.Header.TextAlign)
                {
                    case HorizontalAlignment.Center:
                        sf.Alignment = StringAlignment.Center;
                        break;
                    case HorizontalAlignment.Right:
                        sf.Alignment = StringAlignment.Far;
                        break;
                }
                // Draw the standard header background.
                e.DrawBackground();

                // Draw the header text.
                using (Font headerFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold))
                {
                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Black, e.Bounds, sf);
                }

                // Draw the background for an unselected item.
                using (LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, Color.LightBlue, Color.SkyBlue, LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
                e.DrawText();
            }
            return;
        }
        //----------------------------------------------------------------------------------------------------------------------------  
        private void listViewDevices_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            //Podswietl aktualnie wybrany kanał
            if (e.Item.Tag == selectedPara)
            {
                // Draw the background and focus rectangle for a selected item.
                e.Graphics.FillRectangle(Brushes.LightGray, e.Bounds);
                //      e.DrawFocusRectangle();
            }
            else
            {
                //e.Graphics.FillRectangle(Brushes.White, e.Bounds);
                //           e.DrawFocusRectangle();
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------  
        private void listViewDevices_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            TextFormatFlags flags = TextFormatFlags.Left;

            using (StringFormat sf = new StringFormat())
            {
                // Store the column text alignment, letting it default
                // to Left if it has not been set to Center or Right.
                switch (e.Header.TextAlign)
                {
                    case HorizontalAlignment.Center:
                        sf.Alignment = StringAlignment.Center;
                        flags = TextFormatFlags.HorizontalCenter;
                        break;
                    case HorizontalAlignment.Right:
                        sf.Alignment = StringAlignment.Far;
                        flags = TextFormatFlags.Right;
                        break;
                }
                // Draw the subitem text in red to highlight it. 
                e.Graphics.DrawString(e.SubItem.Text, listViewDevices.Font, Brushes.Green, e.Bounds, sf);
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie ustawienie fali czy mam pytac usera przed startem procesu o tym ze akwizycja danych nie jest wlaczona
         */   
        private void cBoxAskAcq_CheckedChanged(object sender, EventArgs e)
        {
            if (hpt1000 != null)
                hpt1000.AskAcq = cBoxAskAcq.Checked;
        }
        //----------------------------------------------------------------------------------------------------------------------------  

    }
}
