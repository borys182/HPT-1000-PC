using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardScreen
{ 
    public partial class KeybordTxtControl : UserControl
    {
        Dictionary<char, char> listKeysShiftPressd = new Dictionary<char, char>(); //Lista przechowuje para znakow ktore odpowiadaja za wcisniety shift
        //---------------------------------------------------------------------------------------------------------------------------
        public KeybordTxtControl()
        {
            InitializeComponent();
            InitKeysShiftPressd();
        }
        //---------------------------------------------------------------------------------------------------------------------------
        /**
         * Wypelnij liste par znakow ktor odpoiadja danemu przyciskowi kiedy jest wcisniety Shift
         */ 
        private void InitKeysShiftPressd()
        {
            listKeysShiftPressd.Add('1', '!');
            listKeysShiftPressd.Add('2', '@');
            listKeysShiftPressd.Add('3', '#');
            listKeysShiftPressd.Add('4', '$');
            listKeysShiftPressd.Add('5', '%');
            listKeysShiftPressd.Add('6', '^');
            listKeysShiftPressd.Add('7', '&');
            listKeysShiftPressd.Add('8', '*');
            listKeysShiftPressd.Add('9', '(');
            listKeysShiftPressd.Add('0', ')');
            listKeysShiftPressd.Add('-', '_');
            listKeysShiftPressd.Add('=', '+');
            listKeysShiftPressd.Add('[', '{');
            listKeysShiftPressd.Add(']', '}');
            listKeysShiftPressd.Add('\\', '|');
            listKeysShiftPressd.Add(';', ':');
            listKeysShiftPressd.Add('\'', '"');
            listKeysShiftPressd.Add(',', '<');
            listKeysShiftPressd.Add('.', '>');
            listKeysShiftPressd.Add('/', '?');         
        }
        //---------------------------------------------------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest zwrocenie znaku ktory sie znajduje pod danym przyciskiem w sytuacji gdy jest wcisniety szift badz nie
         */
        private string GetKeyHoldShift(String keyIn, bool shiftPressd)
        {
            string keyRes = keyIn;
            try
            {
                char tmp;
                if (shiftPressd)
                {
                    if (listKeysShiftPressd.TryGetValue(Convert.ToChar(keyIn), out tmp))
                        keyRes = tmp.ToString();
                }
                else
                {
                    if (listKeysShiftPressd.ContainsValue(Convert.ToChar(keyIn)))
                    {
                        List<char> keys   = listKeysShiftPressd.Keys.ToList();
                        List<char> values = listKeysShiftPressd.Values.ToList();

                        for (int i = 0; i < values.Count; i++)
                        {
                            if (values[i] == Convert.ToChar(keyIn))
                            {
                                keyRes = keys[i].ToString();
                                break;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                keyRes = keyIn;
            }
            if (keyRes == "&")
                keyRes = "&&";
            return keyRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void SendKey(Control ctr)
        {
            if(ctr != null)
                SendKeys.Send(ctr.Text);
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void btn_1_Click(object sender, EventArgs e)
        {
            SendKeys.Send(btn_1.Text);
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            SendKeys.Send(btn_2.Text);
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            SendKeys.Send(btn_3.Text);
        }
        private void btn_4_Click(object sender, EventArgs e)
        {
            SendKeys.Send(btn_4.Text);
        }
        private void btn_5_Click(object sender, EventArgs e)
        {
            string key = btn_5.Text;
            if (key == "%")
                key = "{%}";
            SendKeys.Send(key);
        }
        private void btn_6_Click(object sender, EventArgs e)
        {
            string key = btn_6.Text;
            if (key == "^")
                key = "{^}";
            SendKeys.Send(key);
        }
        private void btn_7_Click(object sender, EventArgs e)
        {
            string key = btn_7.Text;
            if (key == "&&")
                key = "{&}";
            SendKeys.Send(key);
        }
        private void btn_8_Click(object sender, EventArgs e)
        {
            SendKeys.Send(btn_8.Text);
        }
        private void btn_9_Click(object sender, EventArgs e)
        {
            string key = btn_9.Text;
            if (key == "(")
                key = "{(}";
            SendKeys.Send(key);
        }
        private void btn_0_Click(object sender, EventArgs e)
        {
            string key = btn_0.Text;
            if (key == ")")
                key = "{)}";
            SendKeys.Send(key);
        }
        private void btn_q_Click(object sender, EventArgs e)
        {
            SendKey(btn_q);
        }
        private void btn_w_Click(object sender, EventArgs e)
        {
            SendKey(btn_w);
        }
        private void btn_e_Click(object sender, EventArgs e)
        {
            SendKey(btn_e);
        }
        private void btn_r_Click(object sender, EventArgs e)
        {
            SendKey(btn_r);
        }
        private void btn_t_Click(object sender, EventArgs e)
        {
            SendKey(btn_t);
        }
        private void btn_y_Click(object sender, EventArgs e)
        {
            SendKey(btn_y);
        }
        private void btn_u_Click(object sender, EventArgs e)
        {
            SendKey(btn_u);
        }
        private void btn_i_Click(object sender, EventArgs e)
        {
            SendKey(btn_i);
        }
        private void btn_o_Click(object sender, EventArgs e)
        {
            SendKey(btn_o);
        }
        private void btn_p_Click(object sender, EventArgs e)
        {
            SendKey(btn_p);
        }
        private void btn_a_Click(object sender, EventArgs e)
        {
            SendKey(btn_a);
        }
        private void btn_s_Click(object sender, EventArgs e)
        {
            SendKey(btn_s);
        }
        private void btn_d_Click(object sender, EventArgs e)
        {
            SendKey(btn_d);
        }
        private void btn_f_Click(object sender, EventArgs e)
        {
            SendKey(btn_f);
        }
        private void btn_g_Click(object sender, EventArgs e)
        {
            SendKey(btn_g);
        }
        private void btn_h_Click(object sender, EventArgs e)
        {
            SendKey(btn_h);
        }
        private void btn_j_Click(object sender, EventArgs e)
        {
            SendKey(btn_j);
        }
        private void btn_k_Click(object sender, EventArgs e)
        {
            SendKey(btn_k);
        }
        private void btn_l_Click(object sender, EventArgs e)
        {
            SendKey(btn_l);
        }
        private void btn_z_Click(object sender, EventArgs e)
        {
            SendKey(btn_z);
        }
        private void btn_x_Click(object sender, EventArgs e)
        {
            SendKey(btn_x);
        }
        private void btn_c_Click(object sender, EventArgs e)
        {
            SendKey(btn_c);
        }
        private void btn_v_Click(object sender, EventArgs e)
        {
            SendKey(btn_v);
        }
        private void btn_b_Click(object sender, EventArgs e)
        {
            SendKey(btn_b);
        }
        private void btn_n_Click(object sender, EventArgs e)
        {
            SendKey(btn_n);
        }
        private void btn_m_Click(object sender, EventArgs e)
        {
            SendKey(btn_m);
        }
        private void btn_Bracket_Left_Click(object sender, EventArgs e)
        {
            string key = btn_Bracket_Left.Text;
            if (key == "{")
                key = "{{}";
            SendKeys.Send(key);
        }
        private void btn_Bracket_Right_Click(object sender, EventArgs e)
        {
            string key = btn_Bracket_Right.Text;
            if (key == "}")
                key = "{}}";
            SendKeys.Send(key);
        }
        private void btn_Dash_Click(object sender, EventArgs e)
        {
            SendKeys.Send(btn_dash.Text);
        }
        private void btn_Backspace_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{BACKSPACE}");
        }
        private void btn_Sapce_Click(object sender, EventArgs e)
        {
            SendKeys.Send(" ");
        }
        private void btn_Enter_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{ENTER}");
        }
        //Metoda ustawia wielkie male litery
        private void cBoxCap_CheckedChanged(object sender, EventArgs e)
        {
            // set shift key pressed
            byte[] b = new byte[256];
            b[0x10] = 0x80;

            foreach (Control ctr in Controls)
            {
                if (cBoxCap.Checked)
                    ctr.Text = ctr.Text.ToUpper();
                else
                    ctr.Text = ctr.Text.ToLower();
                //W zaleznosci od stanu shifta to ustaw znak jaki sie kreyj pod danym przyciskiem
                string key = ctr.Text;
                if (key == "&&")
                    key = "&";
                ctr.Text = GetKeyHoldShift(key, cBoxCap.Checked).ToString();
            }
        }
        private void btn_Left_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{LEFT}");
        }
        private void btn_Right_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{RIGHT}");
        }
        private void btn_equal_Click(object sender, EventArgs e)
        {
            string key = btn_equal.Text;
            if (key == "+")
                key = "{+}";
             SendKeys.Send(key);
        }
        private void btn_semicolon_Click(object sender, EventArgs e)
        {
            SendKeys.Send(btn_semicolon.Text);
        }
        private void btn_quation_mark_Click(object sender, EventArgs e)
        {
            SendKeys.Send(btn_quation_mark.Text);
        }
        private void btn_tilde_Click(object sender, EventArgs e)
        {
            SendKeys.Send(btn_tilde.Text);
        }
        private void btn_splash_Click(object sender, EventArgs e)
        {
            SendKeys.Send(btn_splash.Text);
        }
        private void btn_coma_Click(object sender, EventArgs e)
        {
            SendKeys.Send(btn_coma.Text);
        }
        private void btn_dot_Click(object sender, EventArgs e)
        {
            SendKeys.Send(btn_dot.Text);
        }
    }
}
