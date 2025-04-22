// $Id: SystemSetteiHoshuForm.cs 55371 2015-07-10 11:07:15Z t-thanhson@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MasterKyoutsuPopup2.APP;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using SystemSetteiHoshu.Logic;
using SystemSetteiHoshu.Const;
using System.Collections.ObjectModel;
using r_framework.Utility;
using r_framework.Configuration;

using System.ComponentModel;
using System.Linq;
using r_framework.Entity;




namespace SystemSetteiHoshu.APP
{
    /// <summary>
    /// システム設定入力画面
    /// </summary>
    [Implementation]
    public partial class SystemSetteiHoshuForm : SuperForm
    {
        /// <summary>
        /// システム設定入力画面ロジック
        /// </summary>
        private SystemSetteiHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// 品名税区分テーブル
        /// </summary>
        private DataTable HinmeiZeiKbnTable;

        /// <summary>
        /// 計量区分テーブル
        /// </summary>
        private DataTable KeiryouKbnTable;

        /// <summary>
        /// 契約区分テーブル
        /// </summary>
        private DataTable KeiyakuKbnTable;

        /// <summary>
        /// 計上区分テーブル
        /// </summary>
        private DataTable KeijyouKbnTable;

        //20250305
        internal string previousBankShitenCd;
        private bool isBankShitenPopup;

        public Int16 maxWindowCount = 5;

        /// <summary>
        /// デジタコ項目
        /// </summary>
        public string digiCorpId = string.Empty;
        public string digiUserId = string.Empty;
        public string digiPassword = string.Empty;
        public Int16 digiRangeRadius = 100;
        public Int16 digiCarryOverNextDay = 2;
        public Int16 UKEIRESHUKA_GAMEN_SIZE = 1;
        public Int16 DENPYOU_HAKOU_HYOUJI = 2;
        public Int16 SYS_BARCODO_SHINKAKU_KBN = 1;//160029

        /// <summary>
        /// ナビタイム項目
        /// </summary>
        public string naviCorpId = string.Empty;
        public string naviAccount = string.Empty;
        public string naviPassword = string.Empty;
        public Int16 naviSagyouTime = 0;
        public bool naviTraffic = false;
        public bool naviSmartIc = false;
        public bool naviToll = false;
        public Int16 naviGetTime = 1;

        /// <summary>
        /// オプション項目
        /// </summary>
        public Int16 maxInsertCapacity = 0;
        public Decimal maxFileSize = 0;
        public string dbFileConnect = string.Empty;
        public string dbInxsSubappConnectString = string.Empty;
        public string dbInxsSubappConnectName = string.Empty;

        //CongBinh 20210712 #152799 S
        public string SupportKbn = "2";
        public string SupportUrl = "https://shogun-sp.force.com/s/login/";
        //CongBinh 20210712 #152799 E

        //QN_QUAN add 20211229 #158952 S
        public string dbLOGConnect = string.Empty;
        //QN_QUAN add 20211229 #158952 E

        //PhuocLoc 2022/01/04 #158897, #158898 -Start
        public string ModSecretKey = string.Empty;
        public string ModCustomerId = string.Empty;
        public string isUsing = "1";
        public string isNotUsing = "2";
        internal string beforeValue1 = string.Empty;
        internal string beforeValue2 = string.Empty;
        internal string beforeValue3 = string.Empty;
        internal string beforeValue4 = string.Empty;
        //PhuocLoc 2022/01/04 #158897, #158898 -End

        /// <summary>
        /// 空電プッシュ項目
        /// </summary>
        public string KaradenAccessKey = string.Empty;
        public string karadenSecurityCode = string.Empty;
        public int KaradenMaxWordCount = 660;

        // 楽楽明細連携
        public string RakurakuCodeKbn = string.Empty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SystemSetteiHoshuForm()
            : base(WINDOW_ID.M_SYS_INFO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new SystemSetteiHoshuLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            // 未定義のタブを削除する
            this.main_tab.TabPages.Remove(this.shukkin_tabpage);
            this.main_tab.TabPages.Remove(this.zaimu_tabpage);
            this.main_tab.TabPages.Remove(this.siharai_tabpage);
            this.main_tab.TabPages.Remove(this.kobetu_tabpage);
            
            // オプション未使用なら電子契約タブも削除
            if (!AppConfig.AppOptions.IsDenshiKeiyaku())
            {
                this.main_tab.TabPages.Remove(this.denshi_keiyaku_tabpage);
            }

            //SONNT #142900 作業日変更したら、INXS確定日と比較 2020/10 START
            if (!AppConfig.AppOptions.IsInxsUketsuke())
            {
                this.main_tab.TabPages.Remove(this.shougun_inxs_tabpage);
            }
            //SONNT #142900 作業日変更したら、INXS確定日と比較 2020/10 END

            //PhuocLoc 2022/01/04 #158897, #158898 -Start
            if (!AppConfig.AppOptions.IsWANSign())
            {
                this.main_tab.TabPages.Remove(this.wanSign_tabpage);
            }
            //PhuocLoc 2022/01/04 #158897, #158898 -End

            this.sub_tab.TabPages.Remove(this.kansanchi_tabpage);
            this.sub_tab.TabPages.Remove(menu_kengen_tabpage);

            // オプション未使用ならｼｮｰﾄﾒｯｾｰｼﾞタブも削除
            if (!AppConfig.AppOptions.IsSMS())
            {
                this.main_tab.TabPages.Remove(this.sms_tabpage);
            }
            // ファイルアップロードオプション未使用であれば角印項目の削除
            if (!AppConfig.AppOptions.IsFileUpload())
            {
                this.seikyuu_tabpage.Controls.Remove(this.KAKUIN_POSITION_LEFT);
                this.seikyuu_tabpage.Controls.Remove(this.KAKUIN_POSITION_TOP);
                this.seikyuu_tabpage.Controls.Remove(this.label213);
                this.seikyuu_tabpage.Controls.Remove(this.label214);
                this.seikyuu_tabpage.Controls.Remove(this.label215);
                this.seikyuu_tabpage.Controls.Remove(this.btnFileRef);
                this.seikyuu_tabpage.Controls.Remove(this.btnUpload);
                this.seikyuu_tabpage.Controls.Remove(this.btnBrowse);
                this.seikyuu_tabpage.Controls.Remove(this.btnDelete);
                this.seikyuu_tabpage.Controls.Remove(this.KAKUIN_FILE_NAME);
                this.seikyuu_tabpage.Controls.Remove(this.label217);
                this.seikyuu_tabpage.Controls.Remove(this.panel87);
            }
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            bool catchErr = this.logic.WindowInit();
            if (catchErr)
            {
                return;
            }

            // 品名税区分テーブル設定
            catchErr = this.SetHinmeiZeiKbnTable();
            if (catchErr)
            {
                return;
            }

            // 計量区分テーブル設定
            catchErr = this.SetKeiryouKbnTable();
            if (catchErr)
            {
                return;
            }

            // 契約区分テーブル設定
            catchErr = this.SetKeiyakuKbnTable();
            if (catchErr)
            {
                return;
            }

            // 計上区分テーブル設定
            catchErr = this.SetKeijyouKbnTable();
            if (catchErr)
            {
                return;
            }

            this.Search(null, e);

            // 権限チェック
            if (!r_framework.Authority.Manager.CheckAuthority("M261", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                this.logic.DispReferenceMode();
            }
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void Search(object sender, EventArgs e)
        {
            int count = this.logic.Search();
            if (count == 0)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("C001");
                return;
            }
            else if (count < 0)
            {
                return;
            }

            this.logic.SetIchiran();
        }

        /// <summary>
        /// パスワード入力用 InputBox 作成
        /// </summary>
        /// <param name="title"></param>
        /// <param name="promptText"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            textBox.PasswordChar = '*';

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;

            return dialogResult;
        }

