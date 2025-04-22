namespace KyoutsuuIchiran.APP
{
    partial class IchiranSyutsuryokuKoumokuSentakuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TitleLabel = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.button18 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.BackColor = System.Drawing.Color.DodgerBlue;
            this.TitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TitleLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TitleLabel.ForeColor = System.Drawing.Color.White;
            this.TitleLabel.Location = new System.Drawing.Point(12, 9);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(618, 35);
            this.TitleLabel.TabIndex = 7;
            this.TitleLabel.Text = "一覧出力項目選択";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Items.AddRange(new object[] {
            "取引先TEL",
            "取引先FAX",
            "取引先敬称",
            "営業担当部署CD",
            "営業担当者CD",
            "郵便番号",
            "都道府県CD",
            "住所1",
            "住所2",
            "取引状況",
            "中止理由1",
            "中止理由2",
            "部署",
            "担当者",
            "集計項目CD",
            "業種CD",
            "備考1",
            "備考2",
            "備考3",
            "備考4",
            "請求取引区分CD",
            "請求締日1",
            "請求締日2",
            "請求締日3",
            "請求書必着日",
            "回収月",
            "回収日",
            "回収方法",
            "請求情報1",
            "請求情報2",
            "開始売掛残高",
            "請求書書式",
            "請求書書式明細",
            "請求最終取引日時",
            "請求書式",
            "入金明細",
            "請求用紙",
            "請求税区分CD",
            "請求税区分明細CD",
            "請求消費税端数CD",
            "請求金額端数CD",
            "振込銀行CD",
            "支店CD",
            "口座種類CD",
            "口座番号",
            "口座名",
            "請求書送付先1",
            "請求書送付先2",
            "請求書送付先敬称1",
            "請求書送付先敬称2",
            "請求送付先郵便番号",
            "請求送付先住所1",
            "請求送付先住所2",
            "請求送付先部署",
            "請求送付先担当者",
            "請求送付先TEL",
            "請求送付先FAX",
            "入金先CD",
            "支払取引区分CD",
            "支払締日1",
            "支払締日2",
            "支払締日3",
            "支払月",
            "支払日",
            "支払方法",
            "支払情報1",
            "支払情報2",
            "開始買掛残高",
            "支払書書式",
            "支払書書式明細",
            "支払最終取引日時",
            "支払書式",
            "出金明細",
            "支払用紙",
            "支払税区分CD",
            "支払税区分明細CD",
            "支払消費税端数CD",
            "支払金額端数CD",
            "支払書送付先1",
            "支払書送付先2",
            "支払書送付先敬称1",
            "支払書送付先敬称2",
            "支払書送付先郵便番号",
            "支払書送付先住所1",
            "支払書送付先住所2",
            "支払書送付先部署",
            "支払書送付先担当者",
            "支払書送付先TEL",
            "支払書送付先FAX",
            "作成者",
            "作成日時",
            "作成PC",
            "最終更新者",
            "最終更新日時",
            "最終更新PC",
            "削除フラグ"});
            this.listBox1.Location = new System.Drawing.Point(12, 83);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(250, 340);
            this.listBox1.TabIndex = 8;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Items.AddRange(new object[] {
            "取引先CD",
            "拠点CD",
            "取引先名1",
            "取引先名2",
            "取引先略称名",
            "取引先ふりがな"});
            this.listBox2.Location = new System.Drawing.Point(380, 83);
            this.listBox2.Name = "listBox2";
            this.listBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox2.Size = new System.Drawing.Size(250, 340);
            this.listBox2.TabIndex = 9;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(334, 247);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(40, 22);
            this.button5.TabIndex = 168;
            this.button5.Text = "↑";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(334, 275);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 22);
            this.button1.TabIndex = 169;
            this.button1.Text = "↓";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(334, 162);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(40, 22);
            this.button2.TabIndex = 170;
            this.button2.Text = ">";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(268, 162);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(40, 22);
            this.button3.TabIndex = 171;
            this.button3.Text = "<";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.DodgerBlue;
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(12, 60);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(250, 20);
            this.label17.TabIndex = 189;
            this.label17.Text = "選択項目";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(380, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 20);
            this.label1.TabIndex = 190;
            this.label1.Text = "出力項目";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox5.Location = new System.Drawing.Point(380, 441);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(250, 20);
            this.textBox5.TabIndex = 192;
            this.textBox5.TabStop = false;
            this.textBox5.Text = "hogehoge";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.DodgerBlue;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(281, 441);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(93, 20);
            this.label14.TabIndex = 193;
            this.label14.Text = "パターン名";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button18
            // 
            this.button18.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button18.Location = new System.Drawing.Point(550, 489);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(80, 22);
            this.button18.TabIndex = 195;
            this.button18.Text = "閉じる";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // button15
            // 
            this.button15.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button15.Location = new System.Drawing.Point(434, 489);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(80, 22);
            this.button15.TabIndex = 194;
            this.button15.Text = "登　録";
            this.button15.UseVisualStyleBackColor = true;
            // 
            // 一覧出力項目選択
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(642, 523);
            this.Controls.Add(this.button18);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.TitleLabel);
            this.Name = "一覧出力項目選択";
            this.Text = "一覧出力項目選択";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button15;
    }
}