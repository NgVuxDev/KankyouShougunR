// $Id: NyuukinsakiNyuuryokuHoshuForm.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using NyuukinsakiNyuuryokuHoshu.Logic;
using NyuukinsakiNyuuryokuHoshu.Const;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using r_framework.Utility;

namespace NyuukinsakiNyuuryokuHoshu.APP
{
    /// <summary>
    /// 入金先入力保守画面
    /// </summary>
    [Implementation]
    public partial class NyuukinsakiNyuuryokuHoshuForm : SuperForm
    {
        /// <summary>
        /// 入金先入力保守画面ロジック
        /// </summary>
        private NyuukinsakiNyuuryokuHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ(【新規】モード起動時)
        /// </summary>
        public NyuukinsakiNyuuryokuHoshuForm()
            : base(WINDOW_ID.M_NYUUKINSAKI, WINDOW_TYPE.NEW_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new NyuukinsakiNyuuryokuHoshuLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// コンストラクタ(【修正】【削除】【複写】【参照】モード起動時)
        /// </summary>
        /// <param name="windowType">処理モード</param>
        /// <param name="nyuukinsakiCd">選択されたデータの入金先CD</param>
        public NyuukinsakiNyuuryokuHoshuForm(WINDOW_TYPE windowType, string nyuukinsakiCd)
            : base(WINDOW_ID.M_NYUUKINSAKI, windowType)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new NyuukinsakiNyuuryokuHoshuLogic(this);

            this.logic.nyuukinsakiCd = nyuukinsakiCd;

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
            this.logic.WindowInit(base.WindowType);

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.tabControl1 != null)
            {
                this.tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
        }

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

        /// <summary>
        /// 【新規】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CreateMode(object sender, EventArgs e)
        {
            // 権限チェック
            if (!r_framework.Authority.Manager.CheckAuthority("M209", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                return;
            }

            // 処理モード変更
            base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

            // 画面タイトル変更
            base.HeaderFormInit();

            // 画面初期化
            this.logic.nyuukinsakiCd = string.Empty;
            this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent);
        }

        /// <summary>
        /// 【修正】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void UpdateMode(object sender, EventArgs e)
        {
            // 権限チェック
            // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
            if (r_framework.Authority.Manager.CheckAuthority("M209", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                // 処理モード変更
                base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                // 画面タイトル変更
                base.HeaderFormInit();
                // 画面初期化
                this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);
            }
            else if (r_framework.Authority.Manager.CheckAuthority("M209", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                // 処理モード変更
                base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                // 画面タイトル変更
                base.HeaderFormInit();
                // 画面初期化
                this.logic.WindowInitReference((BusinessBaseForm)this.Parent);
            }
            else
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E158", "修正");
            }
        }

        /// <summary>
        /// 取消ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            this.logic.Cancel((BusinessBaseForm)this.Parent);
        }

        /// <summary>
        /// 一覧画面へ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowIchiran(object sender, EventArgs e)
        {
            this.logic.ShowIchiran();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                bool catchErr = this.logic.CreateEntity(false);
                if (catchErr)
                {
                    return;
                }

                switch (base.WindowType)
                {
                    // 新規追加
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // 重複チェック
                        bool result = this.DupliUpdateViewCheck(e, out catchErr);
                        if (catchErr)
                        {
                            return;
                        }
                        if (result)
                        {
                            // 重複していなければ登録を行う
                            this.logic.Regist(base.RegistErrorFlag);
                        }
                        break;

                    // 更新
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        this.logic.Update(base.RegistErrorFlag);
                        break;

                    // 論理削除
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        if (this.logic.CheckDelete())
                        {
                            this.logic.LogicalDelete();
                        }
                        break;

                    default:
                        break;
                }

                if (this.logic.isRegist)
                {
                    // 権限チェック
                    if (r_framework.Authority.Manager.CheckAuthority("M209", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                    {
                        // DB更新後、新規モードで表示
                        base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                        base.HeaderFormInit();
                        this.logic.nyuukinsakiCd = string.Empty;
                        this.logic.isRegist = false;
                        this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent);
                    }
                    else
                    {
                        // 新規権限がない場合は画面Close
                        this.FormClose(sender, e);
                    }
                }
            }
        }

