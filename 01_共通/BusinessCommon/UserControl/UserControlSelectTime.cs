using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shougun.Core.Common.BusinessCommon.UserControl
{
    public partial class UserControlSelectTime : System.Windows.Forms.UserControl
    {
        private short _tabIndex;
        public string[] arrayJikan = new string[] {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24"};
        public string[] arrayBun = new string[] {"00", "05", "10", "15", "20", "25", "30", "35", "40", "45", "50", "55"};

        public UserControlSelectTime()
        {
            InitializeComponent();
        }

        private void UserControlSelectTime_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < arrayJikan.Length; i++)
            {
                this.cmbJikan.Items.Add(arrayJikan[i]);
            }
            for (int j = 0; j < arrayBun.Length; j++)
            {
                this.cmbBun.Items.Add(arrayBun[j]);
            }
        }

        /*public override short TabIndex
        {
            get
            {
                return _tabIndex;
            }
            set
            {
                _tabIndex = value;
                this.cmbJikan.TabIndex = (short)(value++);
                this.cmbBun.TabIndex = (short)(value++);
            }
        }*/     

        /*public string Jikan
        {
            get { return this.cmbJikan.Text; }
            set { this.cmbJikan.Text = value; }
        }

        public string Bun
        {
            get { return this.cmbBun.Text; }
            set { this.cmbBun.Text = value; }
        }*/

        public string getKanji() 
        {
            return this.cmbJikan.Text; 
        }

        public void setKanji(string value)
        {
            this.cmbJikan.Text = value;
        }

        public string getBun()
        {
            return this.cmbBun.Text;
        }

        public void setBun(string value)
        {
            this.cmbBun.Text = value;
        }

        public string getStringJikan()
        {
            return this.cmbJikan.Text.ToString() + ":" + this.cmbBun.Text.ToString();
        }

        private void cmbJikan_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            /*if (e.KeyChar == 13 || e.KeyChar == 8)
            {
                e.Handled = false;
                this.cmbBun.Focus();
                return;
            }

            if (e.KeyChar < 48 || e.KeyChar > 57)
            {
                e.Handled = false;
            }*/
        }

        private void cmbBun_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            /*if (e.KeyChar < 48 || e.KeyChar > 57)
            {
                e.Handled = false;
            }*/
        }

        private void cmbJikan_Leave(object sender, EventArgs e)
        {
            if (this.cmbJikan.Text.ToString() != "") {
                if (checkFullByte(this.cmbJikan.Text.ToString()))
                {
                    this.cmbJikan.Text = "";
                    this.cmbJikan.Focus();
                    return;
                }
                if (int.Parse(this.cmbJikan.Text.ToString()) > 24)
                {
                    this.cmbJikan.Text = "24";
                    this.cmbBun.Text = "00";
                }
            }
        }

        private void cmbBun_Leave(object sender, EventArgs e)
        {
            if (this.cmbJikan.Text.ToString() != "")
            {
                if (int.Parse(this.cmbJikan.Text.ToString()) == 24)
                {
                    this.cmbJikan.Text = "24";
                    this.cmbBun.Text = "00";
                }
                else
                {
                    if (this.cmbBun.Text.ToString() != "")
                    {
                        if (checkFullByte(this.cmbBun.Text.ToString()))
                        {
                            this.cmbBun.Text = "";
                            this.cmbBun.Focus();
                            return;
                        }
                        if (int.Parse(this.cmbBun.Text.ToString()) > 59)
                        {
                            this.cmbBun.Text = "59";
                        }
                    }
                }
            }
            else 
            {
                this.cmbBun.Text = "";
            }
        }

        private bool checkFullByte(string valueInput) 
        {
            try
            {
                byte a = new byte();
                for (int i = 0; i < valueInput.Length; i++)
                {
                    a = Convert.ToByte(valueInput[i]);
                }
            }
            catch(Exception ex){
                ex.ToString();
                return true;
            }
            return false;
        }
    }
}
