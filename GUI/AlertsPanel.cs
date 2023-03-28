using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using HPT1000.Source;
using HPT1000.Source.Program;
using HPT1000.Source.Driver;

namespace HPT1000.GUI
{
    public partial class AlertsPanel : UserControl
    {
        private Source.Driver.HPT1000 hpt1000 = null;
        //W celu optymalizacji dodawania logow do listview tworze loklna liste i tak aby list view pracowala tylko na nowych logach a nie przegladala ciagle calej listy
        private List<ItemLogger> listLogs = new List<ItemLogger>();

        //---------------------------------------------------------------------------------------
        public Source.Driver.HPT1000 HPT1000
        {
            set { hpt1000 = value; }
            get { return hpt1000; }
        }
        //---------------------------------------------------------------------------------------
        public AlertsPanel()
        {
            InitializeComponent();
        }
        //----------------------------------------------------------------------------------
        public void RefreshPanel()
        { 
            //Wyodrebnij z listy wszystkich bledow nowe bledy i je wyswietl
            List<ItemLogger> listNewLogs = new List<ItemLogger>(); //Lista nowych logow od ostatniego Refresh
        
            for (int i = 0; i < Logger.GetLogList().Count; i++)
            {
                ItemLogger itemLog = Logger.GetLogList()[i];
                if(!listLogs.Contains(itemLog))
                {
                    listLogs.Add(itemLog);
                    listNewLogs.Add(itemLog);
                }               
            }
                //przegladaj tylko nowe bledy
            ShowLogViewErrors(listNewLogs);
            ShowLogViewEvents(listNewLogs);
            //Aktualizuj dana na temat starych
            UpdateList();
        }
        //----------------------------------------------------------------------------------
        private void UpdateList()
        {
            for(int i = 0; i < listViewErrors.Items.Count; i++)
            {
                ItemLogger itemLog = (ItemLogger)listViewErrors.Items[i].Tag;
                if(itemLog != null)
                {
                    if(listViewErrors.Items[i].SubItems.Count > 5 && listViewErrors.Items[i].SubItems[5].Text == "" && itemLog.UserConfirm != "")
                        listViewErrors.Items[i].SubItems[5].Text = itemLog.UserConfirm;
                }
            }
        }
        //----------------------------------------------------------------------------------
        private void ShowLogViewErrors(List<ItemLogger> listLogs)
        {
            string aNameProgram     = "Program";
            string aNameSubprogram  = "Subprogram";

            if (listLogs != null)
            {
                for (int i = 0; i < listLogs.Count; i++)
                {
                    ItemLogger itemLog = listLogs[i];

                    if (itemLog.IsError())
                    {
                        if (!IsItemExistInLstView(listViewErrors, itemLog))
                        {
                            aNameProgram = GetNameProgram(itemLog.ProgramID);
                            aNameSubprogram = GetNameSubprogram(itemLog.SubprogramID);

                            ListViewItem aItem = new ListViewItem();

                            string aErrCode = "";

                            if (itemLog.TypeLog == Types.MessageType.Error)
                                aErrCode = "E 0x" + itemLog.GetErrorCode().ToString("X8");

                            if (itemLog.TypeLog == Types.MessageType.Warrning)
                                aErrCode = "W 0x" + itemLog.GetErrorCode().ToString("X8");

                            aItem.Text = aErrCode;
                            aItem.SubItems.Add(itemLog.Time.ToString());
                            aItem.SubItems.Add(itemLog.GetText());
                            aItem.SubItems.Add(aNameProgram);
                            aItem.SubItems.Add(aNameSubprogram);
                            aItem.SubItems.Add(itemLog.UserConfirm);

                            aItem.Tag = itemLog;

                            listViewErrors.Items.Insert(0,aItem);
                        }
                        else
                        {
                            //Aktualizacja wpisu w LogView na temat osoby ktora potwierdzila dany blad
                            ListViewItem aItem = GetItemFromListView(itemLog); // pobierz Item ktroy odpowiadana daenum Logowi (jest on zapisany jako Tag)
                            if (aItem != null && aItem.SubItems.Count > 5 && aItem.SubItems[5].Text == "" && itemLog != null && itemLog.UserConfirm != "")
                                aItem.SubItems[5].Text = itemLog.UserConfirm;
                        }
                    }
                }
            }
        }
        //----------------------------------------------------------------------------------
        private void ShowLogViewEvents(List<ItemLogger> listLogs)
        {
            if (listLogs != null)
            {
                for (int i = 0; i < listLogs.Count; i++)
                {
                    ItemLogger itemLog = listLogs[i];

                    if (itemLog.IsInformation() && !IsItemExistInLstView(listViewEvents, itemLog))
                    {
                        ListViewItem aItem = new ListViewItem();

                        aItem.Text = itemLog.Time.ToString();
                        aItem.SubItems.Add(itemLog.GetText());
                        aItem.Tag = itemLog;

                        listViewEvents.Items.Insert(0,aItem);
                    }
                }
            }
        }
        //----------------------------------------------------------------------------------
        //Funkcja ma za zadanie zwrocenie Itemu z listView ktroy przechowuje dany wpis z Loggera
        public ListViewItem GetItemFromListView(ItemLogger itemLog)
        {
            ListViewItem itemListViewRes = null;

            foreach(ListViewItem itemListView in listViewErrors.Items)
            {
                if (itemListView.Tag == itemLog)
                    itemListViewRes = itemListView;
            }
            return itemListViewRes;
        }
        //----------------------------------------------------------------------------------
        private string GetNameProgram(int aId)
        {
            string aName = "Not running";

            if(hpt1000 != null)
            {
                foreach(Program program in hpt1000.GetPrograms())
                {
                    if(program.GetID() == aId)
                    {
                        aName = program.GetName();
                        break;
                    }
                }
            }

            return aName;
        }
        //----------------------------------------------------------------------------------
        private string GetNameSubprogram(int aId)
        {
            string aName = "Not running";

            if (hpt1000 != null)
            {
                foreach (Program program in hpt1000.GetPrograms())
                {
                    foreach (Subprogram subprogram in program.GetSubprograms())
                    {
                        if (subprogram.ID == aId)
                        {
                            aName = subprogram.GetName();
                            break;
                        }
                    }
                }
            }
            return aName;
        }
        //----------------------------------------------------------------------------------
        private bool IsItemExistInLstView(ListView listView , ItemLogger itemLog)
        {
            bool aRes = false;

            if (listView != null && itemLog != null)
            {
                foreach (ListViewItem aItem in listView.Items)
                {
                    if (itemLog.Equals(aItem.Tag))
                        aRes = true;
                }
            }
            return aRes;
        }
        //----------------------------------------------------------------------------------
        private void listViewErrors_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
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
        //----------------------------------------------------------------------------------
        private void listViewErrors_DrawItem(object sender, DrawListViewItemEventArgs e)
        {            
            if ((e.State & ListViewItemStates.Selected) != 0)
            {
                // Draw the background and focus rectangle for a selected item.
                e.Graphics.FillRectangle(Brushes.LightGray, e.Bounds);
                e.DrawFocusRectangle();
            }
            else
            {
                // Draw the background for an unselected item.
                using (LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal))
                {
                 //   e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }            
            // Draw the item text for views other than the Details view.
            //e.DrawText();      
        }
        //----------------------------------------------------------------------------------
        private void listViewErrors_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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
                if(((ListView)sender).Name == "listViewErrors")
                    e.Graphics.DrawString(e.SubItem.Text, listViewErrors.Font, Brushes.Red, e.Bounds, sf);
                else
                    e.Graphics.DrawString(e.SubItem.Text, listViewErrors.Font, Brushes.Green, e.Bounds, sf);

                // Draw normal text for a subitem with a nonnegative 
                // or nonnumerical value.
                //         e.DrawText(flags);
            }
        }
        //----------------------------------------------------------------------------------
        //Na zdarzenie double click pokaz komunikat zapytania czy mam potwierdzic dany blad
        private void listViewErrors_DoubleClick(object sender, EventArgs e)
        {
            //Jezeli item jeszcze nie jest potwierdzony to pokaz komunikat zapytania
            ItemLogger selectedItemLogger = null;

            foreach (ListViewItem item in listViewErrors.Items)
            {
                if (item.Selected)
                    selectedItemLogger = (ItemLogger)item.Tag;
            }
            if (selectedItemLogger != null && !selectedItemLogger.ConfirmError)
            {
                DialogResult result = MessageBox.Show("Do you want confirm selected errors", "Confirm Errors", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //potwierdz blad dla kazdego zaznaczonego bledu
                if (result == DialogResult.Yes)
                    selectedItemLogger.SetConfirmError();
            }
        }
        //----------------------------------------------------------------------------------
        private void btnClearError_Click(object sender, EventArgs e)
        {
            listViewErrors.Items.Clear();
            if(hpt1000 != null && hpt1000.GetPLC() != null)
            {
                if(hpt1000.GetPLC().SetDevice(Types.ADDR_FLAGE_CLEAR_ERROR, 1)!= 0)
                    MessageBox.Show("Occurred problem during deleting errors from driver PLC", "Clear PLC Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Logger.ClearLogger();
        }
        //----------------------------------------------------------------------------------
    }
}
