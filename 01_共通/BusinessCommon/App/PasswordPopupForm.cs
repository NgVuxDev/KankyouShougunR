using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shougun.Core.Common.BusinessCommon.App
{
    public partial class PasswordPopupForm : Form
    {
        public bool rightpassword = false;
        private string mpassword;

        public PasswordPopupForm(string password)
        {
            InitializeComponent();
            mpassword = password;
        }

        public PasswordPopupForm(string password,string display_KBN)
        {
            InitializeComponent();

            if (password != null)
            {
                mpassword = password;
            }
            else
            {
                mpassword = string.Empty;
            }

            if (display_KBN == "1") //売上確定入力
            {
                this.Text = "売上情報確定解除パスワードを入力";
            }
            else if (display_KBN == "2")    //支払確定入力
            {
                this.Text = "支払情報確定解除パスワードを入力";
            }
            else if (display_KBN == "3")    //伝票確定入力
            {
                this.Text = "伝票情報確定解除パスワードを入力";
            }

        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (mpassword.Equals(this.customTextBox1.Text))
            {
                rightpassword = true;
                this.Close();
            }
            else
            {
                ErrorLabel1.Text = "パスワードが間違っています。もう一度入力してください。";
            }
        }

        private void CancleButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
