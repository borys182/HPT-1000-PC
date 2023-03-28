using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;
namespace HPT1000.Source
{
    /**
     * Klasa jest odpowiedzialna za reprezentowania Usera w aplikacji. Poasiada wszelkie informacje na jego temat
     */ 
    public class User
    {
        private Int32               id;                                                     //ID usera w bazie danych
        private string              name;
        private string              surname;
        private string              login;
        private string              password ;
        private Types.UserPrivilige privilige               = Types.UserPrivilige.Operator;
        private bool                allowChangePsw          = true;                         //falga okresla czy user moze zmienic parametry konta (password)
        private DateTime            dateStartDisableAccount = DateTime.Now;                 //data okresla do kiedy konto ma być aktywne
        private DateTime            dateEndtDisableAccount  = DateTime.Now;                 //data okresla do kiedy konto ma być aktywne
        private Types.TypeDisableAccount disableAccount = Types.TypeDisableAccount.Access;

        //---------------------------------------------------------------------------------------------------------------------
        public Int32 ID
        {
            set { id = value; }
            get { return id; }
        }
        //---------------------------------------------------------------------------------------------------------------------
        public string Name
        {
            set { name = value; }
            get { return name; }
        }
        //---------------------------------------------------------------------------------------------------------------------
        public string Surname
        {
            set { surname = value; }
            get { return surname; }
        }
        //---------------------------------------------------------------------------------------------------------------------
        public string Login
        {
            set { login = value; }
            get { return login; }
        }
        //---------------------------------------------------------------------------------------------------------------------
        public Types.TypeDisableAccount DisableAccount
        {
            set { disableAccount = value; }
            get { return disableAccount; }
        }
        //---------------------------------------------------------------------------------------------------------------------
        public DateTime DateStartDisableAccount
        {
            set { dateStartDisableAccount = value; }
            get { return dateStartDisableAccount; }
        }
        //---------------------------------------------------------------------------------------------------------------------
        public DateTime DateEndDisableAccount
        {
            set { dateEndtDisableAccount = value; }
            get { return dateEndtDisableAccount; }
        }
        //---------------------------------------------------------------------------------------------------------------------
        public bool AllowChangePsw
        {
            set { allowChangePsw = value; }
            get { return allowChangePsw; }
        }
        //---------------------------------------------------------------------------------------------------------------------
        public Types.UserPrivilige Privilige
        {
            set { privilige = value; }
            get { return privilige;  }
        }
        //---------------------------------------------------------------------------------------------------------------------
        public User()
        {

        }
        //---------------------------------------------------------------------------------------------------------------------
        public User(Types.UserPrivilige aPrivilige,string psw,string alogin)
        {
            privilige = aPrivilige;
            password  = psw;
            login     = alogin;
        }
        //---------------------------------------------------------------------------------------------------------------------
        public string Password
        {
            set { password = value; }
            get { return password; }
        }
        //---------------------------------------------------------------------------------------------------------------------
        //Przeciarzenie funkcji ToString ktrora jest wywolywana poprzez mechanizm dodawania jako ItemComboBox Usera
        public override string ToString()
        {
            return login;
        }
        //---------------------------------------------------------------------------------------------------------------------
        //Przeciarzenie funkcji porownywania obiektow. Jest wykorzystywana do dodawania tylko raz danego usera do listy
        public override bool Equals(object obj)
        {
            bool aRes = false;

            if (obj is User)
            {
                User user = (User)obj;

                if (user != null && user.Privilige == privilige && user.Password == password && user.Login == login)
                    aRes = true;

            }
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------
        //Plytka kopia obiektu
        public User Copy()
        {
            return (User)this.MemberwiseClone();
        }
        //---------------------------------------------------------------------------------------------------------------------

    }
}
