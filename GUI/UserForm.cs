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
using HPT1000.Source.Driver;

namespace HPT1000.GUI
{
    //Formatka jest wykorzystywana do tworzenia/edycji uzytkonika
    public partial class UserForm : Form
    {
        User             user       = null; //wskanizk na usera okresla nam czy tworzymy nowego czy edytujemy wybranego
        ApplicationData  appData    = null;
        //------------------------------------------------------------------------------------------------------------
        //Utworzenie formatki oraz ustawienie odpowidnich wskaznikow jak i uzupelnineie listy dostpenych uprawnien usera
        public UserForm()
        {
            InitializeComponent();
            FillLevelsPriviliges();
        }
        //------------------------------------------------------------------------------------------------------------
        //Metoda ma za zadanie przekazanie wskanizka na obiekt usera ktoregho kontem chcemy zarzadzac
        public void SetUserData(User aUser, ApplicationData AppData)
        {
            user = aUser;
            appData = AppData;
        }
        //------------------------------------------------------------------------------------------------------------
        //Wypelnij comboboxa sluzacego do ustawiania uprawnien usera wartoscimi ktore sa w typie wyliczeniowym (wprawdzam stringi z tego enuma) 
        private void FillLevelsPriviliges()
        {
            foreach (string aName in Enum.GetNames((typeof(Types.UserPrivilige))))
                cBoxLevelPriviliges.Items.Add(aName);                
        }
        //------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie ustawienia nazw/widocznosci komponentow w zlaeznosci od tego czy ja wykorzystyjemy do edycji czy tworzenia nowego usera
        private void ShowFormComponent()
        {
            cBoxDisableAcount.Checked = false;

            rBtnAfterData.Enabled = false;
            rBtnImmediately.Enabled = false;
            dateStart.Enabled = false;
            dateEnd.Enabled = false;
            
            //User nie jest null to znaczy ze formatk sluzy do utworzenia nowego usera
            if (user != null)
            {
                btnOK.Text                  = "Save";
                Text                        = "User Modify";
                cBoxEditPsw.Visible         = true;
                cBoxEditPsw.Checked         = false;
                tBoxPassword.Enabled        = false;
                tBoxConfirmPassword.Enabled = false;
            }
            else//user nie jest null to znaczy ze edytujemy qybranego usera
            {
                btnOK.Text                  = "OK";
                Text                        = "User Create";
                cBoxEditPsw.Visible         = false;
                tBoxPassword.Enabled        = true;
                tBoxConfirmPassword.Enabled = true;
            }
        }
        //------------------------------------------------------------------------------------------------------------
        //Funkcja sluzy do pokazania danych usera
        private void ShowUserData()
        {
            if (user != null)
            {
                tBoxName.Text                 = user.Name;
                tBoxSurname.Text              = user.Surname;
                tBoxLogin.Text                = user.Login;
                tBoxPassword.Text             = user.Password;
                cBoxUserChangeAccount.Checked = !user.AllowChangePsw;
                //Zabezpieczam sie na wypdaek proby ustawinie daty spoza zakresu min-max
                try { dateStart.Value = user.DateStartDisableAccount; }
                catch (Exception ex)
                {
                    dateStart.MinDate = DateTime.MinValue;
                    dateStart.MaxDate = DateTime.MaxValue;
                    dateStart.Value   = user.DateStartDisableAccount;
                }
                try { dateEnd.Value = user.DateEndDisableAccount; }
                catch (Exception ex)
                {
                    dateEnd.MinDate = DateTime.MinValue;
                    dateEnd.MaxDate = DateTime.MaxValue;
                    dateEnd.Value   = user.DateEndDisableAccount;
                }
                ShowUserPrivilige(user.Privilige);

                if (user.DisableAccount == Types.TypeDisableAccount.Access)
                {
                    cBoxDisableAcount.Checked = false;

                    rBtnAfterData.Enabled     = false;
                    rBtnImmediately.Enabled   = false;
                    dateStart.Enabled         = false;
                    dateEnd.Enabled           = false;
                }
                else
                {
                    rBtnAfterData.Enabled     = true;
                    rBtnImmediately.Enabled   = true;
                    dateStart.Enabled         = true;
                    dateEnd.Enabled           = true;

                    cBoxDisableAcount.Checked = true;
                    if (user.DisableAccount == Types.TypeDisableAccount.Temporarily)
                        rBtnAfterData.Checked = true;
                    if (user.DisableAccount == Types.TypeDisableAccount.Immediately)
                        rBtnImmediately.Checked = true;
                }
            }              
        }
        //------------------------------------------------------------------------------------------------------------
        //Ustaw pole comboboxa z uprawnoieniami zgodnie z uprawnieniami danego usera
        private void ShowUserPrivilige(Types.UserPrivilige privilige)
        {
            for(int i = 0; i < cBoxLevelPriviliges.Items.Count; i++)
            {
                if (cBoxLevelPriviliges.Items[i].ToString() == privilige.ToString())
                    cBoxLevelPriviliges.SelectedIndex = i;
            }
        }
        //------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie zwracanie uprawnienia zgodnie z ustawionym w comboboxie (Konewrtuje to co jest ustawione w comboxoe na privilige)
        private Types.UserPrivilige GetPrivilige()
        {
            Types.UserPrivilige privilige = Types.UserPrivilige.None;
            if(cBoxLevelPriviliges.SelectedItem != null && Enum.IsDefined(typeof(Types.UserPrivilige), cBoxLevelPriviliges.SelectedItem.ToString()))
                privilige = (Types.UserPrivilige)Enum.Parse(typeof(Types.UserPrivilige), cBoxLevelPriviliges.SelectedItem.ToString());

            return privilige;
        }
        //------------------------------------------------------------------------------------------------------------
        //Sprawdz czy wymagane pola podczas wprowadzania usera sa wypelnione
        private bool IsRequiredFiledFill()
        {
            bool aRes = false;

            if (tBoxLogin.Text != "" && cBoxLevelPriviliges.SelectedIndex >= 0)
                aRes = true;

            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie sprawdzenie czy hasla jest poprawnie potwierdzone
        private bool IsPasswordConfirm()
        {
            bool aRes = false;

            if (tBoxPassword.Text == tBoxConfirmPassword.Text)
                aRes = true;

            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------
        //Funkja tworzy nowego usera sprawdzajac przy tym czy mozna to zrobic (haslo i wymagane pola sa OK)
        private bool CreateNewUser()
        {
            bool aRes = false;

            User aNewUser = new User();

            aNewUser.Name                    = tBoxName.Text;
            aNewUser.Surname                 = tBoxSurname.Text;
            aNewUser.Login                   = tBoxLogin.Text;
            aNewUser.Password                = tBoxPassword.Text;
            aNewUser.AllowChangePsw          = !cBoxUserChangeAccount.Checked;
            aNewUser.DateStartDisableAccount = dateStart.Value;
            aNewUser.DateEndDisableAccount   = dateEnd.Value;
            aNewUser.Privilige               = GetPrivilige();

            if (cBoxDisableAcount.Checked)
            {
                if (rBtnAfterData.Checked)
                    aNewUser.DisableAccount = Types.TypeDisableAccount.Temporarily;
                if (rBtnImmediately.Checked)
                    aNewUser.DisableAccount = Types.TypeDisableAccount.Immediately;
            }
            else
                aNewUser.DisableAccount = Types.TypeDisableAccount.Access;

            bool aAccountIsCorrect = false;
            if (IsPasswordConfirm() && IsRequiredFiledFill())
            {
                aAccountIsCorrect = true;
            }
            else
            {
                //sprawdz czy sie hasla zgadzaja
                if (!IsPasswordConfirm())
                    MessageBox.Show("Can't create new user because confirm password is incorrect", "New user", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //sprawadz czy wymagane pola sa wypelnione
                if (!IsRequiredFiledFill())
                    MessageBox.Show("Can't create new user because require filed aren't fill", "New user", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Dane wprowadzone sa ok to sprobuj utworz usera w bazie danych
            if (appData != null && aAccountIsCorrect)
            {
                if (appData.AddUser(aNewUser) == 0)
                {
                    user = aNewUser;
                    aRes = true;
                    btnOK.Text = "Save";
                }
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie modyfikowanie danego usera
        private bool ModifyUser()
        {
            bool aRes = false;
            if(user != null)
            {
                //Wymagane pola sa uzupelnione i chcac zmienic haslo poprawnie go potwierdzilem to modyfikuj usera
                if(IsRequiredFiledFill() && (!cBoxEditPsw.Checked || (cBoxEditPsw.Checked && IsPasswordConfirm())))
                {
                    user.Name                    = tBoxName.Text;
                    user.Surname                 = tBoxSurname.Text;
                    user.Login                   = tBoxLogin.Text;
                    user.AllowChangePsw          = !cBoxUserChangeAccount.Checked;
                    user.DateStartDisableAccount = dateStart.Value;
                    user.DateEndDisableAccount   = dateEnd.Value;
                    user.Privilige               = GetPrivilige();

                    if(cBoxDisableAcount.Checked)
                    {
                        if(rBtnAfterData.Checked)
                            user.DisableAccount = Types.TypeDisableAccount.Temporarily;
                        if (rBtnImmediately.Checked)
                            user.DisableAccount = Types.TypeDisableAccount.Immediately;
                    }
                    else
                        user.DisableAccount = Types.TypeDisableAccount.Access;

                    if (cBoxEditPsw.Checked)
                        user.Password = tBoxPassword.Text;

                    //Modyfikuj user w bazi danych
                    if (appData != null && appData.ModifyUser(user) == 0)
                        aRes = true;
                }
                else
                    MessageBox.Show("Can't modify user because require fileds aren't fill or confirm password is incorrect", "Modify user", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------
        //Potwierdzenie wykonania operacji dodania/modyfikacji usera
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (user != null)
            {
                if (ModifyUser())
                     MessageBox.Show("User " + user.Login + " data has been modified successfully", "Modify user", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (CreateNewUser())
                    MessageBox.Show("User " + user.Login + "  has been created successfully", "Modify user", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //Po kazdej akcji kasuje ustawienia password 
            cBoxEditPsw.Checked      = false;
            Close();
        }
        //------------------------------------------------------------------------------------------------------------
        //Zamknij formtke nie wprowadzaj zadnych zmian
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        //------------------------------------------------------------------------------------------------------------
        private void cBoxEditPsw_CheckStateChanged(object sender, EventArgs e)
        {
            //Jezeli formatka sluzy do edytowania to ustaw pola hasla do edycji lub nie w zalenozisci od tego co che user zrobi
            if(user != null)
            {
                tBoxPassword.Enabled        = cBoxEditPsw.Checked;
                tBoxConfirmPassword.Enabled = cBoxEditPsw.Checked;
            }
            else//Formatk sluzy do tworzenia nowego usera pola wprawadzania hasla sa zawsze dostepne
            {
                tBoxPassword.Enabled        = true;
                tBoxConfirmPassword.Enabled = true;
            }
        }
        //------------------------------------------------------------------------------------------------------------
        private void UserForm_Load(object sender, EventArgs e)
        {
            //Ustaw odpowiednie komonenty formatki w zaleznosci czy jestem New czy Modify user
            ShowFormComponent();
            //Uzupelnij dane usera na formatce
            ShowUserData();
        }
        //------------------------------------------------------------------------------------------------------------
        private void cBoxDisableAcount_Click(object sender, EventArgs e)
        { 
            rBtnAfterData.Enabled   = cBoxDisableAcount.Checked;
            rBtnImmediately.Enabled = cBoxDisableAcount.Checked;
            dateStart.Enabled       = cBoxDisableAcount.Checked;
            dateEnd.Enabled         = cBoxDisableAcount.Checked;
        }
        //------------------------------------------------------------------------------------------------------------
        private void dateStart_ValueChanged(object sender, EventArgs e)
        {
            //Ustaw in date pod warunkiem ze jest wlaczona opcja
            if (cBoxDisableAcount.Checked)
                dateEnd.MinDate = dateStart.Value;
            else
                dateStart.MinDate = DateTime.MinValue;
        }
        //------------------------------------------------------------------------------------------------------------
        private void dateEnd_ValueChanged(object sender, EventArgs e)
        {
            //Ustaw max date pod warunkiem ze jest wlaczona opcja
            if(cBoxDisableAcount.Checked)
                dateStart.MaxDate = dateEnd.Value;
            else
                dateStart.MaxDate = DateTime.MaxValue;
        }
        //------------------------------------------------------------------------------------------------------------

    }
}
