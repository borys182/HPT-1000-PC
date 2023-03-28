using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using HPT1000.Source;
namespace HPT1000.GUI
{
    //Formatka sluzy do logowania usera. Umozliwia takze zmiane hasla przez usera jezeli posiada takie uprawnienia
    public partial class Login : Form
    {
        //private Source.DB db = null;
        private ApplicationData appData = null; //wskaznik na obiekt aplikacji zawierajacy info na temat userow

        //rozmiary okna logowania w zaleznosci od wybranej opcji i dostepnych uprawnien
        private int[] sizeStandard = new int[] { 226, 160 };
        private int[] sizeEdit     = new int[] { 278, 160 };
        private int[] sizeChange   = new int[] { 278, 270 };

        enum TypeForm { Standard,Edit,Change};  //Okreslenie rozmiaru formy w zaleznosci od tego co robie.
                                                //Jezeli user posiada mozliwosc zmiany swojego haslo do [powiekszam forme do rozmiaru Edit. Jezeli klikne Edit to forma przyjmuje rozmiar Change
        //-------------------------------------------------------------------------------------------------------------
        public Login()
        {        
            InitializeComponent();
            ShowCorrespondingSizeForm(TypeForm.Standard);
        }
        //-------------------------------------------------------------------------------------------------------------
        //Ustaw wskaznik aplikacji zawierajace info na temat userow i aktulnie zalogowanego usera
        public void SetApp(ApplicationData aApp)
        {
            appData = aApp;
        }
        //-------------------------------------------------------------------------------------------------------------
        //Funkcja wykonuje operacje zwizane z logowaniem usera do aplikacji
        private void LoginUser()
        {
            if (appData != null)
            {
                User user = (User)cBoxUsers.SelectedItem;
                bool aRes = appData.LoginUser(user, tBoxPassword.Text);

                if (aRes)
                    Close();
                else
                    MessageBox.Show("User can't be login. Wrong password", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        //Wylistuj wszystkoch userow i dodaj ich do combobxa oraz zaznacz aktualnie zalogowanego usera
        private void ShowUsers()
        {
            int index = -1;
            if (appData != null)
            {
                int i = 0;
                cBoxUsers.Items.Clear();
                foreach (User user in appData.Users)
                {
                    cBoxUsers.Items.Add(user);
                    if (ApplicationData.LoggedUser.Equals(user))
                        index = i;
                    i++;
                }
            }
            cBoxUsers.SelectedIndex = index;
        }
        //-------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie ustawienie rozmiaru okna w zaleznosci od posiadanych uprawnienin i wybranej operacji
        //Pokaz odpowidni komponenty w zaleznosci od operacji (np edit password)
        private void ShowCorrespondingSizeForm(TypeForm kindForm)
        {
            grBoxChange.Visible = false;
            grBoxLogin.Visible  = true;
            switch (kindForm)
            {
                //standardowy user nie posiadajacy uprawnien do zmiany hasloa
                case TypeForm.Standard:
                    Width  = sizeStandard[0];
                    Height = sizeStandard[1];
                    break;
                //user posiada uprawnienia do zmiany hasla dlatego pokaz mu przycisk (poszerz formę) pozwalajacy zmienic haslo
                case TypeForm.Edit:
                    Width  = sizeEdit[0];
                    Height = sizeEdit[1];
                    break;
                //pokaz komponenty do zmiany hasla
                case TypeForm.Change:
                    Width  = sizeChange[0];
                    Height = sizeChange[1];
                    grBoxChange.Visible = true;
                    grBoxLogin.Visible  = false;
                    break;

                default:
                    Width  = sizeStandard[0];
                    Height = sizeStandard[1];
                    break;
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        //Wykonaj operacje logowania do aplikacji
        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginUser();
        }
        //-------------------------------------------------------------------------------------------------------------
        //Zamknij okno logowania
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        //-------------------------------------------------------------------------------------------------------------
        //Zdarzenie nacisniecia klawisza enter wywoluje operacje logowania
        private void tBoxPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                LoginUser();
        }
        //-------------------------------------------------------------------------------------------------------------
        //Pokaz formatke do edycji haslo bo user sobie tego rzada - kliknl przycisk Edit
        private void btnEdit_Click(object sender, EventArgs e)
        {
            ShowCorrespondingSizeForm(TypeForm.Change);
        }
        //-------------------------------------------------------------------------------------------------------------
        //Zdarzenie zmiany wybranego usera z listy. Dostosuj formatke do jego uprawnien
        private void cBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Sprawdz czy wybrany User posiada mozliwosc edycji swojego konta. Jezeli tak to pokaz formatek ktora mu to umozliwi
            User user = (User)cBoxUsers.SelectedItem;
            if (user != null && user.AllowChangePsw)
                ShowCorrespondingSizeForm(TypeForm.Edit);
            else
                ShowCorrespondingSizeForm(TypeForm.Standard);
        }
        //-------------------------------------------------------------------------------------------------------------
        //Zrezygnowana ze zmiany hasla to pokaz formatke bez komponentow edycji
        private void btnCancelChange_Click(object sender, EventArgs e)
        {
            ShowCorrespondingSizeForm(TypeForm.Edit);
        }
        //-------------------------------------------------------------------------------------------------------------
        //Zadanie zmiany hasla. Sprawdz czy wszystkie formalnosci sa dokanan i zmien ewentualnie haslo
        private void btnOkChange_Click(object sender, EventArgs e)
        {
            User user = (User)cBoxUsers.SelectedItem;

            if (user != null)
            {
                //Sprawdz czy user zna aktualne haslo
                if (user.Password == tBoxCurrentPassword.Text)
                {
                    //Sprawdz czy wprowadzone potwierdzenie hasla jest poprawne
                    if (tBoxNewPassword.Text == tBoxConfirmNewPassword.Text)
                    {
                        user.Password = tBoxNewPassword.Text;
                        MessageBox.Show("Password has been changed", "Change password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowCorrespondingSizeForm(TypeForm.Edit); //haslo zostalo zmienieno wiec schowaj komponenty pozwalajace zmienic haslo
                    }
                    else
                        MessageBox.Show("Can't change password because new password and confirm password aren't the same", "Change password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Can't change password because current password is incorect", "Change password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                              
            }
            else
                MessageBox.Show("User hasn't been selected. Please select user to change password", "Change password", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //-------------------------------------------------------------------------------------------------------------
        //Zdarzenie przygotowuje formatke pod nastepne uruchomienie
        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            cBoxUsers.SelectedIndex = -1;
            tBoxPassword.Text = "";

            ShowCorrespondingSizeForm(TypeForm.Standard);
        }
        //-------------------------------------------------------------------------------------------------------------
        //Pokaz userow podczas ladowania
        private void Login_Load(object sender, EventArgs e)
        {
            ShowUsers();
        }
        //-------------------------------------------------------------------------------------------------------------

    }
}
