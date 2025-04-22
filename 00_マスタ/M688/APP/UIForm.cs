using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Shougun.Core.Master.SystemKobetsuSetteiHoshu.Logic;

using r_framework.Dto;
using System.Collections.ObjectModel;

namespace Shougun.Core.Master.SystemKobetsuSetteiHoshu.APP
{
    public partial class UIForm : SuperForm
    {

        #region 画面ロジック
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicCls logic;
        public MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        //初期サイズ表示フラグ
        private bool InitialFlg = false;

        /// <summary>
        /// 前回荷降業者コード
        /// </summary>
        public string beforNioroshiGyousaCD = string.Empty;

        /// <summary>
        /// 前回荷積業者コード
        /// </summary>
        public string beforNitsumiGyousaCD = string.Empty;


        public UIForm()
            : base(WINDOW_ID.M_SYS_KOBETSU_INFO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {

            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicCls(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            // オプション未使用ならｼｮｰﾄﾒｯｾｰｼﾞタブも削除
            if (!AppConfig.AppOptions.IsSMS())
            {
                this.main_tab.TabPages.Remove(this.sms_tabpage);
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

        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            if (this.UPDATE_CSV_OUTPUT.Text == "1" && string.IsNullOrEmpty(this.CSV_OUTPUT_FILE_PATH.Text))
            {
                MessageBoxShowLogic msg = new MessageBoxShowLogic();
                msg.MessageBoxShow("E001", "CSV保管先");
                return;
            }

            if (!base.RegistErrorFlag)
            {
                if (!this.logic.RegistData(false))
                {
                    return;
                }
            }
            else
            {
                return;
            }

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            msgLogic.MessageBoxShow("I001", "登録");
        }

        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            this.logic.Cancel();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.Parent;

            this.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        //thongh 2015/12/23 #12284
        /// <summary>
        /// 画面初回表出処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            if (!this.InitialFlg)
            {
                this.Height -= 7;
                this.InitialFlg = true;
            }
            if (!AppConfig.AppOptions.IsDenshiKeiyaku())
            {
                this.main_tab.TabPages.Remove(this.denshi_keiyaku);
            }

            this.KYOTEN_CD.Focus();
            base.OnShown(e);

        }
        #endregion

        #region 各個処理

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
        /// 個別設定拠点コード変更チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void KYOTEN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool catchErr = false;
            this.KYOTEN_NAME_RYAKU.Text = this.logic.SearchKyotenName(this.KYOTEN_CD.Text, e, out catchErr);
        }

        /// <summary>
        /// 荷降業者CD確定後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIOROSHI_GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.ChangeNioroshiGyoushaCD(((TextBox)sender).Text.ToString());
        }

        /// <summary>
        /// 荷降業者CDポップアップ後処理
        /// </summary>
        public virtual void PopupAfterNioroshiGyoushaCD()
        {
            this.logic.ChangeNioroshiGyoushaCD(NIOROSHI_GYOUSHA_CD.Text);
        }
        /// <summary>
        /// 荷降業者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void NIOROSHI_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.beforNioroshiGyousaCD = this.NIOROSHI_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 荷積業者CD確定後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NITSUMI_GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.ChangeNitsumiGyoushaCD(((TextBox)sender).Text.ToString());
        }

        /// <summary>
        /// 荷積業者CDポップアップ後処理
        /// </summary>
        public virtual void PopupAfterNitsumiGyoushaCD()
        {
            this.logic.ChangeNitsumiGyoushaCD(NITSUMI_GYOUSHA_CD.Text);
        }

