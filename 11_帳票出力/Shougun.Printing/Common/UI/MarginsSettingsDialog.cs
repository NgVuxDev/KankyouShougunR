using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shougun.Printing.Common.UI
{
    public partial class MarginsSettingsDialog : Form
    {
        public Shougun.Printing.Common.Margins Margins { get; set; }

        private TextBox[] valTextBoxs = null;
        
        public MarginsSettingsDialog()
        {
            InitializeComponent();
            this.BackColor = UI.FormStyle.FormBackColor;
            this.label1.ForeColor = UI.FormStyle.LabelForeColor;
            this.label1.BackColor = UI.FormStyle.LabelBackColor;
            this.label2.ForeColor = UI.FormStyle.LabelForeColor;
            this.label2.BackColor = UI.FormStyle.LabelBackColor;
            this.label3.ForeColor = UI.FormStyle.LabelForeColor;
            this.label3.BackColor = UI.FormStyle.LabelBackColor;
            this.label4.ForeColor = UI.FormStyle.LabelForeColor;
            this.label4.BackColor = UI.FormStyle.LabelBackColor;
            this.DialogResult = DialogResult.Cancel;
            this.valTextBoxs = new TextBox[] { this.topTextBox, this.bottomTextBox, this.leftTextBox, this.rightTextBox }; 
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.topTextBox.Text = this.Margins.Top.ToString();
            this.bottomTextBox.Text = this.Margins.Bottom.ToString();
            this.leftTextBox.Text = this.Margins.Left.ToString();
            this.rightTextBox.Text = this.Margins.Right.ToString();
        }

        private void changeClose()
        {
            var values = new double[] { 0d, 0d, 0d, 0d };
            for (int i = 0; i < 4; i++)
            {
                var textBox = this.valTextBoxs[i];
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    if (!double.TryParse(textBox.Text, out values[i]))
                    {
                        ErrorMessageBox.Show("不正な値です");
                        textBox.Focus();
                        return;
                    }
                }
            }

            var margins = new Margins();
            margins.Top = values[0];
            margins.Bottom = values[1];
            margins.Left = values[2];
            margins.Right = values[3];

            this.Margins = margins;
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            this.changeClose();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if ((keyData & (Keys.Alt | Keys.Control)) == Keys.None)
            {
                if ((keyData & Keys.KeyCode) == Keys.Return)
                {
                    this.ProcessTabKey((keyData & Keys.Shift) != Keys.Shift);
                    return true;
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            switch (e.KeyData)
            {
                case Keys.F9:
                    this.changeClose();
                    break;
                case Keys.F12:
                    this.Close();
                    break;
                default:
                    return;
            }
            e.Handled = true;
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.BackColor = FormStyle.FocusedBackColor;
                textBox.SelectAll();
            }
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.BackColor = System.Drawing.Color.White;
            }
        }

        private void textBox_Validating(object sender, CancelEventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                var length = textBox.Text.Length;
                var caretPos = Math.Max(0, textBox.SelectionStart);
                textBox.Text = this.numelicFilter(textBox.Text);
                if (textBox.Text.Length != length)
                {
                    caretPos = Math.Min(Math.Max(caretPos - 1, 0), textBox.Text.Length);
                }
                textBox.SelectionStart = caretPos;
            }
        }

        private string numelicFilter(string text)
        {
            var buf = new StringBuilder();
            int s = 0;
            foreach (char c in text)
            {
                bool ok = true;
                switch (s)
                {
                    case 0:
                        if (c == '+' || c == '-') { s = 1; }
                        else if (c == '0') { s = 2; }
                        else if (c >= '1' && c <= '9') { s = 3; }
                        else { ok = false; }
                        break;
                    case 1:
                        if (c == '0') { s = 2; }
                        else if (c >= '1' && c <= '9') { s = 3; }
                        else { ok = false; }
                        break;
                    case 2:
                        if (c == '.') { s = 10; }
                        else { ok = false; }
                        break;
                    case 3:
                        if (c >= '0' && c <= '9') { s = 4; }
                        else if (c == '.') { s = 10; }
                        else { ok = false; }
                        break;
                    case 4:
                        if (c == '.') { s = 10; }
                        else { ok = false; }
                        break;
                    case 10:
                        if (c >= '0' && c <= '9') { s = 99; }
                        else { ok = false; }
                        break;
                    default:
                        ok = false;
                        break;
                }
                if (ok)
                {
                    buf.Append(c);
                }
            }

            return buf.ToString();
        }
    }
}