        /// <summary>
        /// 入金先CD重複チェック and 修正モード起動要否チェック
        /// </summary>
        /// <param name="e">イベント</param>
        private bool DupliUpdateViewCheck(EventArgs e, out bool catchErr)
        {
            try
            {
                bool result = false;
                catchErr = false;

                // 入金先CDの入力値をゼロパディング
                string zeroPadCd = this.logic.ZeroPadding(this.NYUUKINSAKI_CD.Text).ToUpper();

                // 重複チェック
                NyuukinsakiNyuuryokuHoshuConstans.NyuukinCdLeaveResult isUpdate = this.logic.DupliCheckNyuukinsakiCd(zeroPadCd);

                if (isUpdate == NyuukinsakiNyuuryokuHoshuConstans.NyuukinCdLeaveResult.FALSE_ON)
                {
                    // 権限チェック
                    // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
                    if (r_framework.Authority.Manager.CheckAuthority("M209", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        // 修正モードで表示する
                        this.logic.nyuukinsakiCd = zeroPadCd;

                        base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;

                        // 画面タイトル変更
                        base.HeaderFormInit();

                        this.NYUUKINSAKI_NAME1.Focus();

                        // 修正モードで画面初期化
                        this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);

                        //登録フラグのため削除
                        //result = true;
                    }
                    else if (r_framework.Authority.Manager.CheckAuthority("M209", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        // 参照モードで表示する
                        this.logic.nyuukinsakiCd = zeroPadCd;

                        base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;

                        // 画面タイトル変更
                        base.HeaderFormInit();

                        this.NYUUKINSAKI_NAME1.Focus();

                        // 参照モードで画面初期化
                        this.logic.WindowInitReference((BusinessBaseForm)this.Parent);
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E158", "修正");
                        this.NYUUKINSAKI_CD.Text = string.Empty;
                        this.NYUUKINSAKI_CD.Focus();
                    }
                }
                else if (isUpdate != NyuukinsakiNyuuryokuHoshuConstans.NyuukinCdLeaveResult.TURE_NONE)
                {
                    // 入力した入金先CDが重複した かつ 修正モード未起動の場合
                    this.NYUUKINSAKI_CD.Text = string.Empty;
                    this.NYUUKINSAKI_CD.Focus();
                }
                else
                {
                    // 重複しなければINSERT処理を行うフラグON
                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("DupliUpdateViewCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.ParentForm;

            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// 採番ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_saiban_Click(object sender, EventArgs e)
        {
            // 採番値取得
            this.logic.Saiban();
        }

        /// <summary>
        /// 入金先CDフォーカスアウト時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NYUUKINSAKI_CD_Leave(object sender, EventArgs e)
        {
            // 【新規】モードの場合のみチェック処理を行う
            if (base.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                return;
            }

            // 入力された入金先CD取得
            string inputCd = this.NYUUKINSAKI_CD.Text;
            if (string.IsNullOrWhiteSpace(inputCd))
            {
                return;
            }

            // 重複チェック
            bool catchErr = false;
            this.DupliUpdateViewCheck(e, out catchErr);
        }

        /// <summary>
        /// フリコミ人名重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_furikomi_CellValidating(object sender, CellValidatingEventArgs e)
        {
            if (e.CellName.Equals(Const.NyuukinsakiNyuuryokuHoshuConstans.FURIKOMI_NAME))
            {
                // フリコミ人名重複チェック
                bool isNoErr = this.logic.DupliCheckFurikomiName();

                // エラー処理
                if (!isNoErr)
                {
                    e.Cancel = true;

                    GcMultiRow gc = sender as GcMultiRow;
                    if (gc != null && gc.EditingControl != null)
                    {
                        // 該当行の全セル選択
                        ((TextBoxEditingControl)gc.EditingControl).SelectAll();
                    }

                    return;
                }
            }
        }

        /// <summary>
        /// ヘッダーチェックボックス変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_furikomi_CellContentClick(object sender, CellEventArgs e)
        {
            if (e.Scope == CellScope.ColumnHeader && Ichiran_furikomi.CurrentCell is CheckBoxCell)
            {
                //チェックボックス型セルの値を取得します
                this.logic.ChangeHeaderCheckBox();
            }
        }

        private void rbt_suru_CheckedChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 一覧選択処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_furikomi_RowEnter(object sender, CellEventArgs e)
        {
            // 新規行の場合には削除チェックさせない
            //ヘッダーボックスは除外
            if (e.RowIndex >= 0)
            {
                if (this.Ichiran_furikomi.Rows[e.RowIndex].IsNewRow)
                {
                    ((r_framework.CustomControl.GcCustomCheckBoxCell)this.Ichiran_furikomi.Rows[e.RowIndex]["DELETE_FLG"]).Enabled = false;
                }
                else
                {
                    ((r_framework.CustomControl.GcCustomCheckBoxCell)this.Ichiran_furikomi.Rows[e.RowIndex]["DELETE_FLG"]).Enabled = true;
                }
            }
        }

        private void Ichiran_furikomi_RowLeave(object sender, CellEventArgs e)
        {
            // 新規行の場合には削除チェックさせない
            //ヘッダーボックスは除外
            if (e.RowIndex >= 0)
            {
                if (this.Ichiran_furikomi.Rows[e.RowIndex].IsNewRow)
                {
                    ((r_framework.CustomControl.GcCustomCheckBoxCell)this.Ichiran_furikomi.Rows[e.RowIndex]["DELETE_FLG"]).Enabled = false;
                }
                else
                {
                    ((r_framework.CustomControl.GcCustomCheckBoxCell)this.Ichiran_furikomi.Rows[e.RowIndex]["DELETE_FLG"]).Enabled = true;
                }
            }
        }

        private void Ichiran_furikomi_RowsAdded(object sender, RowsAddedEventArgs e)
        {
            ((r_framework.CustomControl.GcCustomCheckBoxCell)this.Ichiran_furikomi.Rows[e.RowIndex]["DELETE_FLG"]).Enabled = false;
        }

        /// <summary>
        /// 行削除処理の呼び出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeleteRow(object sender, EventArgs e)
        {
            this.logic.DeleteRow();
        }
    }
}
