namespace r_framework.BrowseForFolder
{
    partial class FolderSelectPopup
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
            this.txtOutputDirectory = new System.Windows.Forms.TextBox();
            this.bt_DirSearch = new System.Windows.Forms.Button();
            this.bt_Func9 = new System.Windows.Forms.Button();
            this.bt_Func12 = new System.Windows.Forms.Button();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.lbl_OutputDir = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtOutputDirectory
            // 
            this.txtOutputDirectory.Location = new System.Drawing.Point(12, 77);
            this.txtOutputDirectory.Name = "txtOutputDirectory";
            this.txtOutputDirectory.Size = new System.Drawing.Size(490, 19);
            this.txtOutputDirectory.TabIndex = 1;
            // 
            // bt_DirSearch
            // 
            this.bt_DirSearch.Location = new System.Drawing.Point(508, 73);
            this.bt_DirSearch.Name = "bt_DirSearch";
            this.bt_DirSearch.Size = new System.Drawing.Size(46, 27);
            this.bt_DirSearch.TabIndex = 2;
            this.bt_DirSearch.TabStop = false;
            this.bt_DirSearch.Text = "参照";
            this.bt_DirSearch.UseVisualStyleBackColor = true;
            // 
            // bt_Func9
            // 
            this.bt_Func9.Location = new System.Drawing.Point(12, 128);
            this.bt_Func9.Name = "bt_Func9";
            this.bt_Func9.Size = new System.Drawing.Size(80, 35);
            this.bt_Func9.TabIndex = 3;
            this.bt_Func9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_Func9.UseVisualStyleBackColor = true;
            // 
            // bt_Func12
            // 
            this.bt_Func12.Location = new System.Drawing.Point(474, 128);
            this.bt_Func12.Name = "bt_Func12";
            this.bt_Func12.Size = new System.Drawing.Size(80, 35);
            this.bt_Func12.TabIndex = 4;
            this.bt_Func12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_Func12.UseVisualStyleBackColor = true;
            // 
            // lbl_Title
            // 
            this.lbl_Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Title.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Title.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Title.ForeColor = System.Drawing.Color.White;
            this.lbl_Title.Location = new System.Drawing.Point(12, 9);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(542, 31);
            this.lbl_Title.TabIndex = 0;
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OutputDir
            // 
            this.lbl_OutputDir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_OutputDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_OutputDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_OutputDir.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_OutputDir.ForeColor = System.Drawing.Color.White;
            this.lbl_OutputDir.Location = new System.Drawing.Point(12, 52);
            this.lbl_OutputDir.Name = "lbl_OutputDir";
            this.lbl_OutputDir.Size = new System.Drawing.Size(108, 22);
            this.lbl_OutputDir.TabIndex = 0;
            this.lbl_OutputDir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FolderSelectPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 175);
            this.Controls.Add(this.lbl_OutputDir);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.bt_Func12);
            this.Controls.Add(this.bt_Func9);
            this.Controls.Add(this.bt_DirSearch);
            this.Controls.Add(this.txtOutputDirectory);
            this.Name = "FolderSelectPopup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutputDirectory;
        private System.Windows.Forms.Button bt_DirSearch;
        private System.Windows.Forms.Button bt_Func9;
        private System.Windows.Forms.Button bt_Func12;
        internal System.Windows.Forms.Label lbl_Title;
        internal System.Windows.Forms.Label lbl_OutputDir;
    }
}