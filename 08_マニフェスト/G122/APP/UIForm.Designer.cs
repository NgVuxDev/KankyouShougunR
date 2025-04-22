namespace Shougun.Core.PaperManifest.ManifestNyuryokuIkkatsu
{
    partial class UIForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.NyuryokuIkkatsuItiran = new Shougun.Core.PaperManifest.ManifestNyuryokuIkkatsu.DgvCustom();
            ((System.ComponentModel.ISupportInitialize)(this.NyuryokuIkkatsuItiran)).BeginInit();
            this.SuspendLayout();
            // 
            // NyuryokuIkkatsuItiran
            // 
            this.NyuryokuIkkatsuItiran.AllowUserToResizeColumns = false;
            this.NyuryokuIkkatsuItiran.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.NyuryokuIkkatsuItiran.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.NyuryokuIkkatsuItiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.NyuryokuIkkatsuItiran.DefaultCellStyle = dataGridViewCellStyle2;
            this.NyuryokuIkkatsuItiran.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NyuryokuIkkatsuItiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.NyuryokuIkkatsuItiran.EnableHeadersVisualStyles = false;
            this.NyuryokuIkkatsuItiran.GridColor = System.Drawing.Color.White;
            this.NyuryokuIkkatsuItiran.IsReload = false;
            this.NyuryokuIkkatsuItiran.LinkedDataPanelName = null;
            this.NyuryokuIkkatsuItiran.Location = new System.Drawing.Point(0, 0);
            this.NyuryokuIkkatsuItiran.MultiSelect = false;
            this.NyuryokuIkkatsuItiran.Name = "NyuryokuIkkatsuItiran";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NyuryokuIkkatsuItiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.NyuryokuIkkatsuItiran.RowHeadersVisible = false;
            this.NyuryokuIkkatsuItiran.RowTemplate.Height = 21;
            this.NyuryokuIkkatsuItiran.ShowCellToolTips = false;
            this.NyuryokuIkkatsuItiran.Size = new System.Drawing.Size(1000, 490);
            this.NyuryokuIkkatsuItiran.TabIndex = 439;
            this.NyuryokuIkkatsuItiran.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.NyuryokuIkkatsuItiran_CellBeginEdit);
            this.NyuryokuIkkatsuItiran.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.NyuryokuIkkatsuItiran_CellEnter);
            this.NyuryokuIkkatsuItiran.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.NyuryokuIkkatsuItiran_RowsAdded);
            this.NyuryokuIkkatsuItiran.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NyuryokuIkkatsuItiran_KeyDown);
            this.NyuryokuIkkatsuItiran.KeyUp += new System.Windows.Forms.KeyEventHandler(this.NyuryokuIkkatsuItiran_KeyUp);
            this.NyuryokuIkkatsuItiran.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NyuryokuIkkatsuItiran_MouseUp);
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.NyuryokuIkkatsuItiran);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.Text = "1.請求データ作成時";
            ((System.ComponentModel.ISupportInitialize)(this.NyuryokuIkkatsuItiran)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Shougun.Core.PaperManifest.ManifestNyuryokuIkkatsu.DgvCustom NyuryokuIkkatsuItiran;
    }
}