        /// <summary>
        /// システム設定入力(初回登録)処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Initial(object sender, EventArgs e)
        {
            string inputValue = "";

            if (InputBox("パスワードを入力してください", "パスワード:", ref inputValue) == DialogResult.OK)
            {
                if (Const.SystemSetteiHoshuConstans.MESSAGE_INITAIL_PASSWORDS == inputValue)
                {
                    InitialPopupForm initialPopupForm = new InitialPopupForm();

                    //システム設定入力で変更するコントロールの追加
                    Object[] parentControls = new Object[46];//CongBinh 20210712 #152799

                    parentControls[0] = this.SYS_ZEI_KEISAN_KBN_USE_KBN;
                    parentControls[1] = this.SYS_RENBAN_HOUHOU_KBN;
                    parentControls[2] = this.SYS_MANI_KEITAI_KBN;
                    parentControls[3] = this.SEIKYUU_SHIME_SHORI_KBN;
                    parentControls[4] = this.SHIHARAI_SHIME_SHORI_KBN;
                    parentControls[5] = this.ZAIKO_KANRI;
                    parentControls[6] = this.ZAIKO_HYOUKA_HOUHOU;
                    parentControls[7] = this.CONTENA_KANRI_HOUHOU;
                    parentControls[8] = this.SYS_RECEIPT_RENBAN_HOUHOU_KBN;
                    parentControls[9] = this.SYS_JYURYOU_FORMAT_CD;
                    parentControls[10] = this.SYS_SUURYOU_FORMAT_CD;
                    parentControls[11] = this.SYS_TANKA_FORMAT_CD;
                    parentControls[12] = this.ITAKU_KEIYAKU_SUURYOU_FORMAT_CD;
                    parentControls[13] = this.ITAKU_KEIYAKU_TANKA_FORMAT_CD;
                    parentControls[14] = this.MANIFEST_SUURYO_FORMAT_CD;
                    parentControls[15] = this.digiCorpId;
                    parentControls[16] = this.digiUserId;
                    parentControls[17] = this.digiPassword;
                    parentControls[18] = this.digiRangeRadius;
                    parentControls[19] = this.digiCarryOverNextDay;
                    parentControls[20] = this.naviCorpId;
                    parentControls[21] = this.naviAccount;
                    parentControls[22] = this.naviPassword;
                    parentControls[23] = this.naviSagyouTime;
                    parentControls[24] = this.naviTraffic;
                    parentControls[25] = this.naviSmartIc;
                    parentControls[26] = this.naviToll;
                    parentControls[27] = this.naviGetTime;
                    parentControls[28] = this.maxWindowCount;
                    parentControls[29] = this.UKEIRESHUKA_GAMEN_SIZE;
                    parentControls[30] = this.DENPYOU_HAKOU_HYOUJI;
                    parentControls[31] = this.maxInsertCapacity;
                    parentControls[32] = this.maxFileSize;
                    parentControls[33] = this.dbFileConnect;
                    parentControls[34] = this.dbInxsSubappConnectString;
                    parentControls[35] = this.dbInxsSubappConnectName;

                    //CongBinh 20210712 #152799 S
                    parentControls[36] = this.SupportKbn;
                    parentControls[37] = this.SupportUrl;
                    //CongBinh 20210712 #152799 E
                    //QN_QUAN add 20211229 #158952 S
                    parentControls[38] = this.dbLOGConnect;
                    //QN_QUAN add 20211229 #158952 E
                    parentControls[39] = this.SYS_BARCODO_SHINKAKU_KBN;//160029

                    //PhuocLoc 2022/01/04 #158897, #158898 -Start
                    parentControls[40] = this.ModSecretKey;
                    parentControls[41] = this.ModCustomerId;
                    //PhuocLoc 2022/01/04 #158897, #158898 -End
                    
                    parentControls[42] = this.KaradenAccessKey;
                    parentControls[43] = this.karadenSecurityCode;
                    parentControls[44] = this.KaradenMaxWordCount;

                    // 楽楽明細連携
                    parentControls[45] = this.RakurakuCodeKbn;

                    initialPopupForm.ParentControls = parentControls;

                    //Popup メニューを表示
                    initialPopupForm.ShowDialog();

                    // デジタコ項目を設定
                    this.digiCorpId = parentControls[15].ToString();
                    this.digiUserId = parentControls[16].ToString();
                    this.digiPassword = parentControls[17].ToString();
                    if (!parentControls[18].ToString().Equals(""))
                    {
                        this.digiRangeRadius = Convert.ToInt16(parentControls[18].ToString());
                    }
                    else
                    {
                        this.digiRangeRadius = 0;
                    }
                    this.digiCarryOverNextDay = Convert.ToInt16(parentControls[19].ToString());
                    this.maxWindowCount = Convert.ToInt16(parentControls[28].ToString());

                    // NAVITIME項目を設定
                    this.naviCorpId = parentControls[20].ToString();
                    this.naviAccount=parentControls[21].ToString();
                    this.naviPassword=parentControls[22].ToString();
                    if (!parentControls[23].ToString().Equals(""))
                    {
                        this.naviSagyouTime = Convert.ToInt16(parentControls[23].ToString());
                    }
                    else
                    {
                        this.naviSagyouTime = 0;
                    }
                    this.naviTraffic = (bool)parentControls[24];
                    this.naviSmartIc = (bool)parentControls[25];
                    this.naviToll=(bool)parentControls[26];
                    if (!parentControls[27].ToString().Equals(""))
                    {
                        this.naviGetTime = Convert.ToInt16(parentControls[27].ToString());
                    }
                    else
                    {
                        this.naviGetTime = 1;
                    }

                    this.UKEIRESHUKA_GAMEN_SIZE = Convert.ToInt16(parentControls[29].ToString());
                    if (!parentControls[30].ToString().Equals(""))
                    {
                        this.DENPYOU_HAKOU_HYOUJI = Convert.ToInt16(parentControls[30].ToString());
                    }

                    // オプション項目
                    if (!parentControls[31].ToString().Equals(""))
                    {
                        this.maxInsertCapacity = Convert.ToInt16(parentControls[31].ToString());
                    }
                    else
                    {
                        this.maxInsertCapacity = 0;
                    }
                    if (!parentControls[32].ToString().Equals(""))
                    {
                        this.maxFileSize = Convert.ToDecimal(parentControls[32].ToString());
                    }
                    else
                    {
                        this.maxFileSize = 0;
                    }
                    this.dbFileConnect = parentControls[33].ToString();
                    this.dbInxsSubappConnectString = parentControls[34].ToString();
                    this.dbInxsSubappConnectName = parentControls[35].ToString();
                    //CongBinh 20210712 #152799 S
                    this.SupportKbn = parentControls[36].ToString();
                    this.SupportUrl = parentControls[37].ToString();
                    //CongBinh 20210712 #152799 E
                    //QN_QUAN add 20211229 #158952 S
                    this.dbLOGConnect = parentControls[38].ToString();
                    //QN_QUAN add 20211229 #158952 E

                    //160029 S
                    if (!parentControls[38].ToString().Equals(""))
                    {

                        this.SYS_BARCODO_SHINKAKU_KBN = Convert.ToInt16(parentControls[39].ToString());

                    }
                    //160029 E

                    //PhuocLoc 2022/01/04 #158897, #158898 -Start
                    this.ModSecretKey = parentControls[40].ToString();
                    this.ModCustomerId = parentControls[41].ToString();
                    //PhuocLoc 2022/01/04 #158897, #158898 -End
                    
                    this.KaradenAccessKey = parentControls[42].ToString();
                    this.karadenSecurityCode = parentControls[43].ToString();
                    if (!parentControls[44].ToString().Equals(""))
                    {
                        this.KaradenMaxWordCount = Convert.ToInt16(parentControls[44].ToString());
                    }
                    else
                    {
                        this.KaradenMaxWordCount = 660;
                    }

                    this.RakurakuCodeKbn = parentControls[45].ToString();
                }
                else
                {
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E058");
                }
            }
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            // 入金判断範囲FromToチェック
            if (!base.RegistErrorFlag)
            {
                if (this.logic.fromToCheck(this.NYUUKIN_HANDAN_BEGIN.Text, this.NYUUKIN_HANDAN_END.Text))
                {
                    msgLogic.MessageBoxShow("E032", this.NYUUKIN_HANDAN_BEGIN.DisplayItemName, this.NYUUKIN_HANDAN_END.DisplayItemName);

                    this.NYUUKIN_HANDAN_BEGIN.IsInputErrorOccured = true;
                    this.NYUUKIN_HANDAN_END.IsInputErrorOccured = true;
                    return;
                }
            }
            else
            {
                return;
            }

            if (!this.logic.ShiharaiKakuteiNewPasswordValidate(true, false))
            {
                return;
            }

            if (!this.logic.UriageKakuteiNewPasswordValidate(true, false))
            {
                return;
            }

            // 計量票にバーコードを表示する場合、見出の文字数チェック
            if (this.KEIRYOU_BARCODE_KBN.Text == "1")
            {
                if (!this.logic.KeiryouHyouTitleCheck())
                {
                    msgLogic.MessageBoxShowError("バーコードを印字するため、計量票見出は6文字以下で入力してください。");
                    return;
                }
            }

            if (!base.RegistErrorFlag)
            {
                bool catchErr = this.logic.CreateEntity(false);
                if (catchErr)
                {
                    return;
                }
                this.logic.Regist(base.RegistErrorFlag);
                if (catchErr)
                {
                    return;
                }
                this.Search(sender, e);
            }
        }

        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            this.logic.Cancel();
            Search(sender, e);
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// CD表示変更イベント(回収)
        /// </summary>
        public void CdFormattingKaishuu()
        {
            bool catchErr = false;
            this.SEIKYUU_KAISHUU_HOUHOU.Text = this.logic.CdFormatting(this.SEIKYUU_KAISHUU_HOUHOU.Text.ToString(), this.SEIKYUU_KAISHUU_HOUHOU.MaxLength.ToString(), out catchErr);
        }

        /// <summary>
        /// CD表示変更イベント(支払)
        /// </summary>
        public void CdFormattingShiharai()
        {
            bool catchErr = false;
            this.SHIHARAI_HOUHOU.Text = this.logic.CdFormatting(this.SHIHARAI_HOUHOU.Text.ToString(), this.SHIHARAI_HOUHOU.MaxLength.ToString(), out catchErr);
        }

        /// <summary>
        /// FromToチェック(通知)
        /// </summary>
        public void fromToCheckTuuchi(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 表示条件変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HyojiJokenhanged(object sender, EventArgs e)
        {
            if (this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked == false
                && this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked == false
                    && this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked == false)
            {
                this.DUMY_HYOUJI_JOUKEN.Text = string.Empty;
            }
            else
            {
                this.DUMY_HYOUJI_JOUKEN.Text = "hoge";
            }
        }

        /// <summary>
        /// 請求先締日1変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIMEBI1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.SEIKYUU_SHIMEBI1.Text))
            {
                this.SEIKYUU_SHIMEBI2.Enabled = false;
                this.SEIKYUU_SHIMEBI2.Text = string.Empty;
                this.SEIKYUU_SHIMEBI3.Enabled = false;
                this.SEIKYUU_SHIMEBI3.Text = string.Empty;
            }
            else
            {
                this.SEIKYUU_SHIMEBI2.Enabled = true;
            }
        }

