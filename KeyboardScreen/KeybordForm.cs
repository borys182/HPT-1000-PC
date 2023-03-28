using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardScreen
{
    public partial class Keybord : Form
    {
        //Przeciarzenie parametrow - te miejsce w kodzie zapwania nam to ze kontrlkoi klawiatury nie zabieraja focusda aplikacji glowenj
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams param = base.CreateParams;
                param.ExStyle |= 0x08000000;
                return param;
            }
        }
        public Keybord()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Funkcja timer jest odpowiedzialna za sprawdzanie wartosci przekazanej od aplikacji glownej i wywolanie odpowidniej klawaitury numerczynej badz tekstowej 
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            bool aNumKeyboard = false;
            try
            {
                //Sprawdz jaka forme mam wyswietlic
                aNumKeyboard = (Boolean)Clipboard.GetData("TypeKeyboardAsNum");
            }
            catch (Exception ex) { }
            // Wyswietl odpowiednia klawiature
            if (aNumKeyboard)
            {
                keybordTxtControl.Visible = false;
                keybordNumControl.Visible = true;
                Size = new Size(236, 229);
            }
            else
            {
                keybordNumControl.Visible = false;
                keybordTxtControl.Visible = true;
                Size = new Size(545, 283);
            }
        }     
    }
}
