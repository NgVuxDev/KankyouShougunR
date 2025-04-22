using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;

namespace Shougun.Core.BusinessManagement.DenshiShinseiNaiyouSentakuNyuuryoku
{
    public partial class UIForm : SuperForm
    {
        /// <summary>業者ポップアップからの戻り</summary>
        private readonly string HIKIAI_FLAG_KIZON = "0";
        private readonly string HIKIAI_FLAG_HIKIAI = "1";

        /// <summary>画面ロジック</summary>
        private LogicClass logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>親フォーム</summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>業者CD前回値</summary>
        internal string BeforeGyoushaCd { get; set; }
        internal bool isError { get; set; }

        /// <summary>
        /// 現在業者CD選択中かどうか判定用のフラグ
        /// true:選択中、false:選択中ではない
        /// </summary>
        private bool IsSelectingGyoushaCd { get; set; }

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.T_DENSHI_SHINSEI_NAIYOU_SENTAKU_NYUURYOKU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
            this.IsSelectingGyoushaCd = false;
        }
        #endregion

        #region OnLoad
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.logic.WindowInit();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
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
        #endregion

        #region ファンクションイベント

        #region Claer
        /// <summary>
        /// F7 条件クリア処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ClaerCondition(object sender, EventArgs e)
        {
            this.logic.SearchConditionInit();
        }
        #endregion

        #region Search
        /// <summary>
        /// FormMangerからの検索処理
        /// </summary>
        public virtual void Search()
        {
            try
            {
                // カーソルを待機カーソルに変更
                Cursor.Current = Cursors.WaitCursor;

                if (!base.RegistErrorFlag && this.logic.IsValidSearchValue())
                {
                    // 検索実行(0件時のアラートはなし)
                    this.logic.Search();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // カーソルを元に戻す
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// F8 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            try
            {
                // カーソルを待機カーソルに変更
                Cursor.Current = Cursors.WaitCursor;

                if (!base.RegistErrorFlag && this.logic.IsValidSearchValue())
                {
                    int count = this.logic.Search();
                    if (count == 0)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("C001");
                    }

                    // 使いやすいように明細行へフォーカス
                    this.customDataGridView1.Focus();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // カーソルを元に戻す
                Cursor.Current = Cursors.Default;
            }
        }
        #endregion

        #region 申請
        /// <summary>
        /// F9 申請
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Shinsei(object sender, EventArgs e)
        {
            DataGridViewRow row = this.customDataGridView1.CurrentRow;
            if (row != null)
            {
                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("G560", WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    return;
                }

                this.logic.ShowShinseiWindow(row);
            }
            else
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E051", "対象データ");
            }
        }
        #endregion

        #region Sort
        /// <summary>
        /// F10 並び替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Sort(object sender, EventArgs e)
        {
            this.customSortHeader1.ShowCustomSortSettingDialog();
        }
        #endregion

        #region Close
        /// <summary>
        /// F12 クローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
                var parentForm = (BusinessBaseForm)this.Parent;
                this.Close();
                parentForm.Close();
        }
        #endregion

        #endregion

        #region 各項目のイベント

        #region 申請内容２TextChanged
        /// <summary>
        /// 申請内容２のTextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_shinseiKbn_2_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeVisibleForSearchCondition(this.txt_shinseiKbn_2.Text.Equals(this.logic.SHINSEI_KBN_2_GENBA));
        }
        #endregion

        #region 業者ポップアップの表示前制御
        /// <summary>
        /// 業者ポップアップ表示前制御
        /// </summary>
        public void txt_gyousyaCd_BeforeExec()
        {
            this.txt_hikiaiFlag.Text = string.Empty;
        }
        #endregion

        #region 業者ポップアップの確定後制御
        /// <summary>
        /// 業者ポップアップ確定後制御
        /// </summary>
        public void txt_gyousyaCd_AfterExec()
        {
            this.IsSelectingGyoushaCd = true;

            if (this.HIKIAI_FLAG_KIZON.Equals(this.txt_hikiaiFlag.Text))
            {
                this.txt_gyousyaKbn.Text = this.logic.GYOUSHA_KBN_KIZON;
            }
            else if (this.HIKIAI_FLAG_HIKIAI.Equals(this.txt_hikiaiFlag.Text))
            {
                this.txt_gyousyaKbn.Text = this.logic.GYOUSHA_KBN_HIKIAI;
            }

            // 業者区分を変更すると、業者区分にフォーカスされてしまうため
            // 最後は業者CDにフォーカスする。
            this.txt_gyousyaCd.Focus();

            this.IsSelectingGyoushaCd = false;
        }
        #endregion

        #region 業者CDEnterイベント
        /// <summary>
        /// 業者CDEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_gyousyaCd_Enter(object sender, EventArgs e)
        {
            if (!this.isError)
            {
                this.BeforeGyoushaCd = this.txt_gyousyaCd.Text;
            }
        }
        #endregion

        #region 業者CD検証
        /// <summary>
        /// 業者CD検証
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_gyousyaCd_Validating(object sender, CancelEventArgs e)
        {
            if (!this.logic.IsValidGyoushaCd())
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region 業者区分TextChangedイベント
        /// <summary>
        /// 業者区分TextChangedイベント
        /// 業者区分が変更されたたら業者情報をクリアする。
        /// ポップアップで選択されたときに業者CDの設定→業者区分の設定の順で処理を実行するため
        /// フラグを利用して、ポップアップで業者CD選択中は実行しないように制御。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_gyousyaKbn_TextChanged(object sender, EventArgs e)
        {
            if (!this.IsSelectingGyoushaCd)
            {
                this.txt_gyousyaCd.Text = string.Empty;
                this.txt_gyousyaName.Text = string.Empty;
                this.txt_hikiaiFlag.Text = string.Empty;
            }
        }
        #endregion

        #region 検索条件TextChangedイベント
        /// <summary>
        /// 検索条件TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_searchCondition_kbn_TextChanged(object sender, EventArgs e)
        {
            this.txt_searchCondition_value.Text = string.Empty;
            this.logic.ChangeSearchValueIME();
        }
        #endregion

        #endregion

    }
}