        /// <summary>
        /// 請求先締日1変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIMEBI1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (!string.IsNullOrWhiteSpace(this.SEIKYUU_SHIMEBI2.Text) && this.SEIKYUU_SHIMEBI1.Text.Equals(this.SEIKYUU_SHIMEBI2.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日2");
                e.Cancel = true;
            }
            if (!string.IsNullOrWhiteSpace(this.SEIKYUU_SHIMEBI3.Text) && this.SEIKYUU_SHIMEBI1.Text.Equals(this.SEIKYUU_SHIMEBI3.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日3");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 請求先締日2変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIMEBI2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.SEIKYUU_SHIMEBI2.Text))
            {
                this.SEIKYUU_SHIMEBI3.Enabled = false;
                this.SEIKYUU_SHIMEBI3.Text = string.Empty;
            }
            else
            {
                this.SEIKYUU_SHIMEBI3.Enabled = true;
            }
        }

        /// <summary>
        /// 請求先締日2変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIMEBI2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (!string.IsNullOrWhiteSpace(this.SEIKYUU_SHIMEBI1.Text) && this.SEIKYUU_SHIMEBI2.Text.Equals(this.SEIKYUU_SHIMEBI1.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日1");
                e.Cancel = true;
            }
            if (!string.IsNullOrWhiteSpace(this.SEIKYUU_SHIMEBI3.Text) && this.SEIKYUU_SHIMEBI2.Text.Equals(this.SEIKYUU_SHIMEBI3.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日3");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 請求先締日3変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIMEBI3_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (!string.IsNullOrWhiteSpace(this.SEIKYUU_SHIMEBI1.Text) && this.SEIKYUU_SHIMEBI3.Text.Equals(this.SEIKYUU_SHIMEBI1.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日1");
                e.Cancel = true;
            }
            if (!string.IsNullOrWhiteSpace(this.SEIKYUU_SHIMEBI2.Text) && this.SEIKYUU_SHIMEBI3.Text.Equals(this.SEIKYUU_SHIMEBI2.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日2");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 支払先締日1変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHIMEBI1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI1.Text))
            {
                this.SHIHARAI_SHIMEBI2.Enabled = false;
                this.SHIHARAI_SHIMEBI2.Text = string.Empty;
                this.SHIHARAI_SHIMEBI3.Enabled = false;
                this.SHIHARAI_SHIMEBI3.Text = string.Empty;
            }
            else
            {
                this.SHIHARAI_SHIMEBI2.Enabled = true;
            }
        }

        /// <summary>
        /// 支払先締日1変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHIMEBI1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (!string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI2.Text) && this.SHIHARAI_SHIMEBI1.Text.Equals(this.SHIHARAI_SHIMEBI2.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日2");
                e.Cancel = true;
            }
            if (!string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI3.Text) && this.SHIHARAI_SHIMEBI1.Text.Equals(this.SHIHARAI_SHIMEBI3.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日3");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 支払先締日2変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHIMEBI2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI2.Text))
            {
                this.SHIHARAI_SHIMEBI3.Enabled = false;
                this.SHIHARAI_SHIMEBI3.Text = string.Empty;
            }
            else
            {
                this.SHIHARAI_SHIMEBI3.Enabled = true;
            }
        }

