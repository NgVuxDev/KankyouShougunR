namespace NyuukinsakiNyuuryokuHoshu.APP
{
    partial class NyuukinsakiNyuuryokuHoshuForm
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
            this.components = new System.ComponentModel.Container();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.ShortcutKeyManager shortcutKeyManager1 = new GrapeCity.Win.MultiRow.ShortcutKeyManager();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.ShortcutKeyManager shortcutKeyManager2 = new GrapeCity.Win.MultiRow.ShortcutKeyManager();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NyuukinsakiNyuuryokuHoshuForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_furikomijinmei = new System.Windows.Forms.TabPage();
            this.Ichiran_furikomi = new r_framework.CustomControl.GcCustomMultiRow(this.components);
            this.nyuukinsakiFurikomiDetail1 = new NyuukinsakiNyuuryokuHoshu.MultiRowTemplate.NyuukinsakiFurikomiDetail();
            this.tab_torihikisaki = new System.Windows.Forms.TabPage();
            this.Ichiran_torihikisaki = new r_framework.CustomControl.GcCustomMultiRow(this.components);
            this.nyuukinsakiTorihikisakiDetail1 = new NyuukinsakiNyuuryokuHoshu.MultiRowTemplate.NyuukinsakiTorihikisakiDetail();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.NYUUKINSAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.bt_saiban = new r_framework.CustomControl.CustomButton();
            this.NYUUKINSAKI_FURIGANA = new r_framework.CustomControl.CustomTextBox();
            this.NYUUKINSAKI_NAME1 = new r_framework.CustomControl.CustomTextBox();
            this.NYUUKINSAKI_NAME2 = new r_framework.CustomControl.CustomTextBox();
            this.NYUUKINSAKI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.NYUUKINSAKI_TEL = new r_framework.CustomControl.CustomPhoneNumberTextBox();
            this.NYUUKINSAKI_FAX = new r_framework.CustomControl.CustomPhoneNumberTextBox();
            this.NYUUKINSAKI_POST = new r_framework.CustomControl.CustomPostalCodeTextBox();
            this.TODOUFUKEN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.NYUUKINSAKI_ADDRESS1 = new r_framework.CustomControl.CustomTextBox();
            this.NYUUKINSAKI_ADDRESS2 = new r_framework.CustomControl.CustomTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbt_shinai = new r_framework.CustomControl.CustomRadioButton();
            this.rbt_suru = new r_framework.CustomControl.CustomRadioButton();
            this.TORIKOMI_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.bt_address = new r_framework.CustomControl.CustomAddressSearchButton();
            this.bt_post = new r_framework.CustomControl.CustomPostSearchButton();
            this.NYUUKINSAKI_TODOUFUKEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.tabControl1.SuspendLayout();
            this.tab_furikomijinmei.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran_furikomi)).BeginInit();
            this.tab_torihikisaki.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran_torihikisaki)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_furikomijinmei);
            this.tabControl1.Controls.Add(this.tab_torihikisaki);
            this.tabControl1.Location = new System.Drawing.Point(12, 195);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(716, 257);
            this.tabControl1.TabIndex = 100;
            // 
            // tab_furikomijinmei
            // 
            this.tab_furikomijinmei.Controls.Add(this.Ichiran_furikomi);
            this.tab_furikomijinmei.Location = new System.Drawing.Point(4, 22);
            this.tab_furikomijinmei.Name = "tab_furikomijinmei";
            this.tab_furikomijinmei.Padding = new System.Windows.Forms.Padding(3);
            this.tab_furikomijinmei.Size = new System.Drawing.Size(708, 231);
            this.tab_furikomijinmei.TabIndex = 0;
            this.tab_furikomijinmei.Text = "フリコミ人名";
            this.tab_furikomijinmei.UseVisualStyleBackColor = true;
            // 
            // Ichiran_furikomi
            // 
            this.Ichiran_furikomi.AllowUserToDeleteRows = false;
            this.Ichiran_furikomi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.Ichiran_furikomi.ColumnHeadersDefaultHeaderCellStyle = cellStyle1;
            this.Ichiran_furikomi.CurrentRowBorderLine = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Medium, System.Drawing.Color.Red);
            cellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle2.SelectionBackColor = System.Drawing.Color.Transparent;
            cellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.Ichiran_furikomi.DefaultCellStyle = cellStyle2;
            this.Ichiran_furikomi.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            this.Ichiran_furikomi.Location = new System.Drawing.Point(1, 1);
            this.Ichiran_furikomi.MultiSelect = false;
            this.Ichiran_furikomi.Name = "Ichiran_furikomi";
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftLeft)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftRight)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ReverseSelectCurrentRow)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToPreviousPage)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToNextPage)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            this.Ichiran_furikomi.ShortcutKeyManager = shortcutKeyManager1;
            this.Ichiran_furikomi.ShowCellToolTips = false;
            this.Ichiran_furikomi.Size = new System.Drawing.Size(704, 227);
            this.Ichiran_furikomi.TabIndex = 101;
            this.Ichiran_furikomi.Template = this.nyuukinsakiFurikomiDetail1;
            this.Ichiran_furikomi.RowsAdded += new System.EventHandler<GrapeCity.Win.MultiRow.RowsAddedEventArgs>(this.Ichiran_furikomi_RowsAdded);
            this.Ichiran_furikomi.CellValidating += new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.Ichiran_furikomi_CellValidating);
            this.Ichiran_furikomi.RowEnter += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.Ichiran_furikomi_RowEnter);
            this.Ichiran_furikomi.RowLeave += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.Ichiran_furikomi_RowLeave);
            this.Ichiran_furikomi.CellContentClick += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.Ichiran_furikomi_CellContentClick);
            // 
            // tab_torihikisaki
            // 
            this.tab_torihikisaki.Controls.Add(this.Ichiran_torihikisaki);
            this.tab_torihikisaki.Location = new System.Drawing.Point(4, 22);
            this.tab_torihikisaki.Name = "tab_torihikisaki";
            this.tab_torihikisaki.Padding = new System.Windows.Forms.Padding(3);
            this.tab_torihikisaki.Size = new System.Drawing.Size(708, 231);
            this.tab_torihikisaki.TabIndex = 1;
            this.tab_torihikisaki.Text = "取引先一覧";
            this.tab_torihikisaki.UseVisualStyleBackColor = true;
            // 
            // Ichiran_torihikisaki
            // 
            this.Ichiran_torihikisaki.AllowUserToAddRows = false;
            this.Ichiran_torihikisaki.AllowUserToDeleteRows = false;
            this.Ichiran_torihikisaki.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.Ichiran_torihikisaki.ColumnHeadersDefaultHeaderCellStyle = cellStyle3;
            this.Ichiran_torihikisaki.CurrentRowBorderLine = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Medium, System.Drawing.Color.Red);
            cellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle4.SelectionBackColor = System.Drawing.Color.Transparent;
            cellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.Ichiran_torihikisaki.DefaultCellStyle = cellStyle4;
            this.Ichiran_torihikisaki.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            this.Ichiran_torihikisaki.Location = new System.Drawing.Point(1, 1);
            this.Ichiran_torihikisaki.MultiSelect = false;
            this.Ichiran_torihikisaki.Name = "Ichiran_torihikisaki";
            this.Ichiran_torihikisaki.ReadOnly = true;
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftLeft)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftRight)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), System.Windows.Forms.Keys.End));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ReverseSelectCurrentRow)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToPreviousPage)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToNextPage)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            this.Ichiran_torihikisaki.ShortcutKeyManager = shortcutKeyManager2;
            this.Ichiran_torihikisaki.ShowCellToolTips = false;
            this.Ichiran_torihikisaki.Size = new System.Drawing.Size(704, 227);
            this.Ichiran_torihikisaki.TabIndex = 103;
            this.Ichiran_torihikisaki.Template = this.nyuukinsakiTorihikisakiDetail1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 600;
            this.label1.Text = "入金先CD";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 601;
            this.label2.Text = "フリガナ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 602;
            this.label3.Text = "入金先名1";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 603;
            this.label4.Text = "入金先名2";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(12, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 604;
            this.label5.Text = "略称名";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(12, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 20);
            this.label6.TabIndex = 605;
            this.label6.Text = "電話番号";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(12, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 20);
            this.label7.TabIndex = 606;
            this.label7.Text = "FAX番号";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(510, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 20);
            this.label8.TabIndex = 607;
            this.label8.Text = "郵便番号";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(510, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 20);
            this.label9.TabIndex = 608;
            this.label9.Text = "都道府県";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(510, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 20);
            this.label10.TabIndex = 609;
            this.label10.Text = "住所";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(510, 99);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 20);
            this.label11.TabIndex = 610;
            this.label11.Text = "自動取込";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NYUUKINSAKI_CD
            // 
            this.NYUUKINSAKI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.NYUUKINSAKI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NYUUKINSAKI_CD.ChangeUpperCase = true;
            this.NYUUKINSAKI_CD.CharacterLimitList = null;
            this.NYUUKINSAKI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.NYUUKINSAKI_CD.DBFieldsName = "NYUUKINSAKI_CD";
            this.NYUUKINSAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKINSAKI_CD.DisplayItemName = "入金先CD";
            this.NYUUKINSAKI_CD.DisplayPopUp = null;
            this.NYUUKINSAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_CD.FocusOutCheckMethod")));
            this.NYUUKINSAKI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.NYUUKINSAKI_CD.ForeColor = System.Drawing.Color.Black;
            this.NYUUKINSAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.NYUUKINSAKI_CD.IsInputErrorOccured = false;
            this.NYUUKINSAKI_CD.ItemDefinedTypes = "varchar";
            this.NYUUKINSAKI_CD.Location = new System.Drawing.Point(128, 4);
            this.NYUUKINSAKI_CD.MaxLength = 6;
            this.NYUUKINSAKI_CD.Name = "NYUUKINSAKI_CD";
            this.NYUUKINSAKI_CD.PopupAfterExecute = null;
            this.NYUUKINSAKI_CD.PopupBeforeExecute = null;
            this.NYUUKINSAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKINSAKI_CD.PopupSearchSendParams")));
            this.NYUUKINSAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKINSAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKINSAKI_CD.popupWindowSetting")));
            this.NYUUKINSAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_CD.RegistCheckMethod")));
            this.NYUUKINSAKI_CD.ShortItemName = "入金先CD";
            this.NYUUKINSAKI_CD.Size = new System.Drawing.Size(55, 20);
            this.NYUUKINSAKI_CD.TabIndex = 1;
            this.NYUUKINSAKI_CD.Tag = "半角6桁以内で入力してください";
            this.NYUUKINSAKI_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NYUUKINSAKI_CD.ZeroPaddengFlag = true;
            this.NYUUKINSAKI_CD.Leave += new System.EventHandler(this.NYUUKINSAKI_CD_Leave);
            // 
            // bt_saiban
            // 
            this.bt_saiban.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_saiban.Location = new System.Drawing.Point(189, 4);
            this.bt_saiban.Name = "bt_saiban";
            this.bt_saiban.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_saiban.Size = new System.Drawing.Size(58, 20);
            this.bt_saiban.TabIndex = 2;
            this.bt_saiban.TabStop = false;
            this.bt_saiban.Tag = "入金先CDを採番します";
            this.bt_saiban.Text = "採番";
            this.bt_saiban.UseVisualStyleBackColor = true;
            this.bt_saiban.Click += new System.EventHandler(this.bt_saiban_Click);
            // 
            // NYUUKINSAKI_FURIGANA
            // 
            this.NYUUKINSAKI_FURIGANA.BackColor = System.Drawing.SystemColors.Window;
            this.NYUUKINSAKI_FURIGANA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NYUUKINSAKI_FURIGANA.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.NYUUKINSAKI_FURIGANA.DBFieldsName = "NYUUKINSAKI_FURIGANA";
            this.NYUUKINSAKI_FURIGANA.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKINSAKI_FURIGANA.DisplayItemName = "フリガナ";
            this.NYUUKINSAKI_FURIGANA.DisplayPopUp = null;
            this.NYUUKINSAKI_FURIGANA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_FURIGANA.FocusOutCheckMethod")));
            this.NYUUKINSAKI_FURIGANA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NYUUKINSAKI_FURIGANA.ForeColor = System.Drawing.Color.Black;
            this.NYUUKINSAKI_FURIGANA.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.NYUUKINSAKI_FURIGANA.IsInputErrorOccured = false;
            this.NYUUKINSAKI_FURIGANA.ItemDefinedTypes = "varchar";
            this.NYUUKINSAKI_FURIGANA.Location = new System.Drawing.Point(128, 30);
            this.NYUUKINSAKI_FURIGANA.MaxLength = 80;
            this.NYUUKINSAKI_FURIGANA.Multiline = true;
            this.NYUUKINSAKI_FURIGANA.Name = "NYUUKINSAKI_FURIGANA";
            this.NYUUKINSAKI_FURIGANA.PopupAfterExecute = null;
            this.NYUUKINSAKI_FURIGANA.PopupBeforeExecute = null;
            this.NYUUKINSAKI_FURIGANA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKINSAKI_FURIGANA.PopupSearchSendParams")));
            this.NYUUKINSAKI_FURIGANA.PopupSetFormField = "";
            this.NYUUKINSAKI_FURIGANA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKINSAKI_FURIGANA.PopupWindowName = "";
            this.NYUUKINSAKI_FURIGANA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKINSAKI_FURIGANA.popupWindowSetting")));
            this.NYUUKINSAKI_FURIGANA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_FURIGANA.RegistCheckMethod")));
            this.NYUUKINSAKI_FURIGANA.SetFormField = "";
            this.NYUUKINSAKI_FURIGANA.ShortItemName = "フリガナ";
            this.NYUUKINSAKI_FURIGANA.Size = new System.Drawing.Size(290, 32);
            this.NYUUKINSAKI_FURIGANA.TabIndex = 3;
            this.NYUUKINSAKI_FURIGANA.Tag = "全角４０文字以内で入力してください";
            // 
            // NYUUKINSAKI_NAME1
            // 
            this.NYUUKINSAKI_NAME1.BackColor = System.Drawing.SystemColors.Window;
            this.NYUUKINSAKI_NAME1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NYUUKINSAKI_NAME1.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.NYUUKINSAKI_NAME1.CopyAutoSetControl = "NYUUKINSAKI_NAME_RYAKU";
            this.NYUUKINSAKI_NAME1.CopyAutoSetWithSpace = true;
            this.NYUUKINSAKI_NAME1.DBFieldsName = "NYUUKINSAKI_NAME1";
            this.NYUUKINSAKI_NAME1.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKINSAKI_NAME1.DisplayItemName = "入金先名1";
            this.NYUUKINSAKI_NAME1.DisplayPopUp = null;
            this.NYUUKINSAKI_NAME1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_NAME1.FocusOutCheckMethod")));
            this.NYUUKINSAKI_NAME1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NYUUKINSAKI_NAME1.ForeColor = System.Drawing.Color.Black;
            this.NYUUKINSAKI_NAME1.FuriganaAutoSetControl = "NYUUKINSAKI_FURIGANA";
            this.NYUUKINSAKI_NAME1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.NYUUKINSAKI_NAME1.IsInputErrorOccured = false;
            this.NYUUKINSAKI_NAME1.ItemDefinedTypes = "varchar";
            this.NYUUKINSAKI_NAME1.Location = new System.Drawing.Point(128, 68);
            this.NYUUKINSAKI_NAME1.MaxLength = 40;
            this.NYUUKINSAKI_NAME1.Name = "NYUUKINSAKI_NAME1";
            this.NYUUKINSAKI_NAME1.PopupAfterExecute = null;
            this.NYUUKINSAKI_NAME1.PopupBeforeExecute = null;
            this.NYUUKINSAKI_NAME1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKINSAKI_NAME1.PopupSearchSendParams")));
            this.NYUUKINSAKI_NAME1.PopupSetFormField = "";
            this.NYUUKINSAKI_NAME1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKINSAKI_NAME1.PopupWindowName = "";
            this.NYUUKINSAKI_NAME1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKINSAKI_NAME1.popupWindowSetting")));
            this.NYUUKINSAKI_NAME1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_NAME1.RegistCheckMethod")));
            this.NYUUKINSAKI_NAME1.SetFormField = "";
            this.NYUUKINSAKI_NAME1.ShortItemName = "入金先名1";
            this.NYUUKINSAKI_NAME1.Size = new System.Drawing.Size(290, 20);
            this.NYUUKINSAKI_NAME1.TabIndex = 4;
            this.NYUUKINSAKI_NAME1.Tag = "全角２０文字以内で入力してください";
            // 
            // NYUUKINSAKI_NAME2
            // 
            this.NYUUKINSAKI_NAME2.BackColor = System.Drawing.SystemColors.Window;
            this.NYUUKINSAKI_NAME2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NYUUKINSAKI_NAME2.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.NYUUKINSAKI_NAME2.DBFieldsName = "NYUUKINSAKI_NAME2";
            this.NYUUKINSAKI_NAME2.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKINSAKI_NAME2.DisplayItemName = "入金先名2";
            this.NYUUKINSAKI_NAME2.DisplayPopUp = null;
            this.NYUUKINSAKI_NAME2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_NAME2.FocusOutCheckMethod")));
            this.NYUUKINSAKI_NAME2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NYUUKINSAKI_NAME2.ForeColor = System.Drawing.Color.Black;
            this.NYUUKINSAKI_NAME2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.NYUUKINSAKI_NAME2.IsInputErrorOccured = false;
            this.NYUUKINSAKI_NAME2.ItemDefinedTypes = "varchar";
            this.NYUUKINSAKI_NAME2.Location = new System.Drawing.Point(128, 94);
            this.NYUUKINSAKI_NAME2.MaxLength = 40;
            this.NYUUKINSAKI_NAME2.Name = "NYUUKINSAKI_NAME2";
            this.NYUUKINSAKI_NAME2.PopupAfterExecute = null;
            this.NYUUKINSAKI_NAME2.PopupBeforeExecute = null;
            this.NYUUKINSAKI_NAME2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKINSAKI_NAME2.PopupSearchSendParams")));
            this.NYUUKINSAKI_NAME2.PopupSetFormField = "";
            this.NYUUKINSAKI_NAME2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKINSAKI_NAME2.PopupWindowName = "";
            this.NYUUKINSAKI_NAME2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKINSAKI_NAME2.popupWindowSetting")));
            this.NYUUKINSAKI_NAME2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_NAME2.RegistCheckMethod")));
            this.NYUUKINSAKI_NAME2.SetFormField = "";
            this.NYUUKINSAKI_NAME2.ShortItemName = "入金先名2";
            this.NYUUKINSAKI_NAME2.Size = new System.Drawing.Size(290, 20);
            this.NYUUKINSAKI_NAME2.TabIndex = 5;
            this.NYUUKINSAKI_NAME2.Tag = "全角２０文字以内で入力してください";
            // 
            // NYUUKINSAKI_NAME_RYAKU
            // 
            this.NYUUKINSAKI_NAME_RYAKU.BackColor = System.Drawing.SystemColors.Window;
            this.NYUUKINSAKI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NYUUKINSAKI_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.NYUUKINSAKI_NAME_RYAKU.DBFieldsName = "NYUUKINSAKI_NAME_RYAKU";
            this.NYUUKINSAKI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKINSAKI_NAME_RYAKU.DisplayItemName = "略称名";
            this.NYUUKINSAKI_NAME_RYAKU.DisplayPopUp = null;
            this.NYUUKINSAKI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_NAME_RYAKU.FocusOutCheckMethod")));
            this.NYUUKINSAKI_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NYUUKINSAKI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.NYUUKINSAKI_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.NYUUKINSAKI_NAME_RYAKU.IsInputErrorOccured = false;
            this.NYUUKINSAKI_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.NYUUKINSAKI_NAME_RYAKU.Location = new System.Drawing.Point(128, 120);
            this.NYUUKINSAKI_NAME_RYAKU.MaxLength = 40;
            this.NYUUKINSAKI_NAME_RYAKU.Name = "NYUUKINSAKI_NAME_RYAKU";
            this.NYUUKINSAKI_NAME_RYAKU.PopupAfterExecute = null;
            this.NYUUKINSAKI_NAME_RYAKU.PopupBeforeExecute = null;
            this.NYUUKINSAKI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKINSAKI_NAME_RYAKU.PopupSearchSendParams")));
            this.NYUUKINSAKI_NAME_RYAKU.PopupSetFormField = "";
            this.NYUUKINSAKI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKINSAKI_NAME_RYAKU.PopupWindowName = "";
            this.NYUUKINSAKI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKINSAKI_NAME_RYAKU.popupWindowSetting")));
            this.NYUUKINSAKI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_NAME_RYAKU.RegistCheckMethod")));
            this.NYUUKINSAKI_NAME_RYAKU.SetFormField = "";
            this.NYUUKINSAKI_NAME_RYAKU.ShortItemName = "略称名";
            this.NYUUKINSAKI_NAME_RYAKU.Size = new System.Drawing.Size(290, 20);
            this.NYUUKINSAKI_NAME_RYAKU.TabIndex = 6;
            this.NYUUKINSAKI_NAME_RYAKU.Tag = "全角２０文字以内で入力してください";
            // 
            // NYUUKINSAKI_TEL
            // 
            this.NYUUKINSAKI_TEL.BackColor = System.Drawing.SystemColors.Window;
            this.NYUUKINSAKI_TEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NYUUKINSAKI_TEL.DBFieldsName = "NYUUKINSAKI_TEL";
            this.NYUUKINSAKI_TEL.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKINSAKI_TEL.DisplayItemName = "電話番号";
            this.NYUUKINSAKI_TEL.DisplayPopUp = null;
            this.NYUUKINSAKI_TEL.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_TEL.FocusOutCheckMethod")));
            this.NYUUKINSAKI_TEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.NYUUKINSAKI_TEL.ForeColor = System.Drawing.Color.Black;
            this.NYUUKINSAKI_TEL.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.NYUUKINSAKI_TEL.IsInputErrorOccured = false;
            this.NYUUKINSAKI_TEL.ItemDefinedTypes = "";
            this.NYUUKINSAKI_TEL.Location = new System.Drawing.Point(128, 146);
            this.NYUUKINSAKI_TEL.Name = "NYUUKINSAKI_TEL";
            this.NYUUKINSAKI_TEL.PopupAfterExecute = null;
            this.NYUUKINSAKI_TEL.PopupBeforeExecute = null;
            this.NYUUKINSAKI_TEL.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKINSAKI_TEL.PopupSearchSendParams")));
            this.NYUUKINSAKI_TEL.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKINSAKI_TEL.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKINSAKI_TEL.popupWindowSetting")));
            this.NYUUKINSAKI_TEL.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_TEL.RegistCheckMethod")));
            this.NYUUKINSAKI_TEL.ShortItemName = "電話番号";
            this.NYUUKINSAKI_TEL.Size = new System.Drawing.Size(100, 20);
            this.NYUUKINSAKI_TEL.TabIndex = 7;
            this.NYUUKINSAKI_TEL.Tag = "半角数字（ハイフン可）13文字以内で入力してください";
            // 
            // NYUUKINSAKI_FAX
            // 
            this.NYUUKINSAKI_FAX.BackColor = System.Drawing.SystemColors.Window;
            this.NYUUKINSAKI_FAX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NYUUKINSAKI_FAX.DBFieldsName = "NYUUKINSAKI_FAX";
            this.NYUUKINSAKI_FAX.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKINSAKI_FAX.DisplayItemName = "FAX番号";
            this.NYUUKINSAKI_FAX.DisplayPopUp = null;
            this.NYUUKINSAKI_FAX.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_FAX.FocusOutCheckMethod")));
            this.NYUUKINSAKI_FAX.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.NYUUKINSAKI_FAX.ForeColor = System.Drawing.Color.Black;
            this.NYUUKINSAKI_FAX.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.NYUUKINSAKI_FAX.IsInputErrorOccured = false;
            this.NYUUKINSAKI_FAX.ItemDefinedTypes = "varchar";
            this.NYUUKINSAKI_FAX.Location = new System.Drawing.Point(128, 172);
            this.NYUUKINSAKI_FAX.Name = "NYUUKINSAKI_FAX";
            this.NYUUKINSAKI_FAX.PopupAfterExecute = null;
            this.NYUUKINSAKI_FAX.PopupBeforeExecute = null;
            this.NYUUKINSAKI_FAX.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKINSAKI_FAX.PopupSearchSendParams")));
            this.NYUUKINSAKI_FAX.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKINSAKI_FAX.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKINSAKI_FAX.popupWindowSetting")));
            this.NYUUKINSAKI_FAX.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_FAX.RegistCheckMethod")));
            this.NYUUKINSAKI_FAX.ShortItemName = "FAX番号";
            this.NYUUKINSAKI_FAX.Size = new System.Drawing.Size(100, 20);
            this.NYUUKINSAKI_FAX.TabIndex = 8;
            this.NYUUKINSAKI_FAX.Tag = "半角数字（ハイフン可）13文字以内で入力してください";
            // 
            // NYUUKINSAKI_POST
            // 
            this.NYUUKINSAKI_POST.BackColor = System.Drawing.SystemColors.Window;
            this.NYUUKINSAKI_POST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NYUUKINSAKI_POST.DBFieldsName = "NYUUKINSAKI_POST";
            this.NYUUKINSAKI_POST.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKINSAKI_POST.DisplayItemName = "郵便番号";
            this.NYUUKINSAKI_POST.DisplayPopUp = null;
            this.NYUUKINSAKI_POST.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_POST.FocusOutCheckMethod")));
            this.NYUUKINSAKI_POST.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.NYUUKINSAKI_POST.ForeColor = System.Drawing.Color.Black;
            this.NYUUKINSAKI_POST.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.NYUUKINSAKI_POST.IsInputErrorOccured = false;
            this.NYUUKINSAKI_POST.ItemDefinedTypes = "varchar";
            this.NYUUKINSAKI_POST.Location = new System.Drawing.Point(626, 4);
            this.NYUUKINSAKI_POST.Name = "NYUUKINSAKI_POST";
            this.NYUUKINSAKI_POST.PopupAfterExecute = null;
            this.NYUUKINSAKI_POST.PopupBeforeExecute = null;
            this.NYUUKINSAKI_POST.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKINSAKI_POST.PopupSearchSendParams")));
            this.NYUUKINSAKI_POST.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKINSAKI_POST.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKINSAKI_POST.popupWindowSetting")));
            this.NYUUKINSAKI_POST.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_POST.RegistCheckMethod")));
            this.NYUUKINSAKI_POST.ShortItemName = "郵便番号";
            this.NYUUKINSAKI_POST.Size = new System.Drawing.Size(65, 20);
            this.NYUUKINSAKI_POST.TabIndex = 9;
            this.NYUUKINSAKI_POST.Tag = "郵便番号を指定してください（郵便番号参照ボタンにより、住所からの検索も出来ます）";
            // 
            // TODOUFUKEN_NAME
            // 
            this.TODOUFUKEN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TODOUFUKEN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TODOUFUKEN_NAME.CharactersNumber = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.TODOUFUKEN_NAME.DBFieldsName = "TODOUFUKEN_NAME";
            this.TODOUFUKEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.TODOUFUKEN_NAME.DisplayItemName = "都道府県名";
            this.TODOUFUKEN_NAME.DisplayPopUp = null;
            this.TODOUFUKEN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TODOUFUKEN_NAME.FocusOutCheckMethod")));
            this.TODOUFUKEN_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TODOUFUKEN_NAME.ForeColor = System.Drawing.Color.Black;
            this.TODOUFUKEN_NAME.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TODOUFUKEN_NAME.IsInputErrorOccured = false;
            this.TODOUFUKEN_NAME.ItemDefinedTypes = "varchar";
            this.TODOUFUKEN_NAME.Location = new System.Drawing.Point(645, 30);
            this.TODOUFUKEN_NAME.MaxLength = 8;
            this.TODOUFUKEN_NAME.Name = "TODOUFUKEN_NAME";
            this.TODOUFUKEN_NAME.PopupAfterExecute = null;
            this.TODOUFUKEN_NAME.PopupBeforeExecute = null;
            this.TODOUFUKEN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TODOUFUKEN_NAME.PopupSearchSendParams")));
            this.TODOUFUKEN_NAME.PopupSetFormField = "";
            this.TODOUFUKEN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TODOUFUKEN_NAME.PopupWindowName = "";
            this.TODOUFUKEN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TODOUFUKEN_NAME.popupWindowSetting")));
            this.TODOUFUKEN_NAME.ReadOnly = true;
            this.TODOUFUKEN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TODOUFUKEN_NAME.RegistCheckMethod")));
            this.TODOUFUKEN_NAME.SetFormField = "";
            this.TODOUFUKEN_NAME.ShortItemName = "都道府県名";
            this.TODOUFUKEN_NAME.Size = new System.Drawing.Size(65, 20);
            this.TODOUFUKEN_NAME.TabIndex = 0;
            this.TODOUFUKEN_NAME.TabStop = false;
            this.TODOUFUKEN_NAME.Tag = "都道府県名が表示されます";
            // 
            // NYUUKINSAKI_ADDRESS1
            // 
            this.NYUUKINSAKI_ADDRESS1.BackColor = System.Drawing.SystemColors.Window;
            this.NYUUKINSAKI_ADDRESS1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NYUUKINSAKI_ADDRESS1.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.NYUUKINSAKI_ADDRESS1.CopyAutoSetControl = "";
            this.NYUUKINSAKI_ADDRESS1.DBFieldsName = "NYUUKINSAKI_ADDRESS1";
            this.NYUUKINSAKI_ADDRESS1.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKINSAKI_ADDRESS1.DisplayItemName = "住所1";
            this.NYUUKINSAKI_ADDRESS1.DisplayPopUp = null;
            this.NYUUKINSAKI_ADDRESS1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_ADDRESS1.FocusOutCheckMethod")));
            this.NYUUKINSAKI_ADDRESS1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NYUUKINSAKI_ADDRESS1.ForeColor = System.Drawing.Color.Black;
            this.NYUUKINSAKI_ADDRESS1.FuriganaAutoSetControl = "";
            this.NYUUKINSAKI_ADDRESS1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.NYUUKINSAKI_ADDRESS1.IsInputErrorOccured = false;
            this.NYUUKINSAKI_ADDRESS1.ItemDefinedTypes = "varchar";
            this.NYUUKINSAKI_ADDRESS1.Location = new System.Drawing.Point(626, 56);
            this.NYUUKINSAKI_ADDRESS1.MaxLength = 40;
            this.NYUUKINSAKI_ADDRESS1.Name = "NYUUKINSAKI_ADDRESS1";
            this.NYUUKINSAKI_ADDRESS1.PopupAfterExecute = null;
            this.NYUUKINSAKI_ADDRESS1.PopupBeforeExecute = null;
            this.NYUUKINSAKI_ADDRESS1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKINSAKI_ADDRESS1.PopupSearchSendParams")));
            this.NYUUKINSAKI_ADDRESS1.PopupSetFormField = "";
            this.NYUUKINSAKI_ADDRESS1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKINSAKI_ADDRESS1.PopupWindowName = "";
            this.NYUUKINSAKI_ADDRESS1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKINSAKI_ADDRESS1.popupWindowSetting")));
            this.NYUUKINSAKI_ADDRESS1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_ADDRESS1.RegistCheckMethod")));
            this.NYUUKINSAKI_ADDRESS1.SetFormField = "";
            this.NYUUKINSAKI_ADDRESS1.ShortItemName = "住所1";
            this.NYUUKINSAKI_ADDRESS1.Size = new System.Drawing.Size(290, 20);
            this.NYUUKINSAKI_ADDRESS1.TabIndex = 13;
            this.NYUUKINSAKI_ADDRESS1.Tag = "住所を指定してください（住所参照ボタンにより、郵便番号からの検索も出来ます）";
            // 
            // NYUUKINSAKI_ADDRESS2
            // 
            this.NYUUKINSAKI_ADDRESS2.BackColor = System.Drawing.SystemColors.Window;
            this.NYUUKINSAKI_ADDRESS2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NYUUKINSAKI_ADDRESS2.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.NYUUKINSAKI_ADDRESS2.CopyAutoSetControl = "";
            this.NYUUKINSAKI_ADDRESS2.DBFieldsName = "NYUUKINSAKI_ADDRESS2";
            this.NYUUKINSAKI_ADDRESS2.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKINSAKI_ADDRESS2.DisplayItemName = "住所2";
            this.NYUUKINSAKI_ADDRESS2.DisplayPopUp = null;
            this.NYUUKINSAKI_ADDRESS2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_ADDRESS2.FocusOutCheckMethod")));
            this.NYUUKINSAKI_ADDRESS2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NYUUKINSAKI_ADDRESS2.ForeColor = System.Drawing.Color.Black;
            this.NYUUKINSAKI_ADDRESS2.FuriganaAutoSetControl = "";
            this.NYUUKINSAKI_ADDRESS2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.NYUUKINSAKI_ADDRESS2.IsInputErrorOccured = false;
            this.NYUUKINSAKI_ADDRESS2.ItemDefinedTypes = "varchar";
            this.NYUUKINSAKI_ADDRESS2.Location = new System.Drawing.Point(626, 73);
            this.NYUUKINSAKI_ADDRESS2.MaxLength = 40;
            this.NYUUKINSAKI_ADDRESS2.Name = "NYUUKINSAKI_ADDRESS2";
            this.NYUUKINSAKI_ADDRESS2.PopupAfterExecute = null;
            this.NYUUKINSAKI_ADDRESS2.PopupBeforeExecute = null;
            this.NYUUKINSAKI_ADDRESS2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKINSAKI_ADDRESS2.PopupSearchSendParams")));
            this.NYUUKINSAKI_ADDRESS2.PopupSetFormField = "";
            this.NYUUKINSAKI_ADDRESS2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKINSAKI_ADDRESS2.PopupWindowName = "";
            this.NYUUKINSAKI_ADDRESS2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKINSAKI_ADDRESS2.popupWindowSetting")));
            this.NYUUKINSAKI_ADDRESS2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_ADDRESS2.RegistCheckMethod")));
            this.NYUUKINSAKI_ADDRESS2.SetFormField = "";
            this.NYUUKINSAKI_ADDRESS2.ShortItemName = "住所2";
            this.NYUUKINSAKI_ADDRESS2.Size = new System.Drawing.Size(290, 20);
            this.NYUUKINSAKI_ADDRESS2.TabIndex = 14;
            this.NYUUKINSAKI_ADDRESS2.Tag = "全角２０文字以内で入力してください";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbt_shinai);
            this.panel1.Controls.Add(this.rbt_suru);
            this.panel1.Controls.Add(this.TORIKOMI_KBN);
            this.panel1.Location = new System.Drawing.Point(623, 95);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(225, 28);
            this.panel1.TabIndex = 15;
            this.panel1.TabStop = true;
            // 
            // rbt_shinai
            // 
            this.rbt_shinai.AutoSize = true;
            this.rbt_shinai.DefaultBackColor = System.Drawing.Color.Empty;
            this.rbt_shinai.DisplayItemName = "しない";
            this.rbt_shinai.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rbt_shinai.FocusOutCheckMethod")));
            this.rbt_shinai.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rbt_shinai.LinkedTextBox = "TORIKOMI_KBN";
            this.rbt_shinai.Location = new System.Drawing.Point(109, 6);
            this.rbt_shinai.Name = "rbt_shinai";
            this.rbt_shinai.PopupAfterExecute = null;
            this.rbt_shinai.PopupBeforeExecute = null;
            this.rbt_shinai.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rbt_shinai.PopupSearchSendParams")));
            this.rbt_shinai.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rbt_shinai.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rbt_shinai.popupWindowSetting")));
            this.rbt_shinai.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rbt_shinai.RegistCheckMethod")));
            this.rbt_shinai.ShortItemName = "しない";
            this.rbt_shinai.Size = new System.Drawing.Size(88, 17);
            this.rbt_shinai.TabIndex = 17;
            this.rbt_shinai.Tag = "自動取込をしない場合にはチェックを付けてください";
            this.rbt_shinai.Text = "2．しない";
            this.rbt_shinai.UseVisualStyleBackColor = true;
            this.rbt_shinai.Value = "2";
            // 
            // rbt_suru
            // 
            this.rbt_suru.AutoSize = true;
            this.rbt_suru.DefaultBackColor = System.Drawing.Color.Empty;
            this.rbt_suru.DisplayItemName = "する";
            this.rbt_suru.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rbt_suru.FocusOutCheckMethod")));
            this.rbt_suru.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rbt_suru.LinkedTextBox = "TORIKOMI_KBN";
            this.rbt_suru.Location = new System.Drawing.Point(29, 5);
            this.rbt_suru.Name = "rbt_suru";
            this.rbt_suru.PopupAfterExecute = null;
            this.rbt_suru.PopupBeforeExecute = null;
            this.rbt_suru.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rbt_suru.PopupSearchSendParams")));
            this.rbt_suru.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rbt_suru.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rbt_suru.popupWindowSetting")));
            this.rbt_suru.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rbt_suru.RegistCheckMethod")));
            this.rbt_suru.ShortItemName = "する";
            this.rbt_suru.Size = new System.Drawing.Size(74, 17);
            this.rbt_suru.TabIndex = 16;
            this.rbt_suru.Tag = "自動取込をする場合にはチェックを付けてください";
            this.rbt_suru.Text = "1. する";
            this.rbt_suru.UseVisualStyleBackColor = true;
            this.rbt_suru.Value = "1";
            this.rbt_suru.CheckedChanged += new System.EventHandler(this.rbt_suru_CheckedChanged);
            // 
            // TORIKOMI_KBN
            // 
            this.TORIKOMI_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.TORIKOMI_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIKOMI_KBN.DBFieldsName = "TORIKOMI_KBN";
            this.TORIKOMI_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIKOMI_KBN.DisplayItemName = "自動取込区分";
            this.TORIKOMI_KBN.DisplayPopUp = null;
            this.TORIKOMI_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIKOMI_KBN.FocusOutCheckMethod")));
            this.TORIKOMI_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIKOMI_KBN.ForeColor = System.Drawing.Color.Black;
            this.TORIKOMI_KBN.IsInputErrorOccured = false;
            this.TORIKOMI_KBN.ItemDefinedTypes = "smallint";
            this.TORIKOMI_KBN.LinkedRadioButtonArray = new string[] {
        "rbt_suru",
        "rbt_shinai"};
            this.TORIKOMI_KBN.Location = new System.Drawing.Point(3, 3);
            this.TORIKOMI_KBN.Name = "TORIKOMI_KBN";
            this.TORIKOMI_KBN.PopupAfterExecute = null;
            this.TORIKOMI_KBN.PopupBeforeExecute = null;
            this.TORIKOMI_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIKOMI_KBN.PopupSearchSendParams")));
            this.TORIKOMI_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIKOMI_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIKOMI_KBN.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TORIKOMI_KBN.RangeSetting = rangeSettingDto1;
            this.TORIKOMI_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIKOMI_KBN.RegistCheckMethod")));
            this.TORIKOMI_KBN.ShortItemName = "自動取込区分";
            this.TORIKOMI_KBN.Size = new System.Drawing.Size(20, 20);
            this.TORIKOMI_KBN.TabIndex = 15;
            this.TORIKOMI_KBN.Tag = "【1、2】のいずれかで入力してください";
            this.TORIKOMI_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TORIKOMI_KBN.WordWrap = false;
            // 
            // bt_address
            // 
            this.bt_address.AddressControl = "NYUUKINSAKI_ADDRESS1";
            this.bt_address.AddTodoufukenToAddressFlg = false;
            this.bt_address.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_address.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("bt_address.FocusOutCheckMethod")));
            this.bt_address.Location = new System.Drawing.Point(697, 4);
            this.bt_address.Name = "bt_address";
            this.bt_address.PopupAfterExecute = null;
            this.bt_address.PopupBeforeExecute = null;
            this.bt_address.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("bt_address.PopupSearchSendParams")));
            this.bt_address.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.bt_address.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("bt_address.popupWindowSetting")));
            this.bt_address.PostalCodeControl = "NYUUKINSAKI_POST";
            this.bt_address.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("bt_address.RegistCheckMethod")));
            this.bt_address.Size = new System.Drawing.Size(86, 20);
            this.bt_address.TabIndex = 10;
            this.bt_address.TabStop = false;
            this.bt_address.Tag = "郵便番号から住所を取得します";
            this.bt_address.Text = "住所参照";
            this.bt_address.TodoufukenControl = "NYUUKINSAKI_TODOUFUKEN_CD";
            this.bt_address.UseVisualStyleBackColor = true;
            // 
            // bt_post
            // 
            this.bt_post.AddressControl = "NYUUKINSAKI_ADDRESS1";
            this.bt_post.AddTodoufukenToAddressFlg = false;
            this.bt_post.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_post.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("bt_post.FocusOutCheckMethod")));
            this.bt_post.Location = new System.Drawing.Point(510, 76);
            this.bt_post.Name = "bt_post";
            this.bt_post.PopupAfterExecute = null;
            this.bt_post.PopupBeforeExecute = null;
            this.bt_post.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("bt_post.PopupSearchSendParams")));
            this.bt_post.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.bt_post.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("bt_post.popupWindowSetting")));
            this.bt_post.PostalCodeControl = "NYUUKINSAKI_POST";
            this.bt_post.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("bt_post.RegistCheckMethod")));
            this.bt_post.Size = new System.Drawing.Size(110, 20);
            this.bt_post.TabIndex = 12;
            this.bt_post.TabStop = false;
            this.bt_post.Tag = "住所から郵便番号を取得します";
            this.bt_post.Text = "郵便番号参照";
            this.bt_post.TodoufukenControl = "NYUUKINSAKI_TODOUFUKEN_CD";
            this.bt_post.UseVisualStyleBackColor = true;
            // 
            // NYUUKINSAKI_TODOUFUKEN_CD
            // 
            this.NYUUKINSAKI_TODOUFUKEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.NYUUKINSAKI_TODOUFUKEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NYUUKINSAKI_TODOUFUKEN_CD.CustomFormatSetting = "00";
            this.NYUUKINSAKI_TODOUFUKEN_CD.DBFieldsName = "TODOUFUKEN_CD";
            this.NYUUKINSAKI_TODOUFUKEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKINSAKI_TODOUFUKEN_CD.DisplayItemName = "都道府県CD";
            this.NYUUKINSAKI_TODOUFUKEN_CD.DisplayPopUp = null;
            this.NYUUKINSAKI_TODOUFUKEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_TODOUFUKEN_CD.FocusOutCheckMethod")));
            this.NYUUKINSAKI_TODOUFUKEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.NYUUKINSAKI_TODOUFUKEN_CD.ForeColor = System.Drawing.Color.Black;
            this.NYUUKINSAKI_TODOUFUKEN_CD.FormatSetting = "カスタム";
            this.NYUUKINSAKI_TODOUFUKEN_CD.GetCodeMasterField = "TODOUFUKEN_CD,TODOUFUKEN_NAME";
            this.NYUUKINSAKI_TODOUFUKEN_CD.IsInputErrorOccured = false;
            this.NYUUKINSAKI_TODOUFUKEN_CD.ItemDefinedTypes = "varchar";
            this.NYUUKINSAKI_TODOUFUKEN_CD.Location = new System.Drawing.Point(626, 30);
            this.NYUUKINSAKI_TODOUFUKEN_CD.Name = "NYUUKINSAKI_TODOUFUKEN_CD";
            this.NYUUKINSAKI_TODOUFUKEN_CD.PopupAfterExecute = null;
            this.NYUUKINSAKI_TODOUFUKEN_CD.PopupBeforeExecute = null;
            this.NYUUKINSAKI_TODOUFUKEN_CD.PopupGetMasterField = "TODOUFUKEN_CD,TODOUFUKEN_NAME";
            this.NYUUKINSAKI_TODOUFUKEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKINSAKI_TODOUFUKEN_CD.PopupSearchSendParams")));
            this.NYUUKINSAKI_TODOUFUKEN_CD.PopupSendParams = new string[0];
            this.NYUUKINSAKI_TODOUFUKEN_CD.PopupSetFormField = "NYUUKINSAKI_TODOUFUKEN_CD,TODOUFUKEN_NAME";
            this.NYUUKINSAKI_TODOUFUKEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TODOUFUKEN;
            this.NYUUKINSAKI_TODOUFUKEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.NYUUKINSAKI_TODOUFUKEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKINSAKI_TODOUFUKEN_CD.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.NYUUKINSAKI_TODOUFUKEN_CD.RangeSetting = rangeSettingDto2;
            this.NYUUKINSAKI_TODOUFUKEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_TODOUFUKEN_CD.RegistCheckMethod")));
            this.NYUUKINSAKI_TODOUFUKEN_CD.SetFormField = "NYUUKINSAKI_TODOUFUKEN_CD,TODOUFUKEN_NAME";
            this.NYUUKINSAKI_TODOUFUKEN_CD.ShortItemName = "都道府県CD";
            this.NYUUKINSAKI_TODOUFUKEN_CD.Size = new System.Drawing.Size(20, 20);
            this.NYUUKINSAKI_TODOUFUKEN_CD.TabIndex = 11;
            this.NYUUKINSAKI_TODOUFUKEN_CD.Tag = "都道府県を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.NYUUKINSAKI_TODOUFUKEN_CD.WordWrap = false;
            // 
            // NyuukinsakiNyuuryokuHoshuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(999, 530);
            this.Controls.Add(this.NYUUKINSAKI_TODOUFUKEN_CD);
            this.Controls.Add(this.bt_post);
            this.Controls.Add(this.bt_address);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.NYUUKINSAKI_ADDRESS2);
            this.Controls.Add(this.NYUUKINSAKI_ADDRESS1);
            this.Controls.Add(this.TODOUFUKEN_NAME);
            this.Controls.Add(this.NYUUKINSAKI_POST);
            this.Controls.Add(this.NYUUKINSAKI_FAX);
            this.Controls.Add(this.NYUUKINSAKI_TEL);
            this.Controls.Add(this.bt_saiban);
            this.Controls.Add(this.NYUUKINSAKI_CD);
            this.Controls.Add(this.NYUUKINSAKI_NAME_RYAKU);
            this.Controls.Add(this.NYUUKINSAKI_NAME2);
            this.Controls.Add(this.NYUUKINSAKI_NAME1);
            this.Controls.Add(this.NYUUKINSAKI_FURIGANA);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Name = "NyuukinsakiNyuuryokuHoshuForm";
            this.Text = "NyuukinsakiNyuuryokuHoshuForm";
            this.tabControl1.ResumeLayout(false);
            this.tab_furikomijinmei.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran_furikomi)).EndInit();
            this.tab_torihikisaki.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran_torihikisaki)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomAlphaNumTextBox NYUUKINSAKI_CD;
        internal r_framework.CustomControl.CustomButton bt_saiban;
        internal r_framework.CustomControl.CustomTextBox NYUUKINSAKI_FURIGANA;
        internal System.Windows.Forms.TabControl tabControl1;
        internal r_framework.CustomControl.CustomTextBox NYUUKINSAKI_NAME1;
        internal r_framework.CustomControl.CustomTextBox NYUUKINSAKI_NAME2;
        internal r_framework.CustomControl.CustomTextBox NYUUKINSAKI_NAME_RYAKU;
        internal r_framework.CustomControl.CustomPhoneNumberTextBox NYUUKINSAKI_TEL;
        internal r_framework.CustomControl.CustomPhoneNumberTextBox NYUUKINSAKI_FAX;
        internal r_framework.CustomControl.CustomPostalCodeTextBox NYUUKINSAKI_POST;
        internal r_framework.CustomControl.CustomTextBox TODOUFUKEN_NAME;
        internal r_framework.CustomControl.CustomTextBox NYUUKINSAKI_ADDRESS1;
        internal r_framework.CustomControl.CustomTextBox NYUUKINSAKI_ADDRESS2;
        internal r_framework.CustomControl.CustomNumericTextBox2 TORIKOMI_KBN;
        internal r_framework.CustomControl.GcCustomMultiRow Ichiran_furikomi;
        internal r_framework.CustomControl.GcCustomMultiRow Ichiran_torihikisaki;
        internal System.Windows.Forms.TabPage tab_furikomijinmei;
        internal System.Windows.Forms.TabPage tab_torihikisaki;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.Label label9;
        internal System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Label label11;
        internal System.Windows.Forms.Panel panel1;
        internal r_framework.CustomControl.CustomRadioButton rbt_shinai;
        internal r_framework.CustomControl.CustomRadioButton rbt_suru;
        internal MultiRowTemplate.NyuukinsakiFurikomiDetail nyuukinsakiFurikomiDetail1;
        internal MultiRowTemplate.NyuukinsakiTorihikisakiDetail nyuukinsakiTorihikisakiDetail1;
        internal r_framework.CustomControl.CustomAddressSearchButton bt_address;
        internal r_framework.CustomControl.CustomPostSearchButton bt_post;
        internal r_framework.CustomControl.CustomNumericTextBox2 NYUUKINSAKI_TODOUFUKEN_CD;
    }
}