        /// <summary>
        /// 荷積現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NITSUMI_GENBA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.ChangeNitsumiGenbaCD();
        }
        /// <summary>
        /// 荷積業者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void NITSUMI_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.beforNitsumiGyousaCD = this.NITSUMI_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 荷降現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.ChangeNioroshiGenbaCD();
        }

        /// <summary>
        /// 社内経路名称更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD_Validated(object sender, CancelEventArgs e)
        {
            this.logic.ChangeShanaiKeiroCD();
        }

        #endregion

        /// <summary>
        /// 参照ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void FileRefClick(object sender, EventArgs e)
        {
            this.logic.FileRefClick();
        }

		//PhuocLoc 2020/04/03 #134980 -Start
        /// <summary>
        /// 売上支払請求伝票の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>  
        private void UR_SH_SEIKYUU_PRINT_KBN_TextChanged(object sender, EventArgs e)
        {
            SetShiharaiPrintKbn();
        }

        /// <summary>
        /// 売上支払伝票の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void UR_SH_SHIHARAI_PRINT_KBN_TextChanged(object sender, EventArgs e)
        {
            SetShiharaiPrintKbn();
        }

        /// <summary>
        /// 受入請求伝票の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void UKEIRE_SEIKYUU_PRINT_KBN_TextChanged(object sender, EventArgs e)
        {
            SetUkeirePrintKbn();
        }

        /// <summary>
        /// 受入支払伝票の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void UKEIRE_SHIHARAI_PRINT_KBN_TextChanged(object sender, EventArgs e)
        {
            SetUkeirePrintKbn();
        }


        /// <summary>
        /// 出荷請求伝票の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>  
        private void SHUKKA_SEIKYUU_PRINT_KBN_TextChanged(object sender, EventArgs e)
        {
            SetSyukkaPrintKbn();
        }

        /// <summary>
        /// 出荷支払伝票の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void SHUKKA_SHIHARAI_PRINT_KBN_TextChanged(object sender, EventArgs e)
        {
            SetSyukkaPrintKbn();
        }

        /// <summary>
        /// 売上支払出力区分の設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private bool SetShiharaiPrintKbn()
        {
            try
            {
                if (UR_SH_SEIKYUU_PRINT_KBN.Text.Equals("1") && UR_SH_SHIHARAI_PRINT_KBN.Text.Equals("1"))
                {
                    rb_UR_SH_DENPYOU_PRINT_KBN_1.Enabled = true;
                    rb_UR_SH_DENPYOU_PRINT_KBN_2.Enabled = true;
                    rb_UR_SH_DENPYOU_PRINT_KBN_3.Enabled = true;
                    UR_SH_DENPYOU_PRINT_KBN.Enabled = true;
                    UR_SH_DENPYOU_PRINT_KBN.RangeSetting.Max = 3;
                    UR_SH_DENPYOU_PRINT_KBN.RangeSetting.Min = 1;
                    UR_SH_DENPYOU_PRINT_KBN.Text = "3";
                }
                if ((UR_SH_SEIKYUU_PRINT_KBN.Text.Equals("1") && UR_SH_SHIHARAI_PRINT_KBN.Text.Equals("2")) ||
                    (UR_SH_SEIKYUU_PRINT_KBN.Text.Equals("2") && UR_SH_SHIHARAI_PRINT_KBN.Text.Equals("1")))
                {
                    rb_UR_SH_DENPYOU_PRINT_KBN_1.Enabled = true;
                    rb_UR_SH_DENPYOU_PRINT_KBN_2.Enabled = false;
                    rb_UR_SH_DENPYOU_PRINT_KBN_3.Enabled = false;
                    UR_SH_DENPYOU_PRINT_KBN.Enabled = true;
                    UR_SH_DENPYOU_PRINT_KBN.RangeSetting.Max = 1;
                    UR_SH_DENPYOU_PRINT_KBN.RangeSetting.Min = 1;
                    UR_SH_DENPYOU_PRINT_KBN.Text = "1";
                }
                if (UR_SH_SEIKYUU_PRINT_KBN.Text.Equals("2") && UR_SH_SHIHARAI_PRINT_KBN.Text.Equals("2"))
                {
                    rb_UR_SH_DENPYOU_PRINT_KBN_1.Enabled = false;
                    rb_UR_SH_DENPYOU_PRINT_KBN_2.Enabled = false;
                    rb_UR_SH_DENPYOU_PRINT_KBN_3.Enabled = false;
                    UR_SH_DENPYOU_PRINT_KBN.Enabled = false;
                    UR_SH_DENPYOU_PRINT_KBN.RangeSetting.Max = 0;
                    UR_SH_DENPYOU_PRINT_KBN.RangeSetting.Min = 0;
                    UR_SH_DENPYOU_PRINT_KBN.Text = "1";
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShiharaiPrintKbn", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 受入出力区分の設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private bool SetUkeirePrintKbn()
        {
            try
            {
                if (UKEIRE_SEIKYUU_PRINT_KBN.Text.Equals("1") && UKEIRE_SHIHARAI_PRINT_KBN.Text.Equals("1"))
                {
                    rb_UKEIRE_DENPYOU_PRINT_KBN_1.Enabled = true;
                    rb_UKEIRE_DENPYOU_PRINT_KBN_2.Enabled = true;
                    rb_UKEIRE_DENPYOU_PRINT_KBN_3.Enabled = true;
                    UKEIRE_DENPYOU_PRINT_KBN.Enabled = true;
                    UKEIRE_DENPYOU_PRINT_KBN.RangeSetting.Max = 3;
                    UKEIRE_DENPYOU_PRINT_KBN.RangeSetting.Min = 1;
                    UKEIRE_DENPYOU_PRINT_KBN.Text = "3";
                }
                if ((UKEIRE_SEIKYUU_PRINT_KBN.Text.Equals("1") && UKEIRE_SHIHARAI_PRINT_KBN.Text.Equals("2")) ||
                    (UKEIRE_SEIKYUU_PRINT_KBN.Text.Equals("2") && UKEIRE_SHIHARAI_PRINT_KBN.Text.Equals("1")))
                {
                    rb_UKEIRE_DENPYOU_PRINT_KBN_1.Enabled = true;
                    rb_UKEIRE_DENPYOU_PRINT_KBN_2.Enabled = false;
                    rb_UKEIRE_DENPYOU_PRINT_KBN_3.Enabled = false;
                    UKEIRE_DENPYOU_PRINT_KBN.Enabled = true;
                    UKEIRE_DENPYOU_PRINT_KBN.RangeSetting.Max = 1;
                    UKEIRE_DENPYOU_PRINT_KBN.RangeSetting.Min = 1;
                    UKEIRE_DENPYOU_PRINT_KBN.Text = "1";
                }
                if (UKEIRE_SEIKYUU_PRINT_KBN.Text.Equals("2") && UKEIRE_SHIHARAI_PRINT_KBN.Text.Equals("2"))
                {
                    rb_UKEIRE_DENPYOU_PRINT_KBN_1.Enabled = false;
                    rb_UKEIRE_DENPYOU_PRINT_KBN_2.Enabled = false;
                    rb_UKEIRE_DENPYOU_PRINT_KBN_3.Enabled = false;
                    UKEIRE_DENPYOU_PRINT_KBN.Enabled = false;
                    UKEIRE_DENPYOU_PRINT_KBN.RangeSetting.Max = 0;
                    UKEIRE_DENPYOU_PRINT_KBN.RangeSetting.Min = 0;
                    UKEIRE_DENPYOU_PRINT_KBN.Text = "1";
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetUkeirePrintKbn", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 出荷出力区分の設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private bool SetSyukkaPrintKbn()
        {
            try
            {
                if (SHUKKA_SEIKYUU_PRINT_KBN.Text.Equals("1") && SHUKKA_SHIHARAI_PRINT_KBN.Text.Equals("1"))
                {
                    rb_SHUKKA_DENPYOU_PRINT_KBN_1.Enabled = true;
                    rb_SHUKKA_DENPYOU_PRINT_KBN_2.Enabled = true;
                    rb_SHUKKA_DENPYOU_PRINT_KBN_3.Enabled = true;
                    SHUKKA_DENPYOU_PRINT_KBN.Enabled = true;
                    SHUKKA_DENPYOU_PRINT_KBN.RangeSetting.Max = 3;
                    SHUKKA_DENPYOU_PRINT_KBN.RangeSetting.Min = 1;
                    SHUKKA_DENPYOU_PRINT_KBN.Text = "3";
                }
                if ((SHUKKA_SEIKYUU_PRINT_KBN.Text.Equals("1") && SHUKKA_SHIHARAI_PRINT_KBN.Text.Equals("2")) ||
                    (SHUKKA_SEIKYUU_PRINT_KBN.Text.Equals("2") && SHUKKA_SHIHARAI_PRINT_KBN.Text.Equals("1")))
                {
                    rb_SHUKKA_DENPYOU_PRINT_KBN_1.Enabled = true;
                    rb_SHUKKA_DENPYOU_PRINT_KBN_2.Enabled = false;
                    rb_SHUKKA_DENPYOU_PRINT_KBN_3.Enabled = false;
                    SHUKKA_DENPYOU_PRINT_KBN.Enabled = true;
                    SHUKKA_DENPYOU_PRINT_KBN.RangeSetting.Max = 1;
                    SHUKKA_DENPYOU_PRINT_KBN.RangeSetting.Min = 1;
                    SHUKKA_DENPYOU_PRINT_KBN.Text = "1";
                }
                if (SHUKKA_SEIKYUU_PRINT_KBN.Text.Equals("2") && SHUKKA_SHIHARAI_PRINT_KBN.Text.Equals("2"))
                {
                    rb_SHUKKA_DENPYOU_PRINT_KBN_1.Enabled = false;
                    rb_SHUKKA_DENPYOU_PRINT_KBN_2.Enabled = false;
                    rb_SHUKKA_DENPYOU_PRINT_KBN_3.Enabled = false;
                    SHUKKA_DENPYOU_PRINT_KBN.Enabled = false;
                    SHUKKA_DENPYOU_PRINT_KBN.RangeSetting.Max = 0;
                    SHUKKA_DENPYOU_PRINT_KBN.RangeSetting.Min = 0;
                    SHUKKA_DENPYOU_PRINT_KBN.Text = "1";
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSyukkaPrintKbn", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
        }
        //PhuocLoc 2020/04/03 #134980 -End

        /// <summary>
        /// ファイルアップロード参照ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void FileUploadRefClick(object sender, EventArgs e)
        {
            this.logic.FileUploadRefClick();
        }

        /// <summary>
        /// 初期フォルダ存在確認
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FILE_UPLOAD_PATH_Validating(object sender, CancelEventArgs e)
        {
            string path = this.FILE_UPLOAD_PATH.GetResultText();
            if (!string.IsNullOrEmpty(path))
            {
                if (Directory.Exists(path))
                {
                    return;
                }
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E024", this.FILE_UPLOAD_PATH.DisplayItemName);
                var textBox = this.FILE_UPLOAD_PATH as TextBox;
                if (textBox != null)
                {
                    textBox.SelectAll();
                }
                e.Cancel = true;
            }
        }

        //20250306
        internal virtual void FileRefClick_1(object sender, EventArgs e)
        {
            this.logic.FileRefClick_1();
        }

        internal virtual void FileRefClick_2(object sender, EventArgs e)
        {
            this.logic.FileRefClick_2();
        }

        internal virtual void FileRefClick_3(object sender, EventArgs e)
        {
            this.logic.FileRefClick_3();
        }

        //20250307
        private void TOROKOMI_KANKAKU_Validating(object sender, CancelEventArgs e)
        {
            string input = this.TOROKOMI_KANKAKU.Text.Replace(",", "");
            int tokoromi_kankaku;

            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            if (int.TryParse(input, out tokoromi_kankaku))
            {
                if (tokoromi_kankaku > 0)
                {
                    SelectCheckDto existCheck = new SelectCheckDto();
                    existCheck.CheckMethodName = "必須チェック";

                    Collection<SelectCheckDto> existChecks = new Collection<SelectCheckDto>();
                    existChecks.Add(existCheck);

                    this.TORIKOMI_FORUDA.RegistCheckMethod = existChecks;
                    this.HOKAN_FORUDA.RegistCheckMethod = existChecks;
                }
                else
                {
                    this.TORIKOMI_FORUDA.RegistCheckMethod = null;
                    this.TORIKOMI_FORUDA.RegistCheckMethod = null;
                }
            }
            else
            {
                e.Cancel = true;
                return;
            }
        }

        private void CTI_RENKEI_KANKAKU_Validating(object sender, CancelEventArgs e)
        {
            string input = this.CTI_RENKEI_KANKAKU.Text.Replace(",", "");
            int cti_renkei_kankaku;

            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            if (int.TryParse(input, out cti_renkei_kankaku))
            {
                if (cti_renkei_kankaku > 0)
                {
                    SelectCheckDto existCheck = new SelectCheckDto();
                    existCheck.CheckMethodName = "必須チェック";

                    Collection<SelectCheckDto> existChecks = new Collection<SelectCheckDto>();
                    existChecks.Add(existCheck);

                    this.CTI_RENKEI_FORUDA.RegistCheckMethod = existChecks;
                }
                else
                {
                    this.CTI_RENKEI_FORUDA.RegistCheckMethod = null;
                }
            }
            else
            {
                e.Cancel = true;
                return;
            }
        }
    }
}