        /// <summary>
        /// 支払先締日2変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHIMEBI2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (!string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI1.Text) && this.SHIHARAI_SHIMEBI2.Text.Equals(this.SHIHARAI_SHIMEBI1.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日1");
                e.Cancel = true;
            }
            if (!string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI3.Text) && this.SHIHARAI_SHIMEBI2.Text.Equals(this.SHIHARAI_SHIMEBI3.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日3");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 支払先締日3変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHIMEBI3_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (!string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI1.Text) && this.SHIHARAI_SHIMEBI3.Text.Equals(this.SHIHARAI_SHIMEBI1.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日1");
                e.Cancel = true;
            }
            if (!string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI2.Text) && this.SHIHARAI_SHIMEBI3.Text.Equals(this.SHIHARAI_SHIMEBI2.Text))
            {
                msgLogic.MessageBoxShow("E031", "締日2");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 個別拠点コード選択後処理
        /// </summary>
        public virtual void PopupAfterKyotenCd()
        {
            if (!string.IsNullOrWhiteSpace(this.KYOTEN_CD.Text))
            {
                bool catchErr = false;
                this.KYOTEN_CD.Text = this.logic.CdFormatting(this.KYOTEN_CD.Text.ToString(), this.KYOTEN_CD.MaxLength.ToString(), out catchErr);
            }
        }

        /// <summary>
        /// 請求拠点コード選択後処理
        /// </summary>
        public virtual void PopupAfterSeikyuuKyotenCd()
        {
            if (!string.IsNullOrWhiteSpace(this.SEIKYUU_KYOTEN_CD.Text))
            {
                bool catchErr = false;
                this.SEIKYUU_KYOTEN_CD.Text = this.logic.CdFormatting(this.SEIKYUU_KYOTEN_CD.Text.ToString(), this.SEIKYUU_KYOTEN_CD.MaxLength.ToString(), out catchErr);
            }
        }

        /// <summary>
        /// 支払拠点コード選択後処理
        /// </summary>
        public virtual void PopupAfterShiharaiKyotenCd()
        {
            if (!string.IsNullOrWhiteSpace(this.SHIHARAI_KYOTEN_CD.Text))
            {
                bool catchErr = false;
                this.SHIHARAI_KYOTEN_CD.Text = this.logic.CdFormatting(this.SHIHARAI_KYOTEN_CD.Text.ToString(), this.SHIHARAI_KYOTEN_CD.MaxLength.ToString(), out catchErr);
            }
        }

        /// <summary>
        /// 業者請求拠点コード選択後処理
        /// </summary>
        public virtual void PopupAfterGyoushaSeikyuuKyotenCd()
        {
            if (!string.IsNullOrWhiteSpace(this.GYOUSHA_SEIKYUU_KYOTEN_CD.Text))
            {
                bool catchErr = false;
                this.GYOUSHA_SEIKYUU_KYOTEN_CD.Text = this.logic.CdFormatting(this.GYOUSHA_SEIKYUU_KYOTEN_CD.Text.ToString(), this.GYOUSHA_SEIKYUU_KYOTEN_CD.MaxLength.ToString(), out catchErr);
            }
        }

        /// <summary>
        /// 業者支払拠点コード選択後処理
        /// </summary>
        public virtual void PopupAfterGyoushaShiharaiKyotenCd()
        {
            if (!string.IsNullOrWhiteSpace(this.GYOUSHA_SHIHARAI_KYOTEN_CD.Text))
            {
                bool catchErr = false;
                this.GYOUSHA_SHIHARAI_KYOTEN_CD.Text = this.logic.CdFormatting(this.GYOUSHA_SHIHARAI_KYOTEN_CD.Text.ToString(), this.GYOUSHA_SHIHARAI_KYOTEN_CD.MaxLength.ToString(), out catchErr);
            }
        }

        /// <summary>
        /// 現場請求拠点コード選択後処理
        /// </summary>
        public virtual void PopupAfterGenbaSeikyuuKyotenCd()
        {
            if (!string.IsNullOrWhiteSpace(this.GENBA_SEIKYUU_KYOTEN_CD.Text))
            {
                bool catchErr = false;
                this.GENBA_SEIKYUU_KYOTEN_CD.Text = this.logic.CdFormatting(this.GENBA_SEIKYUU_KYOTEN_CD.Text.ToString(), this.GENBA_SEIKYUU_KYOTEN_CD.MaxLength.ToString(), out catchErr);
            }
        }

        /// <summary>
        /// 現場支払拠点コード選択後処理
        /// </summary>
        public virtual void PopupAfterGenbaShiharaiKyotenCd()
        {
            if (!string.IsNullOrWhiteSpace(this.GENBA_SHIHARAI_KYOTEN_CD.Text))
            {
                bool catchErr = false;
                this.GENBA_SHIHARAI_KYOTEN_CD.Text = this.logic.CdFormatting(this.GENBA_SHIHARAI_KYOTEN_CD.Text.ToString(), this.GENBA_SHIHARAI_KYOTEN_CD.MaxLength.ToString(), out catchErr);
            }
        }

        /// <summary>
        /// 請求書拠点印字選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_KYOTEN_PRINT_KBN_TextChanged(object sender, EventArgs e)
        {
            if (this.SEIKYUU_KYOTEN_PRINT_KBN.Text.Equals("1"))
            {
                this.SEIKYUU_KYOTEN_CD.Enabled = true;
                this.SEIKYUU_KYOTEN_CD_SEARCH.Enabled = true;
            }
            else
            {
                this.SEIKYUU_KYOTEN_CD.Enabled = false;
                this.SEIKYUU_KYOTEN_CD.Text = string.Empty;
                this.SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                this.SEIKYUU_KYOTEN_CD_SEARCH.Enabled = false;
            }
        }

        /// <summary>
        /// 支払書拠点印字選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_KYOTEN_PRINT_KBN_TextChanged(object sender, EventArgs e)
        {
            if (this.SHIHARAI_KYOTEN_PRINT_KBN.Text.Equals("1"))
            {
                this.SHIHARAI_KYOTEN_CD.Enabled = true;
                this.SHIHARAI_KYOTEN_CD_SEARCH.Enabled = true;
            }
            else
            {
                this.SHIHARAI_KYOTEN_CD.Enabled = false;
                this.SHIHARAI_KYOTEN_CD.Text = string.Empty;
                this.SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                this.SHIHARAI_KYOTEN_CD_SEARCH.Enabled = false;
            }
        }

        /// <summary>
        /// 業者請求書拠点印字選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN_TextChanged(object sender, EventArgs e)
        {
            if (this.GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN.Text.Equals("1"))
            {
                this.GYOUSHA_SEIKYUU_KYOTEN_CD.Enabled = true;
                this.GYOUSHA_SEIKYUU_KYOTEN_CD_SEARCH.Enabled = true;
            }
            else
            {
                this.GYOUSHA_SEIKYUU_KYOTEN_CD.Enabled = false;
                this.GYOUSHA_SEIKYUU_KYOTEN_CD.Text = string.Empty;
                this.GYOUSHA_SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                this.GYOUSHA_SEIKYUU_KYOTEN_CD_SEARCH.Enabled = false;
            }
        }

        /// <summary>
        /// 業者支払書拠点印字選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN_TextChanged(object sender, EventArgs e)
        {
            if (this.GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN.Text.Equals("1"))
            {
                this.GYOUSHA_SHIHARAI_KYOTEN_CD.Enabled = true;
                this.GYOUSHA_SHIHARAI_KYOTEN_CD_SEARCH.Enabled = true;
            }
            else
            {
                this.GYOUSHA_SHIHARAI_KYOTEN_CD.Enabled = false;
                this.GYOUSHA_SHIHARAI_KYOTEN_CD.Text = string.Empty;
                this.GYOUSHA_SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                this.GYOUSHA_SHIHARAI_KYOTEN_CD_SEARCH.Enabled = false;
            }
        }

        /// <summary>
        /// 現場請求書拠点印字選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_SEIKYUU_KYOTEN_PRINT_KBN_TextChanged(object sender, EventArgs e)
        {
            if (this.GENBA_SEIKYUU_KYOTEN_PRINT_KBN.Text.Equals("1"))
            {
                this.GENBA_SEIKYUU_KYOTEN_CD.Enabled = true;
                this.GENBA_SEIKYUU_KYOTEN_CD_SEARCH.Enabled = true;
            }
            else
            {
                this.GENBA_SEIKYUU_KYOTEN_CD.Enabled = false;
                this.GENBA_SEIKYUU_KYOTEN_CD.Text = string.Empty;
                this.GENBA_SEIKYUU_KYOTEN_NAME.Text = string.Empty;
                this.GENBA_SEIKYUU_KYOTEN_CD_SEARCH.Enabled = false;
            }
        }

        /// <summary>
        /// 現場支払書拠点印字選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_SHIHARAI_KYOTEN_PRINT_KBN_TextChanged(object sender, EventArgs e)
        {
            if (this.GENBA_SHIHARAI_KYOTEN_PRINT_KBN.Text.Equals("1"))
            {
                this.GENBA_SHIHARAI_KYOTEN_CD.Enabled = true;
                this.GENBA_SHIHARAI_KYOTEN_CD_SEARCH.Enabled = true;
            }
            else
            {
                this.GENBA_SHIHARAI_KYOTEN_CD.Enabled = false;
                this.GENBA_SHIHARAI_KYOTEN_CD.Text = string.Empty;
                this.GENBA_SHIHARAI_KYOTEN_NAME.Text = string.Empty;
                this.GENBA_SHIHARAI_KYOTEN_CD_SEARCH.Enabled = false;
            }
        }

        
        /// <summary>
        /// 個別設定拠点コード変更チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void KYOTEN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //bool catchErr = false;
            //this.KYOTEN_NAME_RYAKU.Text = this.logic.SearchKyotenName(this.KYOTEN_CD.Text, e, out catchErr);
        }
        

        /// <summary>
        /// マニ記載業者変更チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void GYOUSHA_KBN_MANI_CheckedChanged(object sender, EventArgs e)
        //{
        //    this.GyousyaKbnEnableControl(this.GYOUSHA_KBN_MANI.Checked);
        //}


        /// <summary>
        /// マニ記載業者のチェックを判別し、
        /// [マニあり側] もしくは [マニなし側]のどちらか一方を有効にする。
        /// True＝[マニあり側]有効、False＝[マニなし側]有効
        /// </summary>
        /// <param name="gyoushaKbn3Check"></param>
        //private void GyousyaKbnEnableControl(bool gyoushaKbn3Check)
        //{
        //    if (gyoushaKbn3Check)
        //    {
        //        //マニあり側
        //        //自社区分は常に非表示
        //        //this.GYOUSHA_JISHA_KBN.Enabled = true;
        //        this.GYOUSHA_HAISHUTSU_NIZUMI_GYOUSHA_KBN.Enabled = true;
        //        this.GYOUSHA_UNPAN_JUTAKUSHA_KAISHA_KBN.Enabled = true;
        //        this.GYOUSHA_SHOBUN_NIOROSHI_GYOUSHA_KBN.Enabled = true;

        //        //マニなし側
        //        this.GYOUSHA_UNPAN_KAISHA_KBN.Enabled = false;
        //        this.GYOUSHA_UNPAN_KAISHA_KBN.Checked = false;
        //        this.GYOUSHA_NIOROSHI_GYOUSHA_KBN.Enabled = false;
        //        this.GYOUSHA_NIOROSHI_GYOUSHA_KBN.Checked = false;
        //    }
        //    else
        //    {
        //        //マニあり側
        //        //this.GYOUSHA_JISHA_KBN.Enabled = false;
        //        this.GYOUSHA_HAISHUTSU_NIZUMI_GYOUSHA_KBN.Enabled = false;
        //        this.GYOUSHA_UNPAN_JUTAKUSHA_KAISHA_KBN.Enabled = false;
        //        this.GYOUSHA_SHOBUN_NIOROSHI_GYOUSHA_KBN.Enabled = false;
        //        //this.GYOUSHA_JISHA_KBN.Checked = false;
        //        this.GYOUSHA_HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = false;
        //        this.GYOUSHA_UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = false;
        //        this.GYOUSHA_SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = false;

        //        //マニなし側
        //        this.GYOUSHA_UNPAN_KAISHA_KBN.Enabled = true;
        //        this.GYOUSHA_NIOROSHI_GYOUSHA_KBN.Enabled = true;
        //    }
        //}




        /// <summary>
        /// 請求書式1変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_SHOSHIKI_KBN_TextChanged(object sender, EventArgs e)
        {
            // 請求書式明細区分の制限処理
            this.logic.LimitSeikyuuShoshikiMeisaiKbn();
        }

        /// <summary>
        /// 支払書式1変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHOSHIKI_KBN_TextChanged(object sender, EventArgs e)
        {
            // 支払書式明細区分の制限処理
            this.logic.LimitShiharaiShoshikiMeisaiKbn();
        }

        /// <summary>
        /// 指示書／依頼書印刷有無変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void UKETSUKE_SIJISHO_PRINT_KBN_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeUketsukeSijishoPrintKbn();
        }

        /// <summary>
        /// 入金区分表示数変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NYUUKIN_IKKATSU_KBN_DISP_SUU_TextChanged(object sender, EventArgs e)
        {
            if (this.NYUUKIN_IKKATSU_KBN_DISP_SUU.Text.Length > 0 && this.NYUUKIN_IKKATSU_KBN_DISP_SUU.Text.Equals("0"))
            {
                this.NYUUKIN_IKKATSU_KBN_DISP_SUU.Text = string.Empty;
            }
        }

        
        /// <summary>
        /// 荷降業者CD確定後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIOROSHI_GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            //this.logic.ChangeNioroshiGyoushaCD(((TextBox)sender).Text.ToString());
        }
        /// <summary>
        /// 荷積業者CD確定後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NITSUMI_GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            //this.logic.ChangeNitsumiGyoushaCD(((TextBox)sender).Text.ToString());
        }

        /// <summary>
        /// 請求税計算区分変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_ZEI_KEISAN_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            // 請求税区分の制限処理
            this.logic.LimitSeikyuuZeiKbn();
        }

        /// <summary>
        /// 支払税計算区分変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_ZEI_KEISAN_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            // 支払税区分の制限処理
            this.logic.LimitShiharaiZeiKbn();
        }

        /// <summary>
        /// 税区分/締処理利用形態区分変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SYS_ZEI_KEISAN_KBN_USE_KBN_TextChanged(object sender, EventArgs e)
        {
            //税区分利用形態が請求・伝票毎の場合
            if ("1".Equals(this.SYS_ZEI_KEISAN_KBN_USE_KBN.Text))
            {
                // システムタブ
                this.SYS_KAKUTEI__TANNI_KBN.Text = "1";
                this.SYS_KAKUTEI__TANNI_KBN.RangeSetting.Max = 1;
                this.rb_SYS_KAKUTEI__TANNI_KBN_2.Enabled = false;

                ////マスタの取引先-請求情報
                ////税計算区分の3.明細毎を非活性にする。
                //this.rb_SEIKYUU_ZEI_KEISAN_KBN_CD_3.Enabled = false;
                //this.rb_SEIKYUU_ZEI_KEISAN_KBN_CD_1.Enabled = true;
                //this.SEIKYUU_ZEI_KEISAN_KBN_CD.RangeSetting.Min = 1;
                //this.SEIKYUU_ZEI_KEISAN_KBN_CD.RangeSetting.Max = 2;
                //if ("3".Equals(this.SEIKYUU_ZEI_KEISAN_KBN_CD.Text))
                //{
                //    this.SEIKYUU_ZEI_KEISAN_KBN_CD.Text = "1";
                //}

                //マスタの取引先-支払情報
                //this.rb_SHIHARAI_ZEI_KEISAN_KBN_CD_3.Enabled = false;
                //this.rb_SHIHARAI_ZEI_KEISAN_KBN_CD_1.Enabled = true;
                //this.SHIHARAI_ZEI_KEISAN_KBN_CD.RangeSetting.Min = 1;
                //this.SHIHARAI_ZEI_KEISAN_KBN_CD.RangeSetting.Max = 2;
                //if ("3".Equals(this.SHIHARAI_ZEI_KEISAN_KBN_CD.Text))
                //{
                //    this.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = "1";
                //}
            }
            else
            //税区分利用形態が請求・明細毎の場合
            {
                // システムタブ
                this.SYS_KAKUTEI__TANNI_KBN.RangeSetting.Max = 2;
                this.rb_SYS_KAKUTEI__TANNI_KBN_2.Enabled = true;

                ////マスタの取引先-請求情報
                //this.rb_SEIKYUU_ZEI_KEISAN_KBN_CD_3.Enabled = true;
                //this.rb_SEIKYUU_ZEI_KEISAN_KBN_CD_1.Enabled = false;
                //this.SEIKYUU_ZEI_KEISAN_KBN_CD.RangeSetting.Min = 2;
                //this.SEIKYUU_ZEI_KEISAN_KBN_CD.RangeSetting.Max = 3;
                //if ("1".Equals(this.SEIKYUU_ZEI_KEISAN_KBN_CD.Text))
                //{
                //    this.SEIKYUU_ZEI_KEISAN_KBN_CD.Text = "2";
                //}

                ////マスタの取引先-支払情報
                //this.rb_SHIHARAI_ZEI_KEISAN_KBN_CD_3.Enabled = true;
                //this.rb_SHIHARAI_ZEI_KEISAN_KBN_CD_1.Enabled = false;
                //this.SHIHARAI_ZEI_KEISAN_KBN_CD.RangeSetting.Min = 2;
                //this.SHIHARAI_ZEI_KEISAN_KBN_CD.RangeSetting.Max = 3;
                //if ("1".Equals(this.SHIHARAI_ZEI_KEISAN_KBN_CD.Text))
                //{
                //    this.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = "2";
                //}

            }

#warning 暫定対応 不具合管理表 No.2050～2053対応
            #region 不具合管理表 No.2050～2053の暫定対応
            // UnresolvedMergeConflict 不具合管理表 No.2050～2053の暫定対応箇所

            // 不具合管理表 No.2050～2053対応
            // 暫定対応解除時は、下記ソースを削除するだけ
            this.SYS_KAKUTEI__TANNI_KBN.RangeSetting.Max = 1;
            this.SYS_KAKUTEI__TANNI_KBN.LinkedRadioButtonArray = new string[] { "rb_SYS_KAKUTEI__TANNI_KBN_1" };
            this.SYS_KAKUTEI__TANNI_KBN.Tag = "【1】で入力してください";

            this.rb_SYS_KAKUTEI__TANNI_KBN_2.Enabled = false;
            #endregion

            //税計算区分を再設定する
            this.logic.ChangeSeikyuuTorihikiKbn();
            this.logic.ChangeShiharaiTorihikiKbn();
        }

        /// <summary>
        /// 請求書拠点の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_KYOTEN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.SeikyuuKyotenCdValidated())
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 支払書拠点の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_KYOTEN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.ShiharaiKyotenCdValidated())
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 業者請求書拠点の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_SEIKYUU_KYOTEN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.GyoushaSeikyuuKyotenCdValidated())
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 業者支払書拠点の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_SHIHARAI_KYOTEN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.GyoushaShiharaiKyotenCdValidated())
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 現場請求書拠点の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_SEIKYUU_KYOTEN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.GenbaSeikyuuKyotenCdValidated())
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 現場支払書拠点の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_SHIHARAI_KYOTEN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.GenbaShiharaiKyotenCdValidated())
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 支払 確定解除使用の変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_KAKUTEI_USE_KBN_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeShiharaiKakuteiUseKbn();
        }


        /// <summary>
        /// 支払 確定解除パスワード（確認）のチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_KAKUTEI_KAIJO_CONFIRM_PASSWORD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.ShiharaiKakuteiPasswordValidate(true))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 支払 確定解除新パスワード（新確認）のチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.ShiharaiKakuteiConfirmNewPasswordValidate(true, false))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 支払 確定解除新パスワード（新）のチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_KAKUTEI_KAIJO_NEW_PASSWORD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (!this.logic.ShiharaiKakuteiNewPasswordValidate(false, false))
            {
                e.Cancel = true;
            }
        }


        /// <summary>
        /// 売上 確定入力の設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>   
        private void URIAGE_KAKUTEI_USE_KBN_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeUriageKakuteiUseKbn();
        }

        /// <summary>
        /// 売上 確定解除パスワード（確認）のチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void URIAGE_KAKUTEI_KAIJO_CONFIRM_PASSWORD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.UriageKakuteiPasswordValidate(true))
            {
                e.Cancel = true;
            }

        }

        /// <summary>
        /// 売上 確定解除新パスワード（新確認）のチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void URIAGE_KAKUTEI_KAIJO_CONFIRM_NEW_PASSWORD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.UriageKakuteiConfirmNewPasswordValidate(true, false))
            {
                e.Cancel = true;
            }

        }

        /// <summary>
        /// 売上 確定解除新パスワード（新）のチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void URIAGE_KAKUTEI_KAIJO_NEW_PASSWORD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.UriageKakuteiNewPasswordValidate(false, false))
            {
                e.Cancel = true;
            }

        }


        /// <summary>
        /// 積み替え保管 のチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_TSUMIKAEHOKAN_KBN_CheckedChanged(object sender, EventArgs e)
        {
            if (GENBA_TSUMIKAEHOKAN_KBN.Checked)
            {
                // Checked
                GENBA_SHOBUN_NIOROSHI_GENBA_KBN.Checked = false;
                GENBA_SAISHUU_SHOBUNJOU_KBN.Checked = false;
                // Enabled
                GENBA_SHOBUN_NIOROSHI_GENBA_KBN.Enabled = false;
                GENBA_SAISHUU_SHOBUNJOU_KBN.Enabled = false;
            }
            else
            {
                // Checked
                GENBA_SHOBUN_NIOROSHI_GENBA_KBN.Checked = false;
                GENBA_SAISHUU_SHOBUNJOU_KBN.Checked = false;
                // Enabled
                GENBA_SHOBUN_NIOROSHI_GENBA_KBN.Enabled = true;
                GENBA_SAISHUU_SHOBUNJOU_KBN.Enabled = true;
            }

        }


        /// <summary>
        /// 処分事業場 のチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_SHOBUN_JIGYOUJOU_KBN_CheckedChanged(object sender, EventArgs e)
        {
            if (GENBA_SHOBUN_NIOROSHI_GENBA_KBN.Checked)
            {
                // Checked
                GENBA_TSUMIKAEHOKAN_KBN.Checked = false;
                // Enabled
                GENBA_TSUMIKAEHOKAN_KBN.Enabled = false;
            }
            else if (!GENBA_SAISHUU_SHOBUNJOU_KBN.Checked)
            {
                // Checked
                GENBA_TSUMIKAEHOKAN_KBN.Checked = false;
                // Enabled
                GENBA_TSUMIKAEHOKAN_KBN.Enabled = true;
            }
        }


        /// <summary>
        /// 最終処理場 のチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_SAISHUU_SHOBUNJOU_KBN_CheckedChanged(object sender, EventArgs e)
        {
            if (GENBA_SAISHUU_SHOBUNJOU_KBN.Checked)
            {
                // Checked
                GENBA_TSUMIKAEHOKAN_KBN.Checked = false;
                // Enabled
                GENBA_TSUMIKAEHOKAN_KBN.Enabled = false;
            }
            else if (!GENBA_SHOBUN_NIOROSHI_GENBA_KBN.Checked)
            {
                // Checked
                GENBA_TSUMIKAEHOKAN_KBN.Checked = false;
                // Enabled
                GENBA_TSUMIKAEHOKAN_KBN.Enabled = true;
            }
        }

        /// <summary>
        /// 請求取引区分の変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void SEIKYUU_TORIHIKI_KBN_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeSeikyuuTorihikiKbn();
        }

        /// <summary>
        /// 支払取引区分の変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void SHIHARAI_TORIHIKI_KBN_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeShiharaiTorihikiKbn();
        }

        /// <summary>
        /// TAB 選択の変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void main_tab_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            // 支払 tab
            if (e.TabPage.Name.Equals("siharai_tabpage"))
            {
                if (!this.logic.ShiharaiKakuteiPasswordValidate(false))
                {
                    e.Cancel = true;
                    return;
                }
                if (!this.logic.ShiharaiKakuteiConfirmNewPasswordValidate(false, true))
                {
                    e.Cancel = true;
                    return;
                }
            }
            // 売上 tab
            if (e.TabPage.Name.Equals("uriage_tabpage"))
            {
                if (!this.logic.UriageKakuteiPasswordValidate(false))
                {
                    e.Cancel = true;
                    return;
                }
                if (!this.logic.UriageKakuteiConfirmNewPasswordValidate(false, true))
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void kobetu_tabpage_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 荷積現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NITSUMI_GENBA_CD_TextChanged(object sender, EventArgs e)
        {
            //      this.logic.ChangeNitsumiGenbaCD();
        }
        /// <summary>
        /// 荷降現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_TextChanged(object sender, EventArgs e)
        {
            //    this.logic.ChangeNioroshiGenbaCD();
        }
        
        /// <summary>
        /// 荷積現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NITSUMI_GENBA_CD_Validated(object sender, EventArgs e)
        {
            //this.logic.ChangeNitsumiGenbaCD();
        }
        /// <summary>
        /// 荷降現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_Validated(object sender, EventArgs e)
        {
            //this.logic.ChangeNioroshiGenbaCD();
        }
        

        /// <summary>
        /// 品名税区分ポップアップ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HINMEI_ZEI_KBN_CD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                form.table = this.HinmeiZeiKbnTable;
                //form.title = "税区分検索"; 
                //form.headerList = new List<string>();
                //form.headerList.Add("税区分CD");
                //form.headerList.Add("税区分名");
                form.PopupTitleLabel = "税区分検索";
                form.PopupGetMasterField = "CD,VALUE";
                form.PopupDataHeaderTitle = new string[] { "税区分CD", "税区分名" };
                form.ShowDialog();
                if (form.ReturnParams != null)
                {
                    this.HINMEI_ZEI_KBN_CD.Text = form.ReturnParams[0][0].Value.ToString();
                    this.HINMEI_ZEI_KBN_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                }
            }
        }

        /// <summary>
        /// 品名税区分テーブル設定
        /// </summary>
        private bool SetHinmeiZeiKbnTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CD", typeof(string));
                dt.Columns.Add("VALUE", typeof(string));
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                DataRow row;
                row = dt.NewRow();
                row["CD"] = "1";
                row["VALUE"] = "外税";
                dt.Rows.Add(row);
                row = dt.NewRow();
                row["CD"] = "2";
                row["VALUE"] = "内税";
                dt.Rows.Add(row);
                row = dt.NewRow();
                row["CD"] = "3";
                row["VALUE"] = "非課税";
                dt.Rows.Add(row);

                this.HinmeiZeiKbnTable = dt;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHinmeiZeiKbnTable", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 品名税区分検証時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HINMEI_ZEI_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            this.HINMEI_ZEI_KBN_NAME.Text = string.Empty;
            var rows = this.HinmeiZeiKbnTable.Select(string.Format("CD = '{0}'", HINMEI_ZEI_KBN_CD.Text));
            if (rows.Length > 0)
            {
                this.HINMEI_ZEI_KBN_NAME.Text = Convert.ToString(rows[0]["VALUE"]);
            }
            else
            {
                // 有り得ないと思うが
                this.HINMEI_ZEI_KBN_CD.Text = string.Empty;
                this.HINMEI_ZEI_KBN_NAME.Text = string.Empty;
            }
        }

        // No.4089-->
        /// <summary>
        /// 「確定区分を表示」変更(受入)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextChanged_UKEIRE_KAKUTEI_USE_KBN(object sender, EventArgs e)
        {
            //20151015 hoanghm #13316 start
            //if (SystemSetteiHoshuConstans.KAKUTEI_RIYOU_ON.ToString().Equals(UKEIRE_KAKUTEI_USE_KBN.Text))
            //{
            //    UKEIRE_KAKUTEI_FLAG.Enabled = true;
            //    rb_UKEIRE_KAKUTEI_FLAG_1.Enabled = true;
            //    rb_UKEIRE_KAKUTEI_FLAG_2.Enabled = true;
            //}
            //else
            //{
            //    UKEIRE_KAKUTEI_FLAG.Enabled = false;
            //    rb_UKEIRE_KAKUTEI_FLAG_1.Enabled = false;
            //    rb_UKEIRE_KAKUTEI_FLAG_2.Enabled = false;
            //}
            //20151015 hoanghm #13316 end
        }

        /// <summary>
        /// 「確定区分を表示」変更(出荷)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextChanged_SHUKKA_KAKUTEI_USE_KBN(object sender, EventArgs e)
        {
            //20151015 hoanghm #13316 start
            //if (SystemSetteiHoshuConstans.KAKUTEI_RIYOU_ON.ToString().Equals(SHUKKA_KAKUTEI_USE_KBN.Text))
            //{
            //    SHUKKA_KAKUTEI_FLAG.Enabled = true;
            //    rb_SHUKKA_KAKUTEI_FLAG_1.Enabled = true;
            //    rb_SHUKKA_KAKUTEI_FLAG_2.Enabled = true;
            //}
            //else
            //{
            //    SHUKKA_KAKUTEI_FLAG.Enabled = false;
            //    rb_SHUKKA_KAKUTEI_FLAG_1.Enabled = false;
            //    rb_SHUKKA_KAKUTEI_FLAG_2.Enabled = false;
            //}
            //20151015 hoanghm #13316 end
        }

        /// <summary>
        /// 「確定区分を表示」変更(売上支払)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextChanged_UR_SH_KAKUTEI_USE_KBN(object sender, EventArgs e)
        {
            //20151015 hoanghm #13316 start
            //if (SystemSetteiHoshuConstans.KAKUTEI_RIYOU_ON.ToString().Equals(UR_SH_KAKUTEI_USE_KBN.Text))
            //{
            //    UR_SH_KAKUTEI_FLAG.Enabled = true;
            //    rb_UR_SH_KAKUTEI_FLAG_1.Enabled = true;
            //    rb_UR_SH_KAKUTEI_FLAG_2.Enabled = true;
            //}
            //else
            //{
            //    UR_SH_KAKUTEI_FLAG.Enabled = false;
            //    rb_UR_SH_KAKUTEI_FLAG_1.Enabled = false;
            //    rb_UR_SH_KAKUTEI_FLAG_2.Enabled = false;
            //}
            //20151015 hoanghm #13316 end
        }
        // No.4089<--

        /// <summary>
        /// 印字拠点ＣＤ１のバリデーティングイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void INJI_KYOTEN_CD1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool catchErr = false;
            this.INJI_KYOTEN_NAME_RYAKU1.Text = this.logic.SearchKyotenName(this.INJI_KYOTEN_CD1.Text, e, out catchErr);
        }

        /// <summary>
        /// 印字拠点ＣＤ２のバリデーティングイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void INJI_KYOTEN_CD2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool catchErr = false;
            this.INJI_KYOTEN_NAME_RYAKU2.Text = this.logic.SearchKyotenName(this.INJI_KYOTEN_CD2.Text, e, out catchErr);
        }

        /// <summary>
        /// 見積税計算区分変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MITSUMORI_ZEIKEISAN_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            // 見積税区分の制限処理
            this.logic.LimitMitsumoriZeiKbn();
        }

        private void ITAKU_KEIYAKU_CHECK_TextChanged(object sender, EventArgs e)
        {
            if (this.ITAKU_KEIYAKU_CHECK.Text == "1")
            {
                this.pl_ITAKU_KEIYAKU_ALERT_AUTH.Enabled = true;
            }
            else
            {
                this.pl_ITAKU_KEIYAKU_ALERT_AUTH.Enabled = false;
            }
        }

        /// <summary>
        /// 「代納確定を利用」変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DAINO_KAKUTEI_USE_KBN_TextChanged(object sender, EventArgs e)
        {
            if (SystemSetteiHoshuConstans.KAKUTEI_RIYOU_ON.ToString().Equals(DAINO_KAKUTEI_USE_KBN.Text))
            {
                DAINO_KAKUTEI_FLAG.Enabled = true;
                rb_DAINO_KAKUTEI_FLAG_1.Enabled = true;
                rb_DAINO_KAKUTEI_FLAG_2.Enabled = true;
            }
            else
            {
                DAINO_KAKUTEI_FLAG.Enabled = false;
                rb_DAINO_KAKUTEI_FLAG_1.Enabled = false;
                rb_DAINO_KAKUTEI_FLAG_2.Enabled = false;
            }
        }

        private void NYUUKIN_HANDAN_BEGIN_Leave(object sender, EventArgs e)
        {

        }

        private void NYUUKIN_HANDAN_BEGIN_Leave_1(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 計量票レイアウトTextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIRYOU_LAYOUT_KBN_TextChanged(object sender, EventArgs e)
        {
            if ("1".Equals(this.KEIRYOU_LAYOUT_KBN.Text))
            {
                this.KEIRYOU_GOODS_KBN.Text = "2";
                this.rb_KEIRYOU_GOODS_KBN_1.Enabled = false;
            }
            else
            {
                this.rb_KEIRYOU_GOODS_KBN_1.Enabled = true;
            }
        }

        /// <summary>
        /// 計量票表示区分TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIRYOU_TORIHIKISAKI_DISP_KBN_TextChanged(object sender, EventArgs e)
        {
            if ("1".Equals(this.KEIRYOU_TORIHIKISAKI_DISP_KBN.Text))
            {
                this.KEIRYOU_LAYOUT_KBN.Text = "2";
                this.rb_KEIRYOU_LAYOUT_KBN_1.Enabled = false;
            }
            else
            {
                this.rb_KEIRYOU_LAYOUT_KBN_1.Enabled = true;
            }
        }

        /// <summary>
        /// 形態区分CD(受入)のバリデーティングイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIRYOU_UKEIRE_KEITAI_KBN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.UkeireKeitaiKbnCdValidated())
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 形態区分CD(出荷)のバリデーティングイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIRYOU_SHUKKA_KEITAI_KBN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.ShukkaKeitaiKbnCdValidated())
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 計量区分テーブル設定
        /// </summary>
        private bool SetKeiryouKbnTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CD", typeof(string));
                dt.Columns.Add("VALUE", typeof(string));
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                DataRow row;
                row = dt.NewRow();
                row["CD"] = "1";
                row["VALUE"] = "通常登録";
                dt.Rows.Add(row);
                row = dt.NewRow();
                row["CD"] = "2";
                row["VALUE"] = "仮登録";
                dt.Rows.Add(row);
                row = dt.NewRow();
                row["CD"] = "3";
                row["VALUE"] = "計上";
                dt.Rows.Add(row);

                this.KeiryouKbnTable = dt;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetKeiryouKbnTable", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 計量区分(受入)ポップアップ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIRYOU_UKEIRE_KEIRYOU_KBN_CD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                form.table = this.KeiryouKbnTable;
                form.PopupTitleLabel = "計量区分検索";
                form.PopupGetMasterField = "CD,VALUE";
                form.PopupDataHeaderTitle = new string[] { "計量区分CD", "計量区分名" };
                form.ShowDialog();
                if (form.ReturnParams != null)
                {
                    this.KEIRYOU_UKEIRE_KEIRYOU_KBN_CD.Text = form.ReturnParams[0][0].Value.ToString();
                    this.KEIRYOU_UKEIRE_KEIRYOU_KBN_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                }
            }
        }

        /// <summary>
        /// 計量区分(受入)検証時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIRYOU_UKEIRE_KEIRYOU_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            this.KEIRYOU_UKEIRE_KEIRYOU_KBN_NAME.Text = string.Empty;
            var rows = this.KeiryouKbnTable.Select(string.Format("CD = '{0}'", KEIRYOU_UKEIRE_KEIRYOU_KBN_CD.Text));
            if (rows.Length > 0)
            {
                this.KEIRYOU_UKEIRE_KEIRYOU_KBN_NAME.Text = Convert.ToString(rows[0]["VALUE"]);
            }
            else
            {
                // 有り得ないと思うが
                this.KEIRYOU_UKEIRE_KEIRYOU_KBN_CD.Text = string.Empty;
                this.KEIRYOU_UKEIRE_KEIRYOU_KBN_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 計量区分(出荷)ポップアップ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIRYOU_SHUKKA_KEIRYOU_KBN_CD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                form.table = this.KeiryouKbnTable;
                form.PopupTitleLabel = "計量区分検索";
                form.PopupGetMasterField = "CD,VALUE";
                form.PopupDataHeaderTitle = new string[] { "計量区分CD", "計量区分名" };
                form.ShowDialog();
                if (form.ReturnParams != null)
                {
                    this.KEIRYOU_SHUKKA_KEIRYOU_KBN_CD.Text = form.ReturnParams[0][0].Value.ToString();
                    this.KEIRYOU_SHUKKA_KEIRYOU_KBN_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                }
            }
        }

        /// <summary>
        /// 計量区分(出荷)検証時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIRYOU_SHUKKA_KEIRYOU_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            this.KEIRYOU_SHUKKA_KEIRYOU_KBN_NAME.Text = string.Empty;
            var rows = this.KeiryouKbnTable.Select(string.Format("CD = '{0}'", KEIRYOU_SHUKKA_KEIRYOU_KBN_CD.Text));
            if (rows.Length > 0)
            {
                this.KEIRYOU_SHUKKA_KEIRYOU_KBN_NAME.Text = Convert.ToString(rows[0]["VALUE"]);
            }
            else
            {
                // 有り得ないと思うが
                this.KEIRYOU_SHUKKA_KEIRYOU_KBN_CD.Text = string.Empty;
                this.KEIRYOU_SHUKKA_KEIRYOU_KBN_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 計量タブ：請求取引区分の変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIRYOU_SEIKYUU_TORIHIKI_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeKeiryouSeikyuuTorihikiKbn();
        }

        /// <summary>
        /// 計量タブ：支払取引区分の変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIRYOU_SHIHARAI_TORIHIKI_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeKeiryouShiharaiTorihikiKbn();
        }

        /// <summary>
        /// 計量タブ：請求税計算区分変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            // 請求税区分の制限処理
            this.logic.LimitKeiryouSeikyuuZeiKbn();
        }

        /// <summary>
        /// 計量タブ：支払税計算区分変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            // 支払税区分の制限処理
            this.logic.LimitKeiryouShiharaiZeiKbn();
        }

        /// <summary>
        /// 契約区分検索ポップアップ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_TEIKI_KEIYAKU_KBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                var form = new MasterKyoutsuPopupForm();
                form.table = this.KeiyakuKbnTable;
                form.PopupTitleLabel = "契約区分検索";
                form.PopupGetMasterField = "CD,VALUE";
                form.PopupDataHeaderTitle = new string[] { "契約区分CD", "契約区分" };
                form.ShowDialog();
                if (form.ReturnParams != null)
                {
                    this.GENBA_TEIKI_KEIYAKU_KBN.Text = form.ReturnParams[0][0].Value.ToString();
                    this.GENBA_TEIKI_KEIYAKU_KBN_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                }
            }
        }

        /// <summary>
        /// 契約区分テーブル設定
        /// </summary>
        private bool SetKeiyakuKbnTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CD", typeof(string));
                dt.Columns.Add("VALUE", typeof(string));
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                DataRow row;
                row = dt.NewRow();
                row["CD"] = "1";
                row["VALUE"] = "定期";
                dt.Rows.Add(row);
                row = dt.NewRow();
                row["CD"] = "2";
                row["VALUE"] = "単価";
                dt.Rows.Add(row);
                row = dt.NewRow();
                row["CD"] = "3";
                row["VALUE"] = "回収のみ";
                dt.Rows.Add(row);

                this.KeiyakuKbnTable = dt;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetKeiyakuKbnTable", ex);
                this.errmessage.MessageBoxShow("E245");
                return true;
            }
        }

        /// <summary>
        /// 契約区分検証時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_TEIKI_KEIYAKU_KBN_TextChanged(object sender, EventArgs e)
        {
            this.GENBA_TEIKI_KEIYAKU_KBN_NAME.Text = string.Empty;
            var rows = this.KeiyakuKbnTable.Select(string.Format("CD = '{0}'", GENBA_TEIKI_KEIYAKU_KBN.Text));
            if (rows.Length > 0)
            {
                this.GENBA_TEIKI_KEIYAKU_KBN_NAME.Text = Convert.ToString(rows[0]["VALUE"]);
            }
            else
            {
                this.GENBA_TEIKI_KEIYAKU_KBN.Text = string.Empty;
                this.GENBA_TEIKI_KEIYAKU_KBN_NAME.Text = string.Empty;
            }

            // 集計単位の入力制御
            if ("2".Equals(this.GENBA_TEIKI_KEIYAKU_KBN.Text))
            {
                // 「2.単価」選択時のみ、集計単位は入力可
                this.GENBA_TEIKI_KEIJYOU_KBN.Enabled = true;
            }
            else
            {
                this.GENBA_TEIKI_KEIJYOU_KBN.Text = string.Empty;
                this.GENBA_TEIKI_KEIJYOU_KBN_NAME.Text = string.Empty;
                this.GENBA_TEIKI_KEIJYOU_KBN.Enabled = false;
            }
        }

        /// <summary>
        /// 集計単位ポップアップ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_TEIKI_KEIJYOU_KBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                var form = new MasterKyoutsuPopupForm();
                form.table = this.KeijyouKbnTable;
                form.PopupTitleLabel = "集計単位";
                form.PopupGetMasterField = "CD,VALUE";
                form.PopupDataHeaderTitle = new string[] { "集計単位CD", "集計単位" };
                form.ShowDialog();
                if (form.ReturnParams != null)
                {
                    this.GENBA_TEIKI_KEIJYOU_KBN.Text = form.ReturnParams[0][0].Value.ToString();
                    this.GENBA_TEIKI_KEIJYOU_KBN_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                }

            }
        }

        /// <summary>
        /// 計上区分テーブル設定
        /// </summary>
        private bool SetKeijyouKbnTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CD", typeof(string));
                dt.Columns.Add("VALUE", typeof(string));
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                DataRow row;
                row = dt.NewRow();
                row["CD"] = "1";
                row["VALUE"] = "伝票";
                dt.Rows.Add(row);
                row = dt.NewRow();
                row["CD"] = "2";
                row["VALUE"] = "合算";
                dt.Rows.Add(row);
                row = dt.NewRow();

                this.KeijyouKbnTable = dt;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetKeijyouKbnTable", ex);
                this.errmessage.MessageBoxShow("E245");
                return true;
            }
        }

        /// <summary>
        /// 集計単位検証時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_TEIKI_KEIJYOU_KBN_TextChanged(object sender, EventArgs e)
        {
            this.GENBA_TEIKI_KEIJYOU_KBN_NAME.Text = string.Empty;
            var rows = this.KeiyakuKbnTable.Select(string.Format("CD = '{0}'", GENBA_TEIKI_KEIJYOU_KBN.Text));
            if (rows.Length > 0)
            {
                this.GENBA_TEIKI_KEIJYOU_KBN_NAME.Text = Convert.ToString(rows[0]["VALUE"]);
            }
            else
            {
                this.GENBA_TEIKI_KEIJYOU_KBN.Text = string.Empty;
                this.GENBA_TEIKI_KEIJYOU_KBN_NAME.Text = string.Empty;
            }
        }

        private void TORIHIKISAKI_KEISHOU1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.TORIHIKISAKI_KEISHOU1.DroppedDown = false;
        }

        private void TORIHIKISAKI_KEISHOU2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.TORIHIKISAKI_KEISHOU2.DroppedDown = false;
        }

        private void TORIHIKISAKI_KEISHOU1_TextChanged(object sender, EventArgs e)
        {
            this.TORIHIKISAKI_KEISHOU1.DroppedDown = false;
        }

        private void TORIHIKISAKI_KEISHOU2_TextChanged(object sender, EventArgs e)
        {
            this.TORIHIKISAKI_KEISHOU2.DroppedDown = false;
        }

        private void GYOUSHA_KEISHOU1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.GYOUSHA_KEISHOU1.DroppedDown = false;
        }

        private void GYOUSHA_KEISHOU2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.GYOUSHA_KEISHOU2.DroppedDown = false;
        }

        private void GYOUSHA_KEISHOU1_TextChanged(object sender, EventArgs e)
        {
            this.GYOUSHA_KEISHOU1.DroppedDown = false;
        }

        private void GYOUSHA_KEISHOU2_TextChanged(object sender, EventArgs e)
        {
            this.GYOUSHA_KEISHOU2.DroppedDown = false;
        }

        private void GENBA_KEISHOU1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.GENBA_KEISHOU1.DroppedDown = false;
        }

        private void GENBA_KEISHOU2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.GENBA_KEISHOU2.DroppedDown = false;
        }

        private void GENBA_KEISHOU1_TextChanged(object sender, EventArgs e)
        {
            this.GENBA_KEISHOU1.DroppedDown = false;
        }

        private void GENBA_KEISHOU2_TextChanged(object sender, EventArgs e)
        {
            this.GENBA_KEISHOU2.DroppedDown = false;
        }

        /// <summary>
        /// モバイル将軍のシステム設定起動パスワード（確認）のチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_PASSWORD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.MobilePasswordValidate(true))
            {
                e.Cancel = true;
            }

        }

        /// <summary>
        /// モバイル将軍のシステム設定起動パスワード（新確認）のチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MOBILE_SYSTEM_SETTEI_OPEN_CONFIRM_NEW_PASSWORD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.MobileConfirmNewPasswordValidate(true, false))
            {
                e.Cancel = true;
            }

        }

        /// <summary>
        /// モバイル将軍のシステム設定起動パスワード（新）のチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MOBILE_SYSTEM_SETTEI_OPEN_NEW_PASSWORD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.MobileNewPasswordValidate(false, false))
            {
                e.Cancel = true;
            }

        }

        //160028 S
        private void SHIHARAI_KAISHUU_BETSU_TextChanged(object sender, EventArgs e)
        {
            if (this.SHIHARAI_KAISHUU_BETSU.Text == "2")
            {
                this.SHIHARAI_MONTH.Text = string.Empty;
                this.pl_SHIHARAI_MONTH.Enabled = false;
                this.SHIHARAI_KAISHUU_BETSU_NICHIGO.BackColor = Constans.NOMAL_COLOR;
                this.SHIHARAI_KAISHUU_BETSU_NICHIGO.Enabled = true;
            }
            if (this.SHIHARAI_KAISHUU_BETSU.Text == "1")
            {
                this.SHIHARAI_MONTH.Text = "1";
                this.pl_SHIHARAI_MONTH.Enabled = true;
                this.SHIHARAI_KAISHUU_BETSU_NICHIGO.BackColor = Constans.NOMAL_COLOR;
                this.SHIHARAI_KAISHUU_BETSU_NICHIGO.Text = string.Empty;
                this.SHIHARAI_KAISHUU_BETSU_NICHIGO.Enabled = false;
            }
        }

        private void SEIKYUU_KAISHUU_BETSU_TextChanged(object sender, EventArgs e)
        {

            if (this.SEIKYUU_KAISHUU_BETSU.Text == "2")
            {
                this.SEIKYUU_KAISHUU_MONTH.Text = string.Empty;
                this.pl_SEIKYUU_KAISHUU_MONTH.Enabled = false;
                this.SEIKYUU_KAISHUU_BETSU_NICHIGO.BackColor = Constans.NOMAL_COLOR;
                this.SEIKYUU_KAISHUU_BETSU_NICHIGO.Enabled = true;
            }
            if (this.SEIKYUU_KAISHUU_BETSU.Text == "1")
            {
                this.SEIKYUU_KAISHUU_MONTH.Text = "1";
                this.pl_SEIKYUU_KAISHUU_MONTH.Enabled = true;
                this.SEIKYUU_KAISHUU_BETSU_NICHIGO.BackColor = Constans.NOMAL_COLOR;
                this.SEIKYUU_KAISHUU_BETSU_NICHIGO.Text = string.Empty;
                this.SEIKYUU_KAISHUU_BETSU_NICHIGO.Enabled = false;
            }
        }
        //160028 E

        //PhuocLoc 2022/01/04 #158897, #158898 -Start
        private void WAN_SIGN_FIELD_1_TextChanged(object sender, EventArgs e)
        {
            if (this.beforeValue1 != this.WAN_SIGN_FIELD_1.Text)
            {
                if (this.WAN_SIGN_FIELD_1.Text == this.isUsing)
                {
                    this.logic.FieldControlSetting(1, this.WAN_SIGN_FIELD_1.Text);
                }
                else if (this.WAN_SIGN_FIELD_1.Text == this.isNotUsing)
                {
                    var dialogResult = this.errmessage.MessageBoxShow("C128", MessageBoxDefaultButton.Button2, "2", "5");
                    if (dialogResult == System.Windows.Forms.DialogResult.OK
                        || dialogResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.logic.FieldControlSetting(1, this.WAN_SIGN_FIELD_1.Text);
                    }
                    else
                    {
                        this.WAN_SIGN_FIELD_1.Text = this.isUsing;
                    }
                }
                this.beforeValue1 = this.WAN_SIGN_FIELD_1.Text;
            }
        }

        private void WAN_SIGN_FIELD_2_TextChanged(object sender, EventArgs e)
        {
            if (this.beforeValue2 != this.WAN_SIGN_FIELD_2.Text)
            {
                if (this.WAN_SIGN_FIELD_2.Text == this.isUsing)
                {
                    this.logic.FieldControlSetting(2, this.WAN_SIGN_FIELD_2.Text);
                }
                else if (this.WAN_SIGN_FIELD_2.Text == this.isNotUsing)
                {
                    var dialogResult = this.errmessage.MessageBoxShow("C128", MessageBoxDefaultButton.Button2, "3", "5");
                    if (dialogResult == System.Windows.Forms.DialogResult.OK
                        || dialogResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.logic.FieldControlSetting(2, this.WAN_SIGN_FIELD_2.Text);
                    }
                    else
                    {
                        this.WAN_SIGN_FIELD_2.Text = this.isUsing;
                    }
                }
                this.beforeValue2 = this.WAN_SIGN_FIELD_2.Text;
            }
        }

        private void WAN_SIGN_FIELD_3_TextChanged(object sender, EventArgs e)
        {
            if (this.beforeValue3 != this.WAN_SIGN_FIELD_3.Text)
            {
                if (this.WAN_SIGN_FIELD_3.Text == this.isUsing)
                {
                    this.logic.FieldControlSetting(3, this.WAN_SIGN_FIELD_3.Text);
                }
                else if (this.WAN_SIGN_FIELD_3.Text == this.isNotUsing)
                {
                    var dialogResult = this.errmessage.MessageBoxShow("C129", MessageBoxDefaultButton.Button2, "4", "5");
                    if (dialogResult == System.Windows.Forms.DialogResult.OK
                        || dialogResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.logic.FieldControlSetting(3, this.WAN_SIGN_FIELD_3.Text);
                    }
                    else
                    {
                        this.WAN_SIGN_FIELD_3.Text = this.isUsing;
                    }
                }
                this.beforeValue3 = this.WAN_SIGN_FIELD_3.Text;
            }
        }

        private void WAN_SIGN_FIELD_4_TextChanged(object sender, EventArgs e)
        {
            if (this.beforeValue4 != this.WAN_SIGN_FIELD_4.Text)
            {
                if (this.WAN_SIGN_FIELD_4.Text == this.isUsing)
                {
                    this.logic.FieldControlSetting(4, this.WAN_SIGN_FIELD_4.Text);
                }
                else if (this.WAN_SIGN_FIELD_4.Text == this.isNotUsing)
                {
                    var dialogResult = this.errmessage.MessageBoxShow("C130", MessageBoxDefaultButton.Button2, "5");
                    if (dialogResult == System.Windows.Forms.DialogResult.OK
                        || dialogResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.logic.FieldControlSetting(4, this.WAN_SIGN_FIELD_4.Text);
                    }
                    else
                    {
                        this.WAN_SIGN_FIELD_4.Text = this.isUsing;
                    }
                }
                this.beforeValue4 = this.WAN_SIGN_FIELD_4.Text;
            }
        }

        private void WAN_SIGN_FIELD_5_TextChanged(object sender, EventArgs e)
        {
            this.logic.FieldControlSetting(5, this.WAN_SIGN_FIELD_5.Text);
        }
        //PhuocLoc 2022/01/04 #158897, #158898 -End

        private void SMS_DENPYOU_SHURUI_TextChanged(object sender, EventArgs e)
        {
            // 伝票種類の値より、各項目設定
            this.logic.DenpyouShuruiSetting();
        }

        private void SMS_SEND_JOKYO_TextChanged(object sender, EventArgs e)
        {
            // 送信状況の値より、各項目設定
            this.logic.SmsStatusSetting();
        }
        /// <summary>
        /// 参照ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void FileRefClick(object sender, EventArgs e)
        {
            this.logic.FileRefClick();
        }

        /// <summary>
        /// アップロードボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UploadClick(object sender, EventArgs e)
        {
            this.logic.UploadClick();
        }

        /// <summary>
        /// 閲覧ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void BrowseClick(object sender, EventArgs e)
        {
            this.logic.BrowseClick();
        }
        /// <summary>
        /// 削除ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DeleteClick(object sender, EventArgs e)
        {
            this.logic.DeleteClick();
        }

        //20250304
        private void KEIRYO_ORIJINARU_CheckedChanged(object sender, EventArgs e)
        {
            if (this.KEIRYO_ORIJINARU.Checked)
            {
                this.KEIRYOU_TORIHIKISAKI_DISP_KBN.ReadOnly = true;
                this.rb_KEIRYOU_TORIHIKISAKI_DISP_KBN_1.Enabled = false;
                this.rb_KEIRYOU_TORIHIKISAKI_DISP_KBN_2.Enabled = false;

                this.KEIRYOU_LAYOUT_KBN.ReadOnly = true;
                this.rb_KEIRYOU_LAYOUT_KBN_1.Enabled = false;
                this.rb_KEIRYOU_LAYOUT_KBN_2.Enabled = false;

                this.KEIRYOU_GOODS_KBN.ReadOnly = true;
                this.rb_KEIRYOU_GOODS_KBN_1.Enabled = false;
                this.rb_KEIRYOU_GOODS_KBN_2.Enabled = false;
            }
            else
            {
                this.KEIRYOU_TORIHIKISAKI_DISP_KBN.ReadOnly = false;
                this.rb_KEIRYOU_TORIHIKISAKI_DISP_KBN_1.Enabled = true;
                this.rb_KEIRYOU_TORIHIKISAKI_DISP_KBN_2.Enabled = true;

                this.KEIRYOU_LAYOUT_KBN.ReadOnly = false;
                this.rb_KEIRYOU_LAYOUT_KBN_1.Enabled = true;
                this.rb_KEIRYOU_LAYOUT_KBN_2.Enabled = true;

                this.KEIRYOU_GOODS_KBN.ReadOnly = false;
                this.rb_KEIRYOU_GOODS_KBN_1.Enabled = true;
                this.rb_KEIRYOU_GOODS_KBN_2.Enabled = true;
            }
        }

        private void rb_KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD_2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rb_KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD_2.Checked)
            {
                this.rb_KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD_1.Enabled = false;
                this.rb_KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD_3.Enabled = false;
            }
            else
            {
                this.rb_KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD_1.Enabled = true;
                this.rb_KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD_3.Enabled = true;
            }
        }

        private void rb_KEIRYOU_SEIKYUU_ZEI_KBN_CD_1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rb_KEIRYOU_SEIKYUU_ZEI_KBN_CD_1.Checked)
            {
                this.rb_KEIRYOU_SEIKYUU_ZEI_KBN_CD_2.Enabled = false;
                this.rb_KEIRYOU_SEIKYUU_ZEI_KBN_CD_3.Enabled = false;
            }
            else
            {
                this.rb_KEIRYOU_SEIKYUU_ZEI_KBN_CD_2.Enabled = true;
                this.rb_KEIRYOU_SEIKYUU_ZEI_KBN_CD_3.Enabled = true;
            }
        }

        //20250305
        internal void FURIKOMI_BANK_SHITEN_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var bankCd = this.FURIKOMI_BANK_CD.Text;
            var bankShitenCd = this.FURIKOMI_BANK_SHITEN_CD.Text;

            //20150706 #2124 銀行名がブランクの場合はアラートを表示させるようにする。hoanghm start
            if (String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var message = new MessageBoxShowLogic();
                message.MessageBoxShow("E012", "銀行");
                this.FURIKOMI_BANK_SHITEN_CD.Focus();
                return;
            }
            if (this.previousBankShitenCd != bankShitenCd)
            {
                this.isBankShitenPopup = false;
            }
            //20150706 #2124 銀行名がブランクの場合はアラートを表示させるようにする。hoanghm end

            bool catchErr = false;
            if (!String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var bankShitenList = this.logic.GetBankShiten(bankCd, bankShitenCd, out catchErr);
                if (catchErr)
                {
                    return;
                }
                if (bankShitenList.Count == 0)
                {
                    // 該当なしなのでエラー
                    var message = new MessageBoxShowLogic();
                    message.MessageBoxShow("E011", "銀行支店マスタ");

                    this.FURIKOMI_BANK_SHITEN_NAME.Text = String.Empty;
                    this.KOUZA_SHURUI.Text = String.Empty;
                    this.KOUZA_NO.Text = String.Empty;
                    this.KOUZA_NAME.Text = String.Empty;

                    this.previousBankShitenCd = String.Empty;

                    this.isBankShitenPopup = false;

                    e.Cancel = true;
                }
                else if (bankShitenList.Count == 1)
                {
                    // 1件該当なので値をセット
                    var bankShiten = bankShitenList.Where(b => b.BANK_CD == bankCd && b.BANK_SHITEN_CD == bankShitenCd).DefaultIfEmpty(new M_BANK_SHITEN()).FirstOrDefault();
                    this.FURIKOMI_BANK_SHITEN_NAME.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                    this.KOUZA_SHURUI.Text = bankShiten.KOUZA_SHURUI;
                    this.KOUZA_NO.Text = bankShiten.KOUZA_NO;
                    this.KOUZA_NAME.Text = bankShiten.KOUZA_NAME;

                    this.previousBankShitenCd = bankShitenCd;

                    this.isBankShitenPopup = false;
                }
                else if (bankShitenList.Count > 1)
                {
                    if (false == this.isBankShitenPopup && this.previousBankShitenCd != bankShitenCd)
                    {
                        // 複数該当なのでポップアップ表示
                        CustomControlExtLogic.PopUp(this.FURIKOMI_BANK_SHITEN_CD);
                        this.isBankShitenPopup = true;

                        e.Cancel = true;
                    }
                    else
                    {
                        this.isBankShitenPopup = false;
                    }

                    this.previousBankShitenCd = bankShitenCd;
                }
            }
            else
            {
                this.FURIKOMI_BANK_SHITEN_NAME.Text = String.Empty;
                this.KOUZA_SHURUI.Text = String.Empty;
                this.KOUZA_NO.Text = String.Empty;
                this.KOUZA_NAME.Text = String.Empty;

                this.previousBankShitenCd = String.Empty;

                this.isBankShitenPopup = false;
            }

            LogUtility.DebugMethodEnd();
        }

        //20250310
        internal virtual void FurikomiBankCdValidated(object sender, EventArgs e)
        {
            this.logic.FurikomiBankCdValidated();
        }
    }
}
