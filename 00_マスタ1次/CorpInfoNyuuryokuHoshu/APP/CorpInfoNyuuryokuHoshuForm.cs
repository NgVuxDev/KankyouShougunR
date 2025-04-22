// $Id: CorpInfoNyuuryokuHoshuForm.cs 54199 2015-07-01 05:01:14Z minhhoang@e-mall.co.jp $
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CorpInfoNyuuryokuHoshu.Logic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace CorpInfoNyuuryokuHoshu.APP
{
    /// <summary>
    /// 自社情報入力保守画面
    /// </summary>
    [Implementation]
    public partial class CorpInfoNyuuryokuHoshuForm : SuperForm
    {
        /// <summary>
        /// 自社情報入力保守画面ロジック
        /// </summary>
        private CorpInfoNyuuryokuHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// 銀行支店ポップアップ表示フラグ
        /// </summary>
        private bool isBankShitenPopup;
        private bool isBankShitenPopup2;
        private bool isBankShitenPopup3;


        /// <summary>
        /// 銀行支店CD前回入力値
        /// </summary>
        private string previousBankShitenCd;
        private string previousBankShitenCd_2;
        private string previousBankShitenCd_3;

        private string previousFurikomiBankShitenCd;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CorpInfoNyuuryokuHoshuForm()
            : base(WINDOW_ID.M_CORP_INFO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new CorpInfoNyuuryokuHoshuLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
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

            this.Search(null, e);
            
            // Anchorの設定は必ずOnLoadで行うこと
            if (this.tabControl1 != null)
            {
                this.tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            int count = this.logic.Search();
            if (count <= 0)
            {
                return;
            }

            this.logic.SetWindowData();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {

            // 登録番号が入力されている場合、入力文字数をチェック
            if (this.TOUROKU_NO.Text != "" && this.TOUROKU_NO.Text.Length != 14)
            {
                DialogResult result = MessageBox.Show("登録番号は14桁で入力してください。\r登録を継続しますか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                {
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
                if (base.RegistErrorFlag)
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
            bool catchErr = this.logic.Cancel();
            if (catchErr)
            {
                return;
            }
            
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
        /// 銀行CD変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BANK_CD_TextChanged(object sender, EventArgs e)
        {
            this.BANK_NAME_RYAKU.Text = string.Empty;
            this.BANK_SHITEN_CD.Text = string.Empty;
            this.BANK_SHIETN_NAME_RYAKU.Text = string.Empty;
            this.KOUZA_SHURUI.Text = string.Empty;
            this.KOUZA_NO.Text = string.Empty;
            this.KOUZA_NAME.Text = string.Empty;

            this.previousBankShitenCd = string.Empty;
        }

        /// <summary>
        /// 銀行支店CDのバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void BANK_SHITEN_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //20151005 hoanghm start
            var bankCd = this.BANK_CD.Text;
            var bankShitenCd = this.BANK_SHITEN_CD.Text;
            //
            var kouzaShurui = this.KOUZA_SHURUI.Text;
            var kouzaNo = this.KOUZA_NO.Text;
            //

            if (string.IsNullOrEmpty(bankCd) && !string.IsNullOrEmpty(bankShitenCd))
            {
                var message = new MessageBoxShowLogic();
                message.MessageBoxShow("E012", "銀行");
                this.BANK_SHITEN_CD.Focus();
                return;
            }

            if (bankShitenCd.Equals(this.previousBankShitenCd))
            {
                return;
            }

            if (!string.IsNullOrEmpty(bankCd) && !string.IsNullOrEmpty(bankShitenCd))
            {
                bool catchErr = false;
                var bankShitenList = this.logic.GetBankShiten(bankCd, bankShitenCd,out catchErr);
                if (catchErr)
                {
                    return;
                }
                if (bankShitenList.Count == 0)
                {
                    // 該当なしなのでエラー
                    var message = new MessageBoxShowLogic();
                    message.MessageBoxShow("E011", "銀行支店マスタ");

                    this.BANK_SHIETN_NAME_RYAKU.Text = String.Empty;
                    this.KOUZA_SHURUI.Text = String.Empty;
                    this.KOUZA_NO.Text = String.Empty;
                    this.KOUZA_NAME.Text = String.Empty;

                    this.previousBankShitenCd = string.Empty;

                    e.Cancel = true;
                }
                else if (bankShitenList.Count == 1)
                {
                    // 1件該当なので値をセット
                    var bankShiten = bankShitenList.Where(b => b.BANK_CD == bankCd && b.BANK_SHITEN_CD == bankShitenCd).DefaultIfEmpty(new M_BANK_SHITEN()).FirstOrDefault();
                    this.BANK_SHIETN_NAME_RYAKU.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                    this.KOUZA_SHURUI.Text = bankShiten.KOUZA_SHURUI;
                    this.KOUZA_NO.Text = bankShiten.KOUZA_NO;
                    this.KOUZA_NAME.Text = bankShiten.KOUZA_NAME;

                    this.previousBankShitenCd = bankShiten.BANK_SHITEN_CD;
                }
                else if (bankShitenList.Count > 1)
                {
                    if (!string.IsNullOrEmpty(kouzaShurui) && !string.IsNullOrEmpty(kouzaNo))
                    {
                        var bankShiten = bankShitenList.Where(b => b.BANK_CD == bankCd && b.BANK_SHITEN_CD == bankShitenCd && b.KOUZA_SHURUI == kouzaShurui && b.KOUZA_NO == kouzaNo).DefaultIfEmpty(new M_BANK_SHITEN()).FirstOrDefault();
                        this.BANK_SHIETN_NAME_RYAKU.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                        this.KOUZA_SHURUI.Text = bankShiten.KOUZA_SHURUI;
                        this.KOUZA_NO.Text = bankShiten.KOUZA_NO;
                        this.KOUZA_NAME.Text = bankShiten.KOUZA_NAME;

                        this.previousBankShitenCd = bankShiten.BANK_SHITEN_CD;
                    }
                    else
                    { 
                        //Show popup
                        DialogResult result = CustomControlExtLogic.PopUp(this.BANK_SHITEN_CD);

                        if (result == DialogResult.OK)
                        {
                            this.previousBankShitenCd = this.BANK_SHITEN_CD.Text;
                        }

                        e.Cancel = true;
                    }
                }
            }
            /*
            /////////////////////////
            //20150616 #2124 銀行名がブランクの場合はアラートを表示させるようにする。hoanghm start
            if (String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var message = new MessageBoxShowLogic();
                message.MessageBoxShow("E012", "銀行");
                this.BANK_SHITEN_CD.Focus();
                return;
            }
            if (this.previousBankShitenCd != bankShitenCd)
            {
                this.isBankShitenPopup = false;
            }
            //20150616 #2124 銀行名がブランクの場合はアラートを表示させるようにする。hoanghm end

            if (!String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var bankShitenList = this.logic.GetBankShiten(bankCd, bankShitenCd);
                if (bankShitenList.Count == 0)
                {
                    // 該当なしなのでエラー
                    var message = new MessageBoxShowLogic();
                    message.MessageBoxShow("E011", "銀行支店マスタ");

                    this.BANK_SHIETN_NAME_RYAKU.Text = String.Empty;
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
                    this.BANK_SHIETN_NAME_RYAKU.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                    this.KOUZA_SHURUI.Text = bankShiten.KOUZA_SHURUI;
                    this.KOUZA_NO.Text = bankShiten.KOUZA_NO;
                    this.KOUZA_NAME.Text = bankShiten.KOUZA_NAME;

                    this.previousBankShitenCd = bankShitenCd;

                    this.isBankShitenPopup = false;
                }
                else if (bankShitenList.Count > 1)
                {
                    if (false == this.isBankShitenPopup)// && this.previousBankShitenCd != bankShitenCd)
                    {
                        // 複数該当なのでポップアップ表示
                        CustomControlExtLogic.PopUp(this.BANK_SHITEN_CD);
                        isBankShitenPopup = true;

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
                this.BANK_SHIETN_NAME_RYAKU.Text = String.Empty;
                this.KOUZA_SHURUI.Text = String.Empty;
                this.KOUZA_NO.Text = String.Empty;
                this.KOUZA_NAME.Text = String.Empty;

                this.previousBankShitenCd = String.Empty;

                this.isBankShitenPopup = false;
            }
            */
            //20151005 hoanghm end

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 銀行支店CD_2のバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void BANK_SHITEN_CD_2_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var bankCd = this.BANK_CD_2.Text;
            var bankShitenCd = this.BANK_SHITEN_CD_2.Text;
            //
            var kouzaShurui = this.KOUZA_SHURUI_2.Text;
            var kouzaNo = this.KOUZA_NO_2.Text;
            //

            if (string.IsNullOrEmpty(bankCd) && !string.IsNullOrEmpty(bankShitenCd))
            {
                var message = new MessageBoxShowLogic();
                message.MessageBoxShow("E012", "銀行");
                this.BANK_SHITEN_CD_2.Focus();
                return;
            }

            if (bankShitenCd.Equals(this.previousBankShitenCd_2))
            {
                return;
            }

            if (!string.IsNullOrEmpty(bankCd) && !string.IsNullOrEmpty(bankShitenCd))
            {
                bool catchErr = false;
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

                    this.BANK_SHIETN_NAME_RYAKU_2.Text = String.Empty;
                    this.KOUZA_SHURUI_2.Text = String.Empty;
                    this.KOUZA_NO_2.Text = String.Empty;
                    this.KOUZA_NAME_2.Text = String.Empty;

                    this.previousBankShitenCd_2 = string.Empty;

                    e.Cancel = true;
                }
                else if (bankShitenList.Count == 1)
                {
                    // 1件該当なので値をセット
                    var bankShiten = bankShitenList.Where(b => b.BANK_CD == bankCd && b.BANK_SHITEN_CD == bankShitenCd).DefaultIfEmpty(new M_BANK_SHITEN()).FirstOrDefault();
                    this.BANK_SHIETN_NAME_RYAKU_2.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                    this.KOUZA_SHURUI_2.Text = bankShiten.KOUZA_SHURUI;
                    this.KOUZA_NO_2.Text = bankShiten.KOUZA_NO;
                    this.KOUZA_NAME_2.Text = bankShiten.KOUZA_NAME;

                    this.previousBankShitenCd_2 = bankShiten.BANK_SHITEN_CD;
                }
                else if (bankShitenList.Count > 1)
                {
                    if (!string.IsNullOrEmpty(kouzaShurui) && !string.IsNullOrEmpty(kouzaNo))
                    {
                        var bankShiten = bankShitenList.Where(b => b.BANK_CD == bankCd && b.BANK_SHITEN_CD == bankShitenCd && b.KOUZA_SHURUI == kouzaShurui && b.KOUZA_NO == kouzaNo).DefaultIfEmpty(new M_BANK_SHITEN()).FirstOrDefault();
                        this.BANK_SHIETN_NAME_RYAKU_2.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                        this.KOUZA_SHURUI_2.Text = bankShiten.KOUZA_SHURUI;
                        this.KOUZA_NO_2.Text = bankShiten.KOUZA_NO;
                        this.KOUZA_NAME_2.Text = bankShiten.KOUZA_NAME;

                        this.previousBankShitenCd_2 = bankShiten.BANK_SHITEN_CD;
                    }
                    else
                    {
                        //Show popup
                        DialogResult result = CustomControlExtLogic.PopUp(this.BANK_SHITEN_CD_2);

                        if (result == DialogResult.OK)
                        {
                            this.previousBankShitenCd_2 = this.BANK_SHITEN_CD_2.Text;
                        }

                        e.Cancel = true;
                    }
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 銀行支店CD_3のバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void BANK_SHITEN_CD_3_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //20151005 hoanghm start
            var bankCd = this.BANK_CD_3.Text;
            var bankShitenCd = this.BANK_SHITEN_CD_3.Text;
            //
            var kouzaShurui = this.KOUZA_SHURUI_3.Text;
            var kouzaNo = this.KOUZA_NO_3.Text;
            //

            if (string.IsNullOrEmpty(bankCd) && !string.IsNullOrEmpty(bankShitenCd))
            {
                var message = new MessageBoxShowLogic();
                message.MessageBoxShow("E012", "銀行");
                this.BANK_SHITEN_CD_3.Focus();
                return;
            }

            if (bankShitenCd.Equals(this.previousBankShitenCd_3))
            {
                return;
            }

            if (!string.IsNullOrEmpty(bankCd) && !string.IsNullOrEmpty(bankShitenCd))
            {
                bool catchErr = false;
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

                    this.BANK_SHIETN_NAME_RYAKU_3.Text = String.Empty;
                    this.KOUZA_SHURUI_3.Text = String.Empty;
                    this.KOUZA_NO_3.Text = String.Empty;
                    this.KOUZA_NAME_3.Text = String.Empty;

                    this.previousBankShitenCd_3 = string.Empty;

                    e.Cancel = true;
                }
                else if (bankShitenList.Count == 1)
                {
                    // 1件該当なので値をセット
                    var bankShiten = bankShitenList.Where(b => b.BANK_CD == bankCd && b.BANK_SHITEN_CD == bankShitenCd).DefaultIfEmpty(new M_BANK_SHITEN()).FirstOrDefault();
                    this.BANK_SHIETN_NAME_RYAKU_3.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                    this.KOUZA_SHURUI_3.Text = bankShiten.KOUZA_SHURUI;
                    this.KOUZA_NO_3.Text = bankShiten.KOUZA_NO;
                    this.KOUZA_NAME_3.Text = bankShiten.KOUZA_NAME;

                    this.previousBankShitenCd_3 = bankShiten.BANK_SHITEN_CD;
                }
                else if (bankShitenList.Count > 1)
                {
                    if (!string.IsNullOrEmpty(kouzaShurui) && !string.IsNullOrEmpty(kouzaNo))
                    {
                        var bankShiten = bankShitenList.Where(b => b.BANK_CD == bankCd && b.BANK_SHITEN_CD == bankShitenCd && b.KOUZA_SHURUI == kouzaShurui && b.KOUZA_NO == kouzaNo).DefaultIfEmpty(new M_BANK_SHITEN()).FirstOrDefault();
                        this.BANK_SHIETN_NAME_RYAKU_3.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                        this.KOUZA_SHURUI_3.Text = bankShiten.KOUZA_SHURUI;
                        this.KOUZA_NO_3.Text = bankShiten.KOUZA_NO;
                        this.KOUZA_NAME_3.Text = bankShiten.KOUZA_NAME;

                        this.previousBankShitenCd_3 = bankShiten.BANK_SHITEN_CD;
                    }
                    else
                    {
                        //Show popup
                        DialogResult result = CustomControlExtLogic.PopUp(this.BANK_SHITEN_CD_3);

                        if (result == DialogResult.OK)
                        {
                            this.previousBankShitenCd_3 = this.BANK_SHITEN_CD_3.Text;
                        }

                        e.Cancel = true;
                    }
                }
            }
            LogUtility.DebugMethodEnd();
        }
        #region 20151005 hoanghm edit
        ///// <summary> 
        ///// 銀行支店選択ポップアップを閉じたときに処理します
        ///// </summary>
        //public void BANK_SHITEN_CD_PopupAfter()
        //{
        //    LogUtility.DebugMethodStart();

        //    this.isBankShitenPopup = true;

        //    LogUtility.DebugMethodEnd();
        //}
        #endregion

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            base.OnShown(e);
        }

        #region 20151005 hoanghm add

        private void BANK_SHITEN_CD_TextChanged(object sender, EventArgs e)
        {
            this.BANK_SHIETN_NAME_RYAKU.Text = string.Empty;
            this.KOUZA_SHURUI.Text = string.Empty;
            this.KOUZA_NO.Text = string.Empty;
            this.KOUZA_NAME.Text = string.Empty;

            this.previousBankShitenCd = string.Empty;
        }

        #endregion

        /// <summary>
        /// BANK_CD_2_TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BANK_CD_2_TextChanged(object sender, EventArgs e)
        {
            this.BANK_NAME_RYAKU_2.Text = string.Empty;
            this.BANK_SHITEN_CD_2.Text = string.Empty;
            this.BANK_SHIETN_NAME_RYAKU_2.Text = string.Empty;
            this.KOUZA_SHURUI_2.Text = string.Empty;
            this.KOUZA_NO_2.Text = string.Empty;
            this.KOUZA_NAME_2.Text = string.Empty;

            this.previousBankShitenCd_2 = string.Empty;
        }

        /// <summary>
        /// BANK_SHITEN_CD_2_TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BANK_SHITEN_CD_2_TextChanged(object sender, EventArgs e)
        {
            this.BANK_SHIETN_NAME_RYAKU_2.Text = string.Empty;
            this.KOUZA_SHURUI_2.Text = string.Empty;
            this.KOUZA_NO_2.Text = string.Empty;
            this.KOUZA_NAME_2.Text = string.Empty;

            this.previousBankShitenCd_2 = string.Empty;
        }

        /// <summary>
        /// BANK_CD_3_TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BANK_CD_3_TextChanged(object sender, EventArgs e)
        {
            this.BANK_NAME_RYAKU_3.Text = string.Empty;
            this.BANK_SHITEN_CD_3.Text = string.Empty;
            this.BANK_SHIETN_NAME_RYAKU_3.Text = string.Empty;
            this.KOUZA_SHURUI_3.Text = string.Empty;
            this.KOUZA_NO_3.Text = string.Empty;
            this.KOUZA_NAME_3.Text = string.Empty;

            this.previousBankShitenCd_3 = string.Empty;
        }

        /// <summary>
        /// BANK_SHITEN_CD_3_TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BANK_SHITEN_CD_3_TextChanged(object sender, EventArgs e)
        {
            this.BANK_SHIETN_NAME_RYAKU_3.Text = string.Empty;
            this.KOUZA_SHURUI_3.Text = string.Empty;
            this.KOUZA_NO_3.Text = string.Empty;
            this.KOUZA_NAME_3.Text = string.Empty;

            this.previousBankShitenCd_3 = string.Empty;
        }

        private void FURIKOMI_MOTO_BANK_SHITEN_CD_TextChanged(object sender, EventArgs e)
        {
            this.FURIKOMI_MOTO_BANK_SHIETN_NAME_RYAKU.Text = string.Empty;
            this.FURIKOMI_MOTO_KOUZA_SHURUI.Text = string.Empty;
            this.FURIKOMI_MOTO_KOUZA_NO.Text = string.Empty;
            this.FURIKOMI_MOTO_KOUZA_NAME.Text = string.Empty;

            this.previousFurikomiBankShitenCd = string.Empty;
        }

        private void FURIKOMI_MOTO_BANK_CD_TextChanged(object sender, EventArgs e)
        {
            this.FURIKOMI_MOTO_BANK_NAME_RYAKU.Text = string.Empty;
            this.FURIKOMI_MOTO_BANK_SHITEN_CD.Text = string.Empty;
            this.FURIKOMI_MOTO_BANK_SHIETN_NAME_RYAKU.Text = string.Empty;
            this.FURIKOMI_MOTO_KOUZA_SHURUI.Text = string.Empty;
            this.FURIKOMI_MOTO_KOUZA_NO.Text = string.Empty;
            this.FURIKOMI_MOTO_KOUZA_NAME.Text = string.Empty;

            this.previousFurikomiBankShitenCd = string.Empty;
        }

        internal void FURIKOMI_MOTO_BANK_SHITEN_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var bankCd = this.FURIKOMI_MOTO_BANK_CD.Text;
            var bankShitenCd = this.FURIKOMI_MOTO_BANK_SHITEN_CD.Text;
            var kouzaShurui = this.FURIKOMI_MOTO_KOUZA_SHURUI.Text;
            var kouzaNo = this.FURIKOMI_MOTO_KOUZA_NO.Text;

            if (string.IsNullOrEmpty(bankCd) && !string.IsNullOrEmpty(bankShitenCd))
            {
                var message = new MessageBoxShowLogic();
                message.MessageBoxShow("E012", "銀行");
                this.FURIKOMI_MOTO_BANK_SHITEN_CD.Focus();
                return;
            }

            if (bankShitenCd.Equals(this.previousFurikomiBankShitenCd))
            {
                return;
            }

            if (!string.IsNullOrEmpty(bankCd) && !string.IsNullOrEmpty(bankShitenCd))
            {
                bool catchErr = false;
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

                    this.FURIKOMI_MOTO_BANK_SHIETN_NAME_RYAKU.Text = String.Empty;
                    this.FURIKOMI_MOTO_KOUZA_SHURUI.Text = String.Empty;
                    this.FURIKOMI_MOTO_KOUZA_NO.Text = String.Empty;
                    this.FURIKOMI_MOTO_KOUZA_NAME.Text = String.Empty;

                    this.previousFurikomiBankShitenCd = string.Empty;

                    e.Cancel = true;
                }
                else if (bankShitenList.Count == 1)
                {
                    // 1件該当なので値をセット
                    var bankShiten = bankShitenList.Where(b => b.BANK_CD == bankCd && b.BANK_SHITEN_CD == bankShitenCd).DefaultIfEmpty(new M_BANK_SHITEN()).FirstOrDefault();
                    this.FURIKOMI_MOTO_BANK_SHIETN_NAME_RYAKU.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                    this.FURIKOMI_MOTO_KOUZA_SHURUI.Text = bankShiten.KOUZA_SHURUI;
                    this.FURIKOMI_MOTO_KOUZA_NO.Text = bankShiten.KOUZA_NO;
                    this.FURIKOMI_MOTO_KOUZA_NAME.Text = bankShiten.KOUZA_NAME;

                    this.previousFurikomiBankShitenCd = bankShiten.BANK_SHITEN_CD;
                }
                else if (bankShitenList.Count > 1)
                {
                    if (!string.IsNullOrEmpty(kouzaShurui) && !string.IsNullOrEmpty(kouzaNo))
                    {
                        var bankShiten = bankShitenList.Where(b => b.BANK_CD == bankCd && b.BANK_SHITEN_CD == bankShitenCd && b.KOUZA_SHURUI == kouzaShurui && b.KOUZA_NO == kouzaNo).DefaultIfEmpty(new M_BANK_SHITEN()).FirstOrDefault();
                        this.FURIKOMI_MOTO_BANK_SHIETN_NAME_RYAKU.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                        this.FURIKOMI_MOTO_KOUZA_SHURUI.Text = bankShiten.KOUZA_SHURUI;
                        this.FURIKOMI_MOTO_KOUZA_NO.Text = bankShiten.KOUZA_NO;
                        this.FURIKOMI_MOTO_KOUZA_NAME.Text = bankShiten.KOUZA_NAME;

                        this.previousFurikomiBankShitenCd = bankShiten.BANK_SHITEN_CD;
                    }
                    else
                    {
                        //Show popup
                        DialogResult result = CustomControlExtLogic.PopUp(this.FURIKOMI_MOTO_BANK_SHITEN_CD);

                        if (result == DialogResult.OK)
                        {
                            this.previousFurikomiBankShitenCd = this.FURIKOMI_MOTO_BANK_SHITEN_CD.Text;
                        }

                        e.Cancel = true;
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }
    }
}
