using System;
using System.Drawing;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HPT1000.Source;

namespace HPT1000.GUI
{
    //Panel jest wykorzystywany do zarzadzania userami i posiada funkcje takie jak doda/usun/modyfikuj usera
    public partial class UserManagerPanel : UserControl
    {
        private ApplicationData appData = null; //Wskaznik na obiekt aplikacji posiadajacy info o userach
        UserForm userForm = new UserForm();

        //------------------------------------------------------------------------------------------------
        public ApplicationData AppData
        {
            set {appData = value;}
        }
        //Konstruktor panelu
        //------------------------------------------------------------------------------------------------
        public UserManagerPanel()
        {
            InitializeComponent();
        }
        //------------------------------------------------------------------------------------------------
        //Odswiez dane na temat uzytkownikow na liscie. Dodawaj kolejne Itemy do listview
        public void RefreshUsers()
        {
            listViewUsers.Items.Clear();
            if(appData != null)
            {
                foreach(User user in appData.Users)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = user.Name;
                    item.SubItems.Add(user.Surname);
                    item.SubItems.Add(user.Login);
                    item.SubItems.Add(user.Privilige.ToString());

                    if (user.AllowChangePsw)
                        item.SubItems.Add("YES");
                    else
                        item.SubItems.Add("NO");

                    //Wprowadz tekst do bola block okresljacy czy konto jest juz zablokowana czy bdzie zablokowane czasowo czy jest aktywne
                    string blockAccount = "NO";
                    //Sprawdz czy konto nie jest zablokowane caly czas
                    if (user.DisableAccount == Source.Driver.Types.TypeDisableAccount.Immediately)
                        blockAccount = "ALL TIME";
                    if (user.DisableAccount == Source.Driver.Types.TypeDisableAccount.Temporarily)
                    {
                        if (DateTime.Now >= user.DateStartDisableAccount && DateTime.Now <= user.DateEndDisableAccount)
                            blockAccount = "TEMPORARILY";
                        if (DateTime.Now < user.DateStartDisableAccount)
                            blockAccount = "NOT YET";
                        if (DateTime.Now > user.DateEndDisableAccount)
                            blockAccount = "NO MORE";
                    }
                item.SubItems.Add(blockAccount);

                    string startDate = "";
                    string endDate   = "";
                    if (user.DisableAccount == Source.Driver.Types.TypeDisableAccount.Temporarily)
                    {
                        startDate = user.DateStartDisableAccount.ToShortDateString();
                        endDate   = user.DateEndDisableAccount.ToShortDateString();
                    }
                    item.SubItems.Add(startDate);
                    item.SubItems.Add(endDate);

                    item.Tag = user;
                    listViewUsers.Items.Add(item);
                }
            }
        }
        //------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie podanie Usera powiazanego z zaznaczonym aktualnie Itemem listview
        private User GetSelectedUser()
        {
            User user = null;

            foreach (ListViewItem item in listViewUsers.Items)
            {
                if (item.Selected)
                    user = (User)item.Tag;
            }

            return user;
        }
        //------------------------------------------------------------------------------------------------
        //Odswiez liste userow gdy panel dostanie focus
        private void UserManagerPanel_VisibleChanged(object sender, EventArgs e)
        {
            RefreshUsers();
        }
        //------------------------------------------------------------------------------------------------
        //Obsluga zdarzenia podwujnego kliku jako edycji danego usera
        private void listViewUsers_DoubleClick(object sender, EventArgs e)
        {
            User     user     = GetSelectedUser();  //Pobierz usera kryjacego sie poda danym Itemem
            //Pokaz formatke edycji usera
            userForm.SetUserData(user, appData);
            userForm.ShowDialog(this);
        }
        //------------------------------------------------------------------------------------------------
        //Obsluga zdarzenia dodania nowego usera. Jest ona odpowiedzialna tylko za pokazanie formatki sluzacej do tworzenia nowego usera
        private void newUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Utworz i pokaz formatke tworzenia nowego usera
            userForm.SetUserData(null, appData);
            userForm.ShowDialog(this);
        }
        //------------------------------------------------------------------------------------------------
        //Obsluga zdarzenia usuwania usera 
        private void removeUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User         user   = GetSelectedUser();  //Pobierz usera powiazanego z danym Itemem
            DialogResult result = DialogResult.Yes;

            if (user != null)
                result = MessageBox.Show("Are you sure want to remove user " + user.Login, "Remove user", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                                                //Sprwdz czy user zostal wybrany
                if (user != null && appData != null)
                {
                    //Wywolaj funkcje odpowiedzilana za usuwanie usera i odpieci go z listy dostepnych userow
                    string aLogin = user.Login;
                    if (appData.RemoveUser(user) == 0)
                        MessageBox.Show("User " + aLogin + " has been removed", "User remove", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("No selected user to remove", "User remove", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //------------------------------------------------------------------------------------------------
        //Funkcja odpowiedzialna za wizualizacje headera ListView
        private void listViewUsers_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
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
        //Funkcja odpowiedzialna za wizualizacje itema ListView
        private void listViewUsers_DrawItem(object sender, DrawListViewItemEventArgs e)
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
         /*       using (LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal))
                {
                       e.Graphics.FillRectangle(brush, e.Bounds);
                }
          */  }
            // Draw the item text for views other than the Details view.
            //e.DrawText();      
        }
        //----------------------------------------------------------------------------------
        //Funkcja odpowiedzialna za wizualizacje subitema ListView
        private void listViewUsers_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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
                 e.Graphics.DrawString(e.SubItem.Text, listViewUsers.Font, Brushes.Green, e.Bounds, sf);


                // Draw normal text for a subitem with a nonnegative 
                // or nonnumerical value.
                //         e.DrawText(flags);
            }
        }
        //----------------------------------------------------------------------------------
        private void timer_Tick(object sender, EventArgs e)
        {
            if (GetSelectedUser() != null)
                btnRemoveUser.Enabled = true;
            else
                btnRemoveUser.Enabled = false;
        }
        //----------------------------------------------------------------------------------
        //Meotda ma za zadanie zworcenie referencji na obiekt formatki edytujacej konto usera
        public UserForm GetUserForm()
        {
            return userForm;
        }
    }
